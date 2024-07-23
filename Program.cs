using NAudio.Wave;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using systemd;
using System.Windows.Forms;
using Terminal;
using Terminal.Color;
using Terminal.String;
namespace termXV3
{
    public static class INFOS
    {
        public static void Get()
        {         
            while(true)
            {
                Process currentProcess = Process.GetCurrentProcess();
                ProcessThreadCollection threads = currentProcess.Threads;
                long memoryUsageBytes = currentProcess.WorkingSet64;
                double memoryUsageMB = memoryUsageBytes / 1024.0 / 1024.0;
                Console.Title = ($"{memoryUsageMB:F} MB | Threads {threads.Count}");
            }

        }
    }
    public static class CD
    {
        public static string CD_DIR = @"C:\Users\" + Environment.UserName;
        public static string OLD_CDR = null; public static string ADMIN = null;
        public static string tools = CD.CD_DIR + "\\" + "termXtools";

    }
    public static class INPUT_STREAM
    {
        
        private static string ip = GetIp().GetAwaiter().GetResult().AddRight(" ");
        private static string SolveCommand(string command)
        {
            if (String.IsNullOrWhiteSpace(command))
            {

                return null;
            }
            else
            {
                string[] solved = command.Split(' ');
                if (!String.IsNullOrWhiteSpace(solved[0]))
                {
                    return solved[0];
                }
                else
                {
                    return null;
                }
            }
        }
        private static string SolvePath(string command)
        {
            if (String.IsNullOrWhiteSpace(command))
            {
                return null;
            }
            else
            {
                return command.Substring(command.FirstIndexChar(' ') + 1);
            }
        }
        private static string input(string text, string rgb, bool password, bool command, int len = 0)
        {
            string input_stream = "";
            Text.WriteFore(rgb, text);
            while (true)
            {
                ConsoleKeyInfo keys = Console.ReadKey(intercept: true);
                char key = keys.KeyChar;

                if (key == '\r')
                {
                    return input_stream;
                }
                else if (key == '\b' || keys.Key == ConsoleKey.Delete)
                {
                    if (input_stream.Length > 0)
                    {
                        Console.Write("\b \b");
                        input_stream = input_stream.Substring(0, input_stream.Length - 1);
                    }
                }
                else
                {
                    if (keys.Key == ConsoleKey.RightArrow || keys.Key == ConsoleKey.DownArrow || keys.Key == ConsoleKey.UpArrow || keys.Key == ConsoleKey.LeftArrow)
                    {
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                    }
                    else if (keys.Key != ConsoleKey.Tab && char.IsDigit(key) || char.IsWhiteSpace(key) || char.IsLetter(key) || char.IsSymbol(key) || char.IsPunctuation(key))
                    {
                        if (len != 0)
                        {
                            if (input_stream.Length < len)
                            {
                                input_stream += key; if (password is true) Console.Write('*');
                                else if (password is false && command is true && (input_stream.Contains("$") || input_stream.Contains("-")))
                                {
                                    if (!input_stream.Substring(input_stream.FirstIndexChar('$') + 1).Contains(' '))
                                    {
                                        Text.WriteFore(Color.GreenYellow, key.ToString());
                                    }
                                    else if (!input_stream.Substring(input_stream.FirstIndexChar('-') + 1).Contains(' '))
                                    {
                                        Text.WriteFore(Color.SpringGreen, key.ToString());
                                    }
                                    else
                                    {
                                        if (key == '\\')
                                        {
                                            Text.WriteFore(Color.Orange, key.ToString());
                                        }
                                        else if (key == '=')
                                        {
                                            Text.WriteFore(Color.Gray, key.ToString());
                                        }
                                        else if (key == ':')
                                        {
                                            Text.WriteFore(Color.DeepSkyBlue, key.ToString());
                                        }
                                        else if (key == '.')
                                        {
                                            Text.WriteFore(Color.HotPink, key.ToString());
                                        }
                                        else if (key.ToString() == @"""")
                                        {
                                            Text.WriteFore(Color.Orange, key.ToString());
                                        }
                                         
                                        else if (char.IsDigit(key))
                                        {
                                            Text.WriteFore(Color.Tan, key.ToString().Bold());
                                        }
                                        else
                                        {
                                            Text.WriteFore(Color.White, key.ToString());
                                        }
                                    }
                                }
                                else if (password is false && command is true && !input_stream.Contains(" "))
                                {
                                    Text.WriteFore(Color.Yellow, key.ToString());
                                }
                                else
                                {
                                    if (key == '\\')
                                    {
                                        Text.WriteFore(Color.Orange, key.ToString());
                                    }
                                    else if (key == '=')
                                    {
                                        Text.WriteFore(Color.Gray, key.ToString());
                                    }
                                    else if (key == ':')
                                    {
                                        Text.WriteFore(Color.DeepSkyBlue, key.ToString());
                                    }
                                    else if (key == '.')
                                    {
                                        Text.WriteFore(Color.HotPink, key.ToString());
                                    }
                                    else if (key.ToString() == @"""")
                                    {
                                        Text.WriteFore(Color.Orange, key.ToString());
                                    }
                                    else if (char.IsDigit(key))
                                    {
                                        Text.WriteFore(Color.Tan, key.ToString().Bold());
                                    }
                                    else
                                    {
                                        Text.WriteFore(Color.White, key.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            input_stream += key; if (password is true) Console.Write('*');
                            else if (password is false && command is true && (input_stream.Contains("$") || input_stream.Contains("-")))
                            {
                                if (!input_stream.Substring(input_stream.FirstIndexChar('$') + 1).Contains(' '))
                                {
                                    Text.WriteFore(Color.GreenYellow, key.ToString());
                                }
                                else if (!input_stream.Substring(input_stream.FirstIndexChar('-') + 1).Contains(' '))
                                {
                                    Text.WriteFore(Color.SpringGreen, key.ToString());
                                }
                                else
                                {
                                    if (key == '\\')
                                    {
                                        Text.WriteFore(Color.Orange, key.ToString());
                                    }
                                    else if (key == '=')
                                    {
                                        Text.WriteFore(Color.Gray, key.ToString());
                                    }
                                    else if (key == ':')
                                    {
                                        Text.WriteFore(Color.DeepSkyBlue, key.ToString());
                                    }
                                    else if (key == '.')
                                    {
                                        Text.WriteFore(Color.HotPink, key.ToString());
                                    }
                                    else if (key.ToString() == @"""")
                                    {
                                        Text.WriteFore(Color.Orange, key.ToString());
                                    }
                                    else if (char.IsDigit(key))
                                    {
                                        Text.WriteFore(Color.Tan, key.ToString().Bold());
                                    }
                                    else
                                    {
                                        Text.WriteFore(Color.White, key.ToString());
                                    }
                                }
                            }
                            else if (password is false && command is true && !input_stream.Contains(" "))
                            {
                                Text.WriteFore(Color.Yellow, key.ToString());
                            }
                            else
                            {
                                if (key == '\\')
                                {
                                    Text.WriteFore(Color.Orange, key.ToString());
                                }
                                else if (key == '=')
                                {
                                    Text.WriteFore(Color.Gray, key.ToString());
                                }
                                else if (key == ':')
                                {
                                    Text.WriteFore(Color.DeepSkyBlue, key.ToString());
                                }
                                else if (key == '.')
                                {
                                    Text.WriteFore(Color.HotPink, key.ToString());
                                }
                                else if (key.ToString() == @"""")
                                {
                                    Text.WriteFore(Color.Orange, key.ToString());
                                }
                                else if (char.IsDigit(key))
                                {
                                    Text.WriteFore(Color.Tan, key.ToString().Bold());
                                }
                                else
                                {
                                    Text.WriteFore(Color.White, key.ToString());
                                }
                            }
                        }

                    }
                }
            }
        }
        private static bool CanAccessFolder(string folderPath)
        {

            try
            {
                string testFilePath = Path.Combine(folderPath, Path.GetRandomFileName());
                using (FileStream fs = File.Create(testFilePath, 1, FileOptions.DeleteOnClose))
                {
                }
                var files = Directory.GetFiles(folderPath);
                DirectorySecurity security = Directory.GetAccessControl(folderPath);
                AuthorizationRuleCollection rules = security.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));

                foreach (FileSystemAccessRule rule in rules)
                {
                    if ((rule.FileSystemRights & FileSystemRights.WriteData) != 0)
                    {
                        return true;
                    }
                }
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
        public static bool IsStringNumeric(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static class solver
        {
            public static void runed()
            {
                /* C# .txt {RGB,COLOR} to Console.Write */
                string txt = @"C:\Users\suley\OneDrive\Masaüstü\termx\created.txt";
                RenderTXT(txt);
            }
            private static void RenderTXT(string path)
            {
                int row = FileIO.LineCount(path);
                for (int i = 4; i < row; i++)
                {
                    string lines = FileIO.GetLine(path, i);
                    int x = GetX(lines);
                    int y = GetY(lines);
                    string rgb = GetRGB(lines);
                    char c = GetChar(lines);
                    Text.SetWriteFore(rgb, c.ToString(), x, y);
                }
            }
            private static string GetRGB(string txt)
            {
                string rgb = " ";
                int space = 0;
                for (int i = 0; i < txt.Length; i++)
                {
                    if (txt[i] == ' ') space++;

                    if (space == 6 && txt[i] != ' ')
                    {
                        rgb += txt[i];
                    }
                }
                rgb = Text.Replace(rgb, "rgb", "");
                rgb = Text.Replace(rgb, "=", "");
                rgb = rgb.Trim();
                return rgb;
            }
            private static char GetChar(string txt)
            {
                string real_char = " ";
                int space = 0;
                for (int i = 0; i < txt.Length; i++)
                {
                    if (txt[i] == ' ') space++;

                    if (space == 4 && txt[i] != ' ')
                    {
                        real_char += txt[i];
                    }
                }
                real_char = Text.Replace(real_char, "char", "");
                real_char = Text.Replace(real_char, "=", "");
                return real_char[1];

            }
            private static int GetX(string txt)
            {
                string real_x = "";
                for (int i = 0; i < txt.Length; i++)
                {
                    if (txt[i] == ' ') break;
                    else real_x += txt[i];
                }
                real_x = Text.Replace(real_x, "=", "");
                real_x = Text.Replace(real_x, "x", "");
                return int.Parse(real_x);
            }
            private static int GetY(string txt)
            {
                string real_y = "";
                int space = 0;
                for (int i = 0; i < txt.Length; i++)
                {
                    if (txt[i] == ' ') space++;

                    if (space == 2 && txt[i] != ' ')
                    {
                        real_y += txt[i];
                    }
                    else if (space == 3) break;
                }
                real_y = Text.Replace(real_y, "=", "");
                real_y = Text.Replace(real_y, "y", "");
                return int.Parse(real_y);
            }

        }
        public static void Command()
        {
            while (true)
            {
                " ~ ".WriteFore(Color.DodgerBlue);
                Console.Write("[".MakeColored(Color.LightSalmon));
                Console.Write($"Runing on : {Environment.MachineName.Bold()}{" as ".MakeColored(Color.SpringGreen)}{Environment.UserName.Bold()}");
                Console.WriteLine("]".MakeColored(Color.LightSalmon));
                " ┌[".WriteFore(Color.DodgerBlue);
                if (CanAccessFolder(CD.CD_DIR))
                {
                    "Accessible".Italic().MakeColored(Color.LawnGreen).Write();
                }
                else
                {
                    "Inaccessible".Italic().Strikethrough().MakeColored(Color.Red).Write();
                }
                "]─[".WriteFore(Color.DodgerBlue);

                if (CD.ADMIN.Contains("Admin"))
                {

                    "root".WriteFore(Color.YellowGreen);
                }
                else
                {
                    "user".WriteFore(Color.PaleVioletRed);
                }
                if (ip != null)
                {
                    "]─[".WriteFore(Color.DodgerBlue);
                    "IP: ".Write();
                    ip.Trim().WriteFore(Color.DeepPink);
                }
                "]─[".WriteFore(Color.DodgerBlue);
                for (int i = 0; i < CD.CD_DIR.Length; i++)
                {
                    if (CD.CD_DIR[i] == '\\')
                    {
                        "\\".WriteFore(Color.White);
                    }
                    else CD.CD_DIR[i].WriteFore(Color.DeepSkyBlue);
                }
                $"{"]".MakeColored(Color.DodgerBlue)}".WriteLine();
                " │".WriteLineFore(Color.DodgerBlue);
                " └[".WriteFore(Color.DodgerBlue);
                " > ".WriteFore(Color.Gray);
                string ans = input("", Color.Yellow, false, true);
                Text.NewLine(2);
                if (SolveCommand(ans) == "clear")
                {
                    Console.Clear();
                }
                else if (ans.StartsWith("echo"))
                {
                    Console.WriteLine(SolvePath(ans));
                }
                else if(ans.StartsWith("list"))
                {
                    try
                    {
                        Regex regex = new Regex(@"list (\w+) where (\w+)\.(\w+)\(""(.*?)""\)");
                        Match match = regex.Match(ans);
                        if (match.Success)
                        {
                            List<string> list = new List<string>();
                            foreach(var mat in match.Groups)
                            {
                                list.Add(mat.ToString());
                            }
                            //list files where files.contains(".cs")
                            string[] comP = {"contains","end","start"};
                            if (list[0] == "dir" || list[0] == "files" && list[0] == list[1] && (comP.Contains(list[2])))
                            {
                                string[] handleFil = null;
                                if (list[0] == "dir") handleFil = Directory.GetDirectories(CD.CD_DIR);
                                else handleFil = Directory.GetFiles(CD.CD_DIR);

                                if (list[2] == "contains")
                                {
                                    for (int i = 0; i < handleFil.Length; i++)
                                    {
                                        if (handleFil[i].Contains(list[3]))
                                        {
                                            Path.GetFileName(handleFil[i]).WriteLine();
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            Text.NewLine();
                            Console.CursorLeft += 10;
                            "~~>".WriteFore("153,153,153");
                            " + ".WriteFore(Color.Yellow);
                            $"'{ans}'".Write();
                            Console.WriteLine();
                            for (int i = 0; i < 3; i++)
                            {
                                Console.CursorLeft += 10;
                                "~".WriteLineFore("153,153,153");
                            }
                            Console.CursorLeft += 10;
                            for (int i = 0; i < ans.Length + 3; i++)
                            {
                                "~".WriteFore("153,153,153");
                            }
                            "> ".WriteFore("153,153,153");
                            "You may be using the command incorrectly".WriteFore(Color.Yellow);
                            Text.NewLine(2);
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }

                }
                else if(ans.StartsWith("read find") && ans != "read find" && ans.Contains(":"))
                {
                    string[] text = null;
                    try
                    {
                        string solvedPath = SolvePath(ans).Substring(SolvePath(ans).allindex(' ')[0] + 1);
                        string fullPath = solvedPath.Substring(0, solvedPath.FirstIndexChar(':'));
                        string word = solvedPath.Substring(solvedPath.FirstIndexChar(':') + 1);
                        if (fullPath.Contains("\\") == false) fullPath = CD.CD_DIR + "\\" + fullPath;
                        if (File.Exists(fullPath))
                        {
                            text = File.ReadAllLines(fullPath);
                            for(int i = 0;i < text.Length;i++)
                            {
                                if (text[i].Contains(word))
                                {
                                    text[i] = text[i].Replace(word, word.MakeColored(Color.SeaGreen));
                                }
                                Console.WriteLine(text[i]);
                            }
                        }
                        else
                        {
                            Text.NewLine();
                            Console.CursorLeft += 10;
                            "~~>".WriteFore("153,153,153");
                            " + ".WriteFore(Color.Red);
                            $"'{ans}'".Write();
                            Console.WriteLine();
                            for (int i = 0; i < 3; i++)
                            {
                                Console.CursorLeft += 10;
                                "~".WriteLineFore("153,153,153");
                            }
                            Console.CursorLeft += 10;
                            for (int i = 0; i < ans.Length + 3; i++)
                            {
                                "~".WriteFore("153,153,153");
                            }
                            "> ".WriteFore("153,153,153");
                            $"File not found: {fullPath}".WriteFore(Color.Red);
                            Text.NewLine(2);
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }

                }
                else if (SolveCommand(ans) == "gd")
                {
                    if (ans.Contains("$des"))
                    {
                        CD.CD_DIR = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
                    }
                    else if (ans.Contains("$tools"))
                    {
                        CD.CD_DIR = @"C:\Users\" + Environment.UserName + @"\" + "termXtools";
                    }
                    else if (IsStringNumeric(SolvePath(ans.TrimStart())))
                    {
                        try
                        {
                            string[] dirs = Directory.GetDirectories(CD.CD_DIR);
                            CD.CD_DIR = dirs[int.Parse(SolvePath(ans.trimstart()))];
                        }
                        catch (Exception e)
                        {
                            Text.NewLine();
                            Console.CursorLeft += 10;
                            "~~>".WriteFore("153,153,153");
                            " + ".WriteFore(Color.Red);
                            $"'{ans}'".Write();
                            Console.WriteLine();
                            for (int i = 0; i < 3; i++)
                            {
                                Console.CursorLeft += 10;
                                "~".WriteLineFore("153,153,153");
                            }
                            Console.CursorLeft += 10;
                            for (int i = 0; i < ans.Length + 3; i++)
                            {
                                "~".WriteFore("153,153,153");
                            }
                            "> ".WriteFore("153,153,153");
                            $"{e.Message}".WriteFore(Color.Red);
                            Text.NewLine(2);
                        }
                    }
                    else if (ans.Contains("$home"))
                    {
                        CD.CD_DIR = @"C:\Users\" + Environment.UserName;
                    }
                    else if (ans == (@"gd .\topsecret"))
                    {
                        string a = input("\n", Color.LemonChiffon, true, false);
                        if (a == "2.3Aslan" || a == "")
                        {
                            Console.Clear();
                            "[termX]".Blink().Write();
                            "MrRobot".WriteLineFore(Color.Sienna);
                            Text.NewLine();
                            string[] links = { "https://www.dizibox.in/mr-robot-1-sezon-1-bolum-hd-1-izle/", "https://www.dizibox.in/mr-robot-1-sezon-2-bolum-izle/", "https://www.dizibox.in/mr-robot-1-sezon-3-bolum-izle/", "Five Nights at Freddy's Soundtrack - Music Box (Freddy's Music)" };
                            string[] names = { "HELLOFRIEND.MOV 28 May 15", "ones-and-zer0es.mpeg 2 Tem 15", "eps.1.2_d3bug.mkv 9 Tem 15", "8 Agu 14" };

                            for (int i = 0; i < links.Length; i++)
                            {
                                " # ".WriteFore(Color.Gray);
                                links[i].WriteFore(Color.White);
                                names[i].MarginLeft(80 - links[i].Length).WriteFore(Color.Chocolate);
                                Console.WriteLine();
                            }
                            int hnd = 0;
                            Console.SetCursorPosition(3 + links[0].Length + (60 - links[0].Length) + names[0].Length, 0);
                            Console.CursorVisible = false;
                            solver.runed();
                            while (true)
                            {
                                ConsoleKeyInfo key = Console.ReadKey(true);
                                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.Enter)
                                {
                                    if (key.Key == ConsoleKey.UpArrow)
                                    {
                                        if (hnd > 0)
                                        {
                                            hnd--;
                                        }
                                    }
                                    else if (key.Key == ConsoleKey.DownArrow)
                                    {
                                        if (hnd < links.Length - 1)
                                        {
                                            hnd++;
                                        }
                                    }
                                    else if (key.Key == ConsoleKey.Enter)
                                    {
                                        if (hnd != 3)
                                        {
                                            Process.Start(links[hnd]);
                                        }
                                        else
                                        {
                                            Thread musicThread = new Thread(() => PlayMp3File(@"C:\Users\suley\OneDrive\Masaüstü\termx\music\fivenightsatfreddyssoundtrackmusicboxfreddysmusic.mp3"));
                                            musicThread.IsBackground = true;
                                            musicThread.Start();
                                        }
                                    }
                                    Console.Clear();
                                    "0 | [termX]".Blink().WriteFore(Color.Snow);
                                    "MrRobot".WriteLineFore(Color.Sienna);
                                    solver.runed();
                                    Text.NewLine();
                                    Console.SetCursorPosition(0, 2);
                                    for (int i = 0; i < links.Length; i++)
                                    {
                                        if (hnd != i)
                                        {
                                            " # ".WriteFore(Color.Gray);
                                            links[i].WriteFore(Color.White);
                                            names[i].MarginLeft(80 - links[i].Length).WriteFore(Color.Chocolate);
                                        }
                                        else
                                        {
                                            " # ".WriteFore(Color.Gray);
                                            links[i].WriteFore(Color.White);
                                            names[i].MarginLeft(80 - links[i].Length).WriteFore(Color.Chocolate);
                                            " ← ".WriteFore(Color.OrangeRed);
                                            Console.WriteLine();
                                            int text = links[hnd].Length + (80 - links[hnd].Length) + names[hnd].Length;

                                            Console.WriteLine("   " + new string('~', text));
                                        }
                                        Console.WriteLine();
                                    }
                                }
                            }
                        }
                        Console.WriteLine();
                    }
                    else if (Directory.Exists(SolvePath(ans.TrimStart())))
                    {
                        CD.CD_DIR = SolvePath(ans.TrimStart());
                    }
                    else if (Directory.Exists(CD.CD_DIR + @"\" + SolvePath(ans.TrimStart())))
                    {
                        CD.CD_DIR = CD.CD_DIR + @"\" + SolvePath(ans.TrimStart());
                    }
                    else
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $" Path is not valid".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (SolveCommand(ans) == "find" && ans.contains(":") && ans != "find:")
                {
                    string[] text = null;
                    try
                    {
                        string solvedPath = SolvePath(ans).Substring(0, SolvePath(ans).FirstIndexChar(':'));
                        if (solvedPath.Contains("\\") == false) solvedPath = CD.CD_DIR + "\\" + solvedPath;
                        string fullPath = solvedPath;

                        if (File.Exists(fullPath))
                        {
                            text = File.ReadAllLines(fullPath);
                            Text.NewLine();
                            string que = ans.Substring(ans.FirstIndexChar(':') + 1);
                            for (int i = 0; i < text.Length; i++)
                            {
                                if (text[i].Contains(que))
                                {
                                    Console.Write("Found : ".MakeColored(Color.LawnGreen));
                                    Console.WriteLine("on line : " + i + " start index : " + text[i].IndexOf(que));
                                }
                            }
                            Text.NewLine();
                        }
                        else
                        {
                            Text.NewLine();
                            Console.CursorLeft += 10;
                            "~~>".WriteFore("153,153,153");
                            " + ".WriteFore(Color.Red);
                            $"'{ans}'".Write();
                            Console.WriteLine();
                            for (int i = 0; i < 3; i++)
                            {
                                Console.CursorLeft += 10;
                                "~".WriteLineFore("153,153,153");
                            }
                            Console.CursorLeft += 10;
                            for (int i = 0; i < ans.Length + 3; i++)
                            {
                                "~".WriteFore("153,153,153");
                            }
                            "> ".WriteFore("153,153,153");
                            $"File not found: {fullPath}".WriteFore(Color.Red);
                            Text.NewLine(2);
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans == "files")
                {
                    Console.WriteLine();
                    string[] filenames = Directory.GetFiles(CD.CD_DIR);
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        string filename = filenames[i];
                        $"{i}| ".Italic().WriteFore(Color.LightSlateGray);
                        filename = filename.Substring(filename.LastIndexOf(@"\") + 1);
                        filename.MarginRights(filename.Length + 5).WriteFore(Color.Yellow);
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                else if (ans.StartsWith("cp") && ans != "cp")
                {
                    try
                    {
                        string[] files = Directory.GetFiles(CD.CD_DIR);
                        string[] dirs = Directory.GetDirectories(CD.CD_DIR);
                        if (ans == "cp -cd")
                        {
                            Clipboard.SetText(CD.CD_DIR);
                        }
                        else if (ans.Contains("-f"))
                        {
                            Clipboard.SetText(files[int.Parse(ans.Substring(ans.FirstIndexChar('f') + 1))]);
                        }
                        else if (ans.Contains("-d"))
                        {
                            Clipboard.SetText(dirs[int.Parse(ans.Substring(ans.FirstIndexChar('d') + 1))]);
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans == "dir")
                {
                    Console.WriteLine();
                    string[] filenames = Directory.GetDirectories(CD.CD_DIR);
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        string filename = filenames[i];
                        $"{i}| ".Italic().WriteFore(Color.LightSlateGray);
                        filename.Substring(filename.LastIndexOf(@"\") + 1).WriteFore(Color.Yellow);

                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                else if (ans == "-l" && CD.CD_DIR.ToLower() != "c:")
                {
                    if (Directory.Exists(CD.CD_DIR.Substring(0, CD.CD_DIR.LastIndexOf(@"\"))))
                    {
                        CD.OLD_CDR = CD.CD_DIR;
                        CD.CD_DIR = CD.CD_DIR.Substring(0, CD.CD_DIR.LastIndexOf(@"\"));
                    }
                }
                else if (ans == "-r")
                {
                    if (CD.OLD_CDR != null) CD.CD_DIR = CD.OLD_CDR;
                }
                else if (SolveCommand(ans) == "read" && ans != "read")
                {
                    string filename = null;
                    string[] text = null;
                    try
                    {
                        string solvedPath = SolvePath(ans);
                        if (solvedPath.Contains("\\") == false) solvedPath = CD.CD_DIR + "\\" + solvedPath;
                        string fullPath = solvedPath;

                        if (File.Exists(fullPath))
                        {
                            text = File.ReadAllLines(fullPath);
                            filename = Path.GetFileName(fullPath);
                            Text.NewLine();
                            Console.CursorLeft = (Console.WindowWidth / 2) - filename.Length;
                            $" 📄 File : {filename} ".Underline().Bold().WriteLineFore(Color.LightSlateGray);
                            Text.NewLine();
                            if (fullPath.EndsWith(".cs"))
                            {
                                for (int i = 0; i < text.Length; i++)
                                {
                                    if (text[i].Contains("if"))
                                    {
                                        text[i] = text[i].Replace("if", "if".MakeColored("177, 125, 184"));
                                    }
                                    if (text[i].Contains("else"))
                                    {
                                        text[i] = text[i].Replace("else", "else".MakeColored("177, 125, 184"));
                                    }
                                    if (text[i].Contains("else if"))
                                    {
                                        text[i] = text[i].Replace("else if", "else if".MakeColored("177, 125, 184"));
                                    }
                                    if (text[i].Contains("while"))
                                    {
                                        text[i] = text[i].Replace("while", "while".MakeColored("177, 125, 184"));
                                    }
                                    if (text[i].Contains("for"))
                                    {
                                        text[i] = text[i].Replace("for", "for".MakeColored("177, 125, 184"));
                                    }
                                    if (text[i].Contains("foreach"))
                                    {
                                        text[i] = text[i].Replace("foreach", "foreach".MakeColored("177, 125, 184"));
                                    }

                                    if (text[i].Contains("using"))
                                    {
                                        text[i] = text[i].Replace("using", "using".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("static"))
                                    {
                                        text[i] = text[i].Replace("static", "static".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("void"))
                                    {
                                        text[i] = text[i].Replace("void", "void".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("public"))
                                    {
                                        text[i] = text[i].Replace("public", "public".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("private"))
                                    {
                                        text[i] = text[i].Replace("private", "private".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("private"))
                                    {
                                        text[i] = text[i].Replace("private", "private".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("int"))
                                    {
                                        text[i] = text[i].Replace("int", "int".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("string"))
                                    {
                                        text[i] = text[i].Replace("string", "string".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("bool"))
                                    {
                                        text[i] = text[i].Replace("bool", "bool".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("class"))
                                    {
                                        text[i] = text[i].Replace("class", "class".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains(@""""))
                                    {
                                        text[i] = text[i].Replace(@"""", @"""".MakeColored("214, 157, 133"));
                                    }
                                    if (text[i].Contains("$"))
                                    {
                                        text[i] = text[i].Replace("$", "$".MakeColored("214, 157, 133"));
                                    }
                                    if (text[i].Contains("@"))
                                    {
                                        text[i] = text[i].Replace("@", "@".MakeColored("214, 157, 133"));
                                    }
                                    if (text[i].Contains("namespace"))
                                    {
                                        text[i] = text[i].Replace("namespace", "namespace".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("true"))
                                    {
                                        text[i] = text[i].Replace("true", "true".MakeColored("56, 133, 210"));
                                    }
                                    if (text[i].Contains("false"))
                                    {
                                        text[i] = text[i].Replace("false", "false".MakeColored("56, 133, 210"));
                                    }

                                    if (text[i].Contains(")"))
                                    {
                                        text[i] = text[i].Replace(")", ")".MakeColored(Color.Gray));
                                    }
                                    if (text[i].Contains("("))
                                    {
                                        text[i] = text[i].Replace("(", "(".MakeColored(Color.Gray));
                                    }
                                    string pattern = "\"([^\"]*)\"";
                                    string lineCommentPattern = @"\/\/.*";
                                    string blockCommentPattern = @"\/\*.*?\*\/";

                                    for (int j = 0; j < text.Length; j++)
                                    {
                                        if (text[j].Contains(@"""")) text[j] = Regex.Replace(text[j], pattern, match => $@"""{match.Groups[1].Value.AddRight(@"""").MakeColored("214, 157, 133")}");
                                        if (text[j].Contains(@"""")) text[j] = Regex.Replace(text[j], pattern, match => match.Value.MakeColored("214, 157, 133"));
                                        if (text[j].Contains(@"//")) text[j] = Regex.Replace(text[j], lineCommentPattern, match => match.Value.MakeColored(Color.ForestGreen));
                                        if (text[j].Contains(@"*/") || text[j].Contains(@"/*")) text[j] = Regex.Replace(text[j], blockCommentPattern, match => match.Value.MakeColored(Color.ForestGreen));

                                    }
                                }
                            }
                            if (fullPath.EndsWith(".tdb"))
                            {
                                for (int i = 0; i < text.Length; i++)
                                {
                                    if (text[i].Contains("DataGroup ::"))
                                    {
                                        string s = text[i];
                                        string name = null;
                                        int space = 0;
                                        for (int j = 0; j < s.Length; j++)
                                        {
                                            if (s[j] == ' ')
                                            {
                                                space++;
                                            }
                                            if (s[j] == '[') break;
                                            if (space == 2)
                                            {
                                                name += s[j];
                                            }
                                        }
                                        s = s.Replace(name, name.MakeColored("78,201,176"));
                                        s = s.Replace("DataGroup", "DataGroup".MakeColored("19,131,248"));
                                        s = s.Replace("::", "::".MakeColored(Color.SlateGray));
                                        s = s.Replace("out", "out".MakeColored("19,131,248"));
                                        s = s.Replace("true", "true".MakeColored("19,131,248"));
                                        s = s.Replace("false", "false".MakeColored("19,131,248"));
                                        s = s.Replace("=>", "=>".MakeColored(Color.SlateGray));
                                        s = s.Replace("lock", "lock".MakeColored("132, 220, 254"));
                                        s = s.Replace("String", "String".MakeColored("220, 219, 153"));
                                        s = s.Replace("Bool", "Bool".MakeColored("220, 219, 153"));
                                        s = s.Replace("Int", "Int".MakeColored("220, 219, 153"));
                                        text[i] = s;
                                    }
                                    else if (text[i].StartsWith("++"))
                                    {
                                        text[i] = text[i].Replace(text[i], text[i].MakeColored("87, 166, 74"));
                                    }
                                    else
                                    {
                                        text[i] = text[i].Replace("'", "'".MakeColored(Color.SlateGray));
                                        text[i] = text[i].Replace(":", ":".MakeColored(Color.SlateGray));
                                    }
                                }
                            }
                            for (int i = 0; i < text.Length; i++)
                            {
                                $"{i}| ".Italic().WriteFore(Color.LightSlateGray);
                                text[i].WriteLine();
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Text.NewLine();
                            Console.CursorLeft += 10;
                            "~~>".WriteFore("153,153,153");
                            " + ".WriteFore(Color.Red);
                            $"'{ans}'".Write();
                            Console.WriteLine();
                            for (int i = 0; i < 3; i++)
                            {
                                Console.CursorLeft += 10;
                                "~".WriteLineFore("153,153,153");
                            }
                            Console.CursorLeft += 10;
                            for (int i = 0; i < ans.Length + 3; i++)
                            {
                                "~".WriteFore("153,153,153");
                            }
                            "> ".WriteFore("153,153,153");
                            $"File not found: {fullPath}".WriteFore(Color.Red);
                            Text.NewLine(2);
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans == "whereami")
                {
                    Console.Write("You are in ");
                    Console.Write(CD.CD_DIR.MakeColored(Color.Yellow));
                    Console.Write(" as ");
                    Console.WriteLine(Environment.UserName.MakeColored(Color.Yellow));
                }
                else if (ans != "cf" && ans.StartsWith("cf"))
                {
                    try
                    {
                        using (FileStream fs = File.Create(CD.CD_DIR + @"\" + SolvePath(ans)))
                        {
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }

                }
                else if (SolveCommand(ans) == "cd")
                {
                    try
                    {
                        Directory.CreateDirectory(CD.CD_DIR + @"\" + SolvePath(ans));
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }

                }
                else if (SolveCommand(ans) == "zip")
                {
                    try
                    {
                        string a = SolvePath(ans);
                        string zipname = a.Substring(a.LastIndexOf(':') + 1);
                        string path = $@"{CD.CD_DIR}\{zipname}.zip";
                        string[] zips = a.Substring(0, a.LastIndexOf(':')).Split(':');
                        using (FileStream zipToOpen = new FileStream(path, FileMode.Create))
                        {
                            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                            {
                                foreach (string zip in zips)
                                {
                                    AddDirectoryToZip(archive, CD.CD_DIR + @"\" + zip, Path.GetFileName(CD.CD_DIR + @"\" + zip));
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans.StartsWith("rename"))
                {
                    try
                    {
                        string p = ans.Substring(ans.FirstIndexChar(' ') + 1);
                        string[] names = p.Split(':');
                        if (File.Exists(CD.CD_DIR + @"\" + names[0]))
                        {
                            File.Move(CD.CD_DIR + @"\" + names[0], CD.CD_DIR + @"\" + names[1]);
                            FileInfo file = new FileInfo(CD.CD_DIR + @"\" + names[1]);
                            file.Refresh();
                        }
                        else
                        {
                            if (Directory.Exists(CD.CD_DIR + @"\" + names[0]))
                            {
                                Directory.Move(CD.CD_DIR + @"\" + names[0], CD.CD_DIR + @"\" + names[1]);
                                DirectoryInfo file = new DirectoryInfo(CD.CD_DIR + @"\" + names[1]);
                                file.Refresh();
                            }
                            else
                            {
                                Text.NewLine();
                                Console.CursorLeft += 10;
                                "~~>".WriteFore("153,153,153");
                                " + ".WriteFore(Color.Red);
                                $"'{ans}'".Write();
                                Console.WriteLine();
                                for (int i = 0; i < 3; i++)
                                {
                                    Console.CursorLeft += 10;
                                    "~".WriteLineFore("153,153,153");
                                }
                                Console.CursorLeft += 10;
                                for (int i = 0; i < ans.Length + 3; i++)
                                {
                                    "~".WriteFore("153,153,153");
                                }
                                "> ".WriteFore("153,153,153");
                                $" Path is not valid".WriteFore(Color.Red);
                                Text.NewLine(2);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans.StartsWith("cmd"))
                {
                    try
                    {
                        Process cmdProcess = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = $"/c {SolvePath(ans)}";
                        startInfo.RedirectStandardOutput = true;
                        startInfo.UseShellExecute = false;
                        startInfo.CreateNoWindow = true;
                        cmdProcess.StartInfo = startInfo;
                        cmdProcess.Start();
                        string output = cmdProcess.StandardOutput.ReadToEnd();
                        Console.WriteLine(output);
                        cmdProcess.WaitForExit();
                        cmdProcess.Close();
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans.StartsWith("del"))
                {
                    try
                    {
                        string p = ans.Substring(ans.FirstIndexChar(' ') + 1);
                        if (File.Exists(CD.CD_DIR + @"\" + p))
                        {
                            File.Delete(CD.CD_DIR + @"\" + p);
                            FileInfo file = new FileInfo(CD.CD_DIR + @"\" + p);
                            file.Refresh();
                        }
                        else
                        {
                            if (Directory.Exists(CD.CD_DIR + @"\" + p))
                            {
                                Directory.Delete((CD.CD_DIR + @"\" + p), true);
                                DirectoryInfo file = new DirectoryInfo(CD.CD_DIR + @"\" + p);
                                file.Refresh();
                            }
                            else
                            {
                                Text.NewLine();
                                Console.CursorLeft += 10;
                                "~~>".WriteFore("153,153,153");
                                " + ".WriteFore(Color.Red);
                                $"'{ans}'".Write();
                                Console.WriteLine();
                                for (int i = 0; i < 3; i++)
                                {
                                    Console.CursorLeft += 10;
                                    "~".WriteLineFore("153,153,153");
                                }
                                Console.CursorLeft += 10;
                                for (int i = 0; i < ans.Length + 3; i++)
                                {
                                    "~".WriteFore("153,153,153");
                                }
                                "> ".WriteFore("153,153,153");
                                $" Path is not valid".WriteFore(Color.Red);
                                Text.NewLine(2);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }

                }
                else if (ans.StartsWith("det"))
                {
                    try
                    {
                        string p = ans.Substring(ans.FirstIndexChar(' ') + 1);

                        if (p.Contains("\\"))
                        {
                            if (File.Exists(p))
                            {
                                FileInfo file = new FileInfo(p);
                                Text.NewLine();
                                "[FILE]".MakeColored(Color.OliveDrab).WriteLine();
                                $"File name : {p.MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File extension : {file.Extension.MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File creation time : {file.CreationTime.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File last access time : {file.LastAccessTime.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File size : {file.Length.MakeString().AddRight(" byte").MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File is read only : {file.IsReadOnly.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File hash code : {file.GetHashCode().MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                Text.NewLine();
                            }
                            else
                            {
                                if (Directory.Exists(p))
                                {
                                    DirectoryInfo file = new DirectoryInfo(p);
                                    long size = 0;
                                    foreach (var item in Directory.GetFiles(p))
                                    {
                                        FileInfo ss = new FileInfo(item);
                                        size += ss.Length;
                                    }
                                    Text.NewLine();
                                    "[DIR]".MakeColored(Color.OliveDrab).WriteLine();
                                    $"Directory name : {p.MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory size : {size.MakeString().AddRight(" byte").MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory creation time : {file.CreationTime}".WriteLine();
                                    $"Directory last access time : {file.LastAccessTime.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory parent : {file.Parent.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory path : {file.FullName.MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory root : {file.Root.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory hash code : {file.GetHashCode().MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                    Text.NewLine();
                                }
                                else
                                {
                                    Text.NewLine();
                                    Console.CursorLeft += 10;
                                    "~~>".WriteFore("153,153,153");
                                    " + ".WriteFore(Color.Red);
                                    $"'{ans}'".Write();
                                    Console.WriteLine();
                                    for (int i = 0; i < 3; i++)
                                    {
                                        Console.CursorLeft += 10;
                                        "~".WriteLineFore("153,153,153");
                                    }
                                    Console.CursorLeft += 10;
                                    for (int i = 0; i < ans.Length + 3; i++)
                                    {
                                        "~".WriteFore("153,153,153");
                                    }
                                    "> ".WriteFore("153,153,153");
                                    $" Path is not valid".WriteFore(Color.Red);
                                    Text.NewLine(2);
                                }
                            }
                        }
                        else
                        {
                            if (File.Exists(CD.CD_DIR + @"\" + p))
                            {
                                FileInfo file = new FileInfo(CD.CD_DIR + @"\" + p);
                                Text.NewLine();
                                "[FILE]".MakeColored(Color.OliveDrab).WriteLine();
                                $"File name : {p.MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File extension : {file.Extension.MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File creation time : {file.CreationTime.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File last access time : {file.LastAccessTime.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File size : {file.Length.MakeString().AddRight(" byte").MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File is read only : {file.IsReadOnly.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                $"File hash code : {file.GetHashCode().MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                Text.NewLine();
                            }
                            else
                            {
                                if (Directory.Exists(CD.CD_DIR + @"\" + p))
                                {
                                    DirectoryInfo file = new DirectoryInfo(CD.CD_DIR + @"\" + p);
                                    long size = 0;
                                    foreach (var item in Directory.GetFiles(CD.CD_DIR + @"\" + p))
                                    {
                                        FileInfo ss = new FileInfo(item);
                                        size += ss.Length;
                                    }
                                    Text.NewLine();
                                    "[DIR]".MakeColored(Color.OliveDrab).WriteLine();
                                    $"Directory name : {p.MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory size : {size.MakeString().AddRight(" byte").MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory creation time : {file.CreationTime}".WriteLine();
                                    $"Directory last access time : {file.LastAccessTime.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory parent : {file.Parent.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory path : {file.FullName.MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory root : {file.Root.MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                    $"Directory hash code : {file.GetHashCode().MakeString().MakeColored(Color.DarkOrange)}".WriteLine();
                                    Text.NewLine();
                                }
                                else
                                {
                                    Text.NewLine();
                                    Console.CursorLeft += 10;
                                    "~~>".WriteFore("153,153,153");
                                    " + ".WriteFore(Color.Red);
                                    $"'{ans}'".Write();
                                    Console.WriteLine();
                                    for (int i = 0; i < 3; i++)
                                    {
                                        Console.CursorLeft += 10;
                                        "~".WriteLineFore("153,153,153");
                                    }
                                    Console.CursorLeft += 10;
                                    for (int i = 0; i < ans.Length + 3; i++)
                                    {
                                        "~".WriteFore("153,153,153");
                                    }
                                    "> ".WriteFore("153,153,153");
                                    $" Path is not valid".WriteFore(Color.Red);
                                    Text.NewLine(2);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }

                }
                else if (ans == "main")
                {
                    Console.Clear();
                    "termX[Version 3.0]".WriteLine();
                    Text.NewLine();
                    "$ ".WriteFore(Color.Yellow);
                    "Welcome back to termX".WriteLine();
                    "$ ".WriteFore(Color.Yellow);
                    "For current and secure versions, visit ".Write();
                    "https://ccprq.github.io".WriteLineFore(Color.Yellow);
                    Text.NewLine();
                }
                else if (ans == "exit")
                {
                    Environment.Exit(0);
                }
                else if (ans == "hideip")
                {
                    ip = null;
                }
                else if (ans.StartsWith("edit"))
                {
                    try
                    {
                        if (ans.Contains("-a"))
                        {
                            string addres = ans.Substring(ans.IndexOf('a') + 2);
                            string path = addres.Split(':')[0];
                            if (!path.Contains("\\"))
                                path = CD.CD_DIR + "\\" + path;
                            string text = addres.Split(':')[1].Trim();

                            // edit -a test.txt:hello world
                            if (File.Exists(path))
                            {
                                using (StreamWriter writer = new StreamWriter(path, true))
                                {
                                    writer.WriteLine(text);
                                }
                            }
                            else
                            {
                                Text.NewLine();
                                Console.CursorLeft += 10;
                                "~~>".WriteFore("153,153,153");
                                " + ".WriteFore(Color.Red);
                                $"'{ans}'".Write();
                                Console.WriteLine();
                                for (int i = 0; i < 3; i++)
                                {
                                    Console.CursorLeft += 10;
                                    "~".WriteLineFore("153,153,153");
                                }
                                Console.CursorLeft += 10;
                                for (int i = 0; i < ans.Length + 3; i++)
                                {
                                    "~".WriteFore("153,153,153");
                                }
                                "> ".WriteFore("153,153,153");
                                $" Path is not valid".WriteFore(Color.Red);
                                Text.NewLine(2);
                            }
                        }
                        else if (ans.Contains("-s"))
                        {
                            string addres = ans.Substring(ans.IndexOf('s') + 1);
                            string path = addres.Split(':')[0].Trim();
                            string text = addres.Split(':')[1].Trim();

                            if (!path.Contains("\\"))
                                path = CD.CD_DIR + "\\" + path;

                            using (StreamWriter writer = new StreamWriter(path))
                            {
                                writer.WriteLine(text);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans == "showip") { ip = GetIp().GetAwaiter().GetResult().AddRight(" "); }
                else if (ans.StartsWith("ping -f") && ans.Contains("delay=") && ans.Contains("size="))
                {
                    try
                    {
                        string delays = "";
                        string sizes = "";
                        for (int i = ans.IndexOf("delay=") + "delay=".Length; i < ans.Length; i++)
                        {
                            if (ans[i] != ' ')
                            {
                                delays += ans[i];
                            }
                            else break;
                        }
                        for (int i = ans.IndexOf("size=") + "size=".Length; i < ans.Length; i++)
                        {
                            if (ans[i] != ':')
                            {
                                sizes += ans[i];
                            }
                            else break;
                        }
                        int delay = int.Parse(delays);
                        int size = int.Parse(sizes);
                        string host = ans.Substring(ans.IndexOf(':') + 1);
                        Text.NewLine();
                        for (int i = 0; i < size; i++)
                        {
                            var ping = new System.Net.NetworkInformation.Ping();
                            var reply = ping.Send(host);
                            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                            {
                                $"Ping to {host} successful. Roundtrip time: {reply.RoundtripTime} ms".WriteLineFore(Color.LightGreen);
                            }
                            else
                            {
                                $"Ping to {host} failed: {reply.Status}".WriteLineFore(Color.Red);
                                Text.NewLine();
                            }
                            Thread.Sleep(delay);
                        }
                        Text.NewLine();
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans.StartsWith("ping"))
                {
                    string host = SolvePath(ans);
                    var ping = new System.Net.NetworkInformation.Ping();
                    try
                    {
                        var reply = ping.Send(host);
                        if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                        {
                            Text.NewLine();
                            $"Ping to {host} successful. Roundtrip time: {reply.RoundtripTime} ms".WriteLineFore(Color.LightGreen);
                            Text.NewLine();
                        }
                        else
                        {
                            Text.NewLine();
                            $"Ping to {host} failed: {reply.Status}".WriteLineFore(Color.Red);
                            Text.NewLine();
                        }
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans == "termX")
                {
                    @"                                                       ███  
                                                   ▄██████████▀
                                                   ██▀▀███▀      
                                                   ▀   ███ 
                                                       ███     
                                                       ███     
                                                       ███     
                                                      ▄████▀   
                                                              
".WriteLineFore(Color.Yellow);
                    "~".WriteFore(Color.DodgerBlue);
                    " Story of the".Write();
                    " termX".WriteFore(Color.Orange);
                }
                else if (SolveCommand(ans) == "get")
                {
                    try
                    {
                        string a = get(SolvePath(ans)).GetAwaiter().GetResult();

                        a.WriteLine();
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                }
                else if (ans == "help")
                {
                    Text.NewLine();
                    string cmd = "Commands".Underline().MakeColored(Color.LightSlateGray);
                    string usage = "usage".Underline().MakeColored(Color.LightSlateGray);
                    string example = "Example".Underline().MakeColored(Color.LightSlateGray);
                    cmd.MarginRights(20 - "Commands".Length).AddRight(usage).MarginRights((40 - "Example".Length) + 12).AddRight(example).WriteLine();
                    for (int i = 0; i < usages.Length; i++)
                    {
                        commands[i].WriteFore(Color.Yellow);
                        usages[i].MarginLeft(20 - commands[i].Length).WriteFore(Color.White);
                        exp[i].MarginLeft((40 - usages[i].Length) + 10).WriteLineFore(Color.Yellow);
                    }
                    Text.NewLine();
                }
                else if (ans.StartsWith("translate"))
                {
                    string a = TransLate(ans.Substring("translate".Length + 1));
                    Text.NewLine();
                    "Translate : ".WriteFore(Color.LightSlateGray);
                    a.WriteLineFore(Color.Yellow);
                    Text.NewLine();
                }        
                else if (ans == "tree")
                {
                    Console.WriteLine();
                    Console.WriteLine(CD.CD_DIR.MakeColored(Color.Orange));
                    PrintDirectoryTree(CD.CD_DIR, "");
                    Console.WriteLine();
                }
                else if (ans == "ip")
                {
                    NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (NetworkInterface networkInterface in networkInterfaces)
                    {
                        Console.WriteLine("Network adapter ".MakeColored(Color.Gray) + networkInterface.Name.MakeColored(Color.Pink) + " :".MakeColored(Color.Gray));
                        Console.WriteLine();
                        IPInterfaceProperties properties = networkInterface.GetIPProperties();
                        Console.WriteLine("   Connection-specific DNS Suffix    ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + properties.DnsSuffix.MakeString().MakeColored(Color.SteelBlue));
                        Console.WriteLine("   Physical Address                  ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + networkInterface.GetPhysicalAddress().MakeString().MakeColored(Color.SteelBlue));
                        Console.WriteLine("   Operational Status                ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + networkInterface.OperationalStatus.MakeString().MakeColored(Color.SteelBlue));
                        Console.WriteLine("   Speed                             ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + networkInterface.Speed.MakeString().MakeColored(Color.SteelBlue));
                        foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                            {
                                if (ip.Address.IsIPv6LinkLocal)
                                {
                                    Console.WriteLine("   Link-local IPv6 Address           ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + ip.Address.MakeString().MakeColored(Color.SteelBlue));
                                }
                                else
                                {
                                    Console.WriteLine("   IPv6 Address                      ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + ip.Address.MakeString().MakeColored(Color.SteelBlue));
                                }
                            }
                            else if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                Console.WriteLine("   IPv4 Address                      ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + ip.Address.MakeString().MakeColored(Color.SteelBlue));
                                Console.WriteLine("   Subnet Mask                       ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + ip.IPv4Mask.MakeString().MakeColored(Color.SteelBlue));
                            }
                        }
                        foreach (GatewayIPAddressInformation gateway in properties.GatewayAddresses)
                        {
                            Console.WriteLine("   Default Gateway                   ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + gateway.Address.MakeString().MakeColored(Color.SteelBlue));
                        }
                        foreach (IPAddress dns in properties.DnsAddresses)
                        {
                            Console.WriteLine("   DNS Servers                       ".MakeColored(Color.PaleGoldenrod) + ": ".MakeColored(Color.Gray) + dns.MakeString().MakeColored(Color.SteelBlue));
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                else if (ans == "pkg -s")
                {
                    $"{"~".MakeColored(Color.DodgerBlue)} {"termX".MakeColored(Color.Orange)} {"package managament"}".WriteLine();
                    Text.NewLine();
                    Console.WriteLine("     Packages (Still on beta [Testing : 0.0])");
                    Text.NewLine();
                    {
                        "     Python".WriteLineFore(Color.Orange);
                        $"      {"---".MakeColored("87, 166, 74")}python {"#start".MakeColored(Color.Gray)} * {"Package source : "}{"https://www.python.org/downloads/".MakeColored("255, 223, 118")}".WriteLine();
                        $"      {"python ver".MakeColored("87, 166, 74")} {"3.12.4".MakeColored("87, 166, 200").Bold()} command : {"pkg -get py3.12.4".MakeColored("66, 133, 244")}".WriteLine();
                        $"      {"python ver".MakeColored("87, 166, 74")} {"3.12".MakeColored("87, 166, 200").Bold()} command : {"pkg -get py3.12".MakeColored("66, 133, 244")}".WriteLine();
                        $"      {"python ver".MakeColored("87, 166, 74")} {"3.11".MakeColored("87, 166, 200").Bold()} command : {"pkg -get py3.11".MakeColored("66, 133, 244")}".WriteLine();
                        $"      {"python ver".MakeColored("87, 166, 74")} {"3.10".MakeColored("87, 166, 200").Bold()} command : {"pkg -get py3.10".MakeColored("66, 133, 244")}".WriteLine();
                        $"      {"python ver".MakeColored("87, 166, 74")} {"3.9".MakeColored("87, 166, 200").Bold()} command : {"pkg -get py3.9".MakeColored("66, 133, 244")}".WriteLine();
                        $"      {"python ver".MakeColored("87, 166, 74")} {"3.8".MakeColored("87, 166, 200").Bold()} command : {"pkg -get py3.8".MakeColored("66, 133, 244")}".WriteLine();
                        $"      {"---".MakeColored("87, 166, 74")}python {"#end".MakeColored(Color.Gray)}".WriteLine();
                        Text.NewLine();
                        "     Firefox".WriteLineFore(Color.Orange);
                        $"      {"---".MakeColored("87, 166, 74")}firefox {"#start".MakeColored(Color.Gray)} * {"Package source : "}{"https://www.mozilla.org/firefox/download/".MakeColored("255, 223, 118")}".WriteLine();
                        $"      {"Firefox".MakeColored("87, 166, 74")} command : {"pkg -get firefox".MakeColored("66, 133, 244")}".WriteLine();
                        $"      {"---".MakeColored("87, 166, 74")}firefox {"#end".MakeColored(Color.Gray)}".WriteLine();
                    }
                }
                else if (ans.Contains("pkg"))
                {
                    if (ans == "pkg -get py3.12.4")
                    {
                        string fileName = "python-3.12.4-amd64.exe";
                        string filePath = Path.Combine(CD.CD_DIR, fileName);
                        string url = "https://www.python.org/ftp/python/3.12.4/python-3.12.4-amd64.exe";
                        Console.Write("  ~".MakeColored(Color.DodgerBlue));
                        string yon = input($" You sure download {"py3.12.4".MakeColored(Color.GreenYellow)} from {"https://www.python.org/downloads/".MakeColored(Color.GreenYellow)} [Y\\N] ~~> ", Color.White, false, false, 1);
                        Download(fileName, filePath, url);
                    }

                }
                else if (SolveCommand(ans) == "pull")
                {
                    string path = ans.Substring(ans.LastIndexChar('l') + 1);

                }
                else if (ans == "net")
                {
                    var tcpConnections = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections();
                    List<string[]> rows = new List<string[]>();
                    foreach (var connection in tcpConnections)
                    {
                        string localIP = connection.LocalEndPoint.Address.ToString();
                        string remoteIP = connection.RemoteEndPoint.Address.ToString();
                        string state = connection.State.ToString();
                        string localHost = localIP;
                        try
                        {
                            localHost = Dns.GetHostEntry(connection.LocalEndPoint.Address).HostName;
                        }
                        catch (SocketException)
                        {
                        }
                        string remoteHost = remoteIP;
                        try
                        {
                            remoteHost = Dns.GetHostEntry(connection.RemoteEndPoint.Address).HostName;
                        }
                        catch (SocketException)
                        {
                        }
                        string localAddress = $"{localIP.MakeColored(Color.HotPink)}:{connection.LocalEndPoint.Port.MakeString().MakeColored(Color.Chocolate)} ({localHost.MakeColored(Color.DodgerBlue)})";
                        string remoteAddress = $"{remoteIP.MakeColored(Color.HotPink)}:{connection.RemoteEndPoint.Port.MakeString().MakeColored(Color.Chocolate)} ({remoteHost.MakeColored(Color.DodgerBlue)})";
                        rows.Add(new string[] { "TCP", localAddress, remoteAddress, state.MakeColored(Color.PaleGoldenrod) });
                    }

                    foreach (var row in rows)
                    {
                        Console.Write($"{row[0]} ");
                        Console.Write($"{row[1]}");
                        Console.Write(" → ".MakeColored(Color.Lime));
                        Console.Write($"{row[2]}");
                        Console.WriteLine($" {row[3].MakeColored(Color.MediumSpringGreen)}");
                    }

                }
                else if (ans.Contains("https") && ans.Contains(@"/"))
                {
                    Process.Start(ans);
                }
                else if (ans == "root")
                {
                    if (Program.IsRunningAsAdministrator())
                    {
                        Console.WriteLine("You are already root user.".MakeColored(Color.Gold));
                    }
                    else
                    {
                        RestartAsAdministrator();
                    }
                }
                else if (ans.Contains(".exe") && ans.Substring(0, ans.FirstIndexChar(' ')).EndsWith(".exe") && ans.Contains(' '))
                {
                    if (File.Exists(CD.CD_DIR + @"\" + ans.Substring(0, ans.FirstIndexChar(' '))))
                    {
                        try
                        {
                            string exePath = CD.CD_DIR + @"\" + ans.Substring(0, ans.FirstIndexChar(' '));
                            string arguments = ans.Substring(ans.FirstIndexChar(' ') + 1);
                            Process process = new Process();
                            process.StartInfo.FileName = exePath;
                            process.StartInfo.Arguments = arguments;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = true;
                            process.Start();
                            string output = process.StandardOutput.ReadToEnd();
                            Console.WriteLine(output);
                            process.WaitForExit();

                        }
                        catch (Exception e)
                        {
                            Text.NewLine();
                            Console.CursorLeft += 10;
                            "~~>".WriteFore("153,153,153");
                            " + ".WriteFore(Color.Red);
                            $"'{ans}'".Write();
                            Console.WriteLine();
                            for (int i = 0; i < 3; i++)
                            {
                                Console.CursorLeft += 10;
                                "~".WriteLineFore("153,153,153");
                            }
                            Console.CursorLeft += 10;
                            for (int i = 0; i < ans.Length + 3; i++)
                            {
                                "~".WriteFore("153,153,153");
                            }
                            "> ".WriteFore("153,153,153");
                            $"{e.Message}".WriteFore(Color.Red);
                            Text.NewLine(2);
                        }
                    }
                }
                else if (ans.Contains(".exe") && !ans.Contains(' ') && File.Exists(CD.CD_DIR + @"\" + ans))
                {
                    try
                    {
                        Process.Start(CD.CD_DIR + @"\" + ans);
                    }
                    catch (Exception e)
                    {
                        Text.NewLine();
                        Console.CursorLeft += 10;
                        "~~>".WriteFore("153,153,153");
                        " + ".WriteFore(Color.Red);
                        $"'{ans}'".Write();
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.CursorLeft += 10;
                            "~".WriteLineFore("153,153,153");
                        }
                        Console.CursorLeft += 10;
                        for (int i = 0; i < ans.Length + 3; i++)
                        {
                            "~".WriteFore("153,153,153");
                        }
                        "> ".WriteFore("153,153,153");
                        $"{e.Message}".WriteFore(Color.Red);
                        Text.NewLine(2);
                    }
                } 
                else
                {
                    if (!String.IsNullOrWhiteSpace(ans))
                    {
                        bool isnotcommand = true;
                        bool iscommandbutwrongsyntax = false;
                        int trueusage = -1;
                        if (commands.Contains(ans)) isnotcommand = false;
                        for (int i = 0; i < commands.Length; i++)
                        {
                            if (ans.Contains(commands[i]))
                            {
                                iscommandbutwrongsyntax = true;
                                isnotcommand = false;
                                trueusage = i;
                                break;
                            }
                        }
                        if (iscommandbutwrongsyntax)
                        {
                            Text.NewLine();
                            Console.CursorLeft += 10;
                            "~~>".WriteFore("153,153,153");
                            " + ".WriteFore(Color.Yellow);
                            $"'{ans}'".Write();
                            Console.WriteLine();
                            for (int i = 0; i < 5; i++)
                            {
                                Console.CursorLeft += 10;
                                if (i == 1)
                                {
                                    "~".WriteFore("153,153,153");
                                    "  ... Category : commands[isnotcommand] : ".Write();
                                    isnotcommand.WriteLineFore(Color.DodgerBlue);
                                }
                                else if (i == 3)
                                {
                                    "~".WriteFore("153,153,153");
                                    $"  true usage line : {trueusage} / ".WriteFore(Color.Chocolate);
                                    usages[trueusage].WriteFore(Color.Gray);
                                    " / ".WriteFore(Color.Chocolate);
                                    exp[trueusage].WriteLineFore(Color.Gray);
                                }
                                else
                                {
                                    "~".WriteLineFore("153,153,153");
                                }
                            }
                            Console.CursorLeft += 10;
                            for (int i = 0; i < ans.Length + 3; i++)
                            {
                                "~".WriteFore("153,153,153");
                            }
                            "> ".WriteFore("153,153,153");
                            "You may be using the command incorrectly / use : ".WriteFore(Color.Yellow);
                            "'help'".WriteFore(Color.Gray);
                            Text.NewLine(2);
                        }
                        if (isnotcommand)
                        {
                            Text.NewLine();
                            Console.CursorLeft += 10;
                            "~~>".WriteFore("153,153,153");
                            " + ".WriteFore(Color.Red);
                            $"'{ans}'".Write();
                            Console.WriteLine();
                            for (int i = 0; i < 3; i++)
                            {
                                Console.CursorLeft += 10;
                                if (i == 1)
                                {
                                    "~".WriteFore("153,153,153");
                                    "  ... Category : commands[isnotcommand] : ".Write();
                                    isnotcommand.WriteLineFore(Color.DodgerBlue);
                                }
                                else
                                {
                                    "~".WriteLineFore("153,153,153");
                                }
                            }
                            Console.CursorLeft += 10;
                            for (int i = 0; i < ans.Length + 3; i++)
                            {
                                "~".WriteFore("153,153,153");
                            }
                            "> ".WriteFore("153,153,153");
                            "No such valid command found".WriteFore(Color.Red);
                            Text.NewLine(2);
                        }
                    }
                }
                Console.WriteLine();
            }
        }
        public static string[] commands = { "list","read find","echo","cp -f", "cp -d", "cp -cd", "root", "edit -s", "edit -a", "clear", "gd", "files", "dir", "-l", "-r", "read", "c", "cd", "zip", "rename", "del", "det", "main", "exit", "hideip", "showip", "ping", "ping -f", "help", "tree", "translate", "cmd", "net", "ip", "get", "pkg -s", "whereami" };
        public static string[] usages = { "list", "read find<Path>:<Word>","echo <Text>", "cp -f <Number>", "cp -d <Number>", "cp -cd <Number>", "root", "edit -s <Path>:<Text>", "edit -a <Path>:<Text>", "clear", "gd <Path>", "files", "dir", "-l", "-r", "read <File name>", "cf <File name>", "cd <Directory name>", "zip <Directory name >(Stackable:)<Zip name>", "rename <File name>:<New name>", "del <File or dir name>", "det <File or dir name>", "main", "exit", "hideip", "showip", "ping <Address>", "ping -f delay=<Delay> size=<Size>:<Address>", "help", "tree", "translate <Word>", "cmd <CMD Command>", "net", "ip", "get <Web site>", "pkg -s", "whereami" };
        public static string[] exp = { "list", "read find robot.txt:dont","echo termX", "cp -f 1", "cp -d 1", "cp -cd 1", "root", "edit -s test.txt:only text", "edit -a text.txt:last text", "clear", "gd C:\\Example\\Example", "files", "dir", "-l", "-r", "read robot.txt", "cf test.txt", "cd test", "zip test:test2:tests", "rename test.txt:test2.txt", "del test", "det robot.txt", "main", "exit", "showip", "hideip", "ping www.example.com", "ping -f delay=100 size=10:www.example.com", "help", "tree", "translate Hello World!", "cmd ipconfig", "net", "ip", "get https://api.ipify.org", "pkg -s", "whereami" };
        public static void Download(string fileName, string filePath, string url)
        {
            DownloadFileWithProgress(url, filePath, (text) =>
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(text);
                Console.SetCursorPosition(0, Console.CursorTop);
            });
        }
        static void DownloadFileWithProgress(string url, string filePath, Action<string> progressCallback)
        {
            long lastBytesReceived = 0;
            int lastProgress = -1;
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        if (e.ProgressPercentage != lastProgress)
                        {
                            string progressText = $"{e.BytesReceived / 1024 / 1024} MB / {e.TotalBytesToReceive / 1024 / 1024} MB - %{e.ProgressPercentage}/100";
                            progressCallback(progressText);
                            lastBytesReceived = e.BytesReceived;
                            lastProgress = e.ProgressPercentage;
                        }
                    };
                    client.DownloadFileCompleted += (s, e) =>
                    {
                        if (e.Error != null)
                        {
                            string progressText = $"Hata: {e.Error.Message}";
                            progressCallback(progressText);
                        }
                        else
                        {
                            string progressText = "İndirme tamamlandı!";
                            progressCallback(progressText);
                        }
                    };
                    client.DownloadFileAsync(new Uri(url), filePath);
                    while (client.IsBusy)
                    {

                    }
                }
                catch (Exception ex)
                {
                    string progressText = $"Bir hata oluştu: {ex.Message}";
                    progressCallback(progressText);
                }
            }
        }
        private static void PlayMp3File(string mp3FilePath)
        {

            using (var audioFile = new AudioFileReader(mp3FilePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Console.Title = $"{audioFile.CurrentTime.TotalSeconds}";
                    Thread.Sleep(1000);
                }
            }
        }
        static void RestartAsAdministrator()
        {
            var exeName = Process.GetCurrentProcess().MainModule.FileName;
            var startInfo = new ProcessStartInfo("wt.exe")
            {
                Arguments = $"-d \"{Environment.CurrentDirectory}\" cmd.exe /c \"{exeName}\"",
                Verb = "runas",
                UseShellExecute = true
            };

            try
            {
                Process.Start(startInfo);
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Text.NewLine();
                Console.CursorLeft += 10;
                "~~>".WriteFore("153,153,153");
                " + ".WriteFore(Color.Red);
                $"'Error'".Write();
                Console.WriteLine();
                for (int i = 0; i < 3; i++)
                {
                    Console.CursorLeft += 10;
                    "~".WriteLineFore("153,153,153");
                }
                Console.CursorLeft += 10;
                for (int i = 0; i < "Error".Length + 3; i++)
                {
                    "~".WriteFore("153,153,153");
                }
                "> ".WriteFore("153,153,153");
                $"{e.Message}".WriteFore(Color.Red);
                Text.NewLine(2);
            }
        }
        private static string TransLate(string text)
        {
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            service.SuppressInitialDiagnosticInformation = true;
            options.AddArgument("--headless");
            IWebDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("https://translate.google.com/?sl=auto&tl=tr&op=translate");
            IWebElement textBox = driver.FindElement(By.ClassName("er8xn"));
            IWebElement button = driver.FindElement(By.Id("i16"));
            button.Click();
            textBox.SendKeys(text);
            Thread.Sleep(2000);
            IWebElement transed = driver.FindElement(By.ClassName("ryNqvb"));
            string a = transed.Text;
            driver.Quit();
            return a;
        }
        private static void PrintDirectoryTree(string path, string indent)
        {
            try
            {
                var directories = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                for (int i = 0; i < directories.Length; i++)
                {
                    var dir = directories[i];
                    string prefix;
                    if (i == directories.Length - 1 && files.Length == 0)
                    {
                        prefix = "└── ".MakeColored(Color.SpringGreen);
                    }
                    else
                    {
                        prefix = "├── ".MakeColored(Color.SpringGreen);
                    }
                    Console.WriteLine(indent + prefix + Path.GetFileName(dir).MakeColored(Color.Orange).Italic().Bold() + " :: ".MakeColored(Color.White).AddRight("Directory 📂".MakeColored(Color.Yellow)));
                    Console.WriteLine(indent + "│    └── ".MakeColored(Color.SpringGreen) + dir);
                    string newIndent;
                    if (i == directories.Length - 1 && files.Length == 0)
                    {
                        newIndent = indent + "    ";
                    }
                    else
                    {
                        newIndent = indent + "│   ".MakeColored(Color.SpringGreen);
                    }
                    PrintDirectoryTree(dir, newIndent);
                }
                for (int i = 0; i < files.Length; i++)
                {
                    var file = files[i];
                    string prefix;
                    if (i == files.Length - 1)
                    {
                        prefix = "└── ".MakeColored(Color.SpringGreen);
                    }
                    else
                    {
                        prefix = "├── ".MakeColored(Color.SpringGreen);
                    }
                    Console.WriteLine(indent + prefix + $"{i}".MakeColored(Color.LightSlateGray) + "| ".MakeColored(Color.SpringGreen) + Path.GetFileName(file).MakeColored(Color.DeepSkyBlue).Underline().Bold() + " :: ".MakeColored(Color.NavajoWhite).AddRight("File".MakeColored(Color.Yellow)));
                    if (i + 1 != files.Length)
                    {
                        Console.WriteLine(indent + "│".MakeColored(Color.SpringGreen) + "    └── ".MakeColored(Color.SpringGreen) + file.MakeColored(Color.LightGray) + " :: ".MakeColored(Color.NavajoWhite).AddRight("Path".MakeColored(Color.Yellow)));
                    }
                    else Console.WriteLine(indent + "    └── ".MakeColored(Color.SpringGreen) + file.MakeColored(Color.LightGray) + " :: ".MakeColored(Color.NavajoWhite).AddRight("Path".MakeColored(Color.Yellow)));
                }
            }
            catch (Exception e)
            {

                Text.NewLine();
                Console.CursorLeft += 10;
                "~~>".WriteFore("153,153,153");
                " + ".WriteFore(Color.Red);
                $"'Error'".Write();
                Console.WriteLine();
                for (int i = 0; i < 3; i++)
                {
                    Console.CursorLeft += 10;
                    "~".WriteLineFore("153,153,153");
                }
                Console.CursorLeft += 10;
                for (int i = 0; i < "Error".Length + 3; i++)
                {
                    "~".WriteFore("153,153,153");
                }
                "> ".WriteFore("153,153,153");
                $"{e.Message}".WriteFore(Color.Red);
                Text.NewLine(2);
            }
        }
        private static void AddDirectoryToZip(ZipArchive archive, string sourceDir, string entryName)
        {
            foreach (string filePath in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(filePath);
                archive.CreateEntryFromFile(filePath, Path.Combine(entryName, fileName));
            }
            string[] subDirectories = Directory.GetDirectories(sourceDir);
            if (subDirectories.Length == 0)
                return;
            foreach (string subDir in subDirectories)
            {
                AddDirectoryToZip(archive, subDir, entryName);
            }
        }
        private static async Task<string> GetIp()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://api.ipify.org");
                response.EnsureSuccessStatusCode();
                string ipAddress = await response.Content.ReadAsStringAsync();
                return ipAddress;
            }
        }
        private static async Task<string> get(string address)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(address);
                response.EnsureSuccessStatusCode();
                string ipAddress = await response.Content.ReadAsStringAsync();
                return ipAddress;
            }
        }
    }

    internal class Program
    {
        public static bool IsRunningAsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Thread infoget = new Thread(INFOS.Get);
                infoget.IsBackground = true;
                infoget.Start();
                if (!Directory.Exists(CD.CD_DIR + "\\" + "termXtools"))
                {
                    Directory.CreateDirectory(CD.CD_DIR + "\\" + "termXtools");
                }
                if (args != null && args.Length > 0)
                {
                    CD.CD_DIR = args[0];
                }
                Console.Title = "EchoSphere";
                bool a = IsRunningAsAdministrator();
                if (a is true)
                {
                    CD.ADMIN = "RunningAsAdministrator";
                }
                else
                {
                    CD.ADMIN = "RunningAsNormalUser";
                }
                Console.WriteLine("EchoSphere[Version 3.1]");

                Console.OutputEncoding = Encoding.UTF8;
                $@"EchoSphere".Position((Console.WindowWidth / 2) - ("EchoSphere".Length / 2) - 1, 1).WriteLineFore(Color.Orange);
                Text.NewLine(3);
                INPUT_STREAM.Command();
            }catch{}
        }
    }
}