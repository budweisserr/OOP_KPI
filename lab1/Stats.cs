namespace lab1
{

    public class Stats
    {
        public string opponentName { get; }
        public string endGame { get; }
        public uint currentRating { get; }
        public string changedRating { get; }
        public uint index { get; }

        public Stats(uint index, string opponentName, string endGame, string changedRating, uint currentRating)
        {
            this.index = index;
            this.opponentName = opponentName;
            this.endGame = endGame;
            this.changedRating = changedRating;
            this.currentRating = currentRating;
        }
    }
}