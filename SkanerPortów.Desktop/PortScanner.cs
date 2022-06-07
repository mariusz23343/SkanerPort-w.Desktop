using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkanerPortów.Desktop
{
    public class PortScanner
    {
        private string host;
        private PortList portList;
        public Dictionary<int, bool> portDict;

        public PortScanner(string host, int portStart, int portStop)
        {
            this.host = host;
            this.portList = new PortList(portStart, portStop);
            portDict = new Dictionary<int, bool>();
        }
        public void start(int threadCounter)
        {
            Thread[] thread1 = new Thread[threadCounter];


            for (int i = 0; i < threadCounter; i++)
            {
                thread1[i] = new Thread(new ThreadStart(RunScanTcp));
                thread1[i].Start();

            }

            foreach (Thread t in thread1)
            {
                t.Join();
            }

        }
        public void RunScanTcp()
        {

            int port;
            TcpClient tcp = new TcpClient();
            while ((port = portList.NextPort()) != -1)
            {
                Console.Title = "Obecny skanowany port: " + port.ToString();
                try
                {
                    //jesli port jest wolny, to mozna go przypisac do nowego tcpClienta, 
                    tcp = new TcpClient(host, port);
                    Console.WriteLine("Port {0} jest otwarty", port, Console.ForegroundColor = ConsoleColor.Green);
                    portDict.Add(port, true);

                }
                catch //jesli jakas usluga dziala na porcie, nie mozna uruchomic na nim tcp clienta, przez co
                // ten fragment kodu wyrzuci wyjątek, więc port jest dla nas zamknięty
                {
                    Console.WriteLine("Port {0} jest zamknięty", port, Console.ForegroundColor = ConsoleColor.Red);
                    portDict.Add(port, false);
                    continue;
                }
                finally
                {
                    try
                    {
                        tcp.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

        }
    }
}
