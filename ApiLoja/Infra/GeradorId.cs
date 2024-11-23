using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SnowflakeGenerator;

namespace ApiLoja.Infra
{
    public class GeradorId
    {
        static GeradorId()
        {
            Settings settings = new Settings
            {
                MachineID = 1,
                CustomEpoch = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            snowflake = new Snowflake(settings);
        }
        public static long GetId()
        {
            return snowflake.NextID();
        }

        private static readonly Snowflake snowflake;
    }
}