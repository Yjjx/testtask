using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace _3test
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Шанс получение урона после 914")]
        public int CDamage { get; set; } = 50;

        [Description("Шанс получение урона после 914")]
        public int CEffect { get; set; } = 50;

        [Description("Шанс телепорта в случайную комнату после 914")]
        public int CTp { get; set; } = 50;
        [Description("Список негативных эффектов")]
        public List<EffectType> NEffect { get; set; } = new List<EffectType> { };
    }
}
