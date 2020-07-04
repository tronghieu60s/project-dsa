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
            Admin admin = new Admin();
            User user = new User();

            LinkedList<Admin> ListAdmin = admin.Initialization();
            LinkedList<TheTu> ListTheTu = user.Initialization();
            bool adminLogin = admin.Login(ListAdmin);
            if (adminLogin)
                switch (admin.Menu())
                {
                    case 2:
                        user.CreateAccount(ListTheTu);
                        break;
                    default:
                        break;
                }

            //LinkedList<TheTu> ListTheTu = user.Initialization();
            //bool userLogin = user.Login(ListTheTu);
            //if (userLogin)
            //    user.Menu();
        }
    }
}
