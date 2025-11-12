using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStackManager : MonoBehaviour
{
    [Header("Rooms")]
    [SerializeField] private string[] allRooms = { "Nivel1", "Nivel2", "Nivel3" };
    public string[] roomScenes;
    public int runSize = 3;

    [Header("Spawns")]
    public string entrySpawnName = "Spawn_Entry";
    public string returnSpawnName = "Spawn_Return";

    [Header("Final Scene")]
    [Tooltip("Escena a la que se irá cuando termine la rotación completa")]
    public string endSceneName = "ScoreBoard";

    private int index = 0;
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

    // Genera un nuevo conjunto de niveles con todos los niveles aleatorizados
    public void ChargeStack()
    {
        List<string> shuffled = new List<string>(allRooms);

        // Mezcla aleatoria de todos los niveles (Fisher-Yates)
        for (int i = 0; i < shuffled.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffled.Count);
            string temp = shuffled[i];
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }

        roomScenes = shuffled.ToArray();
        index = 0;
    }

    // Devuelve la siguiente o anterior escena según "forward"
    public string GetScene(bool forward)
    {
        isReturn = !forward;

        if (forward)
        {
            index++;
        }
        else
        {
            index--;
        }

        // 🔹 Si ya completó la rotación, va directo a la escena final
        if (forward && index >= roomScenes.Length)
        {
            index = roomScenes.Length - 1; // Mantiene índice válido
            LoadScene(endSceneName);
            return null;
        }

        // Evita valores fuera de rango
        if (index < 0)
        {
            index = 0;
        }

        return roomScenes[index];
    }

    public void LoadScene(string scene)
    {
        if (!string.IsNullOrEmpty(scene))
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void ManageScene()
    {
        if (isReturn)
            PlacePlayer(returnSpawnName);
        else
            PlacePlayer(entrySpawnName);
    }

    void PlacePlayer(string spawnName)
    {
        GameObject spawn = GameObject.Find(spawnName);
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (spawn == null || player == null) return;

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
    public void resetRun() 
    {
        index = 0;
        isReturn = false;
        ChargeStack();
    }
}
