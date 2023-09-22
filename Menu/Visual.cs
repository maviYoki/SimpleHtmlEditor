namespace Menu
{
    public class Visual
    {
        public static void Show()
        {   
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        private static void Line()
        {
            var width = (Console.WindowWidth - 2) / 2;
            Console.Write("+");
            for (int i = 0; i < width-2 ; i++)
                Console.Write("-");

            Console.WriteLine("+");
        }

        public static void DrawScreen()
        {
            Show();

            Line();
            
            var width = (Console.WindowWidth - 2)/2;
            var height = Console.WindowHeight;
            
            for (int i = 0; i < height; i++)
            {
                Console.Write("|");

                for (int j = 0; j < width - 2; j++)
                    Console.Write(" ");

                Console.Write("|");
                Console.Write("\n");
            }
            Line();
            Console.Write('\n');
        }

        public static void WindowSize()
        {
            var height = Console.WindowHeight;
            var width = Console.WindowWidth;


        }
    }
}