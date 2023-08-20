using Exiled.API.Features;
using InventorySystem;
using PlayerRoles;


namespace secondtask
{
    public class Plugin: Plugin<Config>
    {
        public override string Name => "testtask";
        public override string Author => "yjjx";
        public override string Prefix => "ftt";
        public override void OnEnabled()
        {
            base.OnEnabled();
            Log.Info("secondtask is enabled");
        }
    }
}
