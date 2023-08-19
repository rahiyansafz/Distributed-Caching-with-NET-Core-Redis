using RedisConfig;

DistributedCacheTtl distributedCacheTtl = new DistributedCacheTtl();
distributedCacheTtl.Run();

RedisLruPolicyCheck.Run();