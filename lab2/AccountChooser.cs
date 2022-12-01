namespace lab2
{
    public class AccountChooser
    {
        public BaseGameAccount CreateGameAccount(string userName)
        {
            return new GameAccount(userName);
        }
        public BaseGameAccount CreateEasyGameAccount(string userName)
        {
            return new EasyGameAccount(userName);
        }
        public BaseGameAccount CreateBonusGameAccount(string userName)
        {
            return new BonusGameAccount(userName);
        }
    }   
}