using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftServerRCON; 

namespace rconConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var rcon = RCONClient.INSTANCE)
            {
                rcon.setupStream("192.168.1.15", password: "myofrene");
                var answer = rcon.sendMessage(RCONMessageType.Command, "help 1");
                Console.WriteLine(answer.RemoveColorCodes());
                Console.ReadKey();
            }
        }
    }
}
