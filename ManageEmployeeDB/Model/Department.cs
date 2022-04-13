using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEmployeeDB.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Position> positions { get; set; }

        [NotMapped]
        public List<Position> DepartmentPositions 
        {
            get 
            {
                return DataWorker.GetAllPositionsByDepartmentId(Id);
            
            }
        
        
        }

    }
}
