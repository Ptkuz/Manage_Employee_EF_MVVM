using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEmployeeDB.Model
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int MaxNumber { get; set; }
        public List<Employee> Employees { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [NotMapped]
        public Department PositionDepartment
        {
            get
            {
                return DataWorker.GetDepartmentById(DepartmentId);


            }

        }

        [NotMapped]
        public List<Employee> PositionUsers 
        {
            get 
            {
                return DataWorker.GetAllEmployeeByPositionId(Id);
            
            
            }
        
        
        }

    }
}
