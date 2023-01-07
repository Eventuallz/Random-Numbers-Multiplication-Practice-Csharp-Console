using System.Diagnostics;

// stopwatch knowledge: https://www.educba.com/c-sharp-stopwatch/
// and: https://www.dotnetperls.com/stopwatch

// Filepath variable.
var path = $@"{Directory.GetCurrentDirectory()}\Calculations.txt";

// User interface
while (true)
{
    Console.Clear();
    Console.CursorVisible = false;
    Console.WriteLine("Welcome to practicing multiplication!");
    Console.WriteLine("\n[S]tart practicing\n[V]iew past exercises\n[E]xit");
    var choice = Console.ReadKey(true);
    switch (choice.Key)
    {
        // The user can choose how many digit multiplications they want to practice.
        case ConsoleKey.S:
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.Write("Enter digits to multiply by: ");
            var digits = 0;
            var parsed = false;
            var fail = 0;
            do
            {
                if (!parsed && fail > 0) // If the parsing did fail once, display the same but clear the previous lines.
                {
                    for (var i = 2; i <= 3; i++)
                    {
                        ClearLine(0, i);
                    }

                    Console.SetCursorPosition(0, 0);
                    Console.CursorVisible = true;
                    Console.WriteLine("Please enter a number greater than 0.");
                    Console.Write("Enter digits to multiply by: ");
                    parsed = int.TryParse(Console.ReadLine(), out digits);
                    fail++;
                }
                else // Just display the prompt. This will be displayed first.
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Enter digits to multiply by: ");
                    parsed = int.TryParse(Console.ReadLine(), out digits);
                    fail++;
                }
            } while (!parsed || digits == 0);

            // In case of a negative value, the input value gets corrected.
            if (digits < 0) digits *= -1;

            var startValue = "1";
            var endValue = "9";
            for (var i = 1; i < digits; i++)
            {
                if (digits > 1)
                {
                    endValue += "9";
                    startValue += "0";
                }
            }

            for (var i = 0; i <= 3; i++)
            {
                ClearLine(0, i);
            }

            Console.CursorVisible = false;
            Console.WriteLine($"{startValue} {endValue}");
            Console.ReadKey(true);
            Multiplication(Convert.ToInt32(startValue), Convert.ToInt32(endValue), digits.ToString());
        }
            break;

        // Display all exercises.
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

        // Exit.
        case ConsoleKey.E:
        {
            Console.WriteLine("\nExiting...");
            Environment.Exit(exitCode: 0);
        }
            return;

        default:
            Console.WriteLine("Wrong Input");
            break;
    }
}

// Method for multiplying numbers.
void Multiplication(int startValue, int endValue, string saveText)
{
    // Generating random numbers for the exercise.
    var random = new Random();
    int a = random.Next(startValue, endValue), b = random.Next(startValue, endValue);
    var result = a * b;

    // 3 second countdown until start.
    for (var second = 3; second > 0; second--)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write($"Timer starts in {second} ");
        Thread.Sleep(1000);
    }

    // Deleting previous line.
    ClearLine(0, 0);

    // Making stopwatch.
    var stopwatch = new Stopwatch();

    // Starting stopwatch.
    stopwatch.Start();
    // Setting cursor position up again for a prettier display.
    Console.SetCursorPosition(0, 0);
    // Displaying exercise.
    Console.WriteLine($"{a} * {b}");

    // User answer input.
    var userResult = 0;
    var parsed = false;
    var fail = 0;
    do
    {
        if (!parsed && fail > 0) // If the parsing did fail once, display the same but clear the previous lines.
        {
            for (var i = 2; i <= 3; i++)
            {
                ClearLine(0, i);
            }

            Console.SetCursorPosition(0, 2);
            Console.CursorVisible = true;
            Console.WriteLine("Please enter a number.");
            Console.Write("Enter result: ");
            parsed = int.TryParse(Console.ReadLine(), out userResult);
            fail++;
        }
        else // Just display the prompt. This will be displayed first.
        {
            Console.SetCursorPosition(0, 2);
            Console.Write("Enter result: ");
            parsed = int.TryParse(Console.ReadLine(), out userResult);
            fail++;
        }
    } while (!parsed);

    Console.CursorVisible = false;
    
    // Stopping timer and saving time in a variable.
    stopwatch.Stop();
    var total = stopwatch.Elapsed;

    // Boolean to show the correctness of the users answer (for future use).
    var correct = userResult == result;

    ClearLine(0, 2);

    // Display results.
    Console.SetCursorPosition(0, 2);
    Console.Write($"Your answer: {userResult}\nResult:      {result}    \nTotal time: {total}");

    // Prompt to ask for saving the results in a text file.
    Console.CursorVisible = true;
    Console.WriteLine("\nSave operations? (Saved in a local text file) [Y/n] (default is n): ");
    var binary = Console.ReadKey(true);

    // Write results to file.   
    switch (binary.Key)
    {
        case ConsoleKey.Y:
        {
            using var writer = new StreamWriter(path, true);
            writer.WriteLine(
                $"{saveText} digit number multiplications\n{a} * {b} = {result}, Answered: {userResult}, Correct: {correct}\nTotal Time: {total}\n");
        }
            break;

        case ConsoleKey.N:
        default:
            return;
    }
}

// Method for clearing a line.
void ClearLine(int left, int top)
{
    Console.SetCursorPosition(left, top);
    Console.Write(new string(' ', Console.BufferWidth));
}