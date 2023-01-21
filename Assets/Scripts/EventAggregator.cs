using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAggregator
{
    public static Action<GameStates> changeGameStateEvent;
    public static Action<int, int, int> filledValueEvent;
    public static Action<bool> matchedEvent;
    public static Action<int> flippedCardValEvent;
    public static Action cardSelectedEvent;
    public static Action twoCardsSelectedEvent;
    public static Action<LevelData> setupLevelEvent;
    public static Action<int> selectedLevelIndexEvent;
}
