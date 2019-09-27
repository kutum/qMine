using qMine.Context;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace qMine.Models
{
    /// <summary>
    /// Данные Minecraft сервера для rcon подключения
    /// </summary>
    [Table("ServerCredentials")]
    public class ServerCredentials
    {
        public ServerCredentials() { }

        public ServerCredentials(
                                string _Name, 
                                string _IP = "127.0.0.1", 
                                int _Port = 25565, 
                                int _RconPort = 25575, 
                                string _Password = "", 
                                string _SSHLogin = "root", 
                                string _SSHPassword = "", 
                                int _SSHPort = 22, 
                                string _SSHMinecraftServiceName = "",
                                int _RefreshRate = 5000)
        {
            Name = _Name;
            IP = _IP;
            Port = _Port;
            RconPort = _RconPort;
            Password = _Password;
            SSHLogin = _SSHLogin;
            SSHPassword = _SSHPassword;
            SSHPort = _SSHPort;
            SSHMinecraftServiceName = _SSHMinecraftServiceName;
            RefreshRate = _RefreshRate;
        }
        /// <summary>
        /// Ключ
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Display(Name="Username")]
        public string Name { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [Display(Name = "Server IP")]
        public string IP { get; set; }
        /// <summary>
        /// Port
        /// </summary>
        [Display(Name = "Server port")]
        public int Port { get; set; }
        /// <summary>
        /// RCON Port
        /// </summary>
        [Display(Name = "RCON port")]
        public int RconPort { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Display(Name = "RCON Password")]
        public string Password { get; set; }
        /// <summary>
        /// Ssh Username
        /// </summary>
        [Display(Name = "SSH Username")]
        public string SSHLogin { get; set; }
        /// <summary>
        /// Ssh Password
        /// </summary>
        [Display(Name = "SSH Password")]
        public string SSHPassword { get; set; }
        /// <summary>
        /// Ssh Port
        /// </summary>
        [Display(Name = "SSH Port")]
        public int SSHPort { get; set; }
        /// <summary>
        /// Ssh Start Server
        /// </summary>
        [Display(Name = "SSH Name Minecraft Service")]
        public string SSHMinecraftServiceName { get; set; }
        /// <summary>
        /// Refresh status ms
        /// </summary>
        [Display(Name = "Refresh status (ms)")]
        public int RefreshRate { get; set; }
        /// <summary>
        /// Dynmap Link
        /// </summary>
        [Display(Name = "Dynmap Link")]
        public string MapUrl { get; set; }

        public async Task<ServerCredentials> GetServerCredentialsAsync(string UserName)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.ServerCredentials.Where(x => x.Name == UserName).FirstOrDefaultAsync();
            }
        }

        public ServerCredentials GetServerCredentials(string UserName)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.ServerCredentials.FirstOrDefault(x => x.Name == UserName);
            }
        }

    }
 
}