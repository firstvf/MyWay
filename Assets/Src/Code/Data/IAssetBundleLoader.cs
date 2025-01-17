using System;

namespace Assets.Src.Code.Data
{
    public interface IAssetBundleLoader
    {
        void Load<T>(string key, Action<T> callback);
        IEnumerator LoadUrl<T>(string extraKey, string url, Action<T> callback);
    }
}