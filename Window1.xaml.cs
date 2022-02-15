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
using System.Data.SqlClient;

namespace Record
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public Window1(string T, string C, string B)
        {

            InitializeComponent();
            CbFill();
            q.Text = T;
            w.Text = C;
            TxMounth.Text = B;
            q.IsReadOnly = true;
            w.IsReadOnly = true;
            TxMounth.IsReadOnly = true;

        }
        public void CbFill()
        {

            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                try
                {
                    connection.Open();
                    string query1 = $@"SELECT * FROM DaysZ";

                    //----------------------------------------------
                    SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);

                    //----------------------------------------------
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);

                    //----------------------------------------------
                    DataTable dt1 = new DataTable("DaysZ");

                    //----------------------------------------------
                    SDA1.Fill(dt1);

                    //----------------------------------------------
                    CbDays.ItemsSource = dt1.DefaultView;
                    CbDays.DisplayMemberPath = "DAYS";
                    CbDays.SelectedValuePath = "ID";
                    //----------------------------------------------

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
            {
                connection.Open();

                Days();

                string query = $@"SELECT count(ID) FROM Students WHERE IDGroup = '{Saver.idGroup}' ";
                SQLiteCommand cmd = new SQLiteCommand(query, connection); // Колличество учеников 
                Saver.SumStudent = Convert.ToInt32(cmd.ExecuteScalar());
              
                string query1 = $@"SELECT  count() FROM Traffics  
                JOIN Months on Traffics.IDMonth = Months.ID 
                JOIN Years on Traffics.IDYear = Years.ID JOIN Students on Traffics.IDStudent = Students.ID 
                JOIN Groups on Students.IDGroup = Groups.ID 
                WHERE IDGroup = '{Saver.idGroup}' and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and {Saver.Day} = 'б'"; //Количество учеников с Б
                SQLiteCommand cmd1 = new SQLiteCommand(query1, connection); 
                Saver.SumB = Convert.ToInt32(cmd1.ExecuteScalar()); 
               // MessageBox.Show($@"{Saver.SumB}" + "Б");

                string query3 = $@"SELECT  count() FROM Traffics 
                JOIN Months on Traffics.IDMonth = Months.ID 
                JOIN Years on Traffics.IDYear = Years.ID JOIN Students on Traffics.IDStudent = Students.ID  
                JOIN Groups on Students.IDGroup = Groups.ID 
                WHERE IDGroup = '{Saver.idGroup}'  and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and {Saver.Day} = 'п'"; //Количество учеников с П
                SQLiteCommand cmd3 = new SQLiteCommand(query3, connection); 
                Saver.SumP = Convert.ToInt32(cmd3.ExecuteScalar());
                // MessageBox.Show($@"{Saver.SumP}" + " П " + $@"{Saver.Day}");


                string query4 = $@"SELECT  count() FROM Traffics 
                JOIN Months on Traffics.IDMonth = Months.ID 
                JOIN Years on Traffics.IDYear = Years.ID JOIN Students on Traffics.IDStudent = Students.ID  
                JOIN Groups on Students.IDGroup = Groups.ID 
                WHERE IDGroup = '{Saver.idGroup}'  and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and {Saver.Day} = 'н'  ";//Количество учеников с Н
                SQLiteCommand cmd4 = new SQLiteCommand(query4, connection);
                Saver.SumH = Convert.ToInt32(cmd4.ExecuteScalar()); 
               // MessageBox.Show($@"{Saver.SumH}" + " Н " + $@"{Saver.Day}");

                string query5 = $@"SELECT  count() FROM Traffics 
                JOIN Months on Traffics.IDMonth = Months.ID 
                JOIN Years on Traffics.IDYear = Years.ID JOIN Students on Traffics.IDStudent = Students.ID  
                JOIN Groups on Students.IDGroup = Groups.ID 
                WHERE IDGroup = '{Saver.idGroup}'  and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and ({Saver.Day} is NULL or {Saver.Day} = '')  ";//Количество учеников присутствуют
                SQLiteCommand cmd5 = new SQLiteCommand(query5, connection);
                Saver.SumIn = Convert.ToInt32(cmd5.ExecuteScalar());
                //MessageBox.Show($@"{Saver.SumIn}" + " Присутствуют  " + $@"{Saver.Day}");

                Print Window1 = new Print(q.Text, w.Text,TxMounth.Text);
                Window1.Owner = this;
                this.Close();
            }
        }

        private void CbDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String str = ((DataRowView)CbDays.SelectedItem)["DAYS"].ToString(); //Вывел
            Saver.Days = str;
            //if (CbDays.SelectedIndex == 1) {Saver.Day = "Day1"; } 
        }
        public void Days()
        {
            if (Saver.Days == "1") {/* MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day1"; }
            if (Saver.Days == "2") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day2"; }
            if (Saver.Days == "3") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day3"; }
            if (Saver.Days == "4") {/*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day4"; }
            if (Saver.Days == "5") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/; Saver.Day = "Day5"; }
            if (Saver.Days == "6") {/*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day6"; }
            if (Saver.Days == "7") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day7"; }
            if (Saver.Days == "8") {/*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day8"; }
            if (Saver.Days == "9") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day9"; }
            if (Saver.Days == "10") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day10"; }
            if (Saver.Days == "11") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day11"; }
            if (Saver.Days == "12") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day12"; }
            if (Saver.Days == "13") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day13"; }
            if (Saver.Days == "14") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day14"; }
            if (Saver.Days == "15") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day15"; }
            if (Saver.Days == "16") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day16"; }
            if (Saver.Days == "17") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day17"; }
            if (Saver.Days == "18") {/*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day18"; }
            if (Saver.Days == "19") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day19"; }
            if (Saver.Days == "20") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day20"; }
            if (Saver.Days == "21") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day21"; }
            if (Saver.Days == "22") {/*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day22"; }
            if (Saver.Days == "23") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day23"; }
            if (Saver.Days == "24") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day24"; }
            if (Saver.Days == "25") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day25"; }
            if (Saver.Days == "26") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day26"; }
            if (Saver.Days == "27") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day27"; }
            if (Saver.Days == "28") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day28"; }
            if (Saver.Days == "29") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/Saver.Day = "Day29"; }
            if (Saver.Days == "30") {/*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day30"; }
            if (Saver.Days == "31") { /*MessageBox.Show("Days " + $"{Saver.Days}");*/ Saver.Day = "Day31"; }
        }
    }
}
