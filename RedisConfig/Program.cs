using RedisConfig;

DistributedCacheTtl distributedCacheTtl = new();
distributedCacheTtl.Run();

RedisLruPolicyCheck.Run();