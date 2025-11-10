using UnityEngine;
using System.Collections.Generic;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Graph Settings")]
    public GraphController graph;
    public int currentNodeId;
    public int targetNodeId;

    [Header("Movement")]
    public float moveSpeed = 3f;

    private List<GraphNode> path = new List<GraphNode>();
    private int pathIndex = 0;
    private int lastTargetNodeId = -1;

    void Start()
    {
        // Teleport al nodo inicial
        if (graph != null && currentNodeId < graph.nodes.Count)
            transform.position = graph.nodes[currentNodeId].transform.position;

        CalculatePath();
        lastTargetNodeId = targetNodeId;
    }

    void Update()
    {
        if (graph == null || graph.nodes.Count == 0) return;

        // 🟡 Si cambió el destino en tiempo real → recalcula el camino
        if (targetNodeId != lastTargetNodeId)
        {
            CalculatePath();
            lastTargetNodeId = targetNodeId;
        }

        // 🕹 Movimiento a lo largo del camino
        if (path == null || path.Count == 0 || pathIndex >= path.Count) return;

        GraphNode target = path[pathIndex];
        Vector2 dir = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);

        // Llega al siguiente nodo
        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            transform.position = target.transform.position;
            currentNodeId = target.nodeId;
            pathIndex++;

            if (pathIndex >= path.Count)
            {
                path.Clear(); // Llegó al destino final
            }
        }
    }

    public void CalculatePath()
    {
        if (graph == null) return;
        path = graph.FindPathSimple(currentNodeId, targetNodeId);
        pathIndex = 1; // empieza moviéndose hacia el siguiente nodo

        // Por seguridad, si el camino está vacío o no hay conexión
        if (path == null || path.Count < 2)
            pathIndex = 0;
    }

    private void OnDrawGizmos()
    {
        // Visualizar ruta actual
        if (path == null || path.Count < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < path.Count - 1; i++)
            Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
    }
}
