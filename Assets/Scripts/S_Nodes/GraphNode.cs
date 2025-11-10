using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public int nodeId;
    public GraphNode[] connectedNodes; // conexiones directas
    public float[] edgeWeights;        // pesos opcionales

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (connectedNodes == null) return;

        for (int i = 0; i < connectedNodes.Length; i++)
        {
            if (connectedNodes[i] != null)
                Gizmos.DrawLine(transform.position, connectedNodes[i].transform.position);
        }
    }
}
