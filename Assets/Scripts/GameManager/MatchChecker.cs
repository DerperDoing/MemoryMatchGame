using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    private int[] flippedValues;
    private int currentIndex;
    
    private int possibleMatchCount;

    private void OnEnable()
    {
        EventAggregator.flippedCardValEvent += OnCardFlipped;
        EventAggregator.setupLevelEvent += Setup;
    }

    private void OnDisable()
    {
        EventAggregator.flippedCardValEvent -= OnCardFlipped;
        EventAggregator.setupLevelEvent -= Setup;
    }

    private void Start()
    {
        flippedValues = new int[2];        
    }

    private void Setup(LevelData data = null)
    {
        currentIndex = 0;
        flippedValues[0] = -1;
        flippedValues[1] = -2;

        if (data != null)
        {
            int totalCount = (data.ColNum * data.RowNum);
            totalCount = totalCount % 2 == 0 ? totalCount : totalCount - 1;
            possibleMatchCount = totalCount / 2;
        }
    }

    private void OnCardFlipped(int value)
    {        
        flippedValues[currentIndex] = value;
        currentIndex += 1;

        if (currentIndex > 1)
        {
            bool matched = flippedValues[0] == flippedValues[1];

            EventAggregator.matchedEvent?.Invoke(matched);

            currentIndex = 0;

            if (matched)
            {
                possibleMatchCount -= 1;                
                if (possibleMatchCount <= 0)
                {
                    EventAggregator.changeGameStateEvent?.Invoke(GameStates.LevelCompleted);
                }
            }
        }
    }       
}
