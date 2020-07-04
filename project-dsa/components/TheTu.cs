using System;
using System.Collections.Generic;
using System.Text;

namespace project_dsa.components
{
    class TheTu
    {
        private int _id;
        private int _pin;

        public int Id { get => _id; set => _id = value; }
        public int Pin { get => _pin; set => _pin = value; }

        public TheTu(int id, int pin)
        {
            _id = id;
            _pin = pin;
        }
    }
}
