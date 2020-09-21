using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;
using SadConsole.Controls;

namespace LedTableSimulator
{
    public class IpConfigurationConsole : ControlsConsole
    {
        private readonly Mqtt _mqtt;
        private readonly TextBox _textBox;
        public event EventHandler Connected;

        public IpConfigurationConsole(Mqtt mqtt, int width, int height) : base(width, height)
        {
            _mqtt = mqtt;
            Position = new Point(0, 4);
            UseKeyboard = true;
            IsFocused = true;

            var label = new Label(20)
            {
                Position = Point.Zero,
                DisplayText = "MQTT broker IP:"
            };

            _textBox = new TextBox(20)
            {
                Position = new Point(0, 1),
            };

            var button = new Button(20)
            {
                Text = "Connect",
                Position = new Point(0, 2)
            };

            button.Click += async (sender, args) => await ButtonOnClick();

            Add(label);
            Add(_textBox);
            Add(button);

            FocusedControl = _textBox;
        }

        public override async void Update(TimeSpan time)
        {
            base.Update(time);

            if (KeyboardState.IsKeyReleased(Keys.Enter))
            {
                await ButtonOnClick();
            }
        }

        private async Task ButtonOnClick()
        {
            bool connected = await _mqtt.TryConnect(_textBox.Text);

            if (connected)
            {
                Connected?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}