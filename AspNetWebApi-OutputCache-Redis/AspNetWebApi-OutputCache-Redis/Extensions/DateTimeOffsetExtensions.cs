namespace WebApi.OutputCache.Core.Cache.Redis.Extensions
{
    using System;

    public static class DateTimeOffsetExtensions
    {
        public static TimeSpan ToTimeSpan(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.Subtract(DateTimeOffset.Now);
        }
    }
}