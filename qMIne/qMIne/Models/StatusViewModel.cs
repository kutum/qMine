using qMineStat;

namespace qMine.Models
{
    public class StatusViewModel
    {

        public StatusViewModel(ServerCredentials _serverCredentials, MineStat _mineStat,string _StatusText = "")
        {
            MineStat = _mineStat;
            serverCredentials = _serverCredentials;
            StatusText = _StatusText;
        }

        public MineStat MineStat { get; set; }

        public ServerCredentials serverCredentials { get; set; }

        public string StatusText { get; set; }
    }
}