using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "LevelData", menuName = "SO Assets/Level/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    [MinValue(2)]
    private int rowNum = 2;

    [SerializeField]
    [MinValue(2)]
    private int colNum = 2;

    [SerializeField]    
    private float levelTime = 10;

    [SerializeField]
    [MinValue(5.0f)]
    private float rewardTime = 5;

    //Getters
    public int RowNum => rowNum;
    public int ColNum => colNum;
    public float MaxTime => levelTime;
    public float RewardTime => rewardTime;
}
