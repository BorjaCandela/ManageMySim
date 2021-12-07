using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManageMySim.Classes
{
    public class Users
    {
        private string username;
        public Users(string username)
        {
            this.username = username;

        }

        public string getUsername()
        {
            return username;
        }


    }
}
