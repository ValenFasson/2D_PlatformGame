using UnityEngine;

public class GraphController : MonoBehaviour, IGraphTDA
{
    public GraphNode[] nodes;
    private const float INF = 999999f;

    public void InicializarGrafo() { }
    public void AgregarVertice(int v) { }
    public void EliminarVertice(int v) { }

    public void AgregarArista(int v1, int v2, float peso)
    {
        var nodeA = nodes[v1];
        var nodeB = nodes[v2];
        var newConn = new GraphConnection { targetNode = nodeB, weight = peso };
        var list = new GraphConnection[nodeA.connections.Length + 1];
        nodeA.connections.CopyTo(list, 0);
        list[^1] = newConn;
        nodeA.connections = list;
    }

    public void EliminarArista(int v1, int v2)
    {
        var nodeA = nodes[v1];
        nodeA.connections = System.Array.FindAll(nodeA.connections, c => c.targetNode.nodeId != v2);
    }

    public bool ExisteArista(int v1, int v2)
    {
        foreach (var c in nodes[v1].connections)
        {
            if (c.targetNode.nodeId == v2)
            {
                return true;
            }
        }
                return false;
    }

    public float PesoArista(int v1, int v2)
    {
        foreach (var c in nodes[v1].connections)
        {
            if (c.targetNode.nodeId == v2)
            {
                return c.weight;
            }
        }

        return INF;

    }

    public int VerticeMasCercano(Vector2 pos)
    {
        float min = INF; int id = 0;
        for (int i = 0; i < nodes.Length; i++)
        {
            float d = Vector2.Distance(pos, nodes[i].transform.position);
            if (d < min) 
            { 
                min = d; id = i; 
            }
        }
        return id;
    }

    public int[] Dijkstra(int start, int end)
    {
        int n = nodes.Length;
        bool[] vis = new bool[n];
        float[] dist = new float[n];
        int[] prev = new int[n];

        for (int i = 0; i < n; i++) 
        { 
            dist[i] = INF; prev[i] = -1; 
        }

        dist[start] = 0;

        for (int _ = 0; _ < n; _++)
        {
            int u = MinDistance(dist, vis, n);

            if (u == -1)
            {
                break;
            }
                vis[u] = true;

            foreach (var c in nodes[u].connections)
            {
                int v = c.targetNode.nodeId;
                if (vis[v])
                {
                    continue;
                }

                float alt = dist[u] + c.weight;

                if (alt < dist[v]) 
                { 
                    dist[v] = alt; prev[v] = u; 
                }
            }
        }

        System.Collections.Generic.List<int> path = new();

        for (int at = end; at != -1; at = prev[at])
        {
            path.Add(at);
        }

        path.Reverse();

        return path.ToArray();
    }

    private int MinDistance(float[] d, bool[] vis, int n)
    {
        float min = INF; int idx = -1;

        for (int i = 0; i < n; i++)
        {
            if (!vis[i] && d[i] <= min)
            {
                min = d[i]; idx = i;
            }
        }
        return idx;
    }
}
