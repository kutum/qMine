using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qMIne.Models
{
    /// <summary>
    /// Данные Minecraft сервера для rcon подключения
    /// </summary>
    [Table("ServerCredentials")]
    public class ServerCredentials
    {
        public ServerCredentials() { }

        public ServerCredentials(string _Name, string _IP = "127.0.0.1", int _Port = 25575, string _Password = "")
        {
            Name = _Name;
            IP = _IP;
            Port = _Port;
            Password = _Password;
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
        /// Password
        /// </summary>
        [Display(Name = "Server Password")]
        public string Password { get; set; }
    }
}