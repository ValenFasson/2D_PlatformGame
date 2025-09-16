using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneStackManager : MonoBehaviour
{
    // Escenas que actúan como "salas" (añádelas a Build Settings)
    public List<string> roomScenes = new List<string> { "Nivel1", "Nivel2", "Nivel3" };

    // Escena inicial (si está vacío, usa el primer elemento de roomScenes)
    public string initialRoom = "Nivel1";

    // Nombres de los puntos de spawn dentro de cada escena
    public string entrySpawnName = "Spawn_Entry";
    public string returnSpawnName = "Spawn_Return";

    // Singleton del manager (persistente). El jugador maneja su propio singleton.
    static SceneStackManager instance;

    // Pila LIFO de escenas visitadas (historial de navegación)
    Stack<string> history = new Stack<string>();

    // Flag para evitar cargas múltiples
    bool hasInitialized = false;

    void Awake()
    {
        // Garantiza una única instancia y que no se destruya al cambiar de escena
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Solo inicializar una vez
        if (hasInitialized) return;
        hasInitialized = true;

        // Si no hay ninguna sala cargada, cargamos la inicial
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

    // Carga la primera sala (push inicial a la pila)
    public void LoadInitial()
    {
        Debug.Log("LoadInitial() llamado");

        string sceneToLoad = initialRoom;
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            if (roomScenes != null && roomScenes.Count > 0)
            {
                sceneToLoad = roomScenes[0];
            }
        }
        StartCoroutine(LoadRoomRoutine(sceneToLoad, false));
    }

    // Avanza a una escena aleatoria distinta de la actual (push a la pila)
    public void GoToRandomNext()
    {
        Debug.Log("GoToRandomNext() llamado");

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

    // Vuelve a la escena anterior (pop de la pila)
    public void GoBack()
    {
        Debug.Log("GoBack() llamado");

        if (history.Count <= 1) return;

        string current = history.Pop();
        string previous = history.Peek();
        StartCoroutine(LoadRoomRoutine(previous, true, current));
    }

    // Corrutina central: descarga/carga escenas, actualiza pila y coloca al jugador
    System.Collections.IEnumerator LoadRoomRoutine(string target, bool isReturn, string unloadOnly = null)
    {
        Debug.Log($"=== Cargando {target} (isReturn: {isReturn}) ===");

        // Listar escenas antes de descargar
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sc = SceneManager.GetSceneAt(i);
            Debug.Log($"Antes - Escena {i}: {sc.name} (loaded: {sc.isLoaded})");
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
            // Descargar todas las salas
            toUnload.AddRange(roomScenesLoaded);
        }

        // 3) Descargar
        for (int i = 0; i < toUnload.Count; i++)
        {
            Debug.Log($"Descargando: {toUnload[i].name}");
            AsyncOperation uop = SceneManager.UnloadSceneAsync(toUnload[i]);
            if (uop != null)
            {
                while (!uop.isDone) yield return null;
            }
        }

        // 4) Cargar nueva sala
        Debug.Log($"Cargando: {target}");
        AsyncOperation lop = SceneManager.LoadSceneAsync(target, LoadSceneMode.Additive);
        while (!lop.isDone) yield return null;

        Scene loaded = SceneManager.GetSceneByName(target);
        SceneManager.SetActiveScene(loaded);

        // 5) Actualizar pila
        if (!isReturn)
        {
            if (history.Count == 0 || history.Peek() != target)
            {
                history.Push(target);
            }
        }

        // 6) Colocar al jugador
        if (isReturn)
        {
            PlacePlayer(returnSpawnName);
        }
        else
        {
            PlacePlayer(entrySpawnName);
        }

        // Listar escenas después de cargar
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sc = SceneManager.GetSceneAt(i);
            Debug.Log($"Después - Escena {i}: {sc.name} (loaded: {sc.isLoaded})");
        }
    }

    // Busca un Transform con el nombre de spawn y mueve al jugador allí
    void PlacePlayer(string spawnName)
    {
        // Obtiene referencia al jugador por tag (no se guarda ni se persiste aquí)
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

        // Fallback al spawn de entrada si el solicitado no existe
        if (spawn == null)
        {
            for (int i = 0; i < roots.Length; i++)
            {
                Transform[] transforms = roots[i].GetComponentsInChildren<Transform>(true);
                for (int j = 0; j < transforms.Length; j++)
                {
                    if (transforms[j].name == entrySpawnName)
                    {
                        spawn = transforms[j];
                        break;
                    }
                }
                if (spawn != null) break;
            }
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
}