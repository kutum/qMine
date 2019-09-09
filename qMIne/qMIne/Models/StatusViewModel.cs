using qMineStat;

namespace qMIne.Models
{
    public class StatusViewModel
    {
        public StatusViewModel(MineStat _MineStat)
        {
            MineStat = _MineStat;
        }

        public MineStat MineStat { get; set; }
    }
}