using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class SettingsObject : ScriptableObject
{
    public float spehereSpeed = 1f;
    public LevelDifficulty levelDifficulty = LevelDifficulty.Hard;

    public ReusablePlatform PlatformPrefab => _levelsInfo.FirstOrDefault(li => li.levelDifficulty == levelDifficulty).platformPrefab;
    [SerializeField] private List<LevelInfo> _levelsInfo = new List<LevelInfo>();

    public float BoundsLength => _boundsLength;
    [SerializeField] private float _boundsLength = 100f;

    public float BoundsHight => _boundsLength;
    [SerializeField] private float _boundsHight = 5f;

    public int MaxPltaformsCount => _maxPltaformsCount;
    [SerializeField] private int _maxPltaformsCount = 40;


    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsSettingsNames.SphereSpeed))
        {
            spehereSpeed = PlayerPrefs.GetFloat(PlayerPrefsSettingsNames.SphereSpeed);
        }
        
        if (PlayerPrefs.HasKey(PlayerPrefsSettingsNames.LevelDifficulty))
        {
            levelDifficulty = (LevelDifficulty)PlayerPrefs.GetInt(PlayerPrefsSettingsNames.LevelDifficulty);
        }
    }
    
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(PlayerPrefsSettingsNames.SphereSpeed, spehereSpeed);
        PlayerPrefs.SetInt(PlayerPrefsSettingsNames.LevelDifficulty, (int)levelDifficulty);

        PlayerPrefs.Save();
    }
}