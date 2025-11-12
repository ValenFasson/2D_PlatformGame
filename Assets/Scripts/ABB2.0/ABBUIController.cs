using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ABBUIController : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_InputField inputField;
    public TMP_Text outputText;
    public Button addButton;
    public Button removeButton;
    public Button printInOrderButton;

    [Header("Visualizador")]
    public ABBVisualizer visualizer;   // 👈 referencia al visualizador

    private ABB arbol;

    void Start()
    {
        arbol = new ABB();
        arbol.InicializarArbol();

        addButton.onClick.AddListener(AgregarNodo);
        removeButton.onClick.AddListener(EliminarNodo);
        printInOrderButton.onClick.AddListener(ImprimirInOrder);

        outputText.text = "Árbol inicializado.\n";
        if (visualizer != null)
            visualizer.DibujarArbol(arbol);
    }

    void AgregarNodo()
    {
        if (int.TryParse(inputField.text, out int valor))
        {
            arbol.AgregarElem(valor);
            outputText.text += $"Se insertó el valor {valor}\n";
            inputField.text = "";

            if (visualizer != null)
                visualizer.DibujarArbol(arbol);
        }
    }

    void EliminarNodo()
    {
        if (int.TryParse(inputField.text, out int valor))
        {
            arbol.EliminarElem(valor);
            outputText.text += $"Se eliminó el valor {valor}\n";
            inputField.text = "";

            if (visualizer != null)
                visualizer.DibujarArbol(arbol);
        }
    }

    void ImprimirInOrder()
    {
        outputText.text += "\nRecorrido InOrder:\n";
        MostrarInOrder(arbol);
        outputText.text += "\n";
    }

    void MostrarInOrder(ABBTDA nodo)
    {
        if (nodo == null || nodo.ArbolVacio()) return;
        MostrarInOrder(nodo.HijoIzq());
        outputText.text += nodo.Raiz() + " ";
        MostrarInOrder(nodo.HijoDer());
    }
}
