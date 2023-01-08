namespace lab1
{

    public class Stats
    {
        public string OpponentName { get; }
        public string EndGame { get; }
        public uint CurrentRating { get; }
        public string ChangedRating { get; }
        public uint Index { get; }

        public Stats(uint index, string opponentName, string endGame, string changedRating, uint currentRating)
        {
            this.Index = index;
            this.OpponentName = opponentName;
            this.EndGame = endGame;
            this.ChangedRating = changedRating;
            this.CurrentRating = currentRating;
        }
    }
}