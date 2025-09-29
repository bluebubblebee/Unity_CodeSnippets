using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ContentManagement
{ 
    public static class ContentAssetUtility
    {
        private const string k_assetsBaseDirectory = "Assets";

        public static string GetAssetDirectory(string relativePath)
        {
            return Path.Combine(k_assetsBaseDirectory, relativePath);
        }

        public static void MakeDirty(Object target)
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(target);
#endif
        }

        public static void SaveAssets()
        {
#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
#endif
        }

        public static string GetAssetFileName(string name)
        {
            return $"{name}.asset";
        }

        public static string GetPrefabFileName(string name)
        {
            return $"{name}.prefab";
        }

        public static string GetValidAssetName(string name)
        {
            name = name.Replace('\\', '-');
            name = name.Replace('/', '-');
            return name;
        }

        public static void DestroyAsset(Object target)
        {
            Object.Destroy(target);

        }
    }
}
