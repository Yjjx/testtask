using Exiled.API.Features.Items;
using Exiled.API.Interfaces;
using Exiled.Events.Handlers;
using System;
using System.Collections.Generic;

namespace secondtask
{
    public class Config : IConfig
    {
        public List<ItemType> Items { get; set; } = new List<ItemType> { ItemType.KeycardO5 };
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
