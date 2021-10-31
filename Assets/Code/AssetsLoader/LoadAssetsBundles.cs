using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AssetsLoader
{
    public sealed class LoadAssetsBundles : MonoBehaviour
    {
        [SerializeField]
        private string _path = "file://d:/geekbrains.ru/Projects/Unity/Racing/Assets/Resources/Prefabs";

        private uint _version = 0;

        private void Start()
        {
            StartCoroutine(LoadFromWeb(_path));
        }

        private IEnumerator LoadFromWeb(string url)
        {
            UnityWebRequest www = new UnityWebRequest(url);
            DownloadHandlerAssetBundle handler = new DownloadHandlerAssetBundle(www.url, _version, 0);
            www.downloadHandler = handler;
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AssetBundle bundle = handler.assetBundle;
                AssetBundleRequest request = bundle.LoadAssetAsync("Car", typeof(GameObject));
                yield return request;

                GameObject prefab = request.asset as GameObject;
                Instantiate(prefab, Vector3.one, Quaternion.identity);
                bundle.Unload(false);
            }
        }
    }
}
