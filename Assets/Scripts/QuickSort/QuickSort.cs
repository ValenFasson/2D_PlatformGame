using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class QuickSort : MonoBehaviour
{
    public TextMeshProUGUI firstPlaceName;
    public TextMeshProUGUI firstPlaceScore;
    public TextMeshProUGUI secondPlaceName;
    public TextMeshProUGUI secondPlaceScore;
    public TextMeshProUGUI ThirdPlaceName;
    public TextMeshProUGUI ThirdPlaceScore;

    public QuickInfo.Player[] playerArry;

    static public int Partition(QuickInfo.Player[] arr, int left, int right)
    {
        int pivot;
        int aux = (left + right) / 2;   //tomo el valor central del vector
        pivot = arr[aux].score;

        // en este ciclo debo dejar todos los valores menores al pivot
        // a la izquierda y los mayores a la derecha
        while (true)
        {
            while (arr[left].score < pivot)
            {
                left++;
            }
            while (arr[right].score > pivot)
            {
                right--;
            }
            if (left < right)
            {
                QuickInfo.Player temp = arr[right];
                arr[right] = arr[left];
                arr[left] = temp;
            }
            else
            {
                // este es el valor que devuelvo como proxima posicion de
                // la particion en el siguiente paso del algoritmo
                return right;
            }
        }
    }

    static public void quickSort(QuickInfo.Player[] arr, int left, int right)
    {
        int pivot;
        if (left < right)
        {
            pivot = Partition(arr, left, right);
            if (pivot > 1)
            {
                // mitad del lado izquierdo del vector
                quickSort(arr, left, pivot - 1);
            }
            if (pivot + 1 < right)
            {
                // mitad del lado derecho del vector
                quickSort(arr, pivot + 1, right);
            }
        }
    }

    public void LoadArry() 
    {
        playerArry = QuickInfo.playersList.ToArray();
    }

    public void setTextScore() 
    {
        firstPlaceName.text = playerArry[playerArry.Length - 1].name.ToString();
        firstPlaceName.text = playerArry[playerArry.Length - 1].score.ToString();
        secondPlaceName.text = playerArry[playerArry.Length - 2].name.ToString();
        secondPlaceScore.text = playerArry[playerArry.Length - 2].score.ToString();
        ThirdPlaceName.text = playerArry[playerArry.Length - 3].name.ToString();
        ThirdPlaceScore.text = playerArry[playerArry.Length - 3].score.ToString();
    }

    public void Awake()
    {
        LoadArry();
        quickSort(playerArry, 0, playerArry.Length - 1);
        imprimirVector(playerArry); // <--- solo de forma didactica
        setTextScore();
    }
    static void imprimirVector(QuickInfo.Player[] vec)
    {
        for (int i = 0; i < vec.Length; i++)
        {
            Debug.Log(vec[i].name + " " + vec[i].score);
        }
    }
}
