using System.Globalization;
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace ConsoleCalculator;

class Program
{
    static void Main(string[] args)
    {

        List<string> calculations = new List<string>();
        List<double> results = new List<double>();
        int timesUsed = 0;
        
        
        
        bool endApp = false;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        
        while (!endApp)
        {
            if (timesUsed > 0)
            {
                Console.WriteLine($"Calculator Usage Count: {timesUsed}");
            }
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;
            string? previousCalcInput = "";
            double cleanNum1 = 0;
            double cleanNum2 = 0;
            int cleanIndexChoice = 0;
            
            // Previous Calculations 
            if (calculations.Count > 0)
            {
                Console.WriteLine("Would you like to use a previous calculation result? (Y/N)");
                previousCalcInput = Console.ReadLine();
                if (previousCalcInput.ToLower() == "y")
                {
                    int index = 1;
                    for (int i = 0; i < calculations.Count; i++)
                    {
                        Console.WriteLine($"Calculation {index}: {calculations[i]}");
                        index += 1;
                    }
                    
                    Console.WriteLine("Enter the index of the result you'd like use: ");
                    string? indexChoice = Console.ReadLine();
                    
                   
                    while (!int.TryParse(indexChoice, out cleanIndexChoice))
                    {
                        
                        Console.WriteLine("That is not a valid choice. Enter a number");
                        indexChoice = Console.ReadLine();
                    }
                    
                    
                    while (cleanIndexChoice < 1 || cleanIndexChoice > calculations.Count)
                    {
                        Console.WriteLine("Please choose an index within the list range");
                        while (!int.TryParse(indexChoice, out cleanIndexChoice))
                        {
                        
                            Console.WriteLine("That is not a valid choice. Enter a number");
                            indexChoice = Console.ReadLine();
                        }
                    }
                    

                    cleanNum1 = results[cleanIndexChoice - 1];
                    
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }
                    

                }
                else
                {
                    (cleanNum1, cleanNum2) = GetTwoNumbersFromUser();
                }
               
            }
            else
            {

                (cleanNum1, cleanNum2) = GetTwoNumbersFromUser();

            }
            
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tp - Taking the Power");
            Console.WriteLine("\tp10 - 10x");
            Console.WriteLine("\tsin - Trigonometry Function: Sin");
            Console.WriteLine("\tcos - Trigonometry Function: Cos");
            Console.WriteLine("\ttan - Trigonometry Function: Tan");
            Console.WriteLine("\tdel - Delete Calculation History");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == "del")
            {
                calculations.Clear();
                results.Clear();
                Console.WriteLine("All calculation history was deleted");
            } else if (op == null || ! Regex.IsMatch(op, "^(a|s|m|d|r|p|p10|sin|cos|tan)$"))
            {
               Console.WriteLine("Error: Unrecognized input.");
            }
            else
            { 
               try
               {
                  result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                  // add operator logic here
                  string opSymbol = OperatorSwitch(op);
                  string operationStr = $"{cleanNum1} {opSymbol} {cleanNum2} = {result}";
                  calculations.Add(operationStr);
                  results.Add(result);
                  timesUsed++;
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

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        
        return;
    }

    static (double, double) GetTwoNumbersFromUser()
    {
        
        string? numInput1 = "";
        string? numInput2 = "";
        double cleanNum1 = 0;
        double cleanNum2 = 0;
        
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();

        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }

        Console.Write("Type another number, and then press Enter: ");
        numInput2 = Console.ReadLine();

                
        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput2 = Console.ReadLine();
        }

        return (cleanNum1, cleanNum2);
    }

    static string OperatorSwitch(string op)
    {
        string opSymbol = op switch
        {
            "a" => "+",
            "s" => "-",
            "m" => "*",
            "d" => "/",
            "r" => "√",
            "p" => "^",
            "p10" => "10^",
            "sin" => "sin",
            "cos" => "cos",
            "tan" => "tan",
            _ => op
        };

        return opSymbol;
    }
    
    
}