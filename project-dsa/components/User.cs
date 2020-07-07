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

        public User(long id, string name, int balance, string currency) : base(id)
        {
            _name = name;
            _balance = balance;
            _currency = currency;
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

        public string[] GetFileID(long id)
        {
            string path = $"D:/{id}.txt";
            try
            {
                using (StreamReader rd = new StreamReader(path))
                {
                    string line = rd.ReadLine();
                    string[] seperator = new string[] { "#" };
                    string[] arr = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                    return arr;
                }
            }
            catch (Exception)
            {
                using (StreamReader rd = new StreamReader($"D:/TheTu.txt"))
                {

                }
                File.Delete($"D:/LichSu{id}.txt");
                throw;
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
                    _gd.SaveFile(user, "Rut Tien", money, 0);
                    _sp.Await(true, "Rut tien thanh cong!", "Rut tien that bai!");
                }
                else Console.WriteLine("So tien nhap phai la boi so cua 50.");
            }
            else Console.WriteLine("Ban khong du tien.");
        }

        public void Transfers(User user)
        {
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
                    SaveFile(userReceive);
                    SaveFile(user);
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

        public User CreateUser(LinkedList<TheTu> ListTheTu)
        {
            Support _sp = new Support();

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
