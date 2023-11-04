using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace ConsolePing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Hello! if you initialise the ping, just press any key to quit! \n \n \n \n");
            
            Console.WriteLine("Enter IP Address");
            var hostName = Console.ReadLine();

            using (var ping = new Ping())
            {
                try
                {

                    PingReply reply = ping.Send(hostName);

                    bool stoploop = false;
                    int successful = 0;
                    int count = 1; // Initialize the count to 1
                    while (!stoploop)
                    {
                        if (reply.Status == IPStatus.Success)
                        {

                            successful = count;
                            count++;
                            Console.WriteLine($"Ping to ({hostName}) successful. Round-Trip Time: ({reply.RoundtripTime} ms) , Number of Sucessful Pings: ({successful})");


                        }
                        else
                        {
                            Console.WriteLine($"Ping to ({hostName}) failed Statues: {reply.Status}");

                        }
                        Task.Delay(2000).Wait();

                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                            Console.WriteLine("\n Ping Stopped");
                            stoploop = true;
                        }
                    }
                }
                catch (PingException ex)
                {
                    Console.WriteLine($"Ping to ({hostName}) failed. Statues: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }
    }
}

