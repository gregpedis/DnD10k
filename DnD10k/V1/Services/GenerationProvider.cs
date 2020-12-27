using DnD10k.Models;
using DnD10k.Queries;
using DnD10k.V1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.V1.Services
{
    public class GenerationProvider : IGenerationService
    {
        private readonly IDBService _provider;

        public GenerationProvider(IDBService provider)
        {
            _provider = provider;
        }

        public Effect GetRandomEffect()
        {
            var randomId = GenerateRandomId();

            var query = EffectQueries.GetEffectById;
            var selectParams = new { EffectId = randomId };

            var effect = _provider.QueryFirst<Effect>(query, selectParams); ;
            return effect;
        }

        private int GenerateRandomId()
        {
            var rng = new Random();
            var maxValue = GetEffectCount();

            var randomId = rng.Next(0, maxValue);
            return randomId;
        }

        private int GetEffectCount()
        {
            var query = EffectQueries.GetEffectCount;
            var count = _provider.QueryFirstOrDefault<int>(query);
            return count;
        }
    }
}

