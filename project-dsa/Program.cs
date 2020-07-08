using project_dsa.components;
using project_dsa.helps;
using project_dsa.methods;
using System;
using System.Collections.Generic;
using System.Threading;

namespace project_dsa
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu _mn = new Menu();
            AdminMethods _adM = new AdminMethods();
            UserMethods _usM = new UserMethods();
            Admin _ad = new Admin();
            User _user = new User();
            Support _sp = new Support();

            LinkedList<Admin> ListAdmin = _ad.GetFile();
            LinkedList<TheTu> ListTheTu = _user.GetFile();

        mainMenu:
            switch (_mn.MainMenu())
            {
                case 1:
                loginAdminMenu:
                    bool adminLogin = _mn.LoginAdminMenu(ListAdmin);
                adminMenu:
                    if (adminLogin)
                        switch (_mn.AdminMenu())
                        {
                            case 1:
                                _adM.RenderAccount(ListTheTu);
                                _sp.PressKeyToExit();
                                goto adminMenu;
                            case 2:
                                _adM.CreateAccount(ListTheTu);
                                _sp.PressKeyToExit();
                                goto adminMenu;
                            case 3:
                                _adM.DeleteAccount(ListTheTu);
                                _sp.PressKeyToExit();
                                goto adminMenu;
                            case 4:
                                _adM.UnLockAccount(ListTheTu);
                                _sp.PressKeyToExit();
                                goto adminMenu;
                            default:
                                _sp.Await(true, "Dang Xuat Thanh Cong!", "");
                                goto mainMenu;
                        }
                    else
                        goto loginAdminMenu;
                case 2:
                loginUserMenu:
                    User userLogin = _mn.LoginUserMenu(ListTheTu);
                userMenu:
                    if (userLogin.Id != 0)
                        switch (_mn.UserMenu())
                        {
                            case 1:
                                _usM.ShowInfo(userLogin);
                                _sp.PressKeyToExit();
                                goto userMenu;
                            case 2:
                                _usM.Withdrawal(userLogin);
                                _sp.PressKeyToExit();
                                goto userMenu;
                            case 3:
                                _usM.Transfers(userLogin);
                                _sp.PressKeyToExit();
                                goto userMenu;
                            case 4:
                                _usM.RenderTransaction(userLogin);
                                _sp.PressKeyToExit();
                                goto userMenu;
                            case 5:
                                _usM.ChangePin(ListTheTu, userLogin);
                                _sp.PressKeyToExit();
                                goto userMenu;
                            default:
                                _sp.Await(true, "Dang Xuat Thanh Cong!", "");
                                goto mainMenu;
                        }
                    else
                        goto loginUserMenu;
                case 3:
                    break;
                default:
                    break;
            }
        }
    }
}
