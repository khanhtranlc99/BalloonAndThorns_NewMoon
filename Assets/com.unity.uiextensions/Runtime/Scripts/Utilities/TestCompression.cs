using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using System;
using DG.Tweening;
using TMPro;
using System;
public class TestCompression : MonoBehaviour 
{

	public List<GameObject> lsLevel;

	[Button]
	public void HandleRename()
    {
        int startIndex = 41; // i bắt đầu từ 41

        for (int i = 0; i < lsLevel.Count; i++)
        {
            GameObject prefab = lsLevel[i];
            if (prefab != null) // Kiểm tra prefab không null
            {
                string assetPath = AssetDatabase.GetAssetPath(prefab);
                if (!string.IsNullOrEmpty(assetPath))
                {
                    string newName = $"Level_{startIndex + i}.prefab";
                    string newAssetPath = System.IO.Path.GetDirectoryName(assetPath) + "/" + newName;
                    AssetDatabase.RenameAsset(assetPath, newName);
                    AssetDatabase.SaveAssets();
                    Debug.Log($"Đã đổi tên prefab: {prefab.name} thành {newName}");
                }
            }
        }
    }
		
}
