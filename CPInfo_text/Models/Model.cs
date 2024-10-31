using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPInfo_text.Models
{
    internal class Model
    {
        private Computer _computer;
        public Model()
        {
            _computer = new Computer()
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true,
                IsBatteryEnabled = true,
            };
            _computer.Open();
        }

        public List<CzujnikiInfo> DaneCzujnikow()
        {
            List<CzujnikiInfo> listaCzujnikowInfo = new List<CzujnikiInfo>();

            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();
                foreach (var sensors in hardware.Sensors)
                {
                    CzujnikiInfo czujnikiInfo = new CzujnikiInfo();
                    czujnikiInfo.NazwaUrzadzenia = hardware.Name;
                    czujnikiInfo.NazwaCzujnika = sensors.Name;
                    czujnikiInfo.Wartosc = sensors.Value.GetValueOrDefault();
                    czujnikiInfo.TypJednostki = sensors.SensorType.ToString();
                    

                    listaCzujnikowInfo.Add(czujnikiInfo);
                }
                foreach (var subHardware in hardware.SubHardware)
                {
                    subHardware.Update();
                    foreach (var sensor in subHardware.Sensors)
                    {
                        CzujnikiInfo czujnikiInfo = new CzujnikiInfo();
                        czujnikiInfo.NazwaUrzadzenia = hardware.Name;
                        czujnikiInfo.NazwaCzujnika = sensor.Name;
                        czujnikiInfo.Wartosc = sensor.Value.GetValueOrDefault();
                        czujnikiInfo.TypJednostki = sensor.SensorType.ToString();


                        listaCzujnikowInfo.Add(czujnikiInfo);
                    }
                }

                //dorobić jeszcze dla ukrytych
            }

            return listaCzujnikowInfo;
        }
        public void Dispose()
        {
            _computer.Close();
        }
        public void PodzespolyWyswietlane(List<string> listaPodzespolow)
        {
            foreach (var podzespol in listaPodzespolow)
            {
                
            }

            /*"CPU",
                    "Płyta główna",
                    "Pamięć RAM",
                    "Karta graficzna",
                    "Kontroler wentylatorów",
                    "Dyski twarde",
                    "Karty sieciowe",
                    "Bateria",*/
        }

        public string KonverterJednostekEnum(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Voltage:
                    return "V";
                case SensorType.Temperature:
                    return "℃";
                default:
                    return null;
            }
        }
    }
}
