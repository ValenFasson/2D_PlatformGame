using UnityEngine;
using TMPro;

public class ABBVisualizer : MonoBehaviour
{
    [Header("Configuración visual")]
    public GameObject nodePrefab;          // Prefab con un círculo + texto
    public float horizontalSpacing = 2f;   // Distancia horizontal entre nodos
    public float verticalSpacing = 2.5f;   // Distancia vertical entre niveles

    public void DibujarArbol(ABBTDA arbol)
    {
        // Limpia cualquier dibujo anterior
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        if (arbol == null || arbol.ArbolVacio())
            return;

        // Dibuja desde la raíz
        DibujarNodo(arbol, Vector2.zero, 0);
    }

    void DibujarNodo(ABBTDA nodo, Vector2 posicion, int profundidad)
    {
        if (nodo == null || nodo.ArbolVacio()) return;

        // Crea el nodo visual
        GameObject nodoGO = Instantiate(nodePrefab, transform);
        nodoGO.transform.localPosition = posicion;

        TMP_Text label = nodoGO.GetComponentInChildren<TMP_Text>();
        if (label != null)
            label.text = nodo.Raiz().ToString();

        // Calcula desplazamiento horizontal para los hijos
        float offset = horizontalSpacing * Mathf.Pow(0.7f, profundidad);

        // Dibuja subárbol izquierdo
        if (!nodo.HijoIzq().ArbolVacio())
            DibujarNodo(nodo.HijoIzq(), posicion + new Vector2(-offset, -verticalSpacing), profundidad + 1);

        // Dibuja subárbol derecho
        if (!nodo.HijoDer().ArbolVacio())
            DibujarNodo(nodo.HijoDer(), posicion + new Vector2(offset, -verticalSpacing), profundidad + 1);
    }
}
