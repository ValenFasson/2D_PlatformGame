using System;
using System.Collections;
using System.Collections.Generic;
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
        int aux = (left + right) / 2;
        int pivot = arr[aux].score;

        while (true)
        {
            while (arr[left].score > pivot)
                left++;

            while (arr[right].score < pivot)
                right--;

            if (left >= right)
                return right;

            QuickInfo.Player temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;

            left++;
            right--;
        }
    }

    static public void quickSort(QuickInfo.Player[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(arr, left, right);
            if (pivot > 1)
                quickSort(arr, left, pivot);
            if (pivot + 1 < right)
                quickSort(arr, pivot + 1, right);
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
            return;

        if (count >= 1)
        {
            firstPlaceName.text = playerArry[0].name;
            firstPlaceScore.text = playerArry[0].score.ToString();
        }
        if (count >= 2)
        {
            secondPlaceName.text = playerArry[1].name;
            secondPlaceScore.text = playerArry[1].score.ToString();
        }
        if (count >= 3)
        {
            ThirdPlaceName.text = playerArry[2].name;
            ThirdPlaceScore.text = playerArry[2].score.ToString();
        }
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        LoadArry();
        if (playerArry != null && playerArry.Length > 1)
            quickSort(playerArry, 0, playerArry.Length - 1);
        setTextScore();
    }
}
