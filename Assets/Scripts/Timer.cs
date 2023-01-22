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
    private float maxTime;
    private float incrementAmount;
    private float initialFillAmount;
    private float targetFillAmount;

    private IEnumerator coroutine;

    private void OnEnable()
    {
        EventAggregator.setupLevelEvent += SetValues;
        EventAggregator.changeGameStateEvent += OnGameStateChange;
        EventAggregator.matchedEvent += RewardTime;
    }

    private void OnDisable()
    {
        EventAggregator.setupLevelEvent -= SetValues;
        EventAggregator.changeGameStateEvent -= OnGameStateChange;
        EventAggregator.matchedEvent -= RewardTime;

        StopTimer();
    }    

    private void SetValues(LevelData levelData)
    {
        maxTime = levelData.MaxTime;
        incrementAmount = levelData.RewardTime;

        timeLeft = maxTime;

        timerBar.fillAmount = 1;
        initialFillAmount = timerBar.fillAmount;
        targetFillAmount = initialFillAmount;        
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
            targetFillAmount = initialFillAmount * (timeLeft / maxTime);
            timerBar.fillAmount = Mathf.Lerp(timerBar.fillAmount, targetFillAmount, fillSpeed * Time.deltaTime);
        }
        timerBar.fillAmount = 0;

        EventAggregator.changeGameStateEvent?.Invoke(GameStates.LevelFailed);

        StopTimer();
    }

    private void RewardTime(bool matched)
    {
        if (matched)
        {
            timeLeft += incrementAmount;
            timeLeft = timeLeft >= maxTime ? maxTime : timeLeft;
        }
    }

    private void StopTimer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = null;
    }
}
