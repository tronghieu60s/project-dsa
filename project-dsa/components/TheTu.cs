using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using project_dsa.helps;

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
            Support _sp = new Support();
            bool status = false;
            long id; int pin;
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
            int.TryParse(_sp.HidePass(), out pin);
            // checkpass
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                if (id == p.Value.Id && pin == p.Value.Pin)
                {
                    status = true; break;
                }
            }
            _sp.Await(status, "Dang Nhap Thanh Cong!", "Dang Nhap That Bai!");
            return status;
        }

        public void SaveFile(LinkedList<TheTu> ListTheTu)
        {
            // save file
            string path = "D:/TheTu.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(ListTheTu.Count);
                for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
                    sw.WriteLine($"{p.Value.Id}#{p.Value.Pin}#{p.Value.Locked}");
            }
        }

        public void CreateAccount(LinkedList<TheTu> ListTheTu)
        {
            Support _sp = new Support();
            User _user = new User();

            bool locked = false;
            int pin = 123456;
            long id = _sp.RandomID(ListTheTu, 14);
            // create user
            _user.CreateUser(id);
            // create card
            TheTu card = new TheTu(id, pin, locked);
            ListTheTu.AddLast(card);
            SaveFile(ListTheTu);
        }

        public void RenderAccount(LinkedList<TheTu> ListTheTu)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\tID");
            Console.Write($"\t\t\tName");
            Console.Write($"\t\tSo Du");
            Console.Write($"\t\tLoai Tien");
            Console.WriteLine($"\tTrang Thai");
            Console.ResetColor();
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                string path = $"D:/{p.Value.Id}.txt";
                string status = p.Value.Locked ? "Bi Khoa" : "Su Dung";
                using (StreamReader rd = new StreamReader(path))
                {
                    string line = rd.ReadLine();
                    string[] seperator = new string[] { "#" };
                    string[] arr = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                    // convert
                    Console.Write($"\t{arr[0]}");
                    Console.Write($"\t\t{arr[1]}");
                    Console.Write($"\t\t{arr[2]}");
                    Console.Write($"\t\t{arr[3]}");
                    Console.WriteLine($"\t\t{status}");
                }
            }
        }

        public void DeleteAccount(LinkedList<TheTu> ListTheTu)
        {
            Support _sp = new Support();
            RenderAccount(ListTheTu);
            long id; bool status = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nhap ID can xoa:\t");
            Console.ResetColor();
            long.TryParse(Console.ReadLine(), out id);
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
                    break;
                }
            }
            _sp.Await(status, "Xoa Thanh Cong!", "Xoa That Bai!");
            SaveFile(ListTheTu);
        }

        public void UnLockAccount(LinkedList<TheTu> ListTheTu)
        {
            Support _sp = new Support();
            RenderAccount(ListTheTu);
            long id; bool status = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nhap ID can unlock:\t");
            Console.ResetColor();
            long.TryParse(Console.ReadLine(), out id);
            // delete
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                if (p.Value.Id == id)
                {
                    status = true;
                    p.Value.Locked = false;
                    break;
                }
            }
            _sp.Await(status, "Mo khoa Thanh Cong!", "Mo khoa That Bai!");
            SaveFile(ListTheTu);
        }
    }
}
