using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Home,
    LevelSelection,
    Game,
    NextLevel,
    RestartLevel,
    LevelCompleted,
    LevelFailed
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelDatas levelDatas;

    [SerializeField]
    private GameObject cardsGeneratorPrefab;

    public int testLevel = 0;

    private GameObject cardsGenerator;
    private int currentLevelIndex;

    private int cardOpenedCount;

    private void OnEnable()
    {
        EventAggregator.changeGameStateEvent += OnStateChange;
        EventAggregator.cardSelectedEvent += CheckOpenedCardCount;
        EventAggregator.matchedEvent += ResetOpenedCardCount;
    }    

    private void OnDisable()
    {
        EventAggregator.changeGameStateEvent -= OnStateChange;
        EventAggregator.cardSelectedEvent -= CheckOpenedCardCount;
        EventAggregator.matchedEvent -= ResetOpenedCardCount;
    }

    private void Start()
    {
        cardOpenedCount = 0;

        //yield return new WaitForSeconds(4);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("GameManager :: Calling LoadLevel: " + testLevel);
            LoadLevel(testLevel);            
        }
    }

    private void OnStateChange(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.Home:
                break;
            case GameStates.LevelSelection:
                break;
            case GameStates.Game:
                break;
            case GameStates.LevelCompleted:
                break;
            case GameStates.LevelFailed:
                break;
            case GameStates.NextLevel:
                break;
            case GameStates.RestartLevel:
                break;
        }
    }

    private void LoadLevel(int levelIndex)
    {
        if (cardsGenerator != null)
        {
            Debug.Log("GameManager :: Destroying Generator");
            Destroy(cardsGenerator);
        }
        cardsGenerator = Instantiate(cardsGeneratorPrefab);

        EventAggregator.setupLevelEvent?.Invoke(levelDatas.LevelDataList[levelIndex]);
    }

    private void CheckOpenedCardCount()
    {
        cardOpenedCount += 1;
        if (cardOpenedCount > 1)
        {
            EventAggregator.twoCardsSelectedEvent?.Invoke();            
        }
    }

    private void ResetOpenedCardCount(bool val)
    {
        cardOpenedCount = 0;
    }
}
