using System.Collections.Generic;

namespace lab1
{ 
    class GameAccount
    {
        private static uint _index;
        private string UserName { get; }
        private uint CurrentRating { set; get; }

        private uint GamesCount { set; get; }
        private readonly List<Stats> _stats = new List<Stats>();

        public GameAccount(string name)
        {
            UserName = name;
            CurrentRating = 100;
            _index++;
            GamesCount = 0;
        }

        public void WinGame(string opponentName, uint rating)
        {
            CurrentRating += rating;
            GamesCount++;
            var changedRating = new System.Text.StringBuilder();
            changedRating.Append($"+{rating.ToString()}");
            var result = new Stats(_index, opponentName, "Win", changedRating.ToString(), CurrentRating);
            _stats.Add(result);
        }

        public void LoseGame(string opponentName, uint rating)
        {
            if (rating >= CurrentRating)
            {
                CurrentRating = 1;
            }
            else
            {
                CurrentRating -= rating;
            }
            
            GamesCount++;
            var changedRating = new System.Text.StringBuilder();
            changedRating.Append($"-{rating.ToString()}");
            var result = new Stats(_index, opponentName, "Lose", changedRating.ToString(), CurrentRating);
            _stats.Add(result);
        }

        public string GetStats()
        {
            var allStats = new System.Text.StringBuilder();
            allStats.AppendLine($"Stats for {UserName}:");
            allStats.AppendLine("Index\tOpponent\tEnd of game\tEarned rating\tCurrent rating");
            foreach (var item in _stats)
            {
                allStats.AppendLine(
                    $"{item.Index}\t{item.OpponentName}\t\t{item.EndGame}\t\t{item.ChangedRating}\t\t{item.CurrentRating}");
            }

            allStats.AppendLine($"Total games: {GamesCount}");

            return allStats.ToString();
        }
    }
}