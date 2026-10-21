using System;

namespace Lab04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Визначення розташування точки на координатній площині ===");

            // Введення даних
            Console.Write("Введіть координату X: ");
            string inputX = Console.ReadLine();

            Console.Write("Введіть координату Y: ");
            string inputY = Console.ReadLine();

            // Перевіряємо, чи введено саме дійсні числа
            if (!double.TryParse(inputX, out double x) || !double.TryParse(inputY, out double y))
            {
                Console.WriteLine("Помилка: координати мають бути дійсними числами!");
                return;
            }

            // Основне розгалуження 
            string locationMessage;

            if (x == 0 && y == 0)
            {
                locationMessage = "Точка лежить у початку координат (0; 0)";
            }
            else if (x != 0 && y == 0)
            {
                locationMessage = "Точка лежить на осі абсцис (Оx)";
            }
            else if (x == 0 && y != 0)
            {
                locationMessage = "Точка лежить на осі ординат (Оy)";
            }
            else if (x > 0 && y > 0)
            {
                locationMessage = "Точка лежить у I чверті";
            }
            else if (x < 0 && y > 0)
            {
                locationMessage = "Точка лежить у II чверті";
            }
            else if (x < 0 && y < 0)
            {
                locationMessage = "Точка лежить у III чверті";
            }
            else // x > 0 && y < 0
            {
                locationMessage = "Точка лежить у IV чверті";
            }

            // Додаткові обчислення 
            double distance = Math.Sqrt(x * x + y * y); // Відстань від початку координат
            bool isNearAxis = Math.Abs(x) < 1.0 || Math.Abs(y) < 1.0; // Чи близько точка до будь-якої з осей

            string category = distance switch
            {
                0.0 => "Точка в початку координат",
                < 5.0 when isNearAxis => "Ближня зона (поблизу однієї з осей)", 
                < 5.0 => "Ближня зона",
                >= 5.0 and < 15.0 => "Середня зона",
                _ => "Дальня зона"
            };

            // Форматоване виведення результатів
            Console.WriteLine();
            Console.WriteLine($"Координати точки: X = {x:F2}, Y = {y:F2}");
            Console.WriteLine($"Розташування: {locationMessage}");
            Console.WriteLine($"Відстань до початку координат: {distance:F2}");
            Console.WriteLine($"Категорія зони: {category}");
        }
    }
}