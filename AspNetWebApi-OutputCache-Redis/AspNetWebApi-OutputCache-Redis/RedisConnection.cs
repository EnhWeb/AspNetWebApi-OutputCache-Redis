namespace WebApi.OutputCache.Core.Cache.Redis
{
    using System;
    using StackExchange.Redis;
    using WebApi.OutputCache.Core.Cache.Redis.Extensions;

    public class RedisConnection : IRedisConnection
    {
        private readonly ConnectionMultiplexer connection;

        public RedisConnection(RedisConnectionSettings settings)
        {
            if (settings.IsNull())
            {
                throw new ArgumentNullException("settings cannot be null");
            }

            var options = ConfigurationOptions.Parse(settings.ConnectionString);
            options.DefaultDatabase = settings.Database;

            this.connection = ConnectionMultiplexer.Connect(options);
        }

        public IDatabase Database
        {
            get
            {
                return this.connection.GetDatabase();
            }
        }
    }
}