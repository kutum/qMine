using System;
using MinecraftServerRCON;
using qMineStat;
using Renci.SshNet;

namespace testConsole
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
            Console.WriteLine("Minecraft server status of {0} on port {1}:", ms.Address, ms.Port);
            if (ms.ServerUp)
            {
                Console.WriteLine("Server is online running version {0} with {1} out of {2} players.", ms.Version, ms.CurrentPlayers, ms.MaximumPlayers);
                Console.WriteLine("Message of the day: {0}", ms.Motd);
                Console.WriteLine("Latency: {0}ms", ms.Latency);
            }
            else
                Console.WriteLine("Server is offline!");



            ConnectionInfo ConnNfo = new ConnectionInfo("192.168.1.5", 22, "kutum",
             new AuthenticationMethod[]{
                new PasswordAuthenticationMethod("kutum","myofrene")
             }
         );

            using (var sshclient = new SshClient(ConnNfo))
            {
                sshclient.Connect();
                using (var cmd = sshclient.CreateCommand("service spigot status | grep active"))
                {
                    cmd.Execute();
                    Console.WriteLine("Command>" + cmd.CommandText);
                    Console.WriteLine("Return Value = {0}", cmd.Result);
                }
                sshclient.Disconnect();
            }

            Console.WriteLine("---------------------------PRESS ANY KEY");

            Console.ReadKey();
        }

       
    }

}
