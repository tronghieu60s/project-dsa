using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace project_dsa.components
{
    public partial class Admin
    {
        private string _user;
        private string _pass;

        public string User { get => _user; set => _user = value; }
        public string Pass { get => _pass; set => _pass = value; }

        public Admin()
        {
            _user = "";
            _pass = "";
        }

        public Admin(string user, string pass)
        {
            _user = user;
            _pass = pass;
        }

        public LinkedList<Admin> Initialization()
        {
            string path = "D:/Admin.txt";
            LinkedList<Admin> ListAdmin = new LinkedList<Admin>();
            using (StreamReader rd = new StreamReader(path))
            {
                int spt = Convert.ToInt32(rd.ReadLine());
                for (int i = 0; i < spt; i++)
                {
                    string line = rd.ReadLine();
                    string[] seperator = new string[] { "#" };
                    string[] arr = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                    Admin admin = new Admin(arr[0], arr[1]);
                    ListAdmin.AddLast(admin);
                }
            }
            return ListAdmin;
        }

        public bool Login(LinkedList<Admin> ListAdmin)
        {
            bool status = false;
            string user, pass;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\tDANG NHAP ADMIN\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t*");
            Console.WriteLine("*********************************");
            // user
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("User:\t");
            Console.ResetColor();
            user = Console.ReadLine();
            // pass
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Pass:\t");
            Console.ResetColor();
            pass = HidePass();
            Console.WriteLine("\nVui long cho mot chut...");
            // checkpass
            for (LinkedListNode<Admin> p = ListAdmin.First; p != null; p = p.Next)
            {
                if (user == p.Value.User && pass == p.Value.Pass)
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

        public int Menu()
        {
            int select;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\tDANG NHAP ADMIN\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t*");
            Console.WriteLine("*********************************");
            Console.WriteLine("************* MENU **************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Xem danh sach tai khoan");
            Console.WriteLine("2. Them tai khoan");
            Console.WriteLine("3. Xoa tai khoan");
            Console.WriteLine("4. Mo khoa tai khoan");
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
