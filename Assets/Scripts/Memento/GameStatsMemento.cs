public class GameStatsMemento
{
    public int CurrentScore { get; private set; }
    public float Timer { get; private set; }

    public GameStatsMemento(int currentScore, float timer)
    {
        CurrentScore = currentScore;
        Timer = timer;
    }
}
