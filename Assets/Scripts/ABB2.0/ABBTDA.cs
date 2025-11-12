public interface ABBTDA
{
    int Raiz();                   // Devuelve el valor del nodo raíz
    ABBTDA HijoIzq();             // Devuelve el subárbol izquierdo
    ABBTDA HijoDer();             // Devuelve el subárbol derecho
    bool ArbolVacio();            // Indica si el árbol está vacío
    void InicializarArbol();      // Inicializa la raíz en null
    void AgregarElem(int x);      // Inserta un valor en el árbol
    void EliminarElem(int x);     // Elimina un valor del árbol
}
