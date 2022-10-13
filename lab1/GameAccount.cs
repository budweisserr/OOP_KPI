using System.Collections.Generic;

namespace lab1
{

    class GameAccount
    {
        private uint index { set; get; }
        private string UserName { get; }
        private uint CurrentRating { set; get; }

        private uint GamesCount { set; get; }
        private List<Stats> stats = new List<Stats>();

        public GameAccount(string name)
        {
            UserName = name;
            CurrentRating = 100;
            index = 0;
            GamesCount = 0;
        }

        public void WinGame(string opponentName, uint rating)
        {
            CurrentRating += rating;
            index++;
            GamesCount++;
            var changedRating = new System.Text.StringBuilder();
            changedRating.Append($"+{rating.ToString()}");
            var result = new Stats(index, opponentName, "Win", changedRating.ToString(), CurrentRating);
            stats.Add(result);
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

            index++;
            GamesCount++;
            var changedRating = new System.Text.StringBuilder();
            changedRating.Append($"-{rating.ToString()}");
            var result = new Stats(index, opponentName, "Lose", changedRating.ToString(), CurrentRating);
            stats.Add(result);
        }

        public string GetStats()
        {
            var allStats = new System.Text.StringBuilder();
            allStats.AppendLine($"Stats for {UserName}:");
            allStats.AppendLine("Index\tOpponent\tEnd of game\tEarned rating\tCurrent rating");
            foreach (var item in stats)
            {
                allStats.AppendLine(
                    $"{item.index}\t{item.opponentName}\t\t{item.endGame}\t\t{item.changedRating}\t\t{item.currentRating}");
            }

            allStats.AppendLine($"Total games: {GamesCount}");

            return allStats.ToString();
        }
    }
}