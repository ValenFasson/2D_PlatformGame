using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStackManager : MonoBehaviour
{
    [Header("Rooms")]
    [SerializeField] private string[] allRooms = { "Nivel1", "Nivel2", "Nivel3" };
    public int[] numerosElegidos;
    public int runSize = 3;

    [Header("Spawns")]
    public string entrySpawnName = "Spawn_Entry";
    public string returnSpawnName = "Spawn_Return";

    [Header("Final Scene")]
    public string endSceneName = "ScoreBoard";
    public bool isReturn = false;

    [Header("Cola TDA")]
    Cola cola;
    static SceneStackManager instance;

    void Awake()
    {
        cola = new Cola(); // creamos la estructura de cola
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

    public void ChargeStack() 
    {
        cola.InicializarCola();
        numerosElegidos = new int[allRooms.Length];
        for (int j = 0; j < numerosElegidos.Length; j++) //inicializamos el array con todos valores invalidos
            numerosElegidos[j] = -1;

        for (int i = 0; i < allRooms.Length; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, allRooms.Length);
            }
            while (numerosElegidos.Contains(randomIndex)); // repetimos si ya existe el numero
            numerosElegidos[i] = randomIndex; // guardar índice único
            cola.Acolar(allRooms[randomIndex]); // encolar room
        }
    }

    public void GetScene()
    {
        if (cola.ColaVacia()) //si la cola esta vacia...
        {
            SceneManager.LoadScene(endSceneName); //cargamos la escena preasignada
        }
        else 
        {
            string scene = cola.Primero();
            cola.Desacolar();
            SceneManager.LoadScene(scene);
        }
    }
    public void ManageScene() // esta funcion se ejecuta luego de la escena cargada
    {
        string current = SceneManager.GetActiveScene().name; // nombre de escena actual
        if (current == endSceneName) // si la escena actual es igual a la escena final salimos
            return;

        if (isReturn) // si se puede volver a jugar ejecutamos : 
            PlacePlayer(returnSpawnName);
        else
            PlacePlayer(entrySpawnName);
    }

    void PlacePlayer(string spawnName) // ubicamos al player en escena segun el nombre de entrada
    {
        GameObject spawn = GameObject.Find(spawnName); // guardamos el punto de spawn en "spawn"
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // guardamos al Player en "player"

        if (spawn == null || player == null) return; // si no existen salimos

        player.transform.position = spawn.transform.position; // ubicamos al player en posicion y rotacion del punto de spawn
        player.transform.rotation = spawn.transform.rotation;

        Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>(); // guardamos el rigidbody del Player en "rb2d"
        if (rb2d != null)
        {
            rb2d.velocity = Vector2.zero; //detenemos el movimiento del player ???
            rb2d.angularVelocity = 0f;
        }

        Rigidbody rb = player.GetComponent<Rigidbody>(); // hacemos lo mismo pero en 3D?
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb2d.angularVelocity = 0f;
        }
    }

    public void resetRun() //resetea todos los valores para volver a jugar
    {
        isReturn = false;
        ChargeStack(); // hace el proceso de mezcla de nuevo
    }
}
