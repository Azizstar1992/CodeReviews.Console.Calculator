using System.Text.RegularExpressions;
using CalculatorLibrary;
class Program
{
    public static void ClearLastLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, Console.CursorTop);
    }
    static void Main(string[] args)
    {
        Boolean endGame = false;
        Console.Clear();
        Console.WriteLine(" Welcome to the Calculator");
        Calculator calculator = new Calculator();
        while (!endGame)
        {
            string? num1Input = "";
            string? num2Input = "";
            double result = 0;
            Console.WriteLine("Please Enter a number");

            double num1 = 0;
            double num2 = 0;
            num1Input = Console.ReadLine();
            while (!double.TryParse(num1Input, out num1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                
                System.Threading.Thread.Sleep(1500);
                num1Input = Console.ReadLine();
            }
            Console.WriteLine("Please Enter another number");
            num2Input = Console.ReadLine();
            while (!double.TryParse(num2Input, out num2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
               
                System.Threading.Thread.Sleep(1500);
                num2Input = Console.ReadLine();
            }



            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(num1, num2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

           
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endGame = true;

            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;



    }
}
