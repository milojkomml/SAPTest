using System;
using System.Collections.Generic;
using System.Text;

namespace SAPTest.Models
{
    public class Room
    {
        public int IdRoom { get; set; }
        public int[] FreeDays = new int[365];
    }
}
