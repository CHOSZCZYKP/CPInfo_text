using LibreHardwareMonitor.Hardware;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CPInfo_text.Models
{
    internal class SpecyfikacjaKomputera
    {
        public List<string> Specyfikacja { get; set; }
        public SpecyfikacjaKomputera()
        {
            this.Specyfikacja = new List<string>();
            var tasks = new List<Task<List<string>>>
            {
                Task.Run(() => DodanieSpecyfikacjiProcesora()),
                Task.Run(() => DodanieSpecyfikacjiPlytyGlownej()),
                Task.Run(() => DodanieSpecyfikacjiRAM()),
                Task.Run(() => DodanieSpecyfikacjiGPU()),
                Task.Run(() => DodanieSpecyfikacjiDysku()),
                Task.Run(() => DodanieSpecyfikacjiKartSieciowych()),
                Task.Run(() => DodanieSpecyfikacjiBios()),
                Task.Run(() => DodanieSpecyfikacjiSystemu()),
            };
            Task.WhenAll(tasks).Wait();
            foreach (var task in tasks)
            {
                Specyfikacja.AddRange(task.Result);
            }

        }
        private List<string> DodanieSpecyfikacjiProcesora()
        {
            List<string> lista = new List<string>();

            lista.Add("Informacje o procesorze");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            lista.Add($"        {property.Name}: {property.Value}");
                        }

                    }
                }
            }
            return lista;
        }
        private List<string> DodanieSpecyfikacjiPlytyGlownej()
        {
            List<string> lista = new List<string>();

            lista.Add("Informacje o płycie głównej");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_BaseBoard"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            lista.Add($"        {property.Name}: {property.Value}");
                        }
                    }
                }
            }
            return lista;
        }
        private List<string> DodanieSpecyfikacjiRAM()
        {
            List<string> lista = new List<string>();

            lista.Add("Inforamcje o RAM");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory"))
            {
                int licznik = 1;
                foreach (var obj in searcher.Get())
                {
                    lista.Add($"Pamięć {licznik}");

                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            if (property.Name.Equals("Capacity"))
                            {
                                lista.Add($"        {property.Name}: {Convert.ToInt64(obj["Capacity"]) / (1024 * 1024 * 1024)} GB");
                            }
                            else
                            {
                                lista.Add($"        {property.Name}: {property.Value}");
                            }

                        }
                    }
                    licznik++;
                }
            }
            return lista;
        }
        private List<string> DodanieSpecyfikacjiGPU()
        {
            List<string> lista = new List<string>();
            lista.Add("Informacje o karcie graficznej");
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
                                            lista.Add($"        {property.Name}: {sensor.Value.GetValueOrDefault()} MB");
                                        }
                                    }
                                }
                                computer.Close();
                            }
                            else
                            {
                                lista.Add($"        {property.Name}: {property.Value}");
                            }

                        }
                    }
                }
            }
            return lista;
        }
        private List<string> DodanieSpecyfikacjiDysku()
        {
            List<string> lista = new List<string>();
            lista.Add("Informacje o dyskach");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive"))
            {
                int licznik = 1;
                foreach (var obj in searcher.Get())
                {
                    lista.Add($"Dysk {licznik}");
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            if (property.Name.Equals("Size"))
                            {
                                lista.Add($"        {property.Name}: {(UInt64)Convert.ToInt64(obj["Size"]) / (1024 * 1024 * 1024)} GB");
                            }
                            else
                            {
                                lista.Add($"        {property.Name}: {property.Value}");
                            }

                        }
                    }
                }
            }
            return lista;
        }
        private List<string> DodanieSpecyfikacjiKartSieciowych()
        {
            List<string> lista = new List<string>();
            lista.Add("Informacje o kartach sieciowych");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_NetworkAdapter  where NetEnabled=true"))
            {
                int licznik = 1;
                foreach (var obj in searcher.Get())
                {
                    lista.Add($"Karta sieciowa {licznik}");
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            lista.Add($"        {property.Name}: {property.Value}");
                        }
                    }
                    licznik++;
                }
            }
            return lista;
        }
        private List<string> DodanieSpecyfikacjiSystemu()
        {
            List<string> lista = new List<string>();
            lista.Add("Informacje o systemie");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            lista.Add($"        {property.Name}: {property.Value}");
                        }
                    }
                }
            }
            return lista;
        }
        private List<string> DodanieSpecyfikacjiBios()
        {
            List<string> lista = new List<string>();
            lista.Add("Informacje o BIOS-ie");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_BIOS"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            lista.Add($"        {property.Name}: {property.Value}");
                        }
                    }
                }
            }
            return lista;
        }
        public void ZapisDoPliku(string calaSciezka, ProgressTask task)
        {
            using (StreamWriter streamWriter = new StreamWriter(calaSciezka))
            {
                SpecyfikacjaKomputera specyfikacjaKomputera = new SpecyfikacjaKomputera();

                foreach (var info in specyfikacjaKomputera.Specyfikacja)
                {
                    streamWriter.WriteLine(info);
                    task.Increment(1);
                    Thread.Sleep(1);
                }

            }
        }
    }
}
