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
using System.Windows.Shapes;

namespace Record
{
    /// <summary>
    /// Interaction logic for AddStudents.xaml
    /// </summary>
    public partial class AddStudents : Window
    {
        public AddStudents()
        {
            InitializeComponent();
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            AddGroup AdSt = new AddGroup();
            AdSt.Owner = this;
            AdSt.ShowDialog();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtAddGroup_Click(object sender, RoutedEventArgs e)
        {
            AddGroup AdSt = new AddGroup();
            AdSt.Owner = this;
            AdSt.ShowDialog();
        }

        private void BtAddStudent_Click(object sender, RoutedEventArgs e)
        {
            AddStudent AdSt = new AddStudent();
            AdSt.Owner = this;
            AdSt.ShowDialog();
        }

        private void BtAddStudentInMonth_Click(object sender, RoutedEventArgs e)
        {

            AddStudentInMonth AdSt = new AddStudentInMonth();
            AdSt.Owner = this;
            AdSt.ShowDialog();
        }
    }
}
