using UnityEngine;
using System.Collections.Generic;

public class GraphController : MonoBehaviour
{
    public List<GraphNode> nodes = new List<GraphNode>();

    void Awake()
    {
        nodes.Clear();
        foreach (Transform child in transform)
        {
            GraphNode n = child.GetComponent<GraphNode>();
            if (n != null)
                nodes.Add(n);
        }
    }

    // 🔹 Versión simplificada de BFS: encuentra un camino usando conexiones directas
    public List<GraphNode> FindPathSimple(int startId, int endId)
    {
        List<GraphNode> path = new List<GraphNode>();
        if (startId == endId)
        {
            path.Add(nodes[startId]);
            return path;
        }

        bool[] visited = new bool[nodes.Count];
        int[] previous = new int[nodes.Count];

        for (int i = 0; i < previous.Length; i++)
            previous[i] = -1;

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(startId);
        visited[startId] = true;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            GraphNode node = nodes[current];

            foreach (GraphNode neighbor in node.connectedNodes)
            {
                int nid = neighbor.nodeId;
                if (!visited[nid])
                {
                    visited[nid] = true;
                    previous[nid] = current;
                    queue.Enqueue(nid);

                    if (nid == endId)
                    {
                        // reconstrucción del camino
                        List<GraphNode> fullPath = new List<GraphNode>();
                        for (int at = endId; at != -1; at = previous[at])
                            fullPath.Insert(0, nodes[at]);
                        return fullPath;
                    }
                }
            }
        }
        return path;
    }
}
