using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class PlayerContain : MonoBehaviour
{
    public LevelData levelData;
    public InputThone inputThone;
    public void Init()
    {
        string pathLevel = StringHelper.PATH_CONFIG_LEVEL_TEST;
        Debug.LogError(string.Format(pathLevel, UseProfile.CurrentLevel));
        levelData = Instantiate(Resources.Load<LevelData>(string.Format(pathLevel, UseProfile.CurrentLevel)));
        inputThone.thoneDemo = levelData.thoneDemo;
    }

   


}
