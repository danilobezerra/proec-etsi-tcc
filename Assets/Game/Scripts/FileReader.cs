using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_WEBGL
using UnityEngine.Networking;
#endif

namespace Game.Scripts
{
    internal static class FileReader
    {
        private static int[,] ReadAsValues(string input)
        {
            var rows = input.Split('\n');
            var values = new int[rows.Length, rows.Length];
        
            for (var i = 0; i < rows.Length; i++) {
                var columns = rows[i].Split(' ');
            
                for (var j = 0; j < columns.Length; j++) {
                    values[i, j] = int.Parse(columns[j]);
                }
            }

            return values;
        }
        
#if UNITY_EDITOR
        public static IEnumerator LoadStreamingAsset(string filename, UnityAction<int[,]> onReadFile) {
            var path = Path.Combine(Application.streamingAssetsPath, $"{filename}.txt");
            
            var reader = new StreamReader(path);
            var input = reader.ReadToEnd();
            reader.Close();
            
            yield return new WaitForEndOfFrame();
            
            var values = ReadAsValues(input);
            onReadFile(values);
        }
        
#elif UNITY_WEBGL
        public static IEnumerator LoadStreamingAsset(string filename, UnityAction<int[,]> onReadFile) {
            var uri = Path.Combine(Application.streamingAssetsPath, $"{filename}.txt");
            var www = UnityWebRequest.Get(uri);
            
            yield return www.SendWebRequest();
 
            if (www.isNetworkError || www.isHttpError) {
                Debug.LogError(www.error);
            } else {
                var input = www.downloadHandler.text;
                var values = ReadAsValues(input);
                onReadFile(values);
            }
        }
#endif
    }
}
