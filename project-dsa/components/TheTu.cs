using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace project_dsa.components
{
    class TheTu
    {
        private long _id;
        private int _pin;
        private bool _locked;

        public long Id { get => _id; set => _id = value; }
        public int Pin { get => _pin; set => _pin = value; }
        public bool Locked { get => _locked; set => _locked = value; }

        public TheTu()
        {
            _id = 0;
            _pin = 0;
        }

        public TheTu(long id, int pin, bool locked)
        {
            _id = id;
            _pin = pin;
            _locked = locked;
        }

        public LinkedList<TheTu> Initialization()
        {
            back:
            try
            {
                string path = "D:/TheTu.txt";
                LinkedList<TheTu> ListTheTu = new LinkedList<TheTu>();
                using (StreamReader rd = new StreamReader(path))
                {
                    int spt = Convert.ToInt32(rd.ReadLine());
                    for (int i = 0; i < spt; i++)
                    {
                        string line = rd.ReadLine();
                        string[] seperator = new string[] { "#" };
                        string[] arr = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                        // convert
                        long id; long.TryParse(arr[0], out id);
                        bool locked; bool.TryParse(arr[2], out locked);
                        TheTu card = new TheTu(id, Convert.ToInt32(arr[1]), locked);
                        ListTheTu.AddLast(card);
                    }
                }
                return ListTheTu;
            }
            catch (Exception)
            {
                string path = "D:/TheTu.txt";
                using (StreamWriter sw = new StreamWriter(path)) { sw.WriteLine(0); };
                goto back;
                throw;
            }
        }

        public bool Login(LinkedList<TheTu> ListTheTu)
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
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
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

        public void CreateAccount(LinkedList<TheTu> ListTheTu)
        {
            bool status, locked = false;
            int pin = 123456;
            long id;
            do
            {
                status = true;
                id = randomNumber(14);
                for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
                {
                    if (p.Value.Id == id)
                    {
                        status = false; break;
                    }
                }
            } while (!status);

            // create user
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
            {
                sw.Write(id);
                sw.Write("#");
                sw.Write(name);
                sw.Write("#");
                sw.Write(balance);
                sw.Write("#");
                sw.Write(currency);
                sw.Write("#");
            }

            // create card
            TheTu card = new TheTu(id, pin, locked);
            ListTheTu.AddLast(card);
            // save file
            string path = "D:/TheTu.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(ListTheTu.Count);
                for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
                {
                    sw.Write(p.Value.Id);
                    sw.Write("#");
                    sw.Write(p.Value.Pin);
                    sw.Write("#");
                    sw.WriteLine(p.Value.Locked);
                }
            }
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

        private long randomNumber(int length)
        {
            string sNumber = "";
            long lNumber;
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sNumber += rd.Next(1, 9);
            }
            long.TryParse(sNumber, out lNumber);
            return lNumber;
        }
    }
}
