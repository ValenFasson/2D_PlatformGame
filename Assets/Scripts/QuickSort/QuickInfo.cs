using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickInfo : MonoBehaviour
{
    static public QuickInfo QSinfo;
    static public List<Player> playersList = new List<Player>();

    public class Player
    {
        public string name;
        public int score;
    }
    void Awake()
    {
        if (QSinfo != null && QSinfo != this)
        {
            Destroy(gameObject);
            return;
        }
        QSinfo = this;
        DontDestroyOnLoad(gameObject);
    }

    public void newPlayer(string name, int score) 
    {
       Player player = new Player();
        player.name = name;
        player.score = score;
        playersList.Add(player);
    }
}
