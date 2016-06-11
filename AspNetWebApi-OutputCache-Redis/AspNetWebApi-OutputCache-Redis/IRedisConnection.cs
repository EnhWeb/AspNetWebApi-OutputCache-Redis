namespace WebApi.OutputCache.Core.Cache.Redis
{
    using StackExchange.Redis;

    public interface IRedisConnection
    {
        IDatabase Database { get; }
    }
}