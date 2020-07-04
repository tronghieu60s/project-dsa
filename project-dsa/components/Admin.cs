using System;
using System.Collections.Generic;
using System.Text;

namespace project_dsa.components
{
    class Admin
    {
        private string _user;
        private string _pass;

        public string User { get => _user; set => _user = value; }
        public string Pass { get => _pass; set => _pass = value; }

        public Admin(string user, string pass)
        {
            _user = user;
            _pass = pass;
        }
    }
}
