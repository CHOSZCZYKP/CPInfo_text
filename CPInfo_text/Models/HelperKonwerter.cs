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
        public static string KonwerterTypuNaJednostke(SensorType sensorType, string jednostkaTemperatury)
        {
            switch (sensorType)
            {
                case SensorType.Temperature:
                    return jednostkaTemperatury;
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
                    return "";
                case SensorType.Data:
                    return "GB";
                case SensorType.SmallData:
                    return "MB";
                case SensorType.Throughput:
                    return "B/s";
                case SensorType.TimeSpan:
                    return "s";
                case SensorType.Energy:
                    return "J";
                case SensorType.Noise:
                    return "dB";
                default:
                    return "";
            }

        }

        public static float KonwerterCelciuszNaFahrennheit(float temperaturaC)
        {
            return (float)(temperaturaC * 1.8) + 32;
        }
    }
}
