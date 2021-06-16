using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES.Utils
{
    public class JsonTools
    {
        public static string Convert2JsonString<T>(T srcObject)
        {
            string output = JsonConvert.SerializeObject(srcObject);
            return output;
        }

        public static T Convert2FromString<T>(string srcString)
        {
            T deserializedObject = JsonConvert.DeserializeObject<T>(srcString);
            return deserializedObject;
        }



        public static void WriteJsonFile<T>(IEnumerable<T> item, string path)
        {

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, item);
            }

        }

        public static T ReadJsonFile<T>(string path)
        {

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            {
                T result = (T)serializer.Deserialize(file, typeof(T));
                return result;
            }

        }
    }
}
