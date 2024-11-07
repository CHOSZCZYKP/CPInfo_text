using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CPInfo_text.Models
{
    internal class SpecyfikacjaKomputera
    {
        public void WyświetlanieProcesora()
        {
            Console.WriteLine("Informacje o procesorze");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Console.WriteLine($"{property.Name}: {property.Value}");
                        }

                    }
                }
            }
        }
        public void WyświetlaniePlytyGlownej()
        {
            Console.WriteLine("Informacje o płycie głównej");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_BaseBoard"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Console.WriteLine($"{property.Name}: {property.Value}");
                        }
                    }
                }
            }
        }
        public void WyświetlanieRAM()
        {
            Console.WriteLine("Inforamcje o RAM");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory"))
            {
                int licznik = 1;
                foreach (var obj in searcher.Get())
                {
                    Console.WriteLine($"Pamięć {licznik}");

                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            if (property.Name.Equals("Capacity"))
                            {
                                Console.WriteLine($"{property.Name}: {Convert.ToInt64(obj["Capacity"]) / (1024 * 1024 * 1024)} GB");
                            }
                            else
                            {
                                Console.WriteLine($"{property.Name}: {property.Value}");
                            }

                        }
                    }
                    licznik++;
                }
            }
        }
        public void WyświetlanieGPU()
        {
            //zrobić coś taiego że jeśli za wartość adapterRam wstawię wartość totali z libre hardwareMonitor
            Console.WriteLine("Informacje o karcie graficznej");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            if (property.Name.Equals("AdapterRAM"))
                            {
                                Computer computer = new Computer();
                                computer.IsGpuEnabled = true;
                                computer.Open();
                                foreach (var hardware in computer.Hardware)
                                {
                                    foreach (var sensor in hardware.Sensors)
                                    {
                                        if (sensor.Name.Equals("GPU Memory Total"))
                                        {
                                            Console.WriteLine($"{property.Name}: {sensor.Value.GetValueOrDefault()}");
                                        }
                                    }
                                }
                                computer.Close();
                                //Console.WriteLine($"{property.Name}: {(UInt64)Convert.ToInt64(obj["AdapterRAM"]) / (1024 * 1024)} MB");
                            }
                            else
                            {
                                Console.WriteLine($"{property.Name}: {property.Value}");
                            }

                        }
                    }
                }
            }
        }
        public void WyświetlanieDysku()
        {
            Console.WriteLine("Informacje o dyskach");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            if (property.Name.Equals("Size"))
                            {
                                Console.WriteLine($"{property.Name}: {(UInt64)Convert.ToInt64(obj["Size"]) / (1024 * 1024 * 1024)} GB");
                            }
                            else
                            {
                                Console.WriteLine($"{property.Name}: {property.Value}");
                            }

                        }
                    }
                }
            }
        }
        public void WyświetlanieKartSieciowych()
        {
            Console.WriteLine("Informacje o kartach sieciowych");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_NetworkAdapter  where NetEnabled=true"))
            {
                int licznik = 1;
                foreach (var obj in searcher.Get())
                {
                    Console.WriteLine($"Karta sieciowa {licznik}");
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Console.WriteLine($"    {property.Name}: {property.Value}");
                        }
                    }
                    licznik++;
                }
            }
        }
        public void WyświetlanieSystemu()
        {
            Console.WriteLine("Informacje o systemie");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Console.WriteLine($"{property.Name}: {property.Value}");
                        }
                    }
                }
            }
        }
        public void WyświetlanieBios()
        {
            Console.WriteLine("Informacje o BIOS-ie");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_BIOS"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Console.WriteLine($"{property.Name}: {property.Value}");
                        }
                    }
                }
            }
        }
    }
}
