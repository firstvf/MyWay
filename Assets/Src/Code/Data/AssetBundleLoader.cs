using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Src.Code.Data
{
    public class AssetBundleLoader : IAssetBundleLoader
    {
        public void Load<T>(string key, string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            string path = Path.GetDirectoryName(Application.dataPath) + "/AssetsBundles/" + key;

            if (File.Exists(path))
            {
                AssetBundle bundle = AssetBundle.LoadFromFile(path);
                T asset = bundle.LoadAsset<T>(assetName);
                bundle.Unload(false);

                callback.Invoke(asset);
            }
            else Debug.LogError("File does not exist");
        }

        public IEnumerator LoadUrl<T>(string extraKey, string url, string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                    T asset = bundle.LoadAsset<T>(assetName);
                    bundle.Unload(false);

                    callback.Invoke(asset);
                }
                else
                {
                    Debug.Log($"Unable to download {assetName} from link. Use local load");
                    Load<T>(extraKey, assetName, callback);
                }
            }
        }
    }
}