using UnityEngine;

public interface IGraphTDA
{
    void InicializarGrafo();
    void AgregarVertice(int v);
    void AgregarArista(int v1, int v2, float peso);
    void EliminarVertice(int v);
    void EliminarArista(int v1, int v2);
    bool ExisteArista(int v1, int v2);
    float PesoArista(int v1, int v2);
    int VerticeMasCercano(Vector2 posicion);
    int[] Dijkstra(int startId, int endId);
}
