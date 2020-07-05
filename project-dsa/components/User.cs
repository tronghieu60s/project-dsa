using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using project_dsa.helps;

namespace project_dsa.components
{
    class User : TheTu
    {
        private string _name;
        private int _balance;
        private string _currency;

        public string Name { get => _name; set => _name = value; }
        public int Balance { get => _balance; set => _balance = value; }
        public string Currency { get => _currency; set => _currency = value; }

        public User() : base()
        {
            _name = "";
            _balance = 0;
            _currency = "";
        }

        public User(long id, int pin, string name, int balance, string currency, bool locked) : base(id, pin, locked)
        {
            _name = name;
            _balance = balance;
            _currency = currency;
        }

        public int Menu()
        {
            int select;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\tDANG NHAP USER\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t*");
            Console.WriteLine("*********************************");
            Console.WriteLine("************* MENU **************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Xem thong tin tai khoan");
            Console.WriteLine("2. Rut tien");
            Console.WriteLine("3. Chuyen Tien");
            Console.WriteLine("4. Xem noi dung giao dich");
            Console.WriteLine("5. Doi ma pin");
            Console.WriteLine("5. Thoat");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            // select
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ban chon:\t");
            Console.ResetColor();
            int.TryParse(Console.ReadLine(), out select);
            return select;
        }

        public void SaveFile(User user)
        {
            // save file
            string path = $"D:/{user.Id}.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine($"{user.Id}#{user.Name}#{user.Balance}#{user.Currency}");
            }
        }

        public void ShowInfo(User user)
        {
            Console.WriteLine($"ID: {user.Id}");
            Console.WriteLine($"Chu tai khoan: {user.Name}");
            Console.WriteLine($"So du: {user.Balance}");
            Console.WriteLine($"Loai tien te: {user.Currency}");
        }

        public void Withdrawal(User user)
        {
            Support _sp = new Support();
            GiaoDich _gd = new GiaoDich();

            Console.Write("Nhap so tien can rut: ");
            int money; int.TryParse(Console.ReadLine(), out money);
            if (money >= 50000 && user.Balance - money >= 50000)
            {
                if(money % 50 == 0)
                {
                    user.Balance -= money;
                    SaveFile(user);
                    _gd.SaveFile(user, "Rut Tien", money);
                    _sp.Await(true, "Rut tien thanh cong!", "Rut tien that bai!");
                }
                else Console.WriteLine("So tien nhap phai la boi so cua 50.");
            }
            else Console.WriteLine("Ban khong du tien.");
        }

        public void CreateUser(long id)
        {
            Console.Write("Nhap ten khach hang: ");
            string name = Console.ReadLine();
            Console.Write("Nhap so du: ");
            int balance; int.TryParse(Console.ReadLine(), out balance);
            Console.Write("Nhap loai tien te: ");
            string currency = Console.ReadLine();
            string userPath = $"D:/{id}.txt";
            string historyUserPath = $"D:/LichSu{id}.txt";
            using (StreamWriter sw = new StreamWriter(historyUserPath)) { }
            using (StreamWriter sw = new StreamWriter(userPath))
                sw.WriteLine($"{id}#{name}#{balance}#{currency}");
        }
    }
}
