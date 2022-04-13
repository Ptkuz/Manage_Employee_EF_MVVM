
using System.Windows;
using ManageEmployeeDB.Model;
using ManageEmployeeDB.ViewModel;

namespace ManageEmployeeDB.View
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow(Employee employeeToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedEmployee = employeeToEdit;
            DataManageVM.UserName = employeeToEdit.Name;
            DataManageVM.UserSurName = employeeToEdit.SurName;
            DataManageVM.UserPhone = employeeToEdit.Phone;


        }
    }
}
