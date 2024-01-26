namespace Progress
{
    public interface IGameProgressListener
    {
        public void OnGameStart();
        public void OnGameEnd();
        public void OnGamePause();
    }
}