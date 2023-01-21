using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    private int[] flippedValues;
    private int currentIndex;

    private void OnEnable()
    {
        EventAggregator.flippedCardValEvent += OnCardFlipped;
    }

    private void OnDisable()
    {
        EventAggregator.flippedCardValEvent -= OnCardFlipped;
    }

    private void Start()
    {
        currentIndex = 0;
        flippedValues = new int[2];
    }

    private void OnCardFlipped(int value)
    {        
        flippedValues[currentIndex] = value;
        currentIndex += 1;

        if (currentIndex > 1)
        {            
            EventAggregator.matchedEvent?.Invoke(flippedValues[0] == flippedValues[1]);
            currentIndex = 0;
        }
    }    
}
