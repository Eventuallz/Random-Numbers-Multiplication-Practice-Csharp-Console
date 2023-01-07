using System.Diagnostics;
using System.Text.RegularExpressions;

// stopwatch knowledge: https://www.educba.com/c-sharp-stopwatch/
// and: https://www.dotnetperls.com/stopwatch

// Filepath variable.
var path = $@"{Directory.GetCurrentDirectory()}\Calculations.txt";

// User Interface
while (true)
{
    Console.Clear();
    Console.CursorVisible = false;
    Console.WriteLine("Choose practice room (press number in brackets).");
    Console.WriteLine("\n[1]-digit\n[2]-digit\n[3]-digit\n[V]iew past exercises\n[E]xit");
    var choice = Console.ReadKey(true);
    switch (choice.Key)
    {
        case ConsoleKey.D1:
        case ConsoleKey.NumPad1:
        {
            Console.Clear();
            Multiplication(1, 9, "One");
        }
            break;

        case ConsoleKey.D2:
        case ConsoleKey.NumPad2:
        {
            Console.Clear();
            Multiplication(10, 99, "Two");
        }
            break;

        case ConsoleKey.D3:
        case ConsoleKey.NumPad3:
        {
            Console.Clear();
            Multiplication(100, 999, "Three");
        }
            break;

        case ConsoleKey.V:
        {
            Console.Clear();
            var readText = File.ReadAllText(path);
            var lines = readText.Split('\n');
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            Console.ReadKey(true);
        }
            break;

        case ConsoleKey.E:
            Console.WriteLine("\nExiting...");
            return;

        default:
            Console.WriteLine("Wrong Input");
            break;
    }
}

void Multiplication(int startValue, int endValue, string saveText)
{
    var stopwatch = new Stopwatch();
    var random = new Random();

    int a = random.Next(startValue, endValue), b = random.Next(startValue, endValue);
    var result = a * b;


    for (var second = 3; second >= 0; second--)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write($"Timer starts in {second} ");
        Thread.Sleep(1000);
    }

    Console.SetCursorPosition(0, 0);
    Console.Write(new string(' ', Console.BufferWidth));

    stopwatch.Start();
    Console.SetCursorPosition(0, 0);

    Console.WriteLine($"{a} * {b}");

    var userResult = 0;
    var parsed = false;

    do
    {
        if (!parsed)
        {
            Console.SetCursorPosition(0, 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 2);
            Console.Write("Enter result: ");
            parsed = int.TryParse(Console.ReadLine(), out userResult);
        }
        else
        {
            Console.WriteLine("Enter result: ");
        }
    } while (!parsed);

    stopwatch.Stop();
    var total = stopwatch.Elapsed;

    var correct = userResult == result;

    Console.SetCursorPosition(0, 2);
    Console.Write(new string(' ', Console.BufferWidth));

    Console.SetCursorPosition(0, 2);
    Console.Write($"Your answer: {userResult}\nResult:      {result}\nTotal time: {total}");

    Console.CursorVisible = true;
    Console.WriteLine("\nSave operations? (Saved in a local text file) [Y/n] (default is n): ");
    var binary = Console.ReadKey(true);

    switch (binary.Key)
    {
        case ConsoleKey.Y:
        {
            using (var writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"\n{saveText} digit number multiplications");
                writer.WriteLine($"{a} * {b} = {result}, Answered: {userResult}, Correct: {correct}");
                writer.WriteLine($"Total Time: {total}");
            }

            var readText = File.ReadAllText(path);
            // Console.Clear();
            // // Console.WriteLine(readText);
            // var lines = PrintLastLines(readText, 5);
            // foreach (var line in lines)
            // {
            //     Console.WriteLine(line);
            // }
            //
            // Console.ReadKey(true);
        }
            break;

        case ConsoleKey.N:
        default:
            return;
    }
}

static List<string> PrintLastLines(string text, int count)
{
    var lines = new List<string>();
    var match = Regex.Match(text, "^.*$", RegexOptions.Multiline | RegexOptions.RightToLeft);

    while (match.Success && lines.Count < count)
    {
        lines.Insert(0, match.Value);
        match = match.NextMatch();
    }

    return lines;
}