using Microsoft.Xna.Framework;
using SadConsole;
using Game = SadConsole.Game;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace LedTableSimulator
{
    public class Program
    {
        public static void Main()
        {
            Game.Create(20, 10);
            Game.OnInitialize = Init;
            Game.OnUpdate = Update;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            var mqtt = new Mqtt();
            var tableConsole = new TableConsole(20, 10);
            var ipConfigConsole = new IpConfigurationConsole(mqtt, 20, 3);

            mqtt.OnFrameBufferReceived += (sender, bytes) =>
            {
                tableConsole.Draw(bytes);
            };

            ipConfigConsole.Connected += (sender, args) =>
            {
                Global.CurrentScreen = tableConsole;
            };

            Settings.ResizeWindow(800,800);

            Global.CurrentScreen = ipConfigConsole;
        }

        private static void Update(GameTime obj)
        {
            if (Global.KeyboardState.IsKeyReleased(Keys.Escape))
            {
                Game.Instance.Exit();
            }
        }
    }
}