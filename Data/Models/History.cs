using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_Replacement.Data.Models
{
    public class History
    {
        public string StudentID { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Status { get; set; }
    }
}
