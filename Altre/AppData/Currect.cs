using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Altre.AppData
{
    internal class Currect
    {
        public static Users curUser;
        public static PermConct curPermCon;
        public static Employee curEmployee
        {
            get
            {
                if (curUser.user_id != 5)
                {
                    var permCont = ConnectionDB.GetCont().PermConct.FirstOrDefault(x => x.user_id == curUser.user_id);
                    return ConnectionDB.GetCont().Employee.FirstOrDefault(x => x.employee_id == permCont.employee_id);
                }
                else return null;
            }
        }
        public static Departments curDepartment
        {
            get
            {
                if (curUser.user_id != 5)
                {
                    var positions = ConnectionDB.GetCont().Positions.FirstOrDefault(x => x.position_id == curEmployee.position_id);
                    return ConnectionDB.GetCont().Departments.FirstOrDefault(x => x.department_id == positions.department_id);
                }
                else return null;
            }
        }

        public static Employee selEmployee;

        public static List<Employee> EmployeeList;
    }
}
