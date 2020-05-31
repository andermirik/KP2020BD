using ClosedXML.Excel;
using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;


namespace BDcource2020
{
    /// <summary>
    /// Логика взаимодействия для QueryWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window
    {
        string connection_string;
        public QueryWindow(string connection_string)
        {
            InitializeComponent();

            this.connection_string = connection_string;

            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

        }

        private void Move_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void move_rect(object sender)
        {
            //rect.Margin = new Thickness((sender as Button).Margin.Left, (sender as Button).Margin.Top, 0, 0);
            //rect.Height = (sender as Button).Height;
        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        void FillGridFromDataSet(DataSet dataSet)
        {
            try
            {
                dataGridView1.Columns.Clear();
                for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                {
                    var col = dataSet.Tables[0].Columns[i];
                    dataGridView1.Columns.Add(col.ColumnName, col.Caption);
                    if (i == 0) dataGridView1.Columns[col.ColumnName].ReadOnly = true;

                }
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    dataGridView1.Rows.Add(dataSet.Tables[0].Rows[i].ItemArray);

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
                    if (i == dataGridView1.Columns.Count - 1)
                        dataGridView1.Columns[i].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

                }
            }
            catch (Exception e)
            {

            }

        }

        DataSet FillDataSetFromDB(string sql)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connection_string);
                var select = sql;
                var dataAdapter = new NpgsqlDataAdapter(select, connection);
                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return null;
            }
        }

        private void btn_orders_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Заказы_naturaljoin";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }

        private void btn_orders_by_firm_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("Фирма", "Введите фирму");
            if (dialog.ShowDialog() == true)
            {
                string sql = $"SELECT * FROM zakazi_by_firm('{dialog.ResponseText}')";
                FillGridFromDataSet(FillDataSetFromDB(sql));
            }
        }

        private void btn_hospitals_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Больницы_naturaljoin";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }

        private void btn_orders_by_hospital_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("Больница", "Введите Номер больницы");
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string sql = $"SELECT * FROM zakazi_by_hospital({dialog.ResponseText})";
                    FillGridFromDataSet(FillDataSetFromDB(sql));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Вы ввели не число", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_orders_by_last_months_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("Месяцы", "Введите за сколько последних месяцев вернуть заказы");
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string sql = $"SELECT * FROM zakazi_by_last_months({dialog.ResponseText})";
                    FillGridFromDataSet(FillDataSetFromDB(sql));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Вы ввели не число", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_orders_by_last_years_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("Месяцы", "Введите за сколько последних лет вернуть заказы");
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string sql = $"SELECT * FROM zakazi_by_last_years({dialog.ResponseText})";
                    FillGridFromDataSet(FillDataSetFromDB(sql));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Вы ввели не число", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_hospoitals_wiithout_orders_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM view_Больницы_без_заказов";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }

        private void btn_firms_without_orders_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM view_фирмы_без_заказов";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }

        private void btn_firms_without_orders_left_join_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM view_emulate_left_join";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }

        private void btn_avg_orders_by_hospiatl_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM средняя_стоимость_заказа_для_кажд";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }

        private void btn_sum_orders_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM сумма_заказов";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }

        private void btn_avg_bigger_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("Цена", "Введите больше чего");
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string sql = $"SELECT * FROM zakazi_avg_bigger({dialog.ResponseText})";
                    FillGridFromDataSet(FillDataSetFromDB(sql));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Вы ввели не число", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_firms_in_country_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("Страна", "Введите маску страны");
            if (dialog.ShowDialog() == true)
            {
                string sql = $"SELECT * FROM firms_country_like('{dialog.ResponseText}')";
                FillGridFromDataSet(FillDataSetFromDB(sql));
            }
        }

        private void btn_count_firms_by_last_monthes_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("Месяцы", "Введите за сколько последних месяцев вернуть заказы");
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string sql = $"SELECT * FROM zakazi_count_by_last_month({dialog.ResponseText})";
                    FillGridFromDataSet(FillDataSetFromDB(sql));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Вы ввели не число", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_sum_zakazi_by_firm_where_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new MyDialog("количество", "Введите количество товаров для данных");
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string sql = $"SELECT * FROM sum_zakazi_by_firm_where({dialog.ResponseText})";
                    FillGridFromDataSet(FillDataSetFromDB(sql));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Вы ввели не число", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_sum_zakazi_by_firm_where2_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("количество", "Введите количество товаров для данных");
            var dialog2 = new MyDialog("количество", "Введите количество товаров для группы");
            if (dialog.ShowDialog() == true && dialog2.ShowDialog() == true)
            {
                try
                {
                    string sql = $"SELECT * FROM sum_zakazi_by_firm_where({dialog.ResponseText}, {dialog2.ResponseText})";
                    FillGridFromDataSet(FillDataSetFromDB(sql));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Вы ввели не число", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_itog_podzapros_Click(object sender, RoutedEventArgs e)
        {
            string sql = $"SELECT * FROM podzapros_itog()";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }

        private void btn_union_Click(object sender, RoutedEventArgs e)
        {
            string sql = $"SELECT * FROM union_view";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }

        private void btn_in_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("Месяцы", "Введите интервал в месяцах");
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string sql = $"SELECT * FROM hospitals_by_last_months({dialog.ResponseText})";
                    FillGridFromDataSet(FillDataSetFromDB(sql));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Вы ввели не число", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_not_in_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyDialog("Месяцы", "Введите интервал в месяцах");
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string sql = $"SELECT * FROM no_hospitals_by_last_months({dialog.ResponseText})";
                    FillGridFromDataSet(FillDataSetFromDB(sql));
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Вы ввели не число", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_case_Click(object sender, RoutedEventArgs e)
        {
            string sql = $"SELECT * FROM case_view";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }



        private void btn_excel_Click(object sender, RoutedEventArgs e)
        {
            //Creating DataTable
            DataTable dt = new DataTable();

            //Adding the Columns
            foreach (System.Windows.Forms.DataGridViewColumn column in dataGridView1.Columns)
            {
                dt.Columns.Add(column.HeaderText);
            }

            //Adding the Rows
            foreach (System.Windows.Forms.DataGridViewRow row in dataGridView1.Rows)
            {
                dt.Rows.Add();
                foreach (System.Windows.Forms.DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                }
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Sheet");
                    wb.SaveAs(saveFileDialog1.FileName);
                    MessageBox.Show("Сохранено");
                }
            }
        }

        private void btn_charts_Click(object sender, RoutedEventArgs e)
        {
            ChartWindow cw = new ChartWindow(connection_string);
            cw.Show();
        }

        private void btn_firms_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Фирмы_naturaljoin";
            FillGridFromDataSet(FillDataSetFromDB(sql));
        }
    }
}
