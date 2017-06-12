using System;
using System.Linq;
using System.Text;
using System.Threading;
using CarpetFishing.GameTypes;

namespace CarpetFishing
{
    class Program
    {
        private static int[] Grid = null;
        private static int WaitTime = -1;
        private static GameData GameType = null;
        private static bool Notification = false;
        private static GameData[] AvailableGameTypes = null;

        private static char[] Ps = new char[] { '|', '/', '-', '\\' };
        private static int Progress
        {
            get { return _Progress; }
            set
            {
                if (value > 3) { Progress = 0; }
                else { _Progress = value; }
            }
        }
        private static int _Progress = 0;

        static void Main(string[] args)
        {
            try
            {
                AvailableGameTypes = new GameData[]
                {
                    new FishingData(),
                    new DataCollectionData(),
                };

                if (!CheckArgs(args)) return;
                WriteWelcome();
                if (Grid == null) { GetGridSize(); }
                if (WaitTime < 1) { GetWaitTime(); }
                WriteStart();
                MainRoutine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Oops, an error happened: {0}", ex.Message);
                Console.WriteLine("Press any key to close...");
                Console.ReadKey();
            }
        }


        private static bool CheckArgs(string[] args)
        {
            if (args == null) return false;
            {
                bool showHelp = false;

                foreach (var arg in args)
                {
                    if (arg == "-h") { showHelp = true; }
                    else if (arg == "-n") { Notification = true; }
                    else if (arg.Length >= 3 && arg.StartsWith("-t"))
                    {
                        string stime = arg.Substring(2);
                        if (int.TryParse(stime, out WaitTime)) { WaitTime *= 60000; }//Convert to ms
                        else { WaitTime = -1; }
                    }
                    else if (arg.Length >= 5 && arg.StartsWith("-g"))
                    {
                        string[] sgrid = arg.Substring(2).Split(',');
                        if (sgrid.Length == 2)
                        {
                            int X, Y = 1;
                            bool success = int.TryParse(sgrid[0], out X) && int.TryParse(sgrid[1], out Y);
                            if (success) { Grid = new int[] { X, Y }; }
                        }
                    }
                    else if (GameType == null)
                    {
                        GameData found = AvailableGameTypes.FirstOrDefault(t => "-" + t.Tag == arg);
                        if (found != null) GameType = found;
                    }
                }

                if (GameType != null) GameType.Init(args);

                if (showHelp)
                {
                    WriteHelp();
                    return false;
                }
                else
                {
                    if (GameType == null) { GameType = new FishingData(); }
                    return true;
                }
            }
        }

        private static void WriteHelp()
        {
            bool useData = GameType != null;

            StringBuilder builder = new StringBuilder();

            builder.Append("Help for ");
            if (useData) builder.Append(GameType.Title);
            else builder.Append("Carpet Fishing");
            builder.AppendLine();

            builder.AppendLine();
            builder.AppendLine("Arguments:");
            builder.AppendLine("-h\tPrints this help");
            builder.AppendLine("-n\tEnables a notification when a result has been found");
            builder.AppendLine("-t[i]\tTime to wait between results in minutes. Example: -t5");
            builder.AppendLine("-g[x,y]\tThe size of the grid. Example: -g5,5");
            builder.AppendLine();

            if (useData && GameType.Help != null) { builder.AppendLine(GameType.Help); }
            else
            {
                builder.AppendLine("Game Type Arguments:");
                for (int i = 0; i < AvailableGameTypes.Length; i++)
                {
                    builder.Append("-");
                    builder.Append(AvailableGameTypes[i].Tag);
                    builder.Append("\t");
                    builder.Append(AvailableGameTypes[i].Description);
                    builder.AppendLine();
                }
                builder.AppendLine();
                builder.AppendLine("The program type arguments cannot be combined.");
                builder.AppendLine("If multiple program types are given, the first will be used.");
                builder.AppendLine("If no program type is given, it will fall back to standard fishing.");
            }

            if (!(GameType != null && GameType.OmitHowTo))
            {
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendLine("How To:");
                builder.AppendLine("1) Enter the size of your grid. (If not provided by the -g argument)");
                builder.AppendLine("2) Enter the time to wait between results. (If not provided by the -t argument)");
                builder.AppendLine("3) Choose one of the grids where you expect a result and remember its coordinates.");
                builder.AppendLine("4) The program will print out coordinates approximately in the given interval.");
                builder.AppendLine("5) If the program prints out the coordinates you chose, you have won");
                builder.AppendLine("6) Repeat from step 3");
            }
            builder.AppendLine();
            builder.AppendLine("You can stop by closing the console (or usually pressing Ctrl+C)");
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine("This program was inspired by a Dilbert comic, by Scott Adams. (2007-09-30 specifically)");
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine("Program written by Johannes Bildstein <info@fotostein.at> © 2015");
            builder.AppendLine("Program is released under the MIT license");
            builder.AppendLine("Report bugs, problems or expansions to https://github.com/JBildstein/CarpetFishing");
            builder.AppendLine();

            Console.WriteLine(builder.ToString());
        }

        private static void WriteWelcome()
        {
            string standard = "Welcome to ";
            string filler = new string('#', GameType.Title.Length + standard.Length + 6);
            Console.WriteLine(filler);
            Console.WriteLine("## {0}{1} ##", standard, GameType.Title);
            Console.WriteLine(filler);
            Console.WriteLine();
        }

        private static void WriteStart()
        {
            Console.Clear();
            WriteWelcome();
            Console.WriteLine(GameType.Start);
            Console.WriteLine("{0}: {1}m  -  {2}: X:{3} Y:{4}", GameType.Time, WaitTime / 60000, GameType.Grid, Grid[0], Grid[1]);
            Console.WriteLine();
        }

        private static void WriteFound(int x, int y)
        {
            Console.WriteLine("\r{0}", GameType.GetResultString(x, y));
            Console.WriteLine();
        }

        private static void WriteProgress()
        {
            Console.Write("\r{0} {1}", GameType.Progress, Ps[Progress]);
            Progress++;
        }

        private static void GetGridSize()
        {
            Console.WriteLine("{0} (x,y):", GameType.GetGrid);
            Grid = new int[2];

            while (true)
            {
                string input = Console.ReadLine();
                string[] split = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 2 && int.TryParse(split[0], out Grid[0]) && int.TryParse(split[1], out Grid[1])) { break; }
                else { Console.WriteLine("Incorrect format. Example: \"10,5\" or \"10 5\""); }
            }
        }

        private static void GetWaitTime()
        {
            Console.WriteLine("{0} (in minutes):", GameType.GetTime);

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out WaitTime))
                {
                    WaitTime *= 60000;//Convert to ms
                    break;
                }
                else { Console.WriteLine("Incorrect format. Example: \"50\""); }
            }
        }

        private static void MainRoutine()
        {
            Random rd = new Random();
            while (true)
            {
                int wait = rd.Next((int)(WaitTime * 0.9), (int)(WaitTime * 1.1));
                while (wait > 0)
                {
                    Thread.Sleep(150);
                    wait -= 150;
                    WriteProgress();
                }
                WriteFound(rd.Next(Grid[0]), rd.Next(Grid[1]));
                if (Notification) { GameType.Notification(); }
            }
        }
    }
}
