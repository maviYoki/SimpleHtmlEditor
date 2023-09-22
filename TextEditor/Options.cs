using System.Text.RegularExpressions;

namespace TextEditor
{
    public class Options
    {
        public static void WriteOptions()
        {

            Console.Clear();
            Menu.Visual.DrawScreen();

            string[] texts = { "Editor HTML", "What do you want to do? ", "1 - Open file", "2 - Create new file ", "0 - Exit" };

            for (int i = 0; i < texts.Length; i++)
            {
                if (i == 0)
                {
                    Console.SetCursorPosition(3, 3);
                    Console.WriteLine(texts[i]);
                }
                else if (i == 1)
                {
                    Console.SetCursorPosition(3, 5);
                    Console.WriteLine(texts[i]);
                }
                else
                {
                    Console.SetCursorPosition(3, 5 + i);
                    Console.WriteLine(texts[i]);
                }


                Console.SetCursorPosition(3, 10);
                Console.Write("-> ");
            }
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input invalid");
                Thread.Sleep(1500);
                WriteOptions();
                return;
            }

            var option = int.Parse(input);

            switch (option)
            {
                case 0:
                    Console.WriteLine("\nExit...");
                    break;
                case 1:
                    Console.WriteLine("\nOpen file..");
                    OpenFile();
                    break;
                case 2:
                    Console.WriteLine("\nCreate new file..");
                    NewFile();
                    break;
                default:
                    WriteOptions();
                    break;
            }

        }


        public static void OpenFile()
        {
            Console.Clear();
            Menu.Visual.Show();
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("\n Enter the path to the file you want to access:");
            var path = Console.ReadLine();


            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("\n Invalid input");
                ReturningMenu(ReturningMenuText());
                WriteOptions();
                return;
            }
            if (Directory.Exists(path))
            {
                using (var file = new StreamReader(path))
                {
                    Console.WriteLine(file.ReadToEnd());
                }

                Console.WriteLine("\n 'ENTER' press to continue in the menu or 1 to delete file");

                var resposta = Console.ReadKey().Key;

                if (resposta == ConsoleKey.D1)
                {
                    DeleteFile(path);
                }

                if (resposta == ConsoleKey.Enter || resposta == ConsoleKey.D1)
                {
                    ReturningMenu(ReturningMenuText());
                }
            }
            else
            {
                Console.WriteLine("\n Directory does not exist..");
                Thread.Sleep(2000);
                OpenFile();
            }
        }

        public static void NewFile()
        {
            Console.Clear();

            Console.WriteLine("     Enter your text below");
            Console.WriteLine("----------------------------------\n");

            var text = "";

            do
            {
                var textInput = Console.ReadLine();
                text += textInput;
                text += Environment.NewLine;
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            Console.WriteLine("-------------------------------------------\n");

            Replace(text);
        }

        public static void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                Console.WriteLine("\nSuccessfully deleted file\n");
                Thread.Sleep(2000);
            }

            else Console.WriteLine("Null");
        }

        public static void Saved(string text)
        {
            Console.WriteLine("do you Which way salve?");
            var path = Console.ReadLine();

            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Invalid input");
                ReturningMenu(ReturningMenuText());
                return;
            }
            using (var file = new StreamWriter(path))
            {
                file.WriteLine(text);
                Console.WriteLine("\nSuccessfully saved file...");
                Thread.Sleep(1500);
            }

            ReturningMenu(ReturningMenuText());


        }

        public static void ReturningMenu(string text)
        {
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();

                Console.WriteLine($"{text}{i}");
                Thread.Sleep(1000);
            }
            WriteOptions();
        }

        public static string ReturningMenuText()
        {
            return "Retorning to the menu in...";
        }

        public static void Replace(string text)
        {
            var strong = new Regex(@"<\s*strong[^>]*>(.*?)<\s*/\s*strong>");
            var words = text.Split(' ');
            var wasFound = 1;


            for (var i = 0; i < words.Length; i++)
            {

                if (strong.IsMatch(words[i]))
                {
                    wasFound++;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(
                        words[i].Substring(
                            words[i].IndexOf('>') + 1,
                            ((words[i].LastIndexOf('<') - 1) - words[i].IndexOf('>'))
                        )
                    );
                    Console.Write(" ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(words[i]);
                    Console.Write(" ");
                }
            }
            if(wasFound > 1)
            {
                Console.WriteLine();
                Saved(text);
            }else
            {
                Console.WriteLine("\nwrite without spaces between tag closures");
                Thread.Sleep(1500);
                NewFile();
                
            }
        }


    }
}
