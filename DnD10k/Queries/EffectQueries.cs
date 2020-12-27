using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.Queries
{
    public static class EffectQueries
    {
        public static readonly string GetEffectCount = "select count(*) from MagicalEffects;";

        public static readonly string GetEffectById = "select Id,Description from MagicalEffects where Id=@EffectId";
    }
}

