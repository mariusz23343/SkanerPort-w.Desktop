using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkanerPortów.Desktop
{
    public class PortListDto
    {
        public Dictionary<int, bool> portDict { get; set; }
        public string IPv4 { get; set; }
        public DateTime ScanDate { get; set; } = DateTime.Now;
    }
}
