using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CPInfo_text.Models
{
    internal class CzyszczenieDysku
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
        public static void OproznianieKosza()
        {
            // Wywołujemy funkcję SHEmptyRecycleBin, ustawiając odpowiednie flagi
            int result = SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOSOUND);//, RecycleFlags.SHERB_NOCONFIRMATION | RecycleFlags.SHERB_NOPROGRESSUI | RecycleFlags.SHERB_NOSOUND

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

        public static void UswuaniePlikowTymczasowych()
        {
            // Pobierz ścieżkę do katalogu tymczasowego
            string tempPath = Path.GetTempPath();

            Console.WriteLine($"Czyszczenie katalogu: {tempPath}\n");

            try
            {
                // Wywołaj metodę do usunięcia plików tymczasowych
                DeleteTemporaryFiles(tempPath);
                Console.WriteLine("\nCzyszczenie zakończone.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }

        private static void DeleteTemporaryFiles(string path)
        {
            // Sprawdź czy katalog istnieje
            if (Directory.Exists(path))
            {
                // Usuń wszystkie pliki w katalogu
                foreach (string file in Directory.GetFiles(path))
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine($"Usunięto plik: {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Nie udało się usunąć pliku {file}: {ex.Message}");
                    }
                }

                // Przejdź przez wszystkie podkatalogi i usuń pliki w każdym z nich
                foreach (string dir in Directory.GetDirectories(path))
                {
                    try
                    {
                        // Rekurencyjne czyszczenie podkatalogów
                        DeleteTemporaryFiles(dir);
                        // Usuń pusty podkatalog
                        Directory.Delete(dir);
                        Console.WriteLine($"Usunięto katalog: {dir}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Nie udało się usunąć katalogu {dir}: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Katalog {path} nie istnieje.");
            }
        }
    }
}
