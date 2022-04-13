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
using ManageEmployeeDB.ViewModel;
using System.Text.RegularExpressions;

namespace ManageEmployeeDB.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewPositionWindow.xaml
    /// </summary>
    public partial class AddNewPositionWindow : Window
    {
        public AddNewPositionWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }

        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) 
        {

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        
        }
    }
}
