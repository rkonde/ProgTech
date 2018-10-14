using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Event
    {
        public Event(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public Invoice Invoice { get; set; }
    }
}
