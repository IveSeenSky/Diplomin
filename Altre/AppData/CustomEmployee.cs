using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altre.AppData
{
    public partial class Employee
    {
        public string curDep
        {
            get
            {
                var curPose = ConnectionDB.GetCont().Positions.FirstOrDefault(x => x.position_id == x.position_id);
                var curDepo = ConnectionDB.GetCont().Departments.FirstOrDefault(x => x.department_id == curPose.department_id);
                return curDepo.department_name;
            }
        }

        public string curPos
        {
            get
            {
                var curPose = ConnectionDB.GetCont().Positions.FirstOrDefault(x => x.position_id == x.position_id);
                return curPose.position_name;
            }
        }

        public string full_name
        {
            get
            {
                return first_name + " " + middle_name + " " + last_name;
            }
        }
    }
}
