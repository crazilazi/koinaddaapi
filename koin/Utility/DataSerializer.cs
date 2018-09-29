namespace koin.Utility
{
    using System;
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public static class DataSerializer
    {
       
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserializer<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// The json deserializer from file.
        /// </summary>
        /// <param name="JsonPath">
        /// The json path.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T JsonDeserializerFromFile<T>(string JsonPath)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            // Object Obj = null;
            using (var sr = new StreamReader(JsonPath))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return (T)serializer.Deserialize<T>(jsonTextReader);
            }

            // return (T)Obj;
        }

        /// <summary>
        /// The json serializer.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string JsonSerializer<T>(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        /// <summary>
        /// The json serializer save as file.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="JsonPath">
        /// The json path.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void JsonSerializerSaveAsFile<T>(object obj, string JsonPath)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;

            using (var sw = new StreamWriter(JsonPath))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }
    }
}