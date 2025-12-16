public class GameMemento
{
    public string SceneName { get; private set; }
    public PlayerMemento PlayerState { get; private set; }
    public GameStatsMemento StatsState { get; private set; }

    public GameMemento(string sceneName, PlayerMemento playerState, GameStatsMemento statsState)
    {
        SceneName = sceneName;
        PlayerState = playerState;
        StatsState = statsState;
    }
}
