using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        //рівень А
        bool hasPrinted = false;

        Console.Write("Введіть кількість елементів (n): ");
        if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
        {
            double sum = 0.0;

            for (int i = 1; i <= n; i++)
            {
                sum += 1.0 / i;

                // Виведення поточної суми для кожного 5-го кроку
                if (i % 5 == 0)
                {
                    Console.WriteLine($"Проміжна сума на кроці {i}: {sum:F4}");
                }

                // Перевірка, чи сума перевищила 3 
                if (sum >= 3 && !hasPrinted)
                {
                    Console.WriteLine($"Сума гармонічного ряду вперше перевищила або дорівнює 3 на кроці i = {i} (Сума: {sum:F4})");
                    hasPrinted = true;
                }
            }

            // Кінцевий результат після завершення циклу
            Console.WriteLine($"\nОстаточна сума гармонічного ряду для n = {n} дорівнює: {sum:F4}");
        }
        else
        {
            Console.WriteLine("Будь ласка, введіть коректне ціле число більше за 0.");
        }

        //рівень Б

        Console.Write("\nВведіть розмір квадрата n (від 3 до 20): ");
        if (int.TryParse(Console.ReadLine(), out int squareN) && squareN >= 3 && squareN <= 20)
        {
            Console.WriteLine($"\nКвадрат розміром {squareN}x{squareN}:");
            for (int i = 0; i < squareN; i++)
            {
                for (int j = 0; j < squareN; j++)
                {
                    if (i == j || i + j == squareN - 1)
                    {
                        Console.Write("#");
                    }
                    else if (i == 0 || i == squareN - 1 || j == 0 || j == squareN - 1)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Некоректний розмір квадрата.");
        }
    }
}
