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

namespace FinnApp_v4
{
    /// <summary>
    /// Логика взаимодействия для BudgetWindow.xaml
    /// </summary>
    public partial class BudgetWindow : Window
    {
        internal Budget Budget { get; private set; }
        public BudgetWindow(Budget budget)
        {
            InitializeComponent();
            Budget = budget;
            DataContext = Budget;
        }
        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
