using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneStackManager : MonoBehaviour
{
    [SerializeField] private string[] allRooms = { "Nivel1", "Nivel2", "Nivel3" };
    public string[] roomScenes;
    public string[] history;
    private int index = 0;
    public int runSize = 3;
    int numeroRand;
    bool foundScene;
    public string entrySpawnName = "Spawn_Entry";
    public string returnSpawnName = "Spawn_Return";
    static SceneStackManager instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        ChargeStack();
    }
    public void ChargeStack() 
    {
        while (roomScenes.Length == runSize) 
        { 
            foundScene = false;
            numeroRand = Random.Range(0, allRooms.Length -1);
            for (int i = 0; i < history.Length; i++) 
            {
                if (allRooms[numeroRand] == history[i]) 
                {
                    foundScene = true;
                }
            }
            if (!foundScene) 
            {
                roomScenes[index] = allRooms[numeroRand];
                index++;
            }
        }
    }
    public string GetScene(bool forward) 
    {
        if (forward) 
        {
            index++;
        }
        else { index--; }
        return roomScenes[index];
    }
    public void LoadScene(string scene) 
    {
        SceneManager.LoadScene(scene);
    }
    
    public void ManageScene() 
    {
        /*if (isReturn)
        {
            PlacePlayer(returnSpawnName);
        }
        else
        {
            PlacePlayer(entrySpawnName);
        }*/
    }
}