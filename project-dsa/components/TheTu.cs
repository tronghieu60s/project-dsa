using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using project_dsa.helps;

namespace project_dsa.components
{
    class TheTu
    {
        // fields
        private long _id;
        private int _pin;
        private bool _locked;

        // properties
        public long Id { get => _id; set => _id = value; }
        public int Pin { get => _pin; set => _pin = value; }
        public bool Locked { get => _locked; set => _locked = value; }

        // methos
        public TheTu()
        {
            _id = 0;
            _pin = 0;
            _locked = false;
        }

        public TheTu(long id)
        {
            _id = id;
            _pin = 0;
            _locked = false;
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

        public User Login(LinkedList<TheTu> ListTheTu)
        {
            Support _sp = new Support();
            User logged = new User();
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
                    string path = $"D:/{p.Value.Id}.txt";
                    using (StreamReader rd = new StreamReader(path))
                    {
                        string line = rd.ReadLine();
                        string[] seperator = new string[] { "#" };
                        string[] arr = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                        logged = new User(p.Value.Id, arr[1], Convert.ToInt32(arr[2]), arr[3]);
                    }
                    status = true;
                    break;
                }
            }
            _sp.Await(status, "Dang Nhap Thanh Cong!", "Dang Nhap That Bai!");
            return logged;
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

        public void ChangePin(LinkedList<TheTu> ListTheTu, User user)
        {
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
                            SaveFile(ListTheTu);
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
