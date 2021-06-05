#if UNITY_EDITOR


using System;
using UnityEditor;

namespace AssetsLoader
{
    public sealed class CreateAssetBundles
    {
        [MenuItem("Assets/Build Asset bundles")]
        private static void BuildAsset()
        {
            var path = EditorUtility.SaveFolderPanel("Save asset bundles", "", "");
            if(String.IsNullOrEmpty(path)) return;
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }
    }
}

#endif 