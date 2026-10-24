using System;
using System.Globalization;

namespace Lab03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Константа для переведення / нормування (приклад константи задачі)
            const double MinDistanceThreshold = 0.1;

            Console.WriteLine("==================================================");
            Console.WriteLine("   РОЗРАХУНОК ВАРТОСТІ ПОЇЗДКИ ТАКСІ (ВАРІАНТ 1)  ");
            Console.WriteLine("==================================================\n");

            // Введення вихідних даних
            Console.Write("Введіть відстань поїздки (км): ");
            bool okDistance = double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out double distanceKm);

            Console.Write("Введіть тариф за 1 км (грн): ");
            bool okTariffKm = decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tariffPerKm);

            Console.Write("Введіть тариф подачі авто (грн): ");
            bool okBaseFee = decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal baseFee);

            Console.Write("Введіть час очікування (хв): ");
            bool okWaitMinutes = double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out double waitingMinutes);

            Console.Write("Введіть тариф за хвилину очікування (грн): ");
            bool okTariffWait = decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tariffPerWaitMin);

            Console.WriteLine();
            Console.WriteLine($"Розпізнано введення: відстань - {okDistance}, тариф/км - {okTariffKm}, " +
                              $"подача - {okBaseFee}, очікування - {okWaitMinutes}, тариф/хв - {okTariffWait}");
            Console.WriteLine();

            // Корекція можливих від'ємних значень
            distanceKm = Math.Max(0.0, distanceKm);
            waitingMinutes = Math.Max(0.0, waitingMinutes);

            // Вартість складових
            decimal baseCost = baseFee;
            decimal distanceCost = (decimal)distanceKm * tariffPerKm;
            decimal waitingCost = (decimal)waitingMinutes * tariffPerWaitMin;

            // Підсумок 
            decimal totalCost = 0m;
            totalCost += baseCost;
            totalCost += distanceCost;
            totalCost += waitingCost;

            decimal totalCostRoundedUp = (decimal)Math.Ceiling((double)totalCost);

            // Обчислення вартості одного кілометра
            decimal effectiveDistance = (decimal)Math.Max(distanceKm, MinDistanceThreshold);
            decimal costPerKm = totalCost / effectiveDistance;

            // Виведення результатів 
            Console.WriteLine("+------------------------------------------+-----------------+");
            Console.WriteLine($"| {"Показник",-40} | {"Значення",15} |");
            Console.WriteLine("+------------------------------------------+-----------------+");
            Console.WriteLine($"| {"Тариф подачі авто",-40} | {baseCost,15:C} |");
            Console.WriteLine($"| {"Вартість пройденої відстані",-40} | {distanceCost,15:C} |");
            Console.WriteLine($"| {"Вартість часу очікування",-40} | {waitingCost,15:C} |");
            Console.WriteLine("+------------------------------------------+-----------------+");
            Console.WriteLine($"| {"Підсумкова вартість (точна)",-40} | {totalCost,15:N2} грн |");
            Console.WriteLine($"| {"Підсумок (округлено вгору до грн)",-40} | {totalCostRoundedUp,15:F0} грн |");
            Console.WriteLine($"| {"Ефективна вартість 1 км",-40} | {costPerKm,15:F3} грн |");
            Console.WriteLine("+------------------------------------------+-----------------+\n");

            Console.WriteLine("=== ПЕРЕВІРКА ОКРУГЛЕНЬ ТА ДОДАТКОВИЙ ПОКАЗНИК (РІВЕНЬ Б) ===");

            double mainResult = (double)totalCost;
            Console.WriteLine("\nПорівняння методів округлення для підсумкової вартості:");
            Console.WriteLine($"{"Floor",12}{"Ceiling",12}{"Round(x,2)",12}{"Truncate",12}");
            Console.WriteLine($"{Math.Floor(mainResult),12}{Math.Ceiling(mainResult),12}{Math.Round(mainResult, 2),12}{Math.Truncate(mainResult),12}\n");

            double equivalentRadius = Math.Sqrt(distanceKm / Math.PI);

            Console.WriteLine("Додатковий показник: Умовний радіус зони поїздки R = sqrt(S / PI)");
            Console.WriteLine($"Формат F2 : {equivalentRadius:F2} км");
            Console.WriteLine($"Формат N2 : {equivalentRadius:N2} км");
            Console.WriteLine($"Формат E3 : {equivalentRadius:E3} км");
            Console.WriteLine("==================================================");
        }
    }
}