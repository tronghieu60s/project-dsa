using project_dsa.components;
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

            //LinkedList<Admin> ListAdmin = admin.Initialization();
            //LinkedList<TheTu> ListTheTu = user.Initialization();
            //bool adminLogin = admin.Login(ListAdmin);
            //if (adminLogin)
            //    switch (admin.Menu())
            //    {
            //        case 1:
            //            user.RenderAccount(ListTheTu);
            //            break;
            //        case 2:
            //            user.CreateAccount(ListTheTu);
            //            break;
            //        case 3:
            //            user.DeleteAccount(ListTheTu);
            //            break;
            //        case 4:
            //            user.UnLockAccount(ListTheTu);
            //            break;
            //        default:
            //            break;
            //    }

            LinkedList<TheTu> ListTheTu = user.Initialization();
            User userLogin = user.Login(ListTheTu);
            if (userLogin.Id != 0)
                switch (user.Menu())
                {
                    case 1:
                        user.ShowInfo(userLogin);
                        break;
                    case 2:
                        user.Withdrawal(userLogin);
                        break;
                    case 3:
                        user.Transfers(userLogin);
                        break;
                    case 4:
                        user.RenderTransaction(userLogin);
                        break;
                    case 5:
                        user.ChangePin(ListTheTu, userLogin);
                        break;
                    default:
                        break;
                }
        }
    }
}
