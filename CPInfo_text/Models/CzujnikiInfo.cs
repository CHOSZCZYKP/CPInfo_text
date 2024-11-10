using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPInfo_text.Models
{
    internal class CzujnikiInfo
    {
        public string NazwaUrzadzenia { get; set; }
        public string NazwaCzujnika { get; set; }
        public SensorType TypJednostki { get; set; }
        public string Wartosc { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }

    }
}
