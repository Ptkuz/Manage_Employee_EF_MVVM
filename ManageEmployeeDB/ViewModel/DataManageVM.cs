using ManageEmployeeDB.Model;
using ManageEmployeeDB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManageEmployeeDB.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {
        // Свойста для отдела
        public static string DepartmentName { get; set; }

        // Свойства для позиции 
        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxNumber { get; set; }
        public static Department PositionDepartment { get; set; }

        // Свойста для сотрудника
        public static string UserName { get; set; }
        public static string UserSurName { get; set; }
        public static string UserPhone { get; set; }
        public static Position UserPosition { get; set; }

        // Свойства для выделенных элементов

        public TabItem SelectedTabItem { get; set; }
        public static Employee SelectedEmployee { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }
          


        // все отделы

        private List<Department> allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartment 
        {
            get { return allDepartments; }
            set { 
                allDepartments = value;
                NotifyPropertyChanged("AllDepartments");
            }
        
        
        }

        private List<Position> allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get { return allPositions; }
            set
            {
                allPositions = value;
                NotifyPropertyChanged("AllPositions");
            }


        }

        private List<Employee> allEmployees = DataWorker.GetAllEmployees();
        public List<Employee> AllEmployees
        {
            get { return allEmployees; }
            set
            {
                allEmployees = value;
                NotifyPropertyChanged("AllEmployees");
            }


        }





        // Открытие окон
        private void OpenAddDepartmentWindow() 
        { 
            AddNewDepartmentWindow addNewDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterWindowAndOpen(addNewDepartmentWindow);
        }

        private void OpenAddPositionWindow()
        {
            AddNewPositionWindow addNewPositionWindow = new AddNewPositionWindow();
            SetCenterWindowAndOpen(addNewPositionWindow);
        }

        private void OpenAddUserWindow()
        {
            AddNewUserWindow addNewUserWindow = new AddNewUserWindow();
            SetCenterWindowAndOpen(addNewUserWindow);
        }

       

        // Окна редактирования

        private void OpenEditDepartmentWindow(Department department)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(department);
            SetCenterWindowAndOpen(editDepartmentWindow);
        }

        private void OpenEditPositionWindow(Position position)
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow(position);
            SetCenterWindowAndOpen(editPositionWindow);
        }

        private void OpenEditUserWindow(Employee user)
        {
            EditUserWindow editUserWindow = new EditUserWindow(user);
            SetCenterWindowAndOpen(editUserWindow);
        }

        private void SetCenterWindowAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();

        }



        private RelayCommand openAddNewDepartment;
        public RelayCommand OpenAddNewDepartmentWindow
        {
            get
            {
                return openAddNewDepartment ?? new RelayCommand(obj =>
                {
                    OpenAddDepartmentWindow();

                }
                );


            }


        }

        private RelayCommand openAddNewPosition;
        public RelayCommand OpenAddNewPositionWindow
        {
            get
            {
                return openAddNewPosition ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindow();

                }
                );


            }


        }

        private RelayCommand openAddNewUser;
        public RelayCommand OpenAddNewUserWindow
        {
            get
            {
                return openAddNewUser ?? new RelayCommand(obj =>
                {
                    OpenAddUserWindow();

                }
                );


            }


        }

        private RelayCommand oepnEditItemWnd;
        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return oepnEditItemWnd ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    // Если сотрудник
                    if (SelectedTabItem.Name == "EmployeeTab" && SelectedEmployee != null)
                    {
                       OpenEditUserWindow(SelectedEmployee);
                    }

                    // Если позиция
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                       OpenEditPositionWindow(SelectedPosition);
                    }
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindow(SelectedDepartment);
                    }


                    // Если отдел

                    SetNullValuesProperties();
                    ShowMessageToUser(resultStr);



                });

            }

        }

        private RelayCommand edituser;
        public RelayCommand EditUser 
        {
            get 
            {
                return edituser ?? new RelayCommand(obj => 
                {
                    
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    string nullPositionStr = "Не выбрана новая должность";

                    if (SelectedEmployee != null)
                    {
                        if (UserPosition != null)
                        {

                            resultStr = DataWorker.EditEmployee(SelectedEmployee, UserName, UserSurName, UserPhone, UserPosition);
                            UpdateAllDataView();
                            SetNullValuesProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else 
                        {
                            ShowMessageToUser(nullPositionStr);


                        }

                    }
                    else 
                    {
                        ShowMessageToUser(resultStr);

                    }
                
                
                });
            
            }
        
        
        }

        private RelayCommand editPostion;
        public RelayCommand EditPosition
        {
            get
            {
                return editPostion ?? new RelayCommand(obj =>
                {

                    Window window = obj as Window;
                    string resultStr = "Не выбрана позиция";
                    string nullPositionStr = "Не выбран новый отдел";

                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {

                            resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionSalary, PositionMaxNumber, PositionDepartment);
                            UpdateAllDataView();
                            SetNullValuesProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else
                        {
                            ShowMessageToUser(nullPositionStr);


                        }

                    }
                    else
                    {
                        ShowMessageToUser(resultStr);

                    }


                });

            }


        }

        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {

                    Window window = obj as Window;
                    string resultStr = "Не выбран отдел";

                    if (SelectedDepartment != null)
                    {

                            resultStr = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);
                            UpdateAllDataView();
                            SetNullValuesProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();

                    }
                    else
                    {
                        ShowMessageToUser(resultStr);

                    }


                });

            }


        }



        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartmentWindow 
        {
            get 
            {
                return addNewDepartment ?? new RelayCommand(obj => 
                {
                    Window wnd = obj as Window;

                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");

                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        ShowMessageToUser(resultStr);
                        UpdateAllDataView();
                        SetNullValuesProperties();
                        wnd.Close();
                    
                    
                    }
                
                
                });
            
            
            }
        
        
        }

        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;

                    string resultStr = "";
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");

                    }
                    if (PositionSalary == 0) 
                    {
                        SetRedBlockControl(wnd, "NameBlock");

                    }
                    if (PositionMaxNumber == 0) 
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    if (PositionDepartment == null) 
                    {
                        MessageBox.Show("Укажите отдел");
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PositionDepartment);
                        ShowMessageToUser(resultStr);
                        UpdateAllDataView();
                        SetNullValuesProperties();
                        wnd.Close();


                    }


                });


            }


        }

        private RelayCommand addNewUser;
        public RelayCommand AddNewUser
        {
            get
            {
                return addNewUser ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;

                    string resultStr = "";
                    if (UserName == null || UserName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");

                    }
                    if (UserSurName == null || UserSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");

                    }
                    if (UserPhone == null || UserPhone.Replace(" ","").Length==0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    if (UserPosition == null)
                    {
                        MessageBox.Show("Укажите позицию");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateEmployee(UserName, UserSurName, UserPhone, UserPosition);
                        ShowMessageToUser(resultStr);
                        UpdateAllDataView();
                        SetNullValuesProperties();
                        wnd.Close();


                    }


                });


            }


        }

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem 
        {
            get 
            {
                return deleteItem ?? new RelayCommand(obj => 
                {
                    string resultStr = "Ничего не выбрано";
                    // Если сотрудник
                    if (SelectedTabItem.Name == "EmployeeTab" && SelectedEmployee != null) 
                    { 
                        resultStr = DataWorker.DeleteEmployee(SelectedEmployee);
                        UpdateAllDataView();
                    }

                    // Если позиция
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }

                    // Если отдел

                    SetNullValuesProperties();
                    ShowMessageToUser(resultStr);

                
                
                });
            
            }
        
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        { 
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));  
            
        }

        private void SetRedBlockControl(Window wnd, string blockName) 
        { 
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        
        }

        private void ShowMessageToUser(string message) 
        {
            MessageView messageView = new MessageView(message);
            SetCenterWindowAndOpen(messageView);
        
        
        }

        private void SetNullValuesProperties() 
        {
            DepartmentName = null;

            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartment = null;

            UserName = null;
            UserSurName = null;
            UserPhone = null;
            UserPosition = null;
        
        
        }

        private void UpdateAllDataView() 
        {
            UpdateAllDepartmentsView();
            UpdateAllEmployeesView();
            UpdateAllPositionsView();
        
        
        }


        private void UpdateAllDepartmentsView() 
        {
            AllDepartment = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartment;
            MainWindow.AllDepartmentsView.Items.Refresh();
           



        }

        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();


        }

        private void UpdateAllEmployeesView()
        {
            AllEmployees = DataWorker.GetAllEmployees();
            MainWindow.AllEmployeesView.ItemsSource = null;
            MainWindow.AllEmployeesView.Items.Clear();
            MainWindow.AllEmployeesView.ItemsSource = AllEmployees;
            MainWindow.AllEmployeesView.Items.Refresh();


        }

    }
}
