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
    /// Логика взаимодействия для Pred_pechati.xaml
    /// </summary>
    public partial class Pred_pechati : Window
    {
        public Pred_pechati(string T)
        {
            InitializeComponent();
            q.Text = T;
        }

        
    }
}
