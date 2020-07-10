using System;
using System.Collections.Generic;
using System.Text;

namespace SAPTest.Models
{
    public class Booking
    {
        public int IdBooking { get; set; }
        public int StartDay { get; set; }
        public int EndDay { get; set; }
        public bool Status { get; set; }
    }
}
