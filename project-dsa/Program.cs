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
            LinkedList<Admin> ListAdmin = Admin.GetFile();
            LinkedList<TheTu> ListTheTu = TheTu.GetFile();

        mainMenu:
            switch (Menu.MainMenu())
            {
                case 1:
                loginAdminMenu:
                    bool adminLogin = Menu.LoginAdminMenu(ListAdmin);
                adminMenu:
                    if (adminLogin)
                        switch (Menu.AdminMenu())
                        {
                            case 1:
                                Admin.RenderAccount(ListTheTu, "");
                                Support.PressKeyToExit();
                                goto adminMenu;
                            case 2:
                                Admin.CreateAccount(ListTheTu);
                                Support.PressKeyToExit();
                                goto adminMenu;
                            case 3:
                                Admin.DeleteAccount(ListTheTu);
                                Support.PressKeyToExit();
                                goto adminMenu;
                            case 4:
                                Admin.UnLockAccount(ListTheTu);
                                Support.PressKeyToExit();
                                goto adminMenu;
                            default:
                                Support.Await(true, "Dang Xuat Thanh Cong!", "");
                                goto mainMenu;
                        }
                    else
                        goto loginAdminMenu;
                case 2:
                    User userLogin = Menu.LoginUserMenu(ListTheTu);
                    userMenu:
                    switch (Menu.UserMenu())
                    {
                        case 1:
                            User.ShowInfo(userLogin);
                            Support.PressKeyToExit();
                            goto userMenu;
                        case 2:
                            User.Withdrawal(userLogin);
                            Support.PressKeyToExit();
                            goto userMenu;
                        case 3:
                            User.Transfers(userLogin);
                            Support.PressKeyToExit();
                            goto userMenu;
                        case 4:
                            User.RenderTransaction(userLogin);
                            Support.PressKeyToExit();
                            goto userMenu;
                        case 5:
                            User.ChangePin(ListTheTu, userLogin);
                            Support.PressKeyToExit();
                            goto userMenu;
                        default:
                            Support.Await(true, "Dang Xuat Thanh Cong!", "");
                            goto mainMenu;
                    }
                case 3:
                    Menu.InfoTeam();
                    Support.PressKeyToExit();
                    goto mainMenu;
                default:
                    break;
            }
        }
    }
}
