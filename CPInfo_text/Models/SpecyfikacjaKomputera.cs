using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CPInfo_text.Models
{
    internal class SpecyfikacjaKomputera
    {
        public List<string> Specyfikacja { get; set; }
        public SpecyfikacjaKomputera()
        {
            this.Specyfikacja = new List<string>();
            /*DodanieSpecyfikacjiProcesora();
            DodanieSpecyfikacjiPlytyGlownej();
            DodanieSpecyfikacjiRAM();
            DodanieSpecyfikacjiGPU();
            DodanieSpecyfikacjiDysku();
            DodanieSpecyfikacjiKartSieciowych();
            DodanieSpecyfikacjiBios();
            DodanieSpecyfikacjiSystemu();*/
            
        }
        public void DodanieSpecyfikacjiProcesora()
        {
            //Console.WriteLine("Informacje o procesorze");
            Specyfikacja.Add("Informacje o procesorze");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Specyfikacja.Add($"         {property.Name}: {property.Value}");
                            //Console.WriteLine($"{property.Name}: {property.Value}");
                        }

                    }
                }
            }
        }
        public void DodanieSpecyfikacjiPlytyGlownej()
        {
            //Console.WriteLine("Informacje o płycie głównej");
            Specyfikacja.Add("Informacje o płycie głównej");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_BaseBoard"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Specyfikacja.Add($"       {property.Name}: {property.Value}");
                            //Console.WriteLine($"{property.Name}: {property.Value}");
                        }
                    }
                }
            }
        }
        public void DodanieSpecyfikacjiRAM()
        {
            //Console.WriteLine("Inforamcje o RAM");
            Specyfikacja.Add("Inforamcje o RAM");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory"))
            {
                int licznik = 1;
                foreach (var obj in searcher.Get())
                {
                    Specyfikacja.Add($"Pamięć {licznik}");

                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            if (property.Name.Equals("Capacity"))
                            {
                                Specyfikacja.Add($"      {property.Name}: {Convert.ToInt64(obj["Capacity"]) / (1024 * 1024 * 1024)} GB");
                            }
                            else
                            {
                                Specyfikacja.Add($"      {property.Name}: {property.Value}");
                            }

                        }
                    }
                    licznik++;
                }
            }
        }
        public void DodanieSpecyfikacjiGPU()
        {
            Specyfikacja.Add("Informacje o karcie graficznej");
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
                                            Specyfikacja.Add($"     {property.Name}: {sensor.Value.GetValueOrDefault()} MB");
                                        }
                                    }
                                }
                                computer.Close();
                                //Console.WriteLine($"{property.Name}: {(UInt64)Convert.ToInt64(obj["AdapterRAM"]) / (1024 * 1024)} MB");
                            }
                            else
                            {
                                Specyfikacja.Add($"      {property.Name}: {property.Value}");
                            }

                        }
                    }
                }
            }
        }
        public void DodanieSpecyfikacjiDysku()
        {
            Specyfikacja.Add("Informacje o dyskach");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive"))
            {
                int licznik = 1;
                foreach (var obj in searcher.Get())
                {
                    Specyfikacja.Add($"Dysk {licznik}");
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            if (property.Name.Equals("Size"))
                            {
                                Specyfikacja.Add($"     {property.Name}: {(UInt64)Convert.ToInt64(obj["Size"]) / (1024 * 1024 * 1024)} GB");
                            }
                            else
                            {
                                Specyfikacja.Add($"      {property.Name}: {property.Value}");
                            }

                        }
                    }
                }
            }
        }
        public void DodanieSpecyfikacjiKartSieciowych()
        {
            Specyfikacja.Add("Informacje o kartach sieciowych");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_NetworkAdapter  where NetEnabled=true"))
            {
                int licznik = 1;
                foreach (var obj in searcher.Get())
                {
                    Specyfikacja.Add($"Karta sieciowa {licznik}");
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Specyfikacja.Add($"     {property.Name}: {property.Value}");
                        }
                    }
                    licznik++;
                }
            }
        }
        public void DodanieSpecyfikacjiSystemu()
        {
            Specyfikacja.Add("Informacje o systemie");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Specyfikacja.Add($"     {property.Name}: {property.Value}");
                        }
                    }
                }
            }
        }
        public void DodanieSpecyfikacjiBios()
        {
            Specyfikacja.Add("Informacje o BIOS-ie");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_BIOS"))
            {
                foreach (var obj in searcher.Get())
                {
                    foreach (var property in obj.Properties)
                    {
                        if (property.Value != null)
                        {
                            Specyfikacja.Add($"     {property.Name}: {property.Value}");
                        }
                    }
                }
            }
        }
    }
}
