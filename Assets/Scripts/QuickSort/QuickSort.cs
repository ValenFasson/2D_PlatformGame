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
            while (arr[left].score > pivot)  // en lugar de <
            {
                left++;
            }
            while (arr[right].score < pivot) // en lugar de >
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
        int count = playerArry.Length;
        Debug.Log("Cantidad de jugadores en array: " + count);

        for (int i = 0; i < count; i++)
            Debug.Log($"[{i}] {playerArry[i].name} - {playerArry[i].score}");

        if (count == 0)
        {
            Debug.LogWarning("No hay jugadores en la lista.");
            return;
        }

        if (count >= 1)
        {
            firstPlaceName.text = playerArry[count - 1].name;
            firstPlaceScore.text = playerArry[count - 1].score.ToString();
        }
        if (count >= 2)
        {
            secondPlaceName.text = playerArry[count - 2].name;
            secondPlaceScore.text = playerArry[count - 2].score.ToString();
        }
        if (count >= 3)
        {
            ThirdPlaceName.text = playerArry[count - 3].name;
            ThirdPlaceScore.text = playerArry[count - 3].score.ToString();
        }
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        LoadArry();
        quickSort(playerArry, 0, playerArry.Length - 1);
        setTextScore();
    }
}
