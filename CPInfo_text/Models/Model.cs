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
            _computer = new Computer();
            _computer.Open();
        }

        public void DaneCzujnikow(string podzespol)
        {
            switch(podzespol)
            {
                case "CPU":
                    _computer.IsCpuEnabled = true;
                    break;
                case "Płyta główna":
                    _computer.IsMotherboardEnabled = true;
                    break;
                case "Karta graficzna":
                    _computer.IsGpuEnabled = true;
                    break;
                case "Pamięć RAM":
                    _computer.IsMemoryEnabled = true;
                    break;
                case "Dyski twarde":
                    _computer.IsStorageEnabled = true;
                    break;
                case "Karty sieciowe":
                    _computer.IsNetworkEnabled = true;
                    break;
                case "Bateria":
                    _computer.IsBatteryEnabled = true;
                    break;
            }
            //List<CzujnikiInfo> listaCzujnikowInfo = new List<CzujnikiInfo>();
            //_computer.Open();
            ListaCzujnikowInfo.Clear();
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();
                foreach (var sensors in hardware.Sensors)
                {
                    /*CzujnikiInfo czujnikiInfo = new CzujnikiInfo();
                    czujnikiInfo.NazwaUrzadzenia = hardware.Name;
                    czujnikiInfo.NazwaCzujnika = sensors.Name;
                    czujnikiInfo.Wartosc = sensors.Value.GetValueOrDefault();
                    czujnikiInfo.TypJednostki = sensors.SensorType;
                    

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
                        czujnikiInfo.TypJednostki = sensor.SensorType;


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
                foreach (var subhardware in hardware.SubHardware)
                {
                    subhardware.Update();
                }
            }

        }

        public void WylaczenieWszystkichPodzespolow()
        {
            _computer.IsCpuEnabled = false;
            _computer.IsBatteryEnabled = false;
            _computer.IsNetworkEnabled = false;
            _computer.IsGpuEnabled = false;
            _computer.IsMotherboardEnabled = false;
            _computer.IsMemoryEnabled = false;
            _computer.IsStorageEnabled = false;
        }

        
        
    }
}
