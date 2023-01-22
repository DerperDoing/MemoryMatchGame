using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    private Button button;
    private int index;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectLevel);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        index = button.transform.GetSiblingIndex();
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (index + 1).ToString();
    }

    private void SelectLevel()
    {
        EventAggregator.buttonPressedEvent?.Invoke();
        EventAggregator.selectedLevelIndexEvent?.Invoke(index);
    }
}
