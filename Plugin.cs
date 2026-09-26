using System;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace AutoRestart
{
    public class AutoRestartPlugin : Plugin<Config>
    {
        public override string Name => "AutoRestart";
        public override string Prefix => "autorestart";
        public override string Author => "Rude";
        public override Version Version => new Version(1, 0, 0);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Left += OnPlayerLeft;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Left -= OnPlayerLeft;
            base.OnDisabled();
        }

        private void OnPlayerLeft(LeftEventArgs ev)
        {
            Timing.CallDelayed(1f, () =>
            {
                if (Round.IsStarted)
                {
                    int realPlayersCount = Player.List.Count(p => !p.IsNPC);

                    if (realPlayersCount == 0)
                    {
                        Log.Info("Ебать без онлайна да? Ладно перезапустим серв тебе лошара хех (sr)...");
                        Server.ExecuteCommand("sr");
                    }
                }
            });
        }
    }

    public class Config : Exiled.API.Interfaces.IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}