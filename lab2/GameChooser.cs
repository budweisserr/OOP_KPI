namespace lab2
{
    public class GameChooser
    {
        public Game PlayStandartGame(BaseGameAccount first, BaseGameAccount second, int rating)
        {
            return new StandartGame(first, second, rating);
        }
        public Game PlayTrainingGame(BaseGameAccount first, BaseGameAccount second, int rating)
        {
            return new TrainingGame(first, second, rating);
        }

        public Game SinglePlayGame(BaseGameAccount first, BaseGameAccount second, int rating)
        {
            return new SinglePlayGame(first, second, rating);
        }
    }
}