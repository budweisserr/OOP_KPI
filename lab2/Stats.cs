namespace lab2
{
    public class Stats
    {
        public Game Game { get; }
        public string Status { get; }
        public int Rating { get; }
        public int PlayedRating { get; }
        

        public Stats(string status, Game game, int rating, int playedRating)
        {
            this.Game = game;
            this.Status = status;
            this.Rating = rating;
            this.PlayedRating = playedRating;
        }
    }
}