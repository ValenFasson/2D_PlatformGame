using UnityEngine;

[System.Serializable]
public class GraphConnection
{
    public GraphNode targetNode;
    public float weight;
}

public class GraphNode : MonoBehaviour
{
    public int nodeId;
    public GraphConnection[] connections;

    private void OnDrawGizmos()
    {
        if (connections == null) return;
        Gizmos.color = Color.cyan;

        foreach (var c in connections)
        {
            if (c?.targetNode == null) continue;
            Vector3 from = transform.position, to = c.targetNode.transform.position;
            Gizmos.DrawLine(from, to);
#if UNITY_EDITOR
            UnityEditor.Handles.Label((from + to) / 2, c.weight.ToString("F1"));
#endif
        }
    }
}
