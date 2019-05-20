using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProject.Data
{
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string birthday { get; set; }
        public int admin { get; set; }
        public string barcode { get; set; }
        public bool active { get; set; }
    }
}
