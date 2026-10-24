using System;
using System.Text;

namespace Lab01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Встановлення кодування UTF-8
            Console.OutputEncoding = Encoding.UTF8;

            // Верхня межа рамки
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");

            Console.WriteLine("║                    ВІЗИТІВКА СТУДЕНТА                     ║");
            Console.WriteLine("║                                                           ║");

            Console.Write("║            Студент: ");
            Console.WriteLine("Арович Марія Михайлівна               ║");

            Console.WriteLine("║                     Група: \"КН-22\"                        ║");

            Console.WriteLine("║            Спеціальність:\t121 Інженерія ПЗ            ║");

            Console.WriteLine("║                     Рік вступу: 2025                      ║");

            // Нижня межа рамки
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");

            Console.WriteLine("\n--------------------------------------------------------");
            Console.Write("Оновлено: ");
            Console.WriteLine("1 вересня 2025 р.");

        }
    }
}