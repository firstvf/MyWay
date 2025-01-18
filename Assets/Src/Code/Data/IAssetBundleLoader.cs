using System;
using System.Collections;

namespace Assets.Src.Code.Data
{
    public interface IAssetBundleLoader
    {
        void Load<T>(string key, string assetName, Action<T> callback) where T : UnityEngine.Object;
        IEnumerator LoadUrl<T>(string extraKey, string url, string assetName, Action<T> callback) where T : UnityEngine.Object;
    }
}