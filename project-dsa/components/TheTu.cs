using System;
using System.Collections.Generic;
using System.Text;

namespace project_dsa.components
{
    class TheTu
    {
        private long _id;
        private int _pin;

        public long Id { get => _id; set => _id = value; }
        public int Pin { get => _pin; set => _pin = value; }

        public TheTu()
        {
            _id = 0;
            _pin = 0;
        }

        public TheTu(long id, int pin)
        {
            _id = id;
            _pin = pin;
        }
    }
}
