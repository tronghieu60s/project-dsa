using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using project_dsa.helps;

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

        public LinkedList<Admin> GetFile()
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

        public bool Login(LinkedList<Admin> ListAdmin, string user, string pass)
        {
            Support _sp = new Support();
            bool status = false;
            // checkpass
            for (LinkedListNode<Admin> p = ListAdmin.First; p != null; p = p.Next)
            {
                if (user == p.Value.User && pass == p.Value.Pass)
                {
                    status = true; break;
                }
            }
            _sp.Await(status, "Dang nhap thanh cong!", "Tai khoan hoac mat khau khong chinh xac!");
            return status;
        }
    }
}
