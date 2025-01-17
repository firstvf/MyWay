using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Src.Code.Data
{
    public class JsonToFileService : IDataService
    {
        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);

            using (var fileStream = new StreamReader(path))
            {
                var json = fileStream.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T>(json);
                callback.Invoke(data);
            }
        }

        public IEnumerator LoadUrl<T>(string extraKey, string url, Action<T> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            try
            {
                if (request.result != UnityWebRequest.Result.ConnectionError
                || request.result != UnityWebRequest.Result.ProtocolError)
                {
                    string jsonText = request.downloadHandler.text;

                    var data = JsonConvert.DeserializeObject<T>(jsonText);                    
                    callback.Invoke(data);
                }
            }
            catch (Exception)
            {
                Debug.Log("Unable to download from link. Use default load");
                Load<T>(extraKey, callback);
                throw;
            }
        }

        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = BuildPath(key);
            string json = JsonConvert.SerializeObject(data);

            using (var fileStream = new StreamWriter(path))
            {
                fileStream.Write(json);
            }

            callback?.Invoke(true);
        }

        private string BuildPath(string key)
        => Application.streamingAssetsPath + "/" + key + ".json";
    }
}