using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneStackManager : MonoBehaviour
{
    public List<string> roomScenes = new List<string> { "Nivel1", "Nivel2", "Nivel3", "Nivel4" };
    public bool tutorial = false;
    [SerializeField] private string initialRoom = "Nivel4";

    public int rooms = 0;

    public string entrySpawnName = "Spawn_Entry";
    public string returnSpawnName = "Spawn_Return";

    [SerializeField] string defeatSceneName = "DefeatScene";
    [SerializeField] string mainMenuSceneName = "MainMenu";

    [SerializeField] string bootstrapSceneName = "Bootstrap";

    public static SceneStackManager instance;

    // Pila LIFO de escenas visitadas (historial de navegación)
    Stack<string> history = new Stack<string>();


    bool hasInitialized = false;


    private void Update()
    {
        if (rooms == 3)
        {
            GameManager.Instance.PlayerWon();
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;

        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        // Si ya estamos en una escena donde no debe existir, destruirse inmediatamente
        TrySelfDestructForScene(SceneManager.GetActiveScene());

        if (hasInitialized) return;
        hasInitialized = true;

        bool anyLoaded = false;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sc = SceneManager.GetSceneAt(i);
            for (int k = 0; k < roomScenes.Count; k++)
            {
                if (sc.name == roomScenes[k] && sc.isLoaded)
                {
                    anyLoaded = true;
                    break;
                }
            }
            if (anyLoaded) break;
        }

        if (!anyLoaded)
        {
            LoadInitial();
        }
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    public void LoadInitial()
    {
        string sceneToLoad = initialRoom;
        
        
            if (roomScenes != null)
            {
                sceneToLoad = roomScenes[0];
            }
        

        StartCoroutine(LoadRoomRoutine(sceneToLoad, false));

    }

    public void GoToRandomNext()
    {
        string current = null;
        if (history.Count > 0)
        {
            current = history.Peek();
        }

        List<string> candidates = new List<string>();
        for (int i = 0; i < roomScenes.Count; i++)
        {
            if (roomScenes[i] != current)
            {
                candidates.Add(roomScenes[i]);
            }
        }
        if (candidates.Count == 0)
        {
            for (int i = 0; i < roomScenes.Count; i++)
            {
                candidates.Add(roomScenes[i]);
            }
        }

        int idx = Random.Range(0, candidates.Count);
        string next = candidates[idx];
        StartCoroutine(LoadRoomRoutine(next, false));
    }

    // Corrutina central: descarga/carga escenas, actualiza pila y coloca al jugador
    System.Collections.IEnumerator LoadRoomRoutine(string target, bool isReturn, string unloadOnly = null)
    {

        // Listar escenas antes de descargar
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sc = SceneManager.GetSceneAt(i);
        }

        // 1) Obtener todas las escenas de salas cargadas
        List<Scene> roomScenesLoaded = new List<Scene>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sc = SceneManager.GetSceneAt(i);
            for (int k = 0; k < roomScenes.Count; k++)
            {
                if (sc.name == roomScenes[k] && sc.isLoaded)
                {
                    roomScenesLoaded.Add(sc);
                    break;
                }
            }
        }

        // 2) Filtrar qué descargar
        List<Scene> toUnload = new List<Scene>();
        if (!string.IsNullOrEmpty(unloadOnly))
        {
            // Solo descargar la especificada
            for (int i = 0; i < roomScenesLoaded.Count; i++)
            {
                if (roomScenesLoaded[i].name == unloadOnly)
                {
                    toUnload.Add(roomScenesLoaded[i]);
                    break;
                }
            }
        }
        else
        {
            toUnload.AddRange(roomScenesLoaded);
        }

        for (int i = 0; i < toUnload.Count; i++)
        {
            AsyncOperation uop = SceneManager.UnloadSceneAsync(toUnload[i]);
            if (uop != null)
            {
                while (!uop.isDone) yield return null;
            }
        }

        AsyncOperation lop = SceneManager.LoadSceneAsync(target, LoadSceneMode.Additive);
        while (!lop.isDone) yield return null;

        Scene loaded = SceneManager.GetSceneByName(target);
        SceneManager.SetActiveScene(loaded);

        if (!isReturn)
        {
            if (history.Count == 0 || history.Peek() != target)
            {
                history.Push(target);
            }
        }

        if (isReturn)
        {
            PlacePlayer(returnSpawnName);
        }
        else
        {
            PlacePlayer(entrySpawnName);
        }

        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sc = SceneManager.GetSceneAt(i);
        }
    }

    void PlacePlayer(string spawnName)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Scene active = SceneManager.GetActiveScene();
        GameObject[] roots = active.GetRootGameObjects();

        Transform spawn = null;
        for (int i = 0; i < roots.Length; i++)
        {
            Transform[] transforms = roots[i].GetComponentsInChildren<Transform>(true);
            for (int j = 0; j < transforms.Length; j++)
            {
                if (transforms[j].name == spawnName)
                {
                    spawn = transforms[j];
                    break;
                }
            }
            if (spawn != null) break;
        }

        if (spawn != null)
        {
            player.transform.position = spawn.position;
            player.transform.rotation = spawn.rotation;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        TrySelfDestructForScene(newScene);
    }

    void TrySelfDestructForScene(Scene scene)
    {
        if (scene.name == mainMenuSceneName || scene.name == defeatSceneName)
        {
            // Intentar descargar Bootstrap si está cargada
            UnloadBootstrapIfLoaded();
            Destroy(gameObject);
        }
    }

    void UnloadBootstrapIfLoaded()
    {
        if (string.IsNullOrEmpty(bootstrapSceneName)) return;

        // Buscar si Bootstrap está cargada y descargarla
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sc = SceneManager.GetSceneAt(i);
            if (sc.isLoaded && sc.name == bootstrapSceneName)
            {
                SceneManager.UnloadSceneAsync(sc);
                break;
            }
        }
    }

    public void roomsCompleted()
    {
        rooms++;
    }
}