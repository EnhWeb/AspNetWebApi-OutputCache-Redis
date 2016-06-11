namespace WebApi.OutputCache.Core.Cache.Redis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StackExchange.Redis;
    using WebApi.OutputCache.Core.Cache.Redis.Extensions;

    public class RedisOutputCache : IApiOutputCache
    {
        private readonly IRedisConnection redisConnection;
        private readonly ISerializer serializer;

        public RedisOutputCache(IRedisConnection redisConnection, ISerializer serializer)
        {
            if (redisConnection.IsNull())
            {
                throw new ArgumentNullException("redisConnection cannot be null");
            }

            if (serializer.IsNull())
            {
                throw new ArgumentNullException("serializer cannot be null");
            }

            this.redisConnection = redisConnection;
            this.serializer = serializer;
        }

        private IDatabase Redis
        {
            get { return this.redisConnection.Database; }
        }

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
        {
            if (key.IsNull())
            {
                throw new ArgumentNullException("Add - key cannot be null");
            }

            if (String.Equals(o, ""))
            {
                return;
            }

            var serializedObject = this.serializer.Serialize(o);
            var added = this.Redis.StringSet(key, serializedObject, expiration.ToTimeSpan());

            if (dependsOnKey != null && added)
            {
                this.Redis.SetAdd(dependsOnKey, key);
            }
        }

        public bool Contains(string key)
        {
            if (key.IsNull())
            {
                throw new ArgumentNullException("Contains - key cannot be null");
            }

            return this.Redis.KeyExists(key);
        }

        [Obsolete("Use Get<T> instead")]
        public object Get(string key)
        {
            return this.Redis.StringGet(key);
        }

        public T Get<T>(string key) where T : class
        {
            if (key.IsNull())
            {
                throw new ArgumentNullException("Get<T> - key cannot be null");
            }

            var redisValue = this.Redis.StringGet(key);
            return this.serializer.Deserialize<T>(redisValue);
        }

        public void Remove(string key)
        {
            if (key.IsNull())
            {
                throw new ArgumentNullException("Remove - key cannot be null");
            }

            this.Redis.KeyDelete(key);
        }

        public void RemoveStartsWith(string key)
        {
            if (key.IsNull())
            {
                throw new ArgumentNullException("RemoveStartsWith - key cannot be null");
            }

            var memberKeys = this.Redis.SetMembers(key);
            foreach (var memberKey in memberKeys)
            {
                this.Remove(memberKey);
            }

            this.Remove(key);
        }

        public IEnumerable<string> AllKeys
        {
            get
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}