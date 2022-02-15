using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Data.SQLite;
using Record.Connection;

namespace Record
{
    /// <summary>
    /// Логика взаимодействия для Print.xaml
    /// </summary>
    public partial class Print : Window
    {
        public Print(string T, string C, string B)
        {
            InitializeComponent();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBConnection.myConn))
                {
                    AppData.Content = "Дата: " + $@"{Saver.Days}" + "." + $@"{C}" + "." + $@"{B}";
                    //----------------------------------------------
                    AppColvo.Content = "В группе:" + $@" {T} " + " По списку: " + $@"{Saver.SumStudent}";
                    //----------------------------------------------
                    AppGroup.Content = "Присутствуют: " + $@"{Saver.SumIn}";
                    //----------------------------------------------
                    string getValue = "";
                    string proverka = "";
                    int proverkaID = 0;
                    string Famili = "";
                    connection.Open();
                    for (int i = 1; i <= Saver.SumB; i++)
                    {
                        string query = $@"SELECT Students.NSM FROM Traffics
                            JOIN Months on Traffics.IDMonth = Months.ID
                            JOIN Years on Traffics.IDYear = Years.ID
                            JOIN Students on Traffics.IDStudent = Students.ID
                            JOIN Groups on Students.IDGroup = Groups.ID
                            WHERE IDGroup = '{Saver.idGroup}'  and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and {Saver.Day} = 'б' and Students.ID > '{proverkaID}'";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        getValue = cmd.ExecuteScalar().ToString();
                        proverka = getValue;
                        Famili = Famili + "\n" + proverka;
                        // MessageBox.Show("Имя " + proverka);

                        string query1 = $@"SELECT Students.ID FROM Traffics
                            JOIN Months on Traffics.IDMonth = Months.ID
                            JOIN Years on Traffics.IDYear = Years.ID
                            JOIN Students on Traffics.IDStudent = Students.ID
                            JOIN Groups on Students.IDGroup = Groups.ID
                            WHERE IDGroup = '{Saver.idGroup}'  and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and {Saver.Day} = 'б' and Students.ID > '{proverkaID}'";//Ищем ID студентов 
                        SQLiteCommand cmd1 = new SQLiteCommand(query1, connection);
                        int countID2 = int.Parse(cmd1.ExecuteScalar().ToString());
                        // MessageBox.Show($"{countID2}");
                        proverkaID = countID2;

                    }
                    FIOIll.Content = "Оттуствуют по болезни: " + "\n";
                    AppBolen.Content = $@"{Famili}" + "\n";
                    // MessageBox.Show(Famili);
                    //----------------------------------------------
                    string getValueTwo = "";
                    string proverkaTwo = "";
                    int proverkaIDTwo = 0;
                    string FamiliTwo = "";
                    for (int i = 1; i <= Saver.SumP; i++)
                    {
                        string query3 = $@"SELECT Students.NSM FROM Traffics
                            JOIN Months on Traffics.IDMonth = Months.ID
                            JOIN Years on Traffics.IDYear = Years.ID
                            JOIN Students on Traffics.IDStudent = Students.ID
                            JOIN Groups on Students.IDGroup = Groups.ID
                            WHERE IDGroup = '{Saver.idGroup}'  and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and {Saver.Day} = 'п' and Students.ID > '{proverkaIDTwo}'";
                        SQLiteCommand cmd2 = new SQLiteCommand(query3, connection);
                        getValueTwo = cmd2.ExecuteScalar().ToString();
                        proverkaTwo = getValueTwo;
                        FamiliTwo = FamiliTwo + "\n" + proverkaTwo;
                        // MessageBox.Show("Имя " + proverkaTwo);

                        string query4 = $@"SELECT Students.ID FROM Traffics
                            JOIN Months on Traffics.IDMonth = Months.ID
                            JOIN Years on Traffics.IDYear = Years.ID
                            JOIN Students on Traffics.IDStudent = Students.ID
                            JOIN Groups on Students.IDGroup = Groups.ID
                            WHERE IDGroup = '{Saver.idGroup}'  and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and {Saver.Day} = 'п' and Students.ID > '{proverkaIDTwo}'";//Ищем ID студентов 
                        SQLiteCommand cmd3 = new SQLiteCommand(query4, connection);
                        int countID3 = int.Parse(cmd3.ExecuteScalar().ToString());
                        // MessageBox.Show($"{countID3}");
                        proverkaIDTwo = countID3;

                    }
                    FIOAPP.Content = "Оттуствуют по уважительной причине: " + "\n";
                    AppPrechena.Content = $@"{FamiliTwo}" + "\n";
                    //MessageBox.Show(FamiliTwo);
                    //----------------------------------------------
                    //
                    string getValueThree = "";
                    string proverkaThree = "";
                    int proverkaIDThree = 0;
                    string FamiliThree = "";
                    for (int i = 1; i <= Saver.SumH; i++)
                    {
                        string query5 = $@"SELECT Students.NSM FROM Traffics
                            JOIN Months on Traffics.IDMonth = Months.ID
                            JOIN Years on Traffics.IDYear = Years.ID
                            JOIN Students on Traffics.IDStudent = Students.ID
                            JOIN Groups on Students.IDGroup = Groups.ID
                            WHERE IDGroup = '{Saver.idGroup}'  and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and {Saver.Day} = 'н' and Students.ID > '{proverkaIDThree}'";
                        SQLiteCommand cmd5 = new SQLiteCommand(query5, connection);
                        getValueThree = cmd5.ExecuteScalar().ToString();
                        proverkaThree = getValueThree;
                        FamiliThree = FamiliThree + "\n" + proverkaThree;
                        //MessageBox.Show("Имя " + proverkaThree);

                        string query6 = $@"SELECT Students.ID FROM Traffics
                            JOIN Months on Traffics.IDMonth = Months.ID
                            JOIN Years on Traffics.IDYear = Years.ID
                            JOIN Students on Traffics.IDStudent = Students.ID
                            JOIN Groups on Students.IDGroup = Groups.ID
                            WHERE IDGroup = '{Saver.idGroup}'  and IDMonth = '{Saver.idmonth}' and IDYear = '{Saver.idyears}' and {Saver.Day} = 'н' and Students.ID > '{proverkaIDThree}'";//Ищем ID студентов 
                        SQLiteCommand cmd6 = new SQLiteCommand(query6, connection);
                        int countID3 = int.Parse(cmd6.ExecuteScalar().ToString());
                        // MessageBox.Show($"{countID3}");
                        proverkaIDThree = countID3;

                    }
                    FIOН.Content = "Оттуствуют по неуважительной причине: " + "\n";
                    AppН.Content = $@"{FamiliThree}" + "\n";
                    // MessageBox.Show(FamiliThree);
                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message); }
            try
            {
                PrintDialog p = new PrintDialog();

                if (p.ShowDialog() == true)
                {
                    p.PrintVisual(Printtt, "Печать");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Закройте файл, который вы пытаетесь перезаписать");
            }

        }
    }
}
