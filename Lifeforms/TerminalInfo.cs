using System;

namespace Lifeforms
{
    public static class TerminalInfo
    {
        private static int width;
        private static int height;

        public static void Initialize(int _width, int _height)
        {
            width = Math.Min(_width, Console.BufferWidth);
            height = Math.Min(_height, Console.BufferHeight);
        }

        public static int ViewportColumns {  get { return width; } }
        public static int ViewportRows { get { return height; } }
                
        public static int GameboardColumns { get { return width; } }
        public static int GameboardRows { get { return height - 6; } }

    }
}
