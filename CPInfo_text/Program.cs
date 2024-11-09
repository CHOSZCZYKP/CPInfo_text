using CPInfo_text.Controllers;
using CPInfo_text.Models;
using CPInfo_text.Views;
using LibreHardwareMonitor.Hardware;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CPInfo_text
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            View view = new View();
            Model model = new Model();
            Controller controller = new Controller(model, view);
            controller.ControlerGlowneMenu();
            
        }
    }

}
