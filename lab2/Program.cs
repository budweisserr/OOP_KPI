

namespace lab2
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var accountChooser = new AccountChooser();
            var gameChooser = new GameChooser();
            var first = accountChooser.CreateGameAccount("Alex");
            var second = accountChooser.CreateBonusGameAccount("Grisha");
            var third = accountChooser.CreateEasyGameAccount("Oleg");
            int i = 0, j = 1;
            while (i != 6)
            {
                i++;
                switch (j)
                {
                    case 1:
                    {
                        gameChooser.PlayStandartGame(first, second, 120);
                        j++;
                        break;
                    }
                    case 2:
                    {
                        gameChooser.PlayTrainingGame(first, third, 150);
                        j++;
                        break;
                    }
                    case 3:
                    {
                        gameChooser.SinglePlayGame(second, third, 90);
                        j++;
                        break;
                    }
                    case 4:
                    {
                        j = 1;
                        break;
                    }
                }
            }  
            
            first.GetStats();
            second.GetStats();
            third.GetStats();
            
        }
    }
}