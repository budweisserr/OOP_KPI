using System;
using System.Runtime.CompilerServices;


namespace lab2
{
    public abstract class Game
    {
        private static Random _rand = new Random();
        private int _chooser = _rand.Next(0, 2);
        protected BaseGameAccount Winner;
        protected BaseGameAccount Loser;
        private static uint _seed = 1;
        protected uint Index { get; set; }
        protected int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(_rating),"Rating must be positive");
                }
                _rating = value;
            }
        }

        protected virtual void Initialize(BaseGameAccount first, BaseGameAccount second, int rating)
        {
            Rating = rating;

            Winner.WinGame(this);
            Loser.LoseGame(this);
        }

        protected Game(BaseGameAccount first, BaseGameAccount second, int rating)
        { 
           Index = _seed++;
           if (_chooser == 0)
           {
               Winner = first;
               Loser = second;
           }
           else
           {
               Winner = second;
               Loser = first;
           }
           Initialize(first,second,rating);
        }

        public abstract string GameInfo();
    }

    public class StandartGame : Game
    {
        public StandartGame(BaseGameAccount first, BaseGameAccount second, int rating) : base(first, second, rating)
        {
        }
        public override string GameInfo()
        {
            return $"ID of game: {Index} --> {Winner.UserName} has pwned {Loser.UserName} in standart game and earned {_rating} pts";
        }
        
    }

    public class TrainingGame : Game
    {
        protected override void Initialize(BaseGameAccount first, BaseGameAccount second, int rating)
        {
            Rating = 0;
            Winner.WinGame(this);
            Loser.LoseGame(this); 
        }

        public TrainingGame(BaseGameAccount first, BaseGameAccount second, int rating) : base(first, second, rating)
        {
        }
        public override string GameInfo()
        {
            return $"ID of game: {Index} --> {Winner.UserName} has pwned {Loser.UserName} in training game and earned {_rating} pts";
        }
    }

    public class SinglePlayGame : Game
    {
        public SinglePlayGame(BaseGameAccount first, BaseGameAccount second, int rating) : base(first, second, rating)
        {
        }
        public override string GameInfo()
        {
            return $"ID of game: {Index} --> {Winner.UserName} has pwned {Loser.UserName} in single player game and earned {_rating} pts";
        }
    }
}