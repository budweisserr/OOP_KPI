using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    public abstract class BaseGameAccount
    {
        public string UserName { get; }
        protected uint GamesCount { get; private set; }
        private int _curRating;
        protected int CurrentRating
        {
            get => _curRating;
            set
            {
                if (value <= 0)
                    _curRating = 1;
                else _curRating = value;
            }
        }


        protected readonly List<Stats> Stats = new List<Stats>();

        protected BaseGameAccount(string userName)
        {
            UserName = userName;
            GamesCount = 0;
            CurrentRating = 100;
        }

        protected virtual void Update(string status, Game game, int rating)
        {
            GamesCount++;
            Stats current = new Stats(status, game, rating, game.Rating);
            Stats.Add(current);
        }
        public abstract void WinGame(Game game);
        public abstract void LoseGame(Game game);

        public virtual void GetStats()
        {
            Console.WriteLine($"Stats for {UserName}:");
            foreach (var it in Stats)
            {
                var line = new StringBuilder();
                line.Append($"{it.Game.GameInfo()}, so {UserName} {it.Status} {it.PlayedRating} pts and have {it.Rating} pts");
                Console.WriteLine(line.ToString());
            }
            Console.WriteLine($"Current rating: {CurrentRating}");
            Console.WriteLine($"Total games played: {GamesCount}");
        }
    }

    public class GameAccount : BaseGameAccount
    {
        public GameAccount(string userName) : base(userName)
        {
            
        }
        public override void WinGame(Game game)
        {
            if (game.GetType() == typeof(TrainingGame))
                CurrentRating = CurrentRating;
            else CurrentRating += game.Rating;
            Update("Win", game, CurrentRating);
        }

        public override void LoseGame(Game game)
        {
            if (game.GetType() == typeof(SinglePlayGame) || game.GetType() == typeof(TrainingGame))
                CurrentRating = CurrentRating;
            else CurrentRating -= game.Rating;
            Update("Lose", game, CurrentRating);
        }
    }

    public class EasyGameAccount : BaseGameAccount
    {
        public EasyGameAccount(string userName) : base(userName)
        {
            
        } 
        
        public override void WinGame(Game game)
        {
            if (game.GetType() == typeof(TrainingGame))
                CurrentRating = CurrentRating;
            else CurrentRating += game.Rating / 2;
            Update("Win", game, CurrentRating);
        }

        public override void LoseGame(Game game)
        {
            if (game.GetType() == typeof(SinglePlayGame) || game.GetType() == typeof(TrainingGame))
                CurrentRating = CurrentRating;
            else CurrentRating -= game.Rating / 2;
            
            Update("Lose", game, CurrentRating);
        }
    }

    public class BonusGameAccount : BaseGameAccount
    {
        public BonusGameAccount(string userName) : base(userName)
        {
            
        }

        private int _streak;
        private int _added;
        private const int Bonus = 5;

        public override void WinGame(Game game)
        {
            if (game.GetType() == typeof(TrainingGame))
            {
                CurrentRating = CurrentRating;
            }
            else
            {
                _streak++;
                CurrentRating += game.Rating;
            }
            if (_streak >= 3)
            {
                CurrentRating += Bonus;
                _added++;
            }
            Update("Win", game, CurrentRating);
        }

        public override void LoseGame(Game game)
        {
            if (game.GetType() == typeof(SinglePlayGame) || game.GetType() == typeof(TrainingGame))
                CurrentRating = CurrentRating;
            else CurrentRating -= game.Rating;
            
            if (game.GetType() == typeof(TrainingGame))
                _streak = _streak;
            else _streak = 0;
            Update("Lose", game, CurrentRating);
        }
        
        public override void GetStats()
        {
            Console.WriteLine($"Stats for {UserName}:");
            foreach (var it in Stats)
            {
                var line = new StringBuilder();
                line.Append($"{it.Game.GameInfo()}, so {UserName} {it.Status} {it.PlayedRating} pts and have {it.Rating} pts");
                Console.WriteLine(line.ToString());
            }
            Console.WriteLine($"Current rating: {CurrentRating}");
            Console.WriteLine($"Bonuses added: {_added*Bonus}");
            Console.WriteLine($"Total games played: {GamesCount}");
        }
    }
}