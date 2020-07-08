using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using project_dsa.helps;

namespace project_dsa.components
{
    class User : TheTu
    {
        // fields
        private string _name;
        private int _balance;
        private string _currency;

        // properties
        public string Name { get => _name; set => _name = value; }
        public int Balance { get => _balance; set => _balance = value; }
        public string Currency { get => _currency; set => _currency = value; }

        // constructor
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

        // methods
        public void SaveFile(User user)
        {
            // save file
            string path = $"D:/{user.Id}.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine($"{user.Id}#{user.Name}#{user.Balance}#{user.Currency}");
            }
        }

        public User GetFile(long id)
        {
            string path = $"D:/{id}.txt";
            try
            {
                using (StreamReader rd = new StreamReader(path))
                {
                    string line = rd.ReadLine();
                    string[] seperator = new string[] { "#" };
                    string[] arr = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                    User user = new User(id, arr[1], Convert.ToInt32(arr[2]), arr[3]);
                    return user;
                }
            }
            catch (Exception)
            {
                TheTu theTu = new TheTu();
                LinkedList<TheTu> ListTheTu = theTu.GetFile();
                for (LinkedListNode<TheTu> p = ListTheTu.First; p != null; p = p.Next)
                    if(p.Value.Id == id)
                    {
                        ListTheTu.Remove(p.Value);
                        SaveFile(ListTheTu);
                    }
                File.Delete($"D:/LichSu{id}.txt");
                return new User();
                throw;
            }
        }
    }
}
