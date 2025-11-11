using UnityEngine;
using TMPro;

public class FlyingEnemy : MonoBehaviour
{
    public MonoBehaviour graphObject;
    private IGraphTDA graph;
    public float baseSpeed = 2f;
    public float recalcInterval = 2f;
    public string playerTag = "Player";

    public int currentNodeId, targetNodeId, lastKnownPlayerNode;
    public float currentSpeed;

    private GameObject player;
    private int[] path;
    private int pathIndex;
    private float recalcTimer;
    private bool pathCompleted;
    private TextMeshPro label;

    void Start()
    {
        graph = graphObject as IGraphTDA;
        if (graph == null) { enabled = false; return; }

        player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null) { enabled = false; return; }

        label = new GameObject("SpeedLabel").AddComponent<TextMeshPro>();
        label.fontSize = 3;
        label.color = Color.cyan;
        label.alignment = TextAlignmentOptions.Center;

        currentNodeId = graph.VerticeMasCercano(transform.position);
        if (graphObject is GraphController g && currentNodeId < g.nodes.Length)
            transform.position = g.nodes[currentNodeId].transform.position;

        UpdateTargetNode(true);
    }

    void Update()
    {
        if (graph == null || player == null) return;

        recalcTimer += Time.deltaTime;
        if (recalcTimer >= recalcInterval && pathCompleted)
        {
            UpdateTargetNode();
            recalcTimer = 0f;
        }

        MoveAlongPath();

        if (label)
        {
            label.transform.position = transform.position + Vector3.up * 0.8f;
            label.text = $"Speed: {currentSpeed:F2}";
        }
    }

    void UpdateTargetNode(bool force = false)
    {
        int closest = graph.VerticeMasCercano(player.transform.position);
        if (closest != lastKnownPlayerNode || force)
        {
            targetNodeId = closest;
            lastKnownPlayerNode = closest;
            RecalculatePath();
        }
    }

    void RecalculatePath()
    {
        path = graph.Dijkstra(currentNodeId, targetNodeId);
        pathCompleted = path == null || path.Length < 2;
        pathIndex = pathCompleted ? 0 : 1;
    }

    void MoveAlongPath()
    {
        if (path == null || pathIndex >= path.Length) { pathCompleted = true; return; }
        if (graphObject is not GraphController g) return;

        var a = g.nodes[currentNodeId];
        var b = g.nodes[path[pathIndex]];

        float edgeWeight = 1f;
        foreach (var c in a.connections)
            if (c.targetNode == b) { edgeWeight = c.weight; break; }

        currentSpeed = baseSpeed * (2f / Mathf.Max(edgeWeight, 0.1f));
        currentSpeed = Mathf.Clamp(currentSpeed, 0.3f, baseSpeed * 3f);

        transform.position += (b.transform.position - transform.position).normalized * currentSpeed * Time.deltaTime;

        if (Vector2.Distance(transform.position, b.transform.position) < 0.2f)
        {
            transform.position = b.transform.position;
            currentNodeId = b.nodeId;
            pathIndex++;
            if (pathIndex >= path.Length) { pathCompleted = true; path = null; }
        }
    }

    void OnDrawGizmos()
    {
        if (graphObject is not GraphController g || path == null || path.Length < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < path.Length - 1; i++)
            Gizmos.DrawLine(g.nodes[path[i]].transform.position, g.nodes[path[i + 1]].transform.position);

        if (currentNodeId >= 0 && currentNodeId < g.nodes.Length)
        { Gizmos.color = Color.green; Gizmos.DrawSphere(g.nodes[currentNodeId].transform.position, 0.2f); }

        if (lastKnownPlayerNode >= 0 && lastKnownPlayerNode < g.nodes.Length)
        { Gizmos.color = Color.red; Gizmos.DrawSphere(g.nodes[lastKnownPlayerNode].transform.position, 0.2f); }
    }
}
