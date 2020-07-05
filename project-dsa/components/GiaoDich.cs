using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace project_dsa.components
{
    class GiaoDich: TheTu
    {
        private string _type;
        private int _amount;
        private DateTime _time;

        public string Type { get => _type; set => _type = value; }
        public int Amount { get => _amount; set => _amount = value; }
        public DateTime Time { get => _time; set => _time = value; }

        public GiaoDich()
        {
            _type = "";
            _amount = 0;
            _time = new DateTime();
        }

        public GiaoDich(long id, int pin, string type, int amount, DateTime time, bool locked): base(id, pin, locked)
        {
            _type = type;
            _amount = amount;
            _time = time;
        }

        public void SaveFile(User user, string type, int amount)
        {
            // save file
            string path = $"D:/LichSu{user.Id}.txt";
            LinkedList<GiaoDich> ListGiaoDich = new LinkedList<GiaoDich>();
            using (StreamReader rd = new StreamReader(path))
            {
                int spt = Convert.ToInt32(rd.ReadLine());
                for (int i = 0; i < spt; i++)
                {
                    string line = rd.ReadLine();
                    string[] seperator = new string[] { "#" };
                    string[] arr = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                    // convert
                    GiaoDich giaoDich = new GiaoDich(user.Id, user.Pin, type, amount, DateTime.Now, user.Locked);
                    ListGiaoDich.AddLast(giaoDich);
                }
            }
            // save file
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(ListGiaoDich.Count);
                for (LinkedListNode<GiaoDich> p = ListGiaoDich.First; p != null; p = p.Next)
                    sw.WriteLine($"{p.Value.Id}#{p.Value.Type}#{p.Value.Amount}#{p.Value.Time}");
            }
        }
    }
}
