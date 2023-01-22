using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Image timerBar;

    [SerializeField]
    private float fillSpeed = 5f;

    private float timeLeft;
    private float incrementAmount;
    private float initialFillAmount;
    private float targetFillAmount;

    private IEnumerator coroutine;

    private void OnEnable()
    {
        EventAggregator.setupLevelEvent += SetValues;
        EventAggregator.changeGameStateEvent += OnGameStateChange;
    }

    private void OnDisable()
    {
        EventAggregator.setupLevelEvent -= SetValues;
        EventAggregator.changeGameStateEvent -= OnGameStateChange;

        StopTimer();
    }    

    private void SetValues(LevelData levelData)
    {
        timeLeft = levelData.MaxTime;
        incrementAmount = levelData.RewardTime;

        timerBar.fillAmount = 1;
        initialFillAmount = timerBar.fillAmount;
        targetFillAmount = timerBar.fillAmount;
    }

    private void OnGameStateChange(GameStates newState)
    {
        if (newState == GameStates.LevelCompleted)
        {
            StopTimer();
        }
        else if (newState == GameStates.StartLevel)
        {
            StopTimer();

            coroutine = DecrementTimer();
            StartCoroutine(coroutine);
        }        
    }

    IEnumerator DecrementTimer()
    {        
        while (timeLeft > 0)
        {
            yield return new WaitForEndOfFrame();

            timeLeft -= Time.deltaTime;
            targetFillAmount = initialFillAmount * (timeLeft / 60.0f);
            timerBar.fillAmount = Mathf.Lerp(timerBar.fillAmount, targetFillAmount, fillSpeed * Time.deltaTime);
        }

        EventAggregator.changeGameStateEvent?.Invoke(GameStates.LevelFailed);

        StopTimer();
    }

    private void StopTimer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = null;
    }


    //TODO: Remove later
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            timeLeft += incrementAmount;
            targetFillAmount = initialFillAmount * (timeLeft / 60.0f);
        }
    }
}
