using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    private void OnEnable()
    {
        EventAggregator.setupLevelEvent += GenerateCards;
    }

    private void OnDisable()
    {
        EventAggregator.setupLevelEvent -= GenerateCards;
    }
    
    private void GenerateCards(LevelData levelData)
    {
        GenerateMatrix(levelData.RowNum, levelData.ColNum, (row,col,val) =>
        {            
            GameObject card = Instantiate(cardPrefab, new Vector2(col, row), Quaternion.identity, transform);
            card.GetComponent<CardBehaviour>().Init(val);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="columns"></param>
    /// <param name="matrixValue">Row index, Column Index, Item Value</param>
    private void GenerateMatrix(int rows, int columns, Action<int, int, int> matrixValue)
    {
        int[,] matrix = new int[rows, columns];

        int totalCount = rows * columns;
        totalCount = totalCount % 2 == 0 ? totalCount : totalCount - 1;

        int[] numbers = Enumerable.Range(1, totalCount / 2).ToArray();
        int[] duplicateNumbers = numbers.Concat(numbers).ToArray();
        Shuffle(duplicateNumbers);

        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = duplicateNumbers[index];                
                matrixValue?.Invoke(i, j, duplicateNumbers[index]);
                index++;
            }
        }

        //DebugPrintMatrix(matrix, rows, columns);
    }

    private void Shuffle(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, array.Length); ;
            (array[randomIndex], array[i]) = (array[i], array[randomIndex]); //Using tuple to swap. VisualStudio suggestion.
        }
    }

    private void DebugPrintMatrix(int[,] matrix, int rows, int columns)
    {
        string newRow = "";
        for (int i = 0; i < rows; i++)
        {
            newRow += "\n";
            for (int j = 0; j < columns; j++)
            {
                newRow += matrix[i, j].ToString() + " ";
            }
        }
        Debug.Log(newRow);
    }
}
