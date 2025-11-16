// CalculatorLibrary.cs


using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Counter
    {
        private static string filePath = "./counter.json";

        public static int Increment()
        {
            int count = 0;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
                count = data["count"];
            }

            count++;

            var newJson = JsonConvert.SerializeObject(
                new Dictionary<string, int> { { "count", count } },
                Formatting.Indented
            );

            File.WriteAllText(filePath, newJson);

            return count;
        }
    }

    public class Calculator
    {
        private JsonWriter writer;
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;

            writer = new JsonTextWriter(logFile);   // <-- FIXED
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;

                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;

                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;

                case "d":
                    writer.WriteValue("Divide");
                    if (num2 != 0)
                        result = num1 / num2;
                    break;

                default:
                    writer.WriteValue("Invalid");
                    break;
            }

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            int current = Counter.Increment();
            Console.WriteLine($"Program has been run {current} times.");

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}