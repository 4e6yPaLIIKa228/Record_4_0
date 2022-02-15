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
    /// Логика взаимодействия для AddStudentInMonth.xaml
    /// </summary>
    public partial class AddStudentInMonth : Window
    {
        public AddStudentInMonth()
        {
            InitializeComponent();
            CbFill();
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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                if (CbGroups.SelectedIndex == -1 || CbMonth.SelectedIndex == -1 || CbYears.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    connection.Open();
                    int idgrup;
                    //SELECT count(IDGroup) FROM Students WHERE IDGroup = '1'
                    bool resultClass = int.TryParse(CbGroups.SelectedValue.ToString(), out idgrup);
                    string query = $@"SELECT count(IDGroup) FROM Students WHERE IDGroup = '{idgrup}'";//Ищем колличество учеников с выбранной группой
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    int countID = Convert.ToInt32(cmd.ExecuteScalar());
                    //cmd.ExecuteNonQuery();
                   // MessageBox.Show($"{countID}");
                    int proverka = 0;

                    for (int i = 1; i <= countID; i++)
                    {
                        string query1 = $@"SELECT ID FROM Students WHERE IDGroup = '{idgrup}' and ID > '{proverka}'";//Ищем ID студентов 
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        int countID2 = int.Parse(cmd1.ExecuteScalar().ToString());
                      //  MessageBox.Show($"{countID2}");
                        proverka = countID2;
                        ///////////////////////// 
                        int idmonth, idyears;
                        bool resultClass1 = int.TryParse(CbMonth.SelectedValue.ToString(), out idmonth);
                        bool resultClass2 = int.TryParse(CbYears.SelectedValue.ToString(), out idyears);
                        string query4 = $@"SELECT  COUNT(1) FROM Traffics WHERE IDStudent= '{proverka}' AND IDMonth={idmonth} and IDYear=@'{idyears}' "; //Проверка студента на повторное добавление его в новый месяц
                        SQLiteCommand cmd4 = new SQLiteCommand(query4, connection);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 1)
                        {
                            // MessageBox.Show("Ученик уже добавлен в этот месяц");
                            if (countID == 1) { this.Close(); }
                        }
                        else
                        {
                            string query3 = $@"INSERT INTO Traffics (IDStudent,IDMonth,IDYear) values ('{countID2}',{idmonth},{idyears});";
                            SQLiteCommand cmd3 = new SQLiteCommand(query3, connection);
                            cmd3.ExecuteNonQuery();
                            MessageBox.Show("Выполнил для " + $"{countID2}");
                        }

                    }
                    MessageBox.Show("Группа добавленна в новый месяц");
                    this.Close();
                }
            }
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
