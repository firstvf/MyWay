using System;
using UnityEngine;

namespace Assets.Src.Code.Data
{
    public class AssetBundleLoader : IAssetBundleLoader
    {
        public void Load<T>(string key, Action<T> callback);
        public IEnumerator LoadUrl<T>(string extraKey, string url, Action<T> callback);
    }
}