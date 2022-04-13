using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageEmployeeDB.Model.Data;

namespace ManageEmployeeDB.Model
{
    public static class DataWorker
    {

        public static List<Department> GetAllDepartments() 
        {
            using (ApplicationContext db = new ApplicationContext()) 
            { 
                var result = db.Departments.ToList();
                return result; 
            }

        }

        public static List<Position> GetAllPositions()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;

            }

        }

        public static List<Employee> GetAllEmployees()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Employees.ToList();
                return result;
            }

        }




        // Создать отдел
        public static string CreateDepartment(string name) 
        {
            string result = "Уже существует";

            using (ApplicationContext db = new ApplicationContext()) 
            { 
                //Проверяем, существует ли отдел
                bool checkIsExist = db.Departments.Any(el=> el.Name == name);
                if (!checkIsExist) 
                {
                    Department newDepartment = new Department { Name = name};
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                    result = "Сделано";
                
                }
                return result;
            
            }

        }

        public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
        {
            string result = "Уже существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                //Проверяем, существует ли позиция
                bool checkIsExist = db.Positions.Any(el => el.Name == name && el.Salary == salary);
                if (!checkIsExist)
                {
                    Position newPosition = new Position {
                        Name = name, 
                        Salary = salary, 
                        MaxNumber = maxNumber, 
                        DepartmentId = department.Id };
                    db.Positions.Add(newPosition);
                    db.SaveChanges();
                    result = "Сделано";

                }
                return result;

            }

        }

        public static string CreateEmployee(string name, string surName, string phone, Position position)
        {
            string result = "Уже существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                //Проверяем, существует ли позиция
                bool checkIsExist = db.Employees.Any(el => el.Name == name && el.SurName == surName && el.Position == position);
                if (!checkIsExist)
                {
                    Employee newEmployee = new Employee
                    {
                        Name = name,
                        SurName = surName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Employees.Add(newEmployee);
                    db.SaveChanges();
                    result = "Сделано";

                }
                return result;

            }

        }


        public static string DeleteDepartment(Department department) 
        {
            string result = "такого отдела не существует";

            using (ApplicationContext db = new ApplicationContext()) 
            { 
                db.Departments.Remove(department);
                db.SaveChanges(true);
                result = string.Format($"Отдел {department.Name} удален успешно!");
            
            }

            return result; 
        }

        public static string DeletePosition(Position position)
        {
            string result = "такого отдела не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Positions.Remove(position);
                db.SaveChanges(true);
                result = string.Format($"Позиция {position.Name} удалена успешно!");

            }

            return result;
        }

        public static string DeleteEmployee(Employee employee)
        {
            string result = "такого отдела не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.Remove(employee);
                db.SaveChanges(true);
                result = string.Format($"Сотрудник {employee.Name} удален успешно!");

            }

            return result;
        }

        public static string EditDepartment(Department oldDepartment, string name)
        {
            string result = "такого отдела не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                Department department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = name;
                db.SaveChanges();
                result = String.Format($"Отдел {department.Name} изменен");
                 

            }

            return result;
        }

        public static string EditPosition(Position oldPosition, string name, decimal salary, int maxNumber, Department department)
        {
            string result = "такой позиции не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
                position.Name = name;
                position.Salary = salary;
                position.MaxNumber = maxNumber;
                position.DepartmentId = department.Id;
                db.SaveChanges();
                result = String.Format($"Позиция {position.Name} изменена");


            }

            return result;
        }

        public static string EditEmployee(Employee oldEmployee, string name, string surName, string phone, Position position)
        {
            string result = "такой позиции не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                Employee employee = db.Employees.FirstOrDefault(p => p.Id == oldEmployee.Id);
                employee.Name = name;
                employee.SurName = surName;
                employee.Phone = phone;
                employee.PositionId = position.Id;
                db.SaveChanges();
                result = String.Format($"Сотрудник {employee.Name} изменен");


            }

            return result;
        }


        // Получение позиции по ID позиции
        public static Position GetPositionById(int id) 
        {
            using (ApplicationContext db = new ApplicationContext()) 
            {
                Position position = db.Positions.FirstOrDefault(p => p.Id == id);
                return position;
            
            }
        }


        // Получение отдела по ID отдела
        public static Department GetDepartmentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Department department = db.Departments.FirstOrDefault(d => d.Id == id);
                return department;

            }
        }


        // Получение всех пользователей по ID позиции
        public static List<Employee> GetAllEmployeeByPositionId(int id) 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Employee> employees = (from employee in GetAllEmployees() where employee.PositionId == id select employee).ToList();
                return employees;

            }

        }


        // Получение всех позиций по ID отдела
        public static List<Position> GetAllPositionsByDepartmentId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Position> positions = (from position in GetAllPositions() where position.DepartmentId == id select position).ToList();
                return positions;

            }

        }


    }
}
