using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace project_dsa.components
{
    class User: TheTu
    {
        private string _name;
        private int _balance;
        private string _currency;

        public string Name { get => _name; set => _name = value; }
        public int Balance { get => _balance; set => _balance = value; }
        public string Currency { get => _currency; set => _currency = value; }

        public User(): base()
        {
            _name = "";
            _balance = 0;
            _currency = "";
        }

        public User(long id, int pin, string name, int balance, string currency): base(id, pin)
        {
            _name = name;
            _balance = balance;
            _currency = currency;
        }

        public LinkedList<User> Initialization()
        {
            string path = "D:/TheTu.txt";
            LinkedList<User> ListUser = new LinkedList<User>();
            using (StreamReader rd = new StreamReader(path))
            {
                int spt = Convert.ToInt32(rd.ReadLine());
                for (int i = 0; i < spt; i++)
                {
                    string line = rd.ReadLine();
                    string[] seperator = new string[] { "#" };
                    string[] arr = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                    long id; long.TryParse(arr[0], out id);
                    User user = new User(id, Convert.ToInt32(arr[1]), arr[2], Convert.ToInt32(arr[3]), arr[4]);
                    ListUser.AddLast(user);
                }
            }
            return ListUser;
        }

        public bool Login(LinkedList<User> ListUser)
        {
            bool status = false;
            long id;
            int pin;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\tDANG NHAP USER\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t*");
            Console.WriteLine("*********************************");
            // user
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ID:\t");
            Console.ResetColor();
            long.TryParse(Console.ReadLine(), out id);
            // pass
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Pin:\t");
            Console.ResetColor();
            int.TryParse(HidePass(), out pin);
            Console.WriteLine("\nVui long cho mot chut...");
            // checkpass
            for (LinkedListNode<User> p = ListUser.First; p != null; p = p.Next)
            {
                if (id == p.Value.Id && pin == p.Value.Pin)
                {
                    status = true; break;
                }
            }
            Thread.Sleep(1000);
            if (status) Console.WriteLine("Dang Nhap Thanh Cong!");
            else Console.WriteLine("Dang Nhap That Bai!");
            Thread.Sleep(1000);
            return status;
        }

        public void Menu()
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
        }

        private string HidePass()
        {
            string pass = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            return pass;
        }
    }
}
