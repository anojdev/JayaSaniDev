using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public class Employee
    {
        public int EmployeeId { get; set; } 
        public String Email { get; set; }
        public String Name { get; set; }
        public decimal Salary { get; set; }
        public string Group { get; set; }
        public int DepartmentId { get; set; }
    }
    public class  Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }


    }
}
