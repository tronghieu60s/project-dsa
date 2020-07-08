using project_dsa.components;
using project_dsa.helps;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace project_dsa.methods
{
    class UserMethods
    {
        private User _us = new User();
        private GiaoDich _gd = new GiaoDich();
        private Support _sp = new Support();

        public void ShowInfo(User user)
        {
            Console.WriteLine($"\t- ID: {user.Id}");
            Console.WriteLine($"\t- Chu tai khoan: {user.Name}");
            Console.WriteLine($"\t- So du: {user.Balance}");
            Console.WriteLine($"\t- Loai tien te: {user.Currency}");
        }

        public void Withdrawal(User user)
        {
            Console.Write("\t- Nhap so tien can rut: ");
            int money; int.TryParse(Console.ReadLine(), out money);
            if (money >= 50000 && user.Balance - money >= 50000)
            {
                if (money % 50 == 0)
                {
                    user.Balance -= money;
                    _us.SaveFile(user);
                    _gd.SaveFile(user, "Rut Tien", money, 0);
                    _sp.Await(true, "Rut tien thanh cong!", "Rut tien that bai!");
                }
                else _sp.Await(false, "", "So tien nhap phai la boi so cua 50!");
            }
            else _sp.Await(false, "", "Ban khong du tien!");
        }

        public void Transfers(User user)
        {
            Console.Write("\t- Nhap id tai khoan muon chuyen: ");
            long id; long.TryParse(Console.ReadLine(), out id);
            Console.Write("\t- Nhap so tien can chuyen: ");
            int money; int.TryParse(Console.ReadLine(), out money);

            if (money >= 50000 && user.Balance - money >= 50000)
            {
                if (money % 50 == 0)
                {
                    User userReceive = _us.GetFile(id);
                    if(userReceive.Id != 0)
                    {
                        user.Balance -= money;
                        userReceive.Balance += money;
                        _us.SaveFile(userReceive);
                        _us.SaveFile(user);
                        _gd.SaveFile(userReceive, "Nhan Tien", money, user.Id);
                        _gd.SaveFile(user, "Chuyen Tien", money, userReceive.Id);
                        _sp.Await(true, "Chuyen tien thanh cong!", "");
                    }
                    else _sp.Await(false, "", "Khong tim thay nguoi dung nay!");
                }
                else _sp.Await(false, "", "So tien nhap phai la boi so cua 50!");
            }
            else _sp.Await(false, "", "Ban khong du tien!");
        }

        public void RenderTransaction(User user)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\tGiao Dich");
            Console.Write($"\tSo Tien");
            Console.Write($"\t\tThoi Gian");
            Console.WriteLine($"\t\t\tTu");
            Console.ResetColor();
            LinkedList<GiaoDich> ListGiaoDich = _gd.GetFile(user.Id);
            for (LinkedListNode<GiaoDich> p = ListGiaoDich.First; p != null; p = p.Next)
            {
                Console.Write($"\t{p.Value.Type}");
                Console.Write($"\t{p.Value.Amount}");
                Console.Write($"\t\t{p.Value.Time}");
                Console.WriteLine($"\t\t{p.Value.IdTf}");
            }
        }

        public void ChangePin(LinkedList<TheTu> ListTheTu, User user)
        {
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                if (p.Value.Id == user.Id)
                {
                    Console.Write("\t- Nhap ma pin cu: ");
                    int oldPin; int.TryParse(_sp.HidePass(), out oldPin);
                    Console.Write("\n\t- Nhap ma pin moi: ");
                    int newPin; int.TryParse(_sp.HidePass(), out newPin);
                    Console.Write("\n\t- Nhap lai ma pin moi: ");
                    int reNewPin; int.TryParse(_sp.HidePass(), out reNewPin);
                    if (oldPin == p.Value.Pin)
                    {
                        if (newPin == reNewPin)
                        {
                            p.Value.Pin = newPin;
                            _us.SaveFile(ListTheTu);
                            _sp.Await(true, "Doi ma pin thanh cong!", "");
                        }
                        else _sp.Await(false, "", "Nhap ma pin khong khop!");
                    }
                    else _sp.Await(false, "", "Nhap sai ma pin!");
                    break;
                }
            }
        }
    }
}
