using System;
using System.Collections.Generic;
using System.Text;

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

        public User(string name, int balance, string currency, int id, int pin): base(id, pin)
        {
            _name = name;
            _balance = balance;
            _currency = currency;
        }
    }
}
