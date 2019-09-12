using qMineStat;

namespace qMine.Models
{
    public class StatusViewModel
    {
        public StatusViewModel(MineStat _MineStat)
        {
            MineStat = _MineStat;
        }

        public StatusViewModel(MineStat _MineStat,int _RefreshRate, string _StatusText = "")
        {
            MineStat = _MineStat;
            RefreshRate = _RefreshRate;
            StatusText = _StatusText;
        }

        public MineStat MineStat { get; set; }

        public int RefreshRate { get; set; }

        public string StatusText { get; set; }
    }
}