using UnityEditor;

public class BuildAssetBundle
{
    [MenuItem("Assets/BuildAssetBundles")]
    public static void CreateAssetBundle()
    {
        string assetBundleDirectory = "Assets/AssetBundles";

        if (!System.IO.Directory.Exists(assetBundleDirectory))
        {
            System.IO.Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
