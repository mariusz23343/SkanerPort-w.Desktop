using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkanerPortów.Desktop
{
    public class PortList
    {
        private int _start;
        private int _stop;
        private int _ports;
        public PortList(int start, int stop)
        {
            _start = start;
            _stop = stop;
            _ports = _start;
        }
        public bool MorePorts()
        {
            return (_stop - _ports) > 0;
        }
        public int NextPort()
        {
            if (MorePorts())
            {
                return _ports++;
            }
            else return -1;
        }
    }
}
