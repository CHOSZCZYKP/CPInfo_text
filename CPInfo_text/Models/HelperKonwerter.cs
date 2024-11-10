using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static int KonverterMilisekundy(string aktualizacjaInterwalow)
        {
            var regex = new Regex(@"(\d+)\s*(ms|s)", RegexOptions.IgnoreCase);
            var dopasowanie = regex.Match(aktualizacjaInterwalow);

            if (dopasowanie.Success)
            {
                int wartosc = int.Parse(dopasowanie.Groups[1].Value);
                string jednostka = dopasowanie.Groups[2].Value.ToLower();

                if (jednostka == "ms")
                {
                    return wartosc;
                }
                else
                {
                    return wartosc * 1000;
                }
            }
            else
            {
                throw new ArgumentException($"Nieprawidłowy format: {aktualizacjaInterwalow}");
            }
        }
    }
}
