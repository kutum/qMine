using System;
using MinecraftServerRCON;
using qMineStat;

namespace rconConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var rcon = RCONClient.INSTANCE)
            {
                rcon.setupStream("192.168.1.5", password: "myofrene");
                var answer = rcon.sendMessage(RCONMessageType.Command, "help 1");
                Console.WriteLine(answer.RemoveColorCodes());
                
            }

            MineStat ms = new MineStat("192.168.1.5", 25565);
            Console.WriteLine("Minecraft server status of {0} on port {1}:", ms.GetAddress(), ms.GetPort());
            if (ms.IsServerUp())
            {
                Console.WriteLine("Server is online running version {0} with {1} out of {2} players.", ms.GetVersion(), ms.GetCurrentPlayers(), ms.GetMaximumPlayers());
                Console.WriteLine("Message of the day: {0}", ms.GetMotd());
                Console.WriteLine("Latency: {0}ms", ms.GetLatency());
            }
            else
                Console.WriteLine("Server is offline!");

            Console.ReadKey();
        }
    }
}
