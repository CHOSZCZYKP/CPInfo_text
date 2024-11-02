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
        public float Wartosc { get; set; }

        /*public string KonwerterTypuNaJednostke(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Temperature:
                    return "Temperature";
                case SensorType.Voltage:
                    return "Voltage";
                case SensorType.Load:
                    return "Load";
                case SensorType.Current:
                    return "Current";
                case SensorType.Power:
                    return "Power";
                case SensorType.Clock:
                    return "Clock";
                case SensorType.Frequency:
                    return "Frequency";
                
                default:
                    return "";
            }
        }*/
    }
}
