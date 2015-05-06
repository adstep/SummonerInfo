using LolNexusScraper.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LolNexusScraperTests
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using(WebClient client = new WebClient())
            {
                var json = new WebClient().DownloadString("http://www.lolnexus.com/ajax/get-game-info/NA.json?name=DoctorBrowne");
            }

            Stopwatch watch = new Stopwatch();

            watch.Start();
            LolNexusMatch match = new LolNexusMatch("NA", "GochuHunted");
            watch.Stop();

            Console.Out.WriteLine(watch.ElapsedMilliseconds);

        }
    }
}
