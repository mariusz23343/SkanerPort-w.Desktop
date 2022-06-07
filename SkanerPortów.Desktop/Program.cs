using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;

namespace SkanerPortów.Desktop
{
    internal class Program
    {
        static  void Main(string[] args)
        {
            string host;
            int portStart;
            int portStop;
            int threads;
            string ip;

            Console.Write("Podaj adres IP: ");
            ip = Console.ReadLine();
            host = ip;

            string startPort;
            Console.WriteLine("Minimalny numer portu: > 1 <");
            Console.Write("Numer poczatkowy: ");
            startPort = Console.ReadLine();
            Console.WriteLine();

            int number;
            bool resultStrt = Int32.TryParse(startPort, out number);
            if (resultStrt)
            {
                portStart = int.Parse(startPort);
            }
            else
            {
                return;
            }
            string endPort;
            Console.WriteLine("Max port: 65535");
            Console.Write("Podaj port koncowy: ");
            endPort = Console.ReadLine();
            int number1;
            bool resultStrt1 = Int32.TryParse(endPort, out number1);
            if (resultStrt)
            {
                portStop = int.Parse(endPort);
            }
            else
            {
                return;
            }
            string watki;
            Console.Write("Wpisz ile watkow ma byc: ");
            watki = Console.ReadLine();
            Console.WriteLine();

            int num3;
            bool watkiResult = Int32.TryParse(watki, out num3);
            if (watkiResult)
            {
                threads = Int32.Parse(watki);
            }
            else
            {
                return;
            }
            if (resultStrt == true && resultStrt1 == true)
            {
                try
                {
                    portStart = int.Parse(startPort);
                    portStop = int.Parse(endPort);
                }
                catch
                {
                    return;
                }
            }
            if (watkiResult == true)
            {
                try
                {
                    threads = int.Parse(watki);
                }
                catch
                {
                    return;
                }
            }
            PortScanner ps = new PortScanner(host, portStart, portStop);

            ps.start(threads);

            var json = JsonConvert.SerializeObject(ps.portDict);

            Console.WriteLine("TEST JSONA!!!!!!");

            Console.WriteLine(json);

            JsonContent content = JsonContent.Create(ps.portDict);

            SendDataToServer(content);

            Console.ReadKey();
        }

        private static void SendDataToServer(JsonContent content)
        {
            var uri = new Uri("https://localhost:44398/Scanner");

            var client = new HttpClient();

            HttpResponseMessage response = client.PostAsync(uri.ToString(), content).Result;
        }
    }
}
