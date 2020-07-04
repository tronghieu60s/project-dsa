using project_dsa.components;
using project_dsa.helps;
using System;
using System.Collections.Generic;
using System.Threading;

namespace project_dsa
{
    class Program
    {
        static void Main(string[] args)
        {
            //Admin admin = new Admin();
            //LinkedList<Admin> ListAdmin = admin.Initialization();
            //bool adminLogin = admin.Login(ListAdmin);
            //if (adminLogin)
            //    admin.Menu();

            User user = new User();
            LinkedList<User> ListUser = user.Initialization();
            bool userLogin = user.Login(ListUser);
            if (userLogin)
                user.Menu();
        }
    }
}
