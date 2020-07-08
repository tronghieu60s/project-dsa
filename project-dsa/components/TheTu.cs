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
        private int _wrong;
        private static Support _sp = new Support();

        // properties
        public long Id { get => _id; set => _id = value; }
        public int Pin { get => _pin; set => _pin = value; }
        public bool Locked { get => _locked; set => _locked = value; }
        public int Wrong { get => _wrong; set => _wrong = value; }

        // constructor
        public TheTu()
        {
            _id = 0;
            _pin = 0;
            _locked = false;
            _wrong = 0;
        }

        public TheTu(long id)
        {
            _id = id;
            _pin = 0;
            _locked = false;
            _wrong = 0;
        }

        public TheTu(long id, int pin, bool locked)
        {
            _id = id;
            _pin = pin;
            _locked = locked;
            _wrong = 0;
        }

        //methods
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

        public LinkedList<TheTu> GetFile()
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

        public bool Login(LinkedList<TheTu> ListTheTu, long id, int pin)
        {
            bool status = false;
            // checkpass
            for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
            {
                if (id == p.Value.Id)
                {
                    if(p.Value.Wrong < 3)
                    {
                        if (p.Value.Locked)
                        {
                            _sp.Await(false, "", "Tai khoan nay da bi khoa!");
                            break;
                        }
                        else if (pin == p.Value.Pin)
                        {
                            status = true;
                            _sp.Await(true, "Dang nhap thanh cong!", "");
                        }
                        else
                        {
                            p.Value.Wrong++;
                            _sp.Await(false, "", "Tai khoan hoac mat khau khong chinh xac!");
                        }
                    }
                    else
                    {
                        p.Value.Locked = true;
                        SaveFile(ListTheTu);
                        _sp.Await(false, "", "Tai khoan bi khoa do nhap sai qua 3 lan!");
                    }
                    break;
                }
            }
            return status;
        }
    }
}
