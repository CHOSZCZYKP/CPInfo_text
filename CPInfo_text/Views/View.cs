using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CPInfo_text.Views
{
    internal class View
    {

        public void TytulAplikacji()
        {
            string tytul = @"\_   ___ \ \______   \|   |  ____  _/ ____\  ____         _/  |_   ____  ___  ____/  |_ 
/    \  \/  |     ___/|   | /    \ \   __\  /  _ \        \   __\_/ __ \ \  \/  /\   __\
\     \____ |    |    |   ||   |  \ |  |   (  <_> )        |  |  \  ___/  >    <  |  |  
 \______  / |____|    |___||___|  / |__|    \____/  ______ |__|   \___  >/__/\_ \ |__|  
        \/                      \/                 /_____/            \/       \/       ";
            var tytulAplikacji = new Text($"{tytul}", new Style(foreground: Color.Blue, decoration: Decoration.Bold));
            tytulAplikacji.Justification = Justify.Center;
            AnsiConsole.Write(tytulAplikacji);
        }

        public string WidokGlowneMenu()
        {
            string wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Wybierz opcję:")
                .PageSize(5)
                .AddChoices(new[] {
                    "Ustawienia",
                    "Informacje o podzespołach",//dodać jescze opcję jakipodzespół ma jaki sterownik
                    "Czyszczenie dysku",
                    "Informacje",
                    "Wyjdź" 
                })
            );

            AnsiConsole.Clear();
            return wybor;
        }
        public string WidokUstawienia()
        {
            string wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Ustawienia")
                .PageSize(6)
                .AddChoices(new[] 
                {
                    "Reset MIN/MAX",
                    "Wyświetlanie kolumny",
                    "Wybór podzespoółów do monitorowania",
                    "Jednostka temperatury",
                    "Aktualizacja interwałów",
                    "Wróć"
                })
            );

            AnsiConsole.Clear();
            return wybor;
        }
        
        public List<string> WidokWyborPodzespolowDoMonitorowania()
        {
            var wyborPodzespolowDoMoniotorwania = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                .Title("Wybierz które podzespoły chcesz monitorować:")
                .PageSize(9)
                .AddChoices(new[]
                {
                    "CPU",
                    "Płyta główna",
                    "Pamięć RAM",
                    "Karta graficzna",
                    "Kontroler wentylatorów",
                    "Dyski twarde",
                    "Karty sieciowe",
                    "Bateria",
                    "Wróć"
                })
            );
            //to już będzie kontroler zarządzał

            AnsiConsole.Clear();
            return wyborPodzespolowDoMoniotorwania;
        }
        /*public string WidokWidok()
        {
            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Widok")
                .PageSize(3)
                .AddChoices(new[]
                {
                    "Reset MIN/MAX",
                    "Wyświetlanie kolumny",
                    "Wróć"
                })
            );

            AnsiConsole.Clear();
            return wybor;
        }*/

        public List<string> WidokKolumny()
        {
            var wyborKolumn = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                .Title("Wybierz jakie kolumny wyświetlać:")
                .PageSize(4)
                .AddChoices(new[]
                {
                    "Wartość",
                    "Min",
                    "Max",
                    "Wróć",
                })
            );
            //to już będzie kontroler zarządzał
            AnsiConsole.Clear();
            return wyborKolumn;
        }

        /*public void WidokOpcje()
        {
            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Opcje")
                .PageSize(3)
                .AddChoices(new[]
                {
                    "Jednostka temperatury",
                    "Aktualizacja interwałów",
                    "Wróć"
                })
            );
            switch(wybor)
            {
                case "Jednostka temperatury":
                    WidokTemperatur(); 
                    break;
                case "Aktualizacja interwałów":
                    WidokAktualizacjaInterwalow();
                    break;
                case "Wróć":
                    WidokGlowneMenu();
                    break;
            }
            AnsiConsole.Clear();
        }*/

        public string WidokTemperatur()
        {
            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Temperatura")
                .PageSize(3)
                .AddChoices(new[]
                {
                    "Stopnie Celciusza",
                    "Stopnie Farenheita",
                    "Wróć"
                })
            );

            AnsiConsole.Clear();
            return wybor;
        }

        public string WidokInformacjaOProgramie()
        {
            AnsiConsole.Write(new Rule("Informacje o programie").RuleStyle("green").Centered());
            Table tableInfo = new Table();
            tableInfo.Border = TableBorder.Double;
            tableInfo.AddColumn("Nazwa");
            tableInfo.AddColumn("Opis");
            tableInfo.AddRow("Nazwa programu", "CPInfo_text");
            tableInfo.AddRow("Wersja", "1.0.0.1");
            tableInfo.AddRow("Uwagi", "W przypadku niektórych płyt głównych w laptopach program nie jest w stanie pobierać informacji o płycie głównej.");

            AnsiConsole.Write(tableInfo);
            var wybor = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title(@"Kliknij ""Wróć"" jeśli chcesz wrócić do menu głównego")
               .AddChoices("Wróć")
            );

            AnsiConsole.Clear();
            return wybor;
        }

        public string WidokAktualizacjaInterwalow()
        {
            var wybor = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Wybór aktualizacji interwałów")
                .PageSize(7)
                .AddChoices(new[]
                {
                    "250 ms",
                    "500 ms",
                    "1 s",
                    "2 s",
                    "5 s",
                    "10 s",
                    "Wróć"
                })
            ); 
            
            AnsiConsole.Clear();
            return wybor;
        }

        public bool WidokPotwierdzenie()
        {
            var potwierdzenie = AnsiConsole.Prompt(
                new ConfirmationPrompt(@"Czy chcesz wrócić do ustawień nie zapusując zmian? (Nie zaznaczaj ""Wróć"" ta opcja służy do powrotu do ustawień jeśli nie chcesz wprowadzać zmian.)")
                .ShowChoices()
            );

            AnsiConsole.Clear();

            return potwierdzenie;
        }
    }
}
