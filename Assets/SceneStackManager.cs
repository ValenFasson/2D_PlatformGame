using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStackManager : MonoBehaviour
{
    [SerializeField] private string[] allRooms = { "Nivel1", "Nivel2", "Nivel3" };
    public string[] roomScenes;
    private int index = 0;
    public int runSize = 3;
    public string entrySpawnName = "Spawn_Entry";
    public string returnSpawnName = "Spawn_Return";

    public bool isReturn = false;

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

        SceneManager.sceneLoaded += OnSceneLoaded;

        ChargeStack();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ManageScene();
    }

    // Genera un nuevo conjunto de niveles aleatorios sin repetir consecutivamente
    public void ChargeStack()
    {
        if (roomScenes == null || roomScenes.Length != runSize)
        {
            roomScenes = new string[runSize];
        }

        index = 0;

        string lastRoom = null;
        int filled = 0;

        while (filled < runSize)
        {
            int numeroRand = Random.Range(0, allRooms.Length);
            string candidate = allRooms[numeroRand];

            if (candidate != lastRoom)
            {
                roomScenes[filled] = candidate;
                lastRoom = candidate;
                filled++;
            }
        }
    }

    // Devuelve la siguiente o anterior escena según "forward"
    public string GetScene(bool forward)
    {
        if (forward)
        {
            index++;
        }
        else
        {
            index--;
        }

        isReturn = !forward;

        if (index < 0)
        {
            index = 0;
        }

        if (index >= roomScenes.Length)
        {
            index = roomScenes.Length - 1;
        }

        return roomScenes[index];
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    // Posiciona al jugador según si va hacia adelante o hacia atrás
    public void ManageScene()
    {
        if (isReturn)
        {
            PlacePlayer(returnSpawnName);
        }
        else
        {
            PlacePlayer(entrySpawnName);
        }
    }

    void PlacePlayer(string spawnName)
    {
        GameObject spawn = GameObject.Find(spawnName);
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = spawn.transform.position;
        player.transform.rotation = spawn.transform.rotation;

        Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
        }

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb2d.angularVelocity = 0f;
        }
    }
}
