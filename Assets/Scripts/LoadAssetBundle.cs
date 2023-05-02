using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadAssetBundle : MonoBehaviour
{
    public string backgroundBundleName; 
    public string backgroundName; 
    public LevelManager levelManager;

    private void Start()
    {
        StartCoroutine(SawenLoudBackground());
    }

    IEnumerator SawenLoudBackground()
    {
        string bundleURL;
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                bundleURL = "file://" + Application.dataPath + "/AssetBundles/" + backgroundBundleName;
                break;
            case RuntimePlatform.Android:
                bundleURL = Application.streamingAssetsPath + "/" + backgroundBundleName;
                break;
            case RuntimePlatform.IPhonePlayer:
                bundleURL = "file://" + Application.streamingAssetsPath + "/" + backgroundBundleName;
                break;
            default:
                bundleURL = "file://" + Application.dataPath + "/StreamingAssets/" + backgroundBundleName;
                break;
        }

        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error downloading AssetBundle: " + www.error);
            yield break;
        }

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
        if (bundle == null)
        {
            Debug.Log("Error loading AssetBundle");
            yield break;
        }

        Sprite[] sprites = bundle.LoadAllAssets<Sprite>();
        levelManager.NewBeground(sprites);

        bundle.Unload(false);
    }
}
