using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CPInfo_text.Models
{
    class Helper
    {
        //to działa opróżnia kosz systemowy
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        private enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001, // Bez potwierdzenia
            SHERB_NOPROGRESSUI = 0x00000002,   // Bez okna postępu
            SHERB_NOSOUND = 0x00000004         // Bez dźwięku
        }
        public static void EmptyRecycleBin()
        {
            // Wywołujemy funkcję SHEmptyRecycleBin, ustawiając odpowiednie flagi
            int result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOCONFIRMATION | RecycleFlags.SHERB_NOPROGRESSUI | RecycleFlags.SHERB_NOSOUND);

            // Sprawdzamy, czy operacja się powiodła
            if (result != 0)
            {
                throw new InvalidOperationException("Nie udało się opróżnić Kosza. Kod błędu: " + result);
            }
            else
            {
                Console.WriteLine("Kosz został opróżniony.");
            }
        }
    }
    internal class DoTestu
    {
        static void WyświetlanieProcesora()
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
                    /*Console.WriteLine($"Model procesora: {obj["Name"]}");
                    Console.WriteLine($"Producent: {obj["Manufacturer"]}");
                    Console.WriteLine($"Liczba rdzeni: {obj["NumberOfCores"]}");
                    Console.WriteLine($"Liczba wątków: {obj["NumberOfLogicalProcessors"]}");
                    Console.WriteLine($"Maksymalna częstotliwość: {obj["MaxClockSpeed"]} MHz");*/
                }
            }
        }
        static void WyświetlaniePlytyGlownej()
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
        static void WyświetlanieRAM()
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
        static void WyświetlanieGPU()
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
                                Console.WriteLine($"{property.Name}: {(UInt64)Convert.ToInt64(obj["AdapterRAM"]) / (1024 * 1024)} MB");
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
        static void WyświetlanieDysku()
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
        static void WyświetlanieKartSieciowych()
        {
            Console.WriteLine("Informacje o kartach sieciowych");
            using (var searcher = new ManagementObjectSearcher("select * from Win32_NetworkAdapter  where NetEnabled=true"))// where NetEnabled=true jeśli chcę żeby były widoczne tylko te które są włączone
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
        static void WyświetlanieSystemu()
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
        static void WyświetlanieBios()
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
