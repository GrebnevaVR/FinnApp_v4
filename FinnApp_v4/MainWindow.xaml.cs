using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinnApp_v4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TabItem_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            object dataContext = this.DataContext;
            ApplicationViewModel viewModel = (ApplicationViewModel)dataContext;
            viewModel.LoadProjectNames();
        }
    }
}