using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;

namespace LedTableSimulator
{
    public class TableConsole : Console
    {
        public TableConsole(int width, int height) : base(width, height)
        {
        }

        public void Draw(IEnumerable<byte> bytes)
        {
            var frameBuffer = new Queue<byte>(bytes);

            for (int x = 0; x < 10; x++)
            {
                if (x % 2 == 0)
                {
                    for (int y = 9; y >= 0; y--)
                    {
                        DrawBox(frameBuffer, x, y);
                    }
                }
                else
                {
                    for (int y = 0; y < 10; y++)
                    {
                        DrawBox(frameBuffer, x, y);
                    }
                }
            }
        }

        private void DrawBox(Queue<byte> frameBuffer,
                             int x,
                             int y)
        {
            int b = frameBuffer.Dequeue();
            int g = frameBuffer.Dequeue();
            int r = frameBuffer.Dequeue();
            var color = new Color(r, g, b, 255);

            var rect = new Rectangle(x * 2, y, 2, 1);

            DrawBox(rect, new Cell(Color.Transparent, color));
        }
    }
}