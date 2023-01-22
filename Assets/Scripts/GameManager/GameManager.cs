using UnityEngine;

public enum GameStates
{
    Home,
    LevelSelection,
    Game,
    StartLevel,
    NextLevel,
    RestartLevel,
    LevelCompleted,
    LevelFailed
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelDatas levelDatas;

    [Space]
    [SerializeField]
    private Canvas uiCanvas;

    [Header("Prefabs to Spawn")]
    [SerializeField]
    private GameObject cardsGeneratorPrefab;
    private GameObject cardsGeneratorGO;

    [SerializeField]
    private GameObject homePrefab;
    private GameObject homeGO;

    [SerializeField]
    private GameObject gameUIPrefab;
    private GameObject gameUIGO;
    private GameObject startButtonGO;

    [SerializeField]
    private GameObject levelSelectPrefab;
    private GameObject levelSelectGO;

    [SerializeField]
    private GameObject levelSuccessPrefab;
    private GameObject levelSuccessGO;

    [SerializeField]
    private GameObject levelFailPrefab;
    private GameObject levelFailGO;

    public int testLevel = 0;

    private int currentLevelIndex;

    private int cardOpenedCount;

    private void OnEnable()
    {
        EventAggregator.changeGameStateEvent += OnStateChange;
        EventAggregator.cardSelectedEvent += CheckOpenedCardCount;
        EventAggregator.matchedEvent += ResetOpenedCardCount;
        EventAggregator.selectedLevelIndexEvent += LoadLevel;
    }    

    private void OnDisable()
    {
        EventAggregator.changeGameStateEvent -= OnStateChange;
        EventAggregator.cardSelectedEvent -= CheckOpenedCardCount;
        EventAggregator.matchedEvent -= ResetOpenedCardCount;
        EventAggregator.selectedLevelIndexEvent -= LoadLevel;
    }

    private void Start()
    {
        ResetOpenedCardCount();
    }

    //TODO: Remove later
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {            
            LoadLevel(testLevel);            
        }
    }

    private void OnStateChange(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.Home:
                SpawnHome();
                break;

            case GameStates.LevelSelection:
                SpawnLevelSelection();
                break;

            case GameStates.Game:
                SpawnGame();
                break;

            case GameStates.StartLevel:
                OnLevelStart();
                break;

            case GameStates.LevelCompleted:
                OnLevelSuccess();
                break;

            case GameStates.LevelFailed:
                OnLevelFail();
                break;

            case GameStates.NextLevel:
                LoadLevel(currentLevelIndex + 1);
                break;

            case GameStates.RestartLevel:
                LoadLevel(currentLevelIndex);
                break;
        }
    }

    private void SpawnHome()
    {
        ClearCanvas();

        homeGO = Instantiate(homePrefab, uiCanvas.transform);
    }

    private void SpawnLevelSelection()
    {
        ClearCanvas();
        
        levelSelectGO = Instantiate(levelSelectPrefab, uiCanvas.transform);
    }

    private void SpawnGame()
    {
        ClearCanvas();

        gameUIGO = Instantiate(gameUIPrefab, uiCanvas.transform);
    }

    private void LoadLevel(int levelIndex)
    {        
        ResetOpenedCardCount();

        currentLevelIndex = levelIndex;

        if (homeGO != null) Destroy(homeGO);        
        if (levelSelectGO != null) Destroy(levelSelectGO);

        if (gameUIGO == null)
        {
            gameUIGO = Instantiate(gameUIPrefab, uiCanvas.transform);
            startButtonGO = gameUIGO.transform.Find("StartButton").gameObject;
        }

        if (startButtonGO != null) startButtonGO.SetActive(true);

        if (cardsGeneratorGO != null) Destroy(cardsGeneratorGO);
        cardsGeneratorGO = Instantiate(cardsGeneratorPrefab);

        EventAggregator.setupLevelEvent?.Invoke(levelDatas.LevelDataList[levelIndex]);
    }

    private void OnLevelStart()
    {
        if (startButtonGO != null) { startButtonGO.SetActive(false); }

        EventAggregator.matchedEvent?.Invoke(false);
        EventAggregator.startLevelEvent?.Invoke();
    }

    private void OnLevelSuccess()
    {        
        if (levelSuccessGO == null)
        {
            levelSuccessGO = Instantiate(levelSuccessPrefab, uiCanvas.transform);
        }

        if (currentLevelIndex == levelDatas.LevelDataList.Capacity - 1)
        {
            levelSuccessGO.transform.Find("NextButton").gameObject.SetActive(false);
        }

        levelSuccessGO.SetActive(true);
    }

    private void OnLevelFail()
    {
        if (levelFailGO == null)
        {
            levelFailGO = Instantiate(levelFailGO, uiCanvas.transform);
        }        

        levelFailGO.SetActive(true);
    }

    private void CheckOpenedCardCount()
    {
        cardOpenedCount += 1;
        if (cardOpenedCount > 1)
        {
            EventAggregator.twoCardsSelectedEvent?.Invoke();            
        }
    } 

    private void ResetOpenedCardCount(bool val = true)
    {
        cardOpenedCount = 0;
    }

    private void ClearCanvas()
    {
        for (int i = 0; i < uiCanvas.transform.childCount; i++)
        {
            Destroy(uiCanvas.transform.GetChild(i).gameObject);
        }
    }
}
