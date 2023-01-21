using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Matrix : MonoBehaviour
{
    [SerializeField] int r = 3;
    [SerializeField] int c = 5;

    //For testing
    [NaughtyAttributes.Button("Regen Matrix")]
    private void MethodTwo()
    {
        GenerateMatrix(r, c);
    }

    public void GenerateMatrix(int rows, int columns)
    {
        ValidateValues(ref rows, ref columns);

        Generate(rows, columns);
    }

    private void ValidateValues(ref int rows, ref int columns)
    {
        if (rows != columns && ((rows * columns) % 2 != 0))
        {
            if (columns > 3)
            {
                columns -= 1;
            }
            else
            {
                rows -= 1;
            }
        }
    }

    private void Generate(int rows, int columns)
    {       
        int[,] matrix = new int[rows, columns];
        
        int[] numbers = Enumerable.Range(1, rows * columns / 2).ToArray();
        int[] duplicateNumbers = numbers.Concat(numbers).ToArray();
        Shuffle(duplicateNumbers);

        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = duplicateNumbers[index];
                EventAggregator.filledValueEvent?.Invoke(i, j, duplicateNumbers[index]);
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
            (array[randomIndex], array[i]) = (array[i], array[randomIndex]); //Using tuple to swap. VSStudio suggestion.
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
