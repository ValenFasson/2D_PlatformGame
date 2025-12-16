using UnityEngine;

public class GameStatsOriginator : MonoBehaviour
{
    public Singleton singleton;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = Singleton.instance;
        }
    }

    public GameStatsMemento SaveState()
    {
        if (singleton == null)
        {
            singleton = Singleton.instance;
        }

        return new GameStatsMemento(singleton.CurrentScore, singleton.TimerCounter);
    }

    public void RestoreState(GameStatsMemento memento)
    {
        if (singleton == null)
        {
            singleton = Singleton.instance;
        }

        if (singleton == null)
        {
            return;
        }

        singleton.CurrentScore = memento.CurrentScore;
        singleton.TimerCounter = memento.Timer;
    }
}
