using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altre.AppData
{
    internal class ConnectionDB
    {
        public static AltrbaseEntities context;

        public static AltrbaseEntities GetCont()
        {
            if (context == null) 
                context = new AltrbaseEntities();
            return context;
        }
    }
}
