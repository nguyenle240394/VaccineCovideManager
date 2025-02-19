﻿using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace VaccineCovidManager.MongoDB;

[DependsOn(
    typeof(VaccineCovidManagerTestBaseModule),
    typeof(VaccineCovidManagerMongoDbModule)
    )]
public class VaccineCovidManagerMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = VaccineCovidManagerMongoDbFixture.ConnectionString.Split('?');
        var connectionString = stringArray[0].EnsureEndsWith('/') +
                                   "Db_" +
                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });
    }
}
