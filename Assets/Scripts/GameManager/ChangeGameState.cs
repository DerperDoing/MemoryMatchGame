using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGameState : MonoBehaviour
{
    [SerializeField]
    private GameStates switchTo;

    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(ChangeState);
        }
    }

    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void ChangeState()
    {
        EventAggregator.buttonPressedEvent?.Invoke();
        EventAggregator.changeGameStateEvent?.Invoke(switchTo);
    }
}
