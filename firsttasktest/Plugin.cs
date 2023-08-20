using Exiled.API.Features;
using InventorySystem;
using PlayerRoles;


namespace firsttasktest
{
    public class Plugin: Plugin<Config>
    {
        public override string Name => "testtask";
        public override string Author => "yjjx";
        public override string Prefix => "ftt";
        public override void OnEnabled()
        {
            base.OnEnabled();
            Log.Info("ftt is enabled");
            Exiled.Events.Handlers.Player.Spawned += Player_Spawned;
            Exiled.Events.Handlers.Warhead.Detonated += Warhead_Detonated;
        }

        private void Warhead_Detonated()
        {
            foreach (Exiled.API.Features.Player Player in Exiled.API.Features.Player.List) 
            {
                Player.Kill("Вы были уничтожены боеголовкой Омега"); //просто убивает всех.
            }
        }

        private void Player_Spawned(Exiled.Events.EventArgs.Player.SpawnedEventArgs ev)
        {
            if (ev.Player.Role.Type is RoleTypeId.Scientist) //проверяет что роль игрока учёный.
            {
                ev.Player.Health = 500;
                ev.Player.Inventory.ServerAddItem(ItemType.SCP500);
                ev.Player.Inventory.ServerAddAmmo(ItemType.Ammo9x19, 100);
                /*
                 * ну и выдаём нужные вещи:
                 * 500 хп.
                 * scp 500.
                 * 100 патронов 9x19.
                 */
            }
        }
    }
}
