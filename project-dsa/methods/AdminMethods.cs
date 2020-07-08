using project_dsa.components;
using project_dsa.helps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace project_dsa.methods
{
    class AdminMethods
    {
        private User _us = new User();
        private TheTu _cd = new TheTu();
        private Support _sp = new Support();

        public void RenderAccount(LinkedList<TheTu> ListTheTu)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\tID");
            Console.Write($"\t\t\tName");
            Console.Write($"\t\t\tSo Du");
            Console.Write($"\t\t\tLoai Tien");
            Console.WriteLine($"\tTrang Thai");
            Console.ResetColor();
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                User user = _us.GetFile(p.Value.Id);
                if(user.Id != 0)
                {
                    string status = Convert.ToBoolean(p.Value.Locked) ? "Bi Khoa" : "Su Dung";
                    Console.Write($"\t{user.Id}");
                    Console.Write($"\t\t{user.Name}");
                    Console.Write($"\t\t{user.Balance}");
                    Console.Write($"\t\t\t{user.Currency}");
                    Console.WriteLine($"\t\t{status}");
                }
            }
        }

        public void CreateAccount(LinkedList<TheTu> ListTheTu)
        {
            int pin = 123456;
            User user = CreateUser(ListTheTu);
            TheTu card = new TheTu(user.Id, pin, false);
            ListTheTu.AddLast(card);

            _cd.SaveFile(ListTheTu);
            _sp.Await(true, $"Tao tai khoan {user.Id} - " +
                $"{user.Name} - " +
                $"{user.Balance} {user.Currency} thanh cong!", "");
        }

        public void DeleteAccount(LinkedList<TheTu> ListTheTu)
        {
            RenderAccount(ListTheTu);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nhap ID can xoa:\t");
            Console.ResetColor();
            long id; long.TryParse(Console.ReadLine(), out id);
            bool status = false;

            // delete
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                if (p.Value.Id == id)
                {
                    status = true;
                    ListTheTu.Remove(p);
                    // delete file
                    File.Delete($"D:/{p.Value.Id}.txt");
                    File.Delete($"D:/LichSu{p.Value.Id}.txt");
                    _cd.SaveFile(ListTheTu);
                    break;
                }
            }
            _sp.Await(status, "Xoa Thanh Cong!", "Khong tim thay ID nay!");
        }

        public void UnLockAccount(LinkedList<TheTu> ListTheTu)
        {
            RenderAccount(ListTheTu);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nhap ID can unlock:\t");
            Console.ResetColor();
            long id; long.TryParse(Console.ReadLine(), out id);
            bool status = false;

            // unlock
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                if (p.Value.Id == id)
                {
                    status = true;
                    p.Value.Locked = false;
                    _cd.SaveFile(ListTheTu);
                    break;
                }
            }
            _sp.Await(status, "Mo khoa Thanh Cong!", "Khong tim thay ID nay!");
        }

        public User CreateUser(LinkedList<TheTu> ListTheTu)
        {
            int balance;
            string name, currency;
            long id = _sp.RandomID(ListTheTu, 14);
            do
            {
                Console.Write("\t- Ten khach hang: ");
                name = Console.ReadLine();
            } while (name.Length == 0);
            do
            {
                Console.Write("\t- So du: ");
                int.TryParse(Console.ReadLine(), out balance);
            } while (balance == 0);
            do
            {
                Console.Write("\t- Loai tien te: ");
                currency = Console.ReadLine();
            } while (currency.Length == 0);

            User user = new User(id, name, balance, currency);

            // create file
            string userPath = $"D:/{id}.txt";
            string historyUserPath = $"D:/LichSu{id}.txt";
            using (StreamWriter sw = new StreamWriter(historyUserPath)) sw.WriteLine(0);
            using (StreamWriter sw = new StreamWriter(userPath))
                sw.WriteLine($"{user.Id}#{user.Name}#{user.Balance}#{user.Currency}");
            return user;
        }
    }
}
