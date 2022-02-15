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
    /// Логика взаимодействия для AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public AddStudent()
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
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {

                if (String.IsNullOrEmpty(TbNSM.Text) || CbGroups.SelectedIndex == -1 || CbMonth.SelectedIndex == -1 || CbYears.SelectedIndex == -1)
                {
                    MessageBox.Show("Заполните поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    connection.Open();
                    int idgrup,idmonth,idyears;
                    bool resultClass = int.TryParse(CbGroups.SelectedValue.ToString(), out idgrup);
                    string query = $@"INSERT INTO Students ('NSM','IDGroup') VALUES (@NSM,@IDGroup)";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    try
                    {
                        cmd.Parameters.AddWithValue("@NSM", TbNSM.Text);
                        cmd.Parameters.AddWithValue("@IDGroup", idgrup);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Добавил");
                        this.Close();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    string query2 = $@"SELECT ID FROM Students WHERE NSM=@NSM";
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);
                    cmd2.Parameters.AddWithValue("@NSM", TbNSM.Text); //.ToLower()
                    int countID = Convert.ToInt32(cmd2.ExecuteScalar());
                    bool resultClass1 = int.TryParse(CbMonth.SelectedValue.ToString(), out idmonth);
                    bool resultClass2 = int.TryParse(CbYears.SelectedValue.ToString(), out idyears);

                    string query3 = $@"INSERT INTO Traffics (IDStudent,IDMonth,IDYear) values ('{countID}',{idmonth},{idyears});";
                    SQLiteCommand cmd3 = new SQLiteCommand(query3, connection);
                    try
                    {
                        //cmd3.Parameters.AddWithValue("@IDStudent", countID); //.ToLower()
                        //cmd3.Parameters.AddWithValue("@IDMonth", idmonth); //.ToLower()
                        //cmd3.Parameters.AddWithValue("@IDYear", idyears); //.ToLower()
                        cmd3.ExecuteNonQuery();
                       // MessageBox.Show("Добавил2");
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    string query4 = $@"SELECT ID FROM Traffics WHERE IDStudent ='{countID}' and IDMonth='{idmonth}' and IDYear='{idyears}'"; //ID в таблице трафиика
                    SQLiteCommand cmd4 = new SQLiteCommand(query4, connection);
                    int TrafficID = Convert.ToInt32(cmd4.ExecuteScalar());
                    string query5 = $@"UPDATE  Students SET IDTraffic=@IDTraffic WHERE ID=@ID;"; //Добавление IdTraffic в таблицу Students
                    SQLiteCommand cmd5 = new SQLiteCommand(query5, connection);
                    try
                    {
                        cmd5.Parameters.AddWithValue("@ID", countID);
                        cmd5.Parameters.AddWithValue("@IDTraffic", TrafficID);
                        cmd5.ExecuteNonQuery();
                        //MessageBox.Show("Данные изменены");
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
