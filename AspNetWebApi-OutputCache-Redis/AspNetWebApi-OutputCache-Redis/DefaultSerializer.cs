using System;

namespace WebApi.OutputCache.Core.Cache.Redis
{
    using Jil;

    public class DefaultSerializer : ISerializer
    {
        public T Deserialize<T>(string objectString)
        {
            return JSON.Deserialize<T>(objectString);
        }

        public string Serialize<T>(T value)
        {
            return JSON.Serialize(value);
        }
    }
}