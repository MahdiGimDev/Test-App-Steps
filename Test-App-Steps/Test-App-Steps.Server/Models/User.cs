using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_App_Steps.Server.Models
{
    public class User
    {

         public enum RoleEnum
        {
            admin,//0
            employee//1
        }

  




        public int userId{get; set;}
        public string  userName { get; set; }
        public RoleEnum userRole { get; set; }
        public string userEmail { get; set; }
        public string userPassword { get; set; }

       




    }
}
