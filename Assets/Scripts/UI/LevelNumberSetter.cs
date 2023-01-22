using UnityEngine;
using TMPro;

public class LevelNumberSetter : MonoBehaviour
{
    private TextMeshProUGUI tm;

    private void OnEnable()
    {
        EventAggregator.setupLevelEvent += SetLevelNumber;
    }

    private void OnDisable()
    {
        EventAggregator.setupLevelEvent -= SetLevelNumber;
    }

    private void SetLevelNumber(LevelData lvlData)
    {
        if (tm == null)
        {
            tm = GetComponent<TextMeshProUGUI>();
        }

        tm.text = $"Level {EventAggregator.getCurrentLvlIndexEvent?.Invoke() + 1}";
    }
}
