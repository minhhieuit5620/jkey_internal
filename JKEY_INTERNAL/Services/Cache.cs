using StackExchange.Redis;
using StackRedis.L1;
using StackRedis.L1.Notifications;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Hazelcast;
using Microsoft.Extensions.Logging;
using Hazelcast.DistributedObjects;
using System.Security.AccessControl;
using Newtonsoft.Json.Linq;

namespace JKEY_INTERNAL.Services
{
    /// <summary>
    /// Simple IDP Caching will have 2 level:
    ///  - Level 1: Local cache in application memory by static dictinary
    ///  - Level 2: Caching in redis and sharing between nodes
    /// Application will check in local cache first, if not exist in cache L1 will try to get it from cache L2
    /// If both level does not exist needed data, app will get it from db and update to 2 cache L1, L2
    /// </summary>
    public static class Cache
    {


        private const string RedisKey = "Redis";
        private const string HazelcastKey = "Hazelcast";
        static IDatabase _redisDirectDb;
        static NotificationDatabase _otherClientDb;
        static RedisL1Database _memDb;
        private static IServer server;

        //private static CallMonitoringRedisDatabase _callMonMemDb;
        public static void InitRedis(string RedisHost, int timeout)
        {
            var options = new ConfigurationOptions();
            options.AllowAdmin = true;
            string[] listHost = RedisHost.Split(';');
            foreach (string host in listHost)
            {
                options.EndPoints.Add(host.Trim());
            }
            options.AsyncTimeout = timeout;
            options.SyncTimeout = timeout;
            options.ConnectTimeout = timeout;
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
            server = connection.GetServer(connection.GetEndPoints().First());
            server.ConfigSet("notify-keyspace-events", "KEA");
            _redisDirectDb = connection.GetDatabase();

            //Construct the in-memory cache using an Implemention of IDatabase that counts calls (so tests can tell which requests made it to the network)
            //_callMonMemDb = new CallMonitoringRedisDatabase(_redisDirectDb);
            //_memDb = new RedisL1Database(_callMonMemDb);

            ////Get a notification database to simulate another client
            //_otherClientDb = new NotificationDatabase(_redisDirectDb, Guid.NewGuid().ToString());

            //_callMonMemDb.Calls = 0;
        }

        public static IHazelcastClient _HazelcastClient;
        public static IHMap<string, object> _HazelcastMap;
        static string _cacheType = RedisKey;

        public static void Init(string defaultForData, string HazelcastHost, string ClusterName, string RedisHost, int RedisTimeout)
        {
            _cacheType = defaultForData;
            switch (_cacheType)
            {
                case RedisKey:
                    InitRedis(RedisHost, RedisTimeout);
                    break;
                case HazelcastKey:
                    InitHazelcast(HazelcastHost, ClusterName);
                    break;
                default:
                    break;
            }
        }

        public static async void InitHazelcast(string HazelcastHost, string clusterName)
        {
            try
            {
                var optionbuilder = new HazelcastOptionsBuilder()
                .WithDefault("Logging:LogLevel:Default", LogLevel.None)
                .WithDefault("Logging:LogLevel:Hazelcast", LogLevel.Information)
                .With(x => x.Networking.Addresses.Clear());
                string[] listHost = HazelcastHost.Split(';');
                optionbuilder.With(x => x.ClusterName = clusterName);
                foreach (string add in listHost)
                {
                    optionbuilder.With(x => x.Networking.Addresses.Add(add.Trim()));
                }
                var options = optionbuilder.Build();

                _HazelcastClient = await HazelcastClientFactory.StartNewClientAsync(options);
                _HazelcastMap = await _HazelcastClient.GetMapAsync<string, object>("APIGW");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void SetString(string key, string value, string module = "cache_Jkey")
        {
            _HazelcastMap.SetAsync($"{module}_{key}", value).GetAwaiter().GetResult();
            //if (_cacheType.Equals(RedisKey))
            //{
            //    _otherClientDb.StringSetAsync($"{module}_{key}", value, null, When.Always, CommandFlags.None).GetAwaiter().GetResult();
            //}
            //else
            //{
            //    _HazelcastMap.SetAsync($"{module}_{key}", value).GetAwaiter().GetResult();
            //}
        }
        public static string GetString(string key, string module = "cache_Jkey")
        {
            //if (_cacheType.Equals(RedisKey))
            //{
            //    return _memDb.StringGetAsync($"{module}_{key}").GetAwaiter().GetResult().ToString();
            //}
            //else
            //{

            //}
            return _HazelcastMap.GetAsync($"{module}_{key}").GetAwaiter().GetResult()?.ToString();
        }

        public static void DelString(string key, string module = "cache_Jkey")
        {
            //if (_cacheType.Equals(RedisKey))
            //{
            //    _otherClientDb.KeyDeleteAsync($"{module}_{key}").GetAwaiter().GetResult();
            //}
            //else
            //{
            //    _HazelcastMap.TryRemoveAsync($"{module}_{key}").GetAwaiter().GetResult();
            //}
            _HazelcastMap.TryRemoveAsync($"{module}_{key}").GetAwaiter().GetResult();
        }

        public static string GetString(object key)
        {
            throw new NotImplementedException();
        }
    }
}
