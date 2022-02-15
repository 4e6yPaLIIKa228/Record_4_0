using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using Record.Connection;
using System.Data;

namespace Record
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();      
            CbFill();
            CbMonth.IsEnabled = false;
            CbYears.IsEnabled = false;
            //this.DGStudents.Columns[0].IsReadOnly = true;
        }
        
       
        public void CbFill()  //Данные для комбобоксов 
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query1 = $@"SELECT * FROM Groups"; // Группы
                    string query2 = $@"SELECT * FROM Months"; // Типы
                    string query3 = $@"SELECT * FROM Years"; // Типы

                    //----------------------------------------------
                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);
                    SQLiteCommand cmd3 = new SQLiteCommand(query3, connection);

                    //----------------------------------------------
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    SQLiteDataAdapter SDA2 = new SQLiteDataAdapter(cmd2);
                    SQLiteDataAdapter SDA3 = new SQLiteDataAdapter(cmd3);
                    //----------------------------------------------
                    DataTable dt1 = new DataTable("Groups");
                    DataTable dt2 = new DataTable("Months");
                    DataTable dt3 = new DataTable("Years");
                    //----------------------------------------------
                    SDA1.Fill(dt1);
                    SDA2.Fill(dt2);
                    SDA3.Fill(dt3);
                    //----------------------------------------------
                    CbGroups.ItemsSource = dt1.DefaultView;
                    CbGroups.DisplayMemberPath = "NameGroup";
                    CbGroups.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CbMonth.ItemsSource = dt2.DefaultView;
                    CbMonth.DisplayMemberPath = "Month";
                    CbMonth.SelectedValuePath = "ID";
                    //----------------------------------------------
                    CbYears.ItemsSource = dt3.DefaultView;
                    CbYears.DisplayMemberPath = "Year";
                    CbYears.SelectedValuePath = "ID";


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e) //Группы
        {
            if (CbGroups.SelectedIndex != -1) { CbMonth.IsEnabled = true; } else { CbMonth.IsEnabled = true; }
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    String str = ((DataRowView)CbGroups.SelectedItem)["NameGroup"].ToString(); //Вывел
                    bool resultClass = int.TryParse(CbGroups.SelectedValue.ToString(), out Saver.idGroup);
                    Saver.groups = str;
                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
        }      
        public void Traffics()//дни
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    string query = $@"SELECT Students.NSM,Traffics.Day1 FROM Traffics
                                    JOIN Students On Traffics.IDStudents = Students.ID";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("Students");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DGStudents.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
        }

        private void CbYears_SelectionChanged(object sender, SelectionChangedEventArgs e)//Годы
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    bool resultClass = int.TryParse(CbYears.SelectedValue.ToString(), out Saver.idyears);
                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
            Search();
        }

        private void CbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e) //Месяцы
        {
            if (CbMonth.SelectedIndex != -1) { CbYears.IsEnabled = true; } else { CbYears.IsEnabled = true; }
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    String str = ((DataRowView)CbMonth.SelectedItem)["Month"].ToString(); //Вывел
                    bool resultClass = int.TryParse(CbMonth.SelectedValue.ToString(), out Saver.idmonth);
                    Saver.month = str;
                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
        }

        private void Search()//Поиск для DataGrid
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                     connection.Open();
                    string query = $@"SELECT Students.ID,Students.NSM, Groups.NameGroup, Months.Month,Years.Year, Traffics.Day1,Traffics.Day2,Traffics.Day3,Traffics.Day4,Traffics.Day5,Traffics.Day6,Traffics.Day7,Traffics.Day8,Traffics.Day9,Traffics.Day10,Traffics.Day11,Traffics.Day12,Traffics.Day13,Traffics.Day14,Traffics.Day15,Traffics.Day16,Traffics.Day17,Traffics.Day18,Traffics.Day19,Traffics.Day20,Traffics.Day21,Traffics.Day22,Traffics.Day23,Traffics.Day24,Traffics.Day25,Traffics.Day26,Traffics.Day27,Traffics.Day28,Traffics.Day29,Traffics.Day30,Traffics.Day31,Traffics.SumP,Traffics.SumH,Traffics.SumB  FROM Traffics 
                                        JOIN Months on Traffics.IDMonth = Months.ID
                                        JOIN Years on Traffics.IDYear = Years.ID
                                        JOIN Students on Traffics.IDStudent = Students.ID
                                        JOIN Groups on Students.IDGroup = Groups.ID
                                        WHERE  Groups.ID = '{Saver.idGroup}' and Traffics.IDMonth = '{Saver.idmonth}' and Years.ID = '{Saver.idyears}'";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("Traffics");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DGStudents.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                    foreach (DataRow row in DT.Rows)
                    {
                        int countB = 0, countP = 0, countH = 0;

                        for (int i = 1; i <= 31; i++)
                        {
                            string temp = Convert.ToString(row[$@"Day{i}"]);
                            if (temp == "б")
                            {
                                countB++;
                            }
                            if (temp == "п")
                            {
                                countP++;
                            }
                            if (temp == "н")
                            {
                                countH++;
                            }
                        }
                        row["SumB"] = countB;
                        row["SumP"] = countP;
                        row["SumH"] = countH;
                    }
                }
            }
            catch (Exception exp) 

            { MessageBox.Show(exp.Message); }

        }
        private void BtSearch_Click(object sender, RoutedEventArgs e)//Поиск по нажатия
        {
            if ((CbGroups.SelectedIndex != -1) && (CbMonth.SelectedIndex != -1) && (CbYears.SelectedIndex != -1)) 
            {
                Search();
                SumTotoalB();
                SumTotoalP();
                SumTotoalН();
                SumTotoalT();
            }
            else
            {
                MessageBox.Show("Выберите три критерия поиска");
            }
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)//Сохранение данные при их изменениях(по кнопке), по сути не нужно(для вида)
        {
            //Stroka();
        }

        private void DGStudents_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)//обновляем данные при их изменениях(enter, ....
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    if (DGStudents.SelectedItems.Count > 0)
                    {
                        DataRowView row = (DataRowView)DGStudents.SelectedItems[0];
                        Stroka();
                        connection.Open();
                        for (int i = 1; i <= 31; i++)
                        {
                            string temp = Convert.ToString(row[$@"Day{i}"]);
                            if (temp == "б")
                            {
                                string query = $@"UPDATE  Traffics SET  Day{i} = '{temp}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery();
                            }
                            if (temp == "п")
                            {
                                string query = $@"UPDATE  Traffics SET  Day{i} = '{temp}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery();
                            }
                            if (temp == "н")
                            {
                                string query = $@"UPDATE  Traffics SET  Day{i} = '{temp}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery();
                            }
                            if (temp == "")
                            {
                                string query = $@"UPDATE  Traffics SET  Day{i} = '{temp}' WHERE Traffics.ID = '{Saver.IDNSM}' and Traffics.IDMonth ='{Saver.idmonth}' "; SQLiteCommand cmd = new SQLiteCommand(query, connection); cmd.ExecuteNonQuery();
                            }
                        }
                        SumTotoalB();
                        SumTotoalP();
                        SumTotoalН();
                        SumTotoalT();
                        Search();
                    }
                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); } 
        }
        public void SumTotoalT() //Колличество студентов присутствует()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string[] test = new string[31];
                    for (int i = 1; i <= 31; i++) {
                      
                        string query = $@"SELECT  count() FROM Traffics 
                        JOIN Months on Traffics.IDMonth = Months.ID 
                        JOIN Years on Traffics.IDYear = Years.ID 
                        JOIN Students on Traffics.IDStudent = Students.ID 
                        JOIN Groups on Students.IDGroup = Groups.ID  WHERE Groups.ID = '{Saver.idGroup}' and Traffics.IDMonth = '{Saver.idmonth}' and Years.ID = '{Saver.idyears}' and  (Day{i} is NULL or Day{i} = '')";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        int DayB = Convert.ToInt32(cmd.ExecuteScalar());
                        string s = DayB.ToString();
                        test[i-1] = s;
                        Output.Text = string.Join("      ", test);
                    }
                }
            }
            catch (Exception exp)

            { MessageBox.Show(exp.Message); }
        }
        public void SumTotoalН() //Колличество студентов незаконно(н)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string[] test = new string[31];
                    for (int i = 1; i <= 31; i++)
                    {
                        string query = $@"SELECT  count() FROM Traffics  
                        JOIN Months on Traffics.IDMonth = Months.ID 
                         JOIN Years on Traffics.IDYear = Years.ID JOIN Students on Traffics.IDStudent = Students.ID 
					    JOIN Groups on Students.IDGroup = Groups.ID
                        WHERE Groups.ID = '{Saver.idGroup}' and Traffics.IDMonth = '{Saver.idmonth}' and Years.ID = '{Saver.idyears}' and  Day{i} = 'н'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        int DayB = Convert.ToInt32(cmd.ExecuteScalar());
                        string s1 = DayB.ToString();
                        test[i - 1] = s1;
                        H1.Text = string.Join("      ", test);
                    }
                }
            }
            catch (Exception exp)

            { MessageBox.Show(exp.Message); }
        }
        public void SumTotoalP() //Колличество студентов с причной
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string[] test = new string[31];
                    for (int i = 1; i <= 31; i++)
                    {
                        string query = $@"SELECT  count() FROM Traffics  
                        JOIN Months on Traffics.IDMonth = Months.ID 
                         JOIN Years on Traffics.IDYear = Years.ID JOIN Students on Traffics.IDStudent = Students.ID 
					    JOIN Groups on Students.IDGroup = Groups.ID
                        WHERE Groups.ID = '{Saver.idGroup}' and Traffics.IDMonth = '{Saver.idmonth}' and Years.ID = '{Saver.idyears}' and  Day{i} = 'п'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        int DayB = Convert.ToInt32(cmd.ExecuteScalar());
                        string s1 = DayB.ToString();
                        test[i - 1] = s1;
                        P1.Text = string.Join("      ", test);
                    }
                }
            }
            catch (Exception exp)

            { MessageBox.Show(exp.Message); }
        }

        public void SumTotoalB() //Колличество студентов с болезнью
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    connection.Open();
                    string[] test = new string[31];
                    for (int i = 1; i <= 31; i++)
                    {
                        {
                            string query = $@"SELECT  count() FROM Traffics  
                            JOIN Months on Traffics.IDMonth = Months.ID 
                            JOIN Years on Traffics.IDYear = Years.ID JOIN Students on Traffics.IDStudent = Students.ID 
					        JOIN Groups on Students.IDGroup = Groups.ID
                            WHERE Groups.ID = '{Saver.idGroup}' and Traffics.IDMonth = '{Saver.idmonth}' and Years.ID = '{Saver.idyears}' and  Day{i} = 'б'";
                            SQLiteCommand cmd = new SQLiteCommand(query, connection);
                            int DayB = Convert.ToInt32(cmd.ExecuteScalar());
                            string s = DayB.ToString();
                            test[i - 1] = s;
                            B1.Text = string.Join("      ", test);
                        }
                    }
                }
            }
            catch (Exception exp)

            { MessageBox.Show(exp.Message); }
        }
        
        public void Stroka()//Узнаем кого выбрали + его айди(в студентах)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    foreach (var item in DGStudents.SelectedItems.Cast<DataRowView>())
                    {
                        DataRowView row = (DataRowView)DGStudents.SelectedItems[0];
                        Saver.NameFirst = row["NSM"].ToString(); 
                        connection.Open();
                        string query = $@"SELECT ID FROM  Students  WHERE NSM=@NSM ";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.Parameters.AddWithValue("@NSM", Saver.NameFirst);
                        
                        int countID = Convert.ToInt32(cmd.ExecuteScalar());
                       // MessageBox.Show($@"{countID}" + " IDSTudent");
                        cmd.ExecuteNonQuery();
                        string query2 = $@"SELECT ID FROM Traffics WHERE IDStudent ='{countID}' and IDMonth='{Saver.idmonth}' and IDYear='{Saver.idyears}'"; //ID студента в таблице трафиика
                        SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);
                        int TrafficID = Convert.ToInt32(cmd2.ExecuteScalar());
                        Saver.IDNSM = TrafficID;
                       // MessageBox.Show($@"{Saver.IDNSM}" + " IDTraffic");
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {

            AddStudents AdSt = new AddStudents();
            AdSt.Owner = this;
            //AdSt.Show();
            AdSt.ShowDialog();


        }

        private void w_Click(object sender, RoutedEventArgs e)
        {
            if ((CbGroups.SelectedIndex != -1) && (CbMonth.SelectedIndex != -1) && (CbYears.SelectedIndex != -1))
            {
                Window1 Wi = new Window1(CbGroups.Text, CbMonth.Text, CbYears.Text);
                Wi.Show();
            }
            else
            {
                MessageBox.Show("Выберите три критерия ");
            }
           
        }

        private void Obrat_Click(object sender, RoutedEventArgs e)
        {
            ObratConn AdSt = new ObratConn();
            AdSt.Owner = this;
            //AdSt.Show();
            AdSt.ShowDialog();
        }

        private void DGStudents_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                   // Saver.Den = DGStudents.CurrentCell.Column.DisplayIndex;
                   // DataRowView dataRow = (DataRowView)DGStudents.SelectedItem;

                    //string cellValue = dataRow.Row.ItemArray[index].ToString();
                   // MessageBox.Show($"{Saver.Den}");
                    // MessageBox.Show($"{cellValue}");

            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("");
            }
        }
    }  
}
