using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProject.Data
{
    class Ticket
    {
        //public string id { get; set; }
        public string valid_from { get; set; }
        public string valid_until { get; set; }
        public int nr_of_entries { get; set; }
        public int nr_of_entries_day { get; set; }
        public int hour_from { get; set; }
        public int hour_until { get; set; }
        public int weekend { get; set; }
        public string barcode { get; set; }
    }
}
