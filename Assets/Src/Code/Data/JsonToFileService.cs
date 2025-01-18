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
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string jsonText = request.downloadHandler.text;

                    var data = JsonConvert.DeserializeObject<T>(jsonText);
                    callback.Invoke(data);
                }
                else
                {
                    Debug.Log($"Unable to download {extraKey} from link. Use local load");
                    Load<T>(extraKey, callback);
                }
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