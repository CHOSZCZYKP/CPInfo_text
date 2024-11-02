using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPInfo_text.Models
{
    internal class HelperKonwerter
    {
        public static string KonwerterTypuNaJednostke(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Temperature:
                    return "Stopnie C lub F";
                case SensorType.Voltage:
                    return "V";
                case SensorType.Load:
                    return "%";
                case SensorType.Current:
                    return "A";
                case SensorType.Power:
                    return "W";
                case SensorType.Clock:
                    return "MHz";
                case SensorType.Frequency:
                    return "Hz";
                case SensorType.Fan:
                    return "RPM";
                case SensorType.Flow:
                    return "Flow";
                case SensorType.Control:
                    return "%";
                case SensorType.Level:
                    return "%";
                case SensorType.Factor:
                    return "Factor";
                case SensorType.Data:
                    return "GB";
                case SensorType.SmallData:
                    return "SmallData";
                case SensorType.Throughput:
                    return "KB/s";
                case SensorType.TimeSpan:
                    return "TimeSpan";
                case SensorType.Energy:
                    return "Energy";
                case SensorType.Noise:
                    return "Noise";
                default:
                    return "";
            }

        }
    }
}
