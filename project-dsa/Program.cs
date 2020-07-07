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
            Menu menu = new Menu();
            Admin admin = new Admin();
            User user = new User();
            Support _sp = new Support();

            LinkedList<Admin> ListAdmin = admin.Initialization();
            LinkedList<TheTu> ListTheTu = user.Initialization();

        mainMenu:
            switch (menu.MainMenu())
            {
                case 1:
                    loginAdminMenu:
                    bool adminLogin = menu.LoginAdminMenu(ListAdmin);
                    adminMenu:
                    if (adminLogin)
                        switch (menu.AdminMenu())
                        {
                            case 1:
                                user.RenderAccount(ListTheTu);
                                _sp.PressKeyToExit();
                                goto adminMenu;
                            case 2:
                                user.CreateAccount(ListTheTu); 
                                _sp.PressKeyToExit();
                                goto adminMenu;
                            case 3:
                                user.DeleteAccount(ListTheTu);
                                _sp.PressKeyToExit();
                                goto adminMenu;
                            case 4:
                                user.UnLockAccount(ListTheTu);
                                _sp.PressKeyToExit();
                                goto adminMenu;
                            default:
                                _sp.Await(true, "Dang Xuat Thanh Cong!", "");
                                goto mainMenu;
                        }
                    else
                        goto loginAdminMenu;
                    break;
                case 2:
                    User userLogin = user.Login(ListTheTu);
                    if (userLogin.Id != 0)
                        switch (menu.UserMenu())
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
                                _sp.Await(true, "Dang Xuat Thanh Cong!", "");
                                goto mainMenu;
                        }
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
    }
}
