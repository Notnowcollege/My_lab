using System;

namespace Lab02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //ОГОЛОШЕННЯ ЗМІННИХ ТА КОНСТАНТ 
            const string StoreName = "TechStore";                   
            int orderId = 100_500;                                 
            string productName = "Бездротові навушники";            
            byte initialQuantity = 2;                             
            decimal unitPrice = 1250.50m;                           
            double unitWeightKg = 0.35;                            
            double discountPercent = 0.10;                         
            bool isPaid = true;                                   
            char warehouseCode = 'A';                               
            var orderDate = "06.10.2026";                           

            //ВИВЕДЕННЯ ТАБЛИЦІ ТИПІВ І РОЗМІРІВ
            Console.WriteLine("=== ТАБЛИЦЯ ЗМІННИХ, ТИПІВ ТА РОЗМІРІВ ===");
            Console.WriteLine($"{"Змінна",-16} {"Тип .NET",-10} {"Розмір",-12} {"Значення"}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"orderId",-16} {orderId.GetType().Name,-10} {sizeof(int) + " байт",-12} {orderId}");
            Console.WriteLine($"{"productName",-16} {productName.GetType().Name,-10} {"змінний",-12} {productName}");
            Console.WriteLine($"{"initialQuantity",-16} {initialQuantity.GetType().Name,-10} {sizeof(byte) + " байт",-12} {initialQuantity}");
            Console.WriteLine($"{"unitPrice",-16} {unitPrice.GetType().Name,-10} {sizeof(decimal) + " байт",-12} {unitPrice}");
            Console.WriteLine($"{"unitWeightKg",-16} {unitWeightKg.GetType().Name,-10} {sizeof(double) + " байт",-12} {unitWeightKg}");
            Console.WriteLine($"{"discountPercent",-16} {discountPercent.GetType().Name,-10} {sizeof(double) + " байт",-12} {discountPercent}");
            Console.WriteLine($"{"isPaid",-16} {isPaid.GetType().Name,-10} {sizeof(bool) + " байт",-12} {isPaid}");
            Console.WriteLine($"{"warehouseCode",-16} {warehouseCode.GetType().Name,-10} {sizeof(char) + " байт",-12} {warehouseCode}");
            Console.WriteLine($"{"orderDate",-16} {orderDate.GetType().Name,-10} {"змінний",-12} {orderDate}");
            Console.WriteLine();

            // МЕЖІ ТИПІВ ТА ЗНАЧЕННЯ DEFAULT 
            Console.WriteLine("=== МЕЖІ ЧИСЛОВИХ ТИПІВ ===");
            Console.WriteLine($"int:     від {int.MinValue} до {int.MaxValue}");
            Console.WriteLine($"byte:    від {byte.MinValue} до {byte.MaxValue}");
            Console.WriteLine($"double:  від {double.MinValue} до {double.MaxValue}");
            Console.WriteLine($"decimal: від {decimal.MinValue} до {decimal.MaxValue}");
            Console.WriteLine();

            Console.WriteLine("=== ЗНАЧЕННЯ ЗА ЗАМОВЧУВАННЯМ (default) ===");
            Console.WriteLine($"default(int):    {default(int)}");
            Console.WriteLine($"default(bool):   {default(bool)}");
            Console.WriteLine($"default(char):   '\\0' (символ із кодом {(int)default(char)})");
            Console.WriteLine($"default(string): {default(string) ?? "null (відсутнє значення)"}");
            Console.WriteLine();

            //ЗЧИТУВАННЯ ВВЕДЕННЯ ТА ОБЧИСЛЕННЯ 
            Console.Write("Введіть кількість товару для замовлення: ");
            string? inputString = Console.ReadLine();

            // TryParse
            bool parseSuccess = byte.TryParse(inputString, out byte orderQuantity);

            Console.WriteLine($"\nРозпізнано ввід: {parseSuccess}");
            Console.WriteLine($"Отримане значення кількості: {orderQuantity} шт.");
            Console.WriteLine();

            // Обчислення підсумкових величин
            double totalWeightKg = orderQuantity * unitWeightKg;
            decimal rawTotalCost = orderQuantity * unitPrice;
            decimal discountAmount = rawTotalCost * (decimal)discountPercent;
            decimal finalCost = rawTotalCost - discountAmount;

            // D6, C, P1, F2, N0
            Console.WriteLine("=== ПІДСУМОК ЗАМОВЛЕННЯ ===");
            Console.WriteLine($"Магазин:              {StoreName}");
            Console.WriteLine($"Дата замовлення:      {orderDate}");
            Console.WriteLine($"Номер замовлення:      {orderId:D6}");                          
            Console.WriteLine($"Товар:                {productName} (Склад '{warehouseCode}')");
            Console.WriteLine($"Ціна за одиницю:       {unitPrice:C}");                           
            Console.WriteLine($"Кількість у чеку:     {orderQuantity:N0} шт.");                 
            Console.WriteLine($"Загальна вага:        {totalWeightKg:F2} кг");                  
            Console.WriteLine($"Розмір знижки:         {discountPercent:P1}");                   
            Console.WriteLine($"Сума до сплати:        {finalCost:C}");
            Console.WriteLine($"Статус оплати:        {(isPaid ? "Оплачено" : "Не оплачено")}");
            Console.WriteLine();

            // ТРИ ВИДИ ПЕРЕТВОРЕНЬ ТИПІВ 
            Console.WriteLine("=== РІВЕНЬ Б: ДЕМОНСТРАЦІЯ ПЕРЕТВОРЕНЬ ТИПІВ ===");

            double sampleValue = totalWeightKg > 0 ? totalWeightKg : 3.75;

            // 1) Неявне розширення
            double weightImplicit = sampleValue;

            // 2) Явне звуження зі втратою дробової частини 
            int weightExplicit = (int)sampleValue;

            // 3) Перетворення через Convert.ToInt32 
            int weightConvert = Convert.ToInt32(sampleValue);

            Console.WriteLine($"Вхідне дробове значення: {sampleValue}");
            Console.WriteLine($"1. Неявне розширення (double):          {weightImplicit} (Тип: {weightImplicit.GetType().Name})");
            Console.WriteLine($"2. Явне звуження (int) зі відкиданням:  {weightExplicit} (Тип: {weightExplicit.GetType().Name})");
            Console.WriteLine($"3. Округлення через Convert.ToInt32:    {weightConvert} (Тип: {weightConvert.GetType().Name})");
            Console.WriteLine();

            // МОДЕЛЮВАННЯ ПЕРЕПОВНЕННЯ 
            Console.WriteLine("=== РІВЕНЬ Б: МОДЕЛЮВАННЯ ПЕРЕПОВНЕННЯ (OVERFLOW) ===");

            int largeQuantity = 300;

            byte overflowByte = (byte)largeQuantity;

            Console.WriteLine($"Спроба примусово покласти int = {largeQuantity} у byte (max 255):");
            Console.WriteLine($"Результат без перевірки: {overflowByte} (сталася спотворення/втрата даних!)");

            // Вираз checked перевіряє переповнення під час виконання.
            //byte checkedByte = checked((byte)largeQuantity);
        }
    }
}