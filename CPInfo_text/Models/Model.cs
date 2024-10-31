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
        public List<ISensor> ListaCzujnikowInfo { get; set; }
        public Model()
        {
            this.ListaCzujnikowInfo = new List<ISensor>();
            _computer = new Computer()
            {
                IsCpuEnabled = true,
                /*IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true,
                IsBatteryEnabled = true,*/
            };
            
        }

        public void DaneCzujnikow()
        {
            //List<CzujnikiInfo> listaCzujnikowInfo = new List<CzujnikiInfo>();
            _computer.Open();
            ListaCzujnikowInfo.Clear();
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();
                foreach (var sensors in hardware.Sensors)
                {
                    /*Sensor czujnikiInfo = new CzujnikiInfo();
                    czujnikiInfo.NazwaUrzadzenia = hardware.Name;
                    czujnikiInfo.NazwaCzujnika = sensors.Name;
                    czujnikiInfo.Wartosc = sensors.Value.GetValueOrDefault();
                    czujnikiInfo.TypJednostki = sensors.SensorType.ToString();
                    

                    ListaCzujnikowInfo.Add(czujnikiInfo);*/
                    ListaCzujnikowInfo.Add(sensors);
                }
                foreach (var subHardware in hardware.SubHardware)
                {
                    subHardware.Update();
                    foreach (var sensor in subHardware.Sensors)
                    {
                        /*CzujnikiInfo czujnikiInfo = new CzujnikiInfo();
                        czujnikiInfo.NazwaUrzadzenia = hardware.Name;
                        czujnikiInfo.NazwaCzujnika = sensor.Name;
                        czujnikiInfo.Wartosc = sensor.Value.GetValueOrDefault();
                        czujnikiInfo.TypJednostki = sensor.SensorType.ToString();


                        ListaCzujnikowInfo.Add(czujnikiInfo);*/
                        ListaCzujnikowInfo.Add(sensor);
                    }
                }

            }

            //return listaCzujnikowInfo;
        }
        public void Dispose()
        {
            _computer.Close();
        }

        public void AktualizacjaCzujnikow()
        {
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();
            }
        }

        
    }
}
