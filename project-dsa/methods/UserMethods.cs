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
        public void ShowInfo(User user)
        {
            Console.WriteLine($"ID: {user.Id}");
            Console.WriteLine($"Chu tai khoan: {user.Name}");
            Console.WriteLine($"So du: {user.Balance}");
            Console.WriteLine($"Loai tien te: {user.Currency}");
        }

        public void Withdrawal(User user)
        {
            User _user = new User();
            Support _sp = new Support();
            GiaoDich _gd = new GiaoDich();

            Console.Write("Nhap so tien can rut: ");
            int money; int.TryParse(Console.ReadLine(), out money);
            if (money >= 50000 && user.Balance - money >= 50000)
            {
                if (money % 50 == 0)
                {
                    user.Balance -= money;
                    _user.SaveFile(user);
                    _gd.SaveFile(user, "Rut Tien", money, 0);
                    _sp.Await(true, "Rut tien thanh cong!", "Rut tien that bai!");
                }
                else Console.WriteLine("So tien nhap phai la boi so cua 50.");
            }
            else Console.WriteLine("Ban khong du tien.");
        }

        public void Transfers(User user)
        {
            User _user = new User();
            Support _sp = new Support();
            GiaoDich _gd = new GiaoDich();
            User userReceive;

            Console.Write("Nhap id tai khoan muon chuyen: ");
            string id = Console.ReadLine();
            Console.Write("Nhap so tien can chuyen: ");
            int money; int.TryParse(Console.ReadLine(), out money);

            if (money >= 50000 && user.Balance - money >= 50000)
            {
                if (money % 50 == 0)
                {
                    string userPath = $"D:/{id}.txt";
                    using (StreamReader rd = new StreamReader(userPath))
                    {
                        string line = rd.ReadLine();
                        string[] seperator = new string[] { "#" };
                        string[] arr = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                        long idUser; long.TryParse(arr[0], out idUser);
                        userReceive = new User(idUser, arr[1], Convert.ToInt32(arr[2]), arr[3]);
                    }
                    user.Balance -= money;
                    userReceive.Balance += money;
                    _user.SaveFile(userReceive);
                    _user.SaveFile(user);
                    _gd.SaveFile(userReceive, "Nhan Tien", money, user.Id);
                    _gd.SaveFile(user, "Chuyen Tien", money, userReceive.Id);
                    _sp.Await(true, "Chuyen tien thanh cong!", "Chuyen tien that bai!");
                }
                else Console.WriteLine("So tien nhap phai la boi so cua 50.");
            }
            else Console.WriteLine("Ban khong du tien.");
        }

        public void RenderTransaction(User user)
        {
            Console.Write($"\tGiao Dich");
            Console.Write($"\tSo Tien");
            Console.Write($"\t\tThoi Gian");
            Console.WriteLine($"\t\t\tTu");
            GiaoDich _gd = new GiaoDich();
            LinkedList<GiaoDich> ListGiaoDich = _gd.GetTransaction($"{user.Id}");
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
            User _user = new User();
            Support _sp = new Support();

            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                if (p.Value.Id == user.Id)
                {
                    Console.Write("Nhap ma pin cu: ");
                    int oldPin; int.TryParse(_sp.HidePass(), out oldPin);
                    Console.Write("Nhap ma pin moi: ");
                    int newPin; int.TryParse(_sp.HidePass(), out newPin);
                    Console.Write("Nhap lai ma pin moi: ");
                    int reNewPin; int.TryParse(_sp.HidePass(), out reNewPin);
                    if (oldPin == p.Value.Pin)
                    {
                        if (newPin == reNewPin)
                        {
                            p.Value.Pin = newPin;
                            _user.SaveFile(ListTheTu);
                            Console.WriteLine("Doi ma pin thanh cong.");
                        }
                        else Console.WriteLine("Nhap ma pin khong khop.");
                    }
                    else Console.WriteLine("Nhap sai ma pin.");
                    break;
                }
            }
        }
    }
}
