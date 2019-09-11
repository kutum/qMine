using qMineStat;

namespace qMine.Models
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