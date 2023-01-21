using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "LevelData", menuName = "SO Assets/Level/Level Data Holder")]
public class LevelDatas : ScriptableObject
{
    [SerializeField]
    [Expandable]
    private List<LevelData> levelDatas;

    public List<LevelData> LevelDataList => levelDatas;
}
