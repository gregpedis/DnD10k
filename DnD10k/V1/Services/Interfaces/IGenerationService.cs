using DnD10k.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.V1.Services.Interfaces
{
    public interface IGenerationService
    {
        Effect GetRandomEffect();
    }
}

