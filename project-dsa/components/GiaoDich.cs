using System;
using System.Collections.Generic;
using System.Text;

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

        public GiaoDich(int id, int pin, string type, int amount, DateTime time): base(id, pin)
        {
            _type = type;
            _amount = amount;
            _time = time;
        }
    }
}
