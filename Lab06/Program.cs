using System;
using System.Text;

namespace LibraryApp
{
    internal class Program
    {
        const int Capacity = 100;

        // паралельних масивів 
        static int[] ids = new int[Capacity];          
        static string[] titles = new string[Capacity];  
        static string[] authors = new string[Capacity]; 
        static int[] publishYears = new int[Capacity];   
        static string[] genres = new string[Capacity];   
        static int[] copyCounts = new int[Capacity];     
        static bool[] isIssued = new bool[Capacity];     

        static int count = 0;   // Скільки перших комірок реально заповнено
        static int nextId = 1;  // Лічильник

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Seed(); // записи як приклад

            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine(" АБІС \"Бібліотека\": Облік книжкового фонду ");
                Console.WriteLine("1 – Додати книгу");
                Console.WriteLine("2 – Показати всі книги");
                Console.WriteLine("3 – Знайти книгу за інвентарним номером");
                Console.WriteLine("4 – Видалити книгу за інвентарним номером");
                Console.WriteLine("0 – Вихід");
                Console.Write("Ваш вибір: ");

                string choice = (Console.ReadLine() ?? "").Trim();
                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        PrintAll();
                        break;
                    case "3":
                        FindById();
                        break;
                    case "4":
                        DeleteById();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Помилка: Немає такого пункту меню.");
                        break;
                }
            }

            Console.WriteLine("Роботу програми завершено.");
        }

        // Запис даних 
        static void Store(string title, string author, int year, string genre, int copies, bool issued)
        {
            ids[count] = nextId;
            titles[count] = title;
            authors[count] = author;
            publishYears[count] = year;
            genres[count] = genre;
            copyCounts[count] = copies;
            isIssued[count] = issued;

            count++;   
            nextId++;  
        }

        // Демонстраційні дані
        static void Seed()
        {
            Store("Кобзар", "Тарас Шевченко", 1840, "Поезія", 12, false);
            Store("Тигролови", "Іван Багряний", 1944, "Пригодницький роман", 5, true);
            Store("Місто", "Валер'ян Підмогильний", 1928, "Урбаністичний роман", 8, false);
        }

        // Пошук індексу книги за її інвентарним номером
        static int IndexOfId(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (ids[i] == id) return i;
            }
            return -1;
        }

        // 1: Додати книгу
        static void AddBook()
        {
            // Перевірка вводу інформації
            if (count >= Capacity)
            {
                Console.WriteLine($"Сховище заповнене: більше ніж {Capacity} книг не вміщується.");
                return;
            }

            Console.WriteLine("\n--- Нова книга ---");

            Console.Write("Назва книги: ");
            string title = (Console.ReadLine() ?? "").Trim();
            if (title.Length == 0)
            {
                Console.WriteLine("Назва не може бути порожньою. Книгу не додано.");
                return;
            }

            Console.Write("Автор: ");
            string author = (Console.ReadLine() ?? "").Trim();
            if (author.Length == 0)
            {
                Console.WriteLine("Автор не може бути порожнім. Книгу не додано.");
                return;
            }

            Console.Write("Рік видання: ");
            if (!int.TryParse(Console.ReadLine(), out int year) || year < 868 || year > DateTime.Now.Year)
            {
                Console.WriteLine($"Рік має бути цілим числом від 868 до {DateTime.Now.Year}. Книгу не додано.");
                return;
            }

            Console.Write("Жанр: ");
            string genre = (Console.ReadLine() ?? "").Trim();
            if (genre.Length == 0)
            {
                Console.WriteLine("Жанр не може бути порожнім. Книгу не додано.");
                return;
            }

            Console.Write("Кількість примірників: ");
            if (!int.TryParse(Console.ReadLine(), out int copies) || copies < 0)
            {
                Console.WriteLine("Кількість примірників має бути невід'ємним цілим числом. Книгу не додано.");
                return;
            }

            Console.Write("Видана зараз на руки? (1 - Так, 0 - Ні): ");
            string issuedInput = (Console.ReadLine() ?? "").Trim();
            bool issued = issuedInput == "1" || issuedInput.ToLower() == "так";

            Store(title, author, year, genre, copies, issued);
            Console.WriteLine($"Додано книгу №{ids[count - 1]} у позицію {count - 1}. Зайнято комірок: {count} зі {Capacity}.");
        }

        // 2: Показати всі книги
        static void PrintAll()
        {
            if (count == 0)
            {
                Console.WriteLine("Книжковий фонд порожній. Записів немає.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine(" Інв.№ | Назва                | Автор               | Рік  | Жанр                 | Прим. | Стан");
            Console.WriteLine("-------+----------------------+---------------------+------+----------------------+-------+----------");

            int totalCopies = 0;
            int issuedCount = 0;

            for (int i = 0; i < count; i++)
            {
                string state = isIssued[i] ? "Видана" : "В наявності";
                Console.WriteLine($"{ids[i],6} | {Truncate(titles[i], 20),-20} | {Truncate(authors[i], 19),-19} | {publishYears[i],4} | {Truncate(genres[i], 20),-20} | {copyCounts[i],5} | {state}");

                totalCopies += copyCounts[i];
                if (isIssued[i]) issuedCount++;
            }

            Console.WriteLine("-------+----------------------+---------------------+------+----------------------+-------+----------");
            Console.WriteLine($"Усього найменувань: {count}, загальний фонд: {totalCopies} прим., видано на руки: {issuedCount} найм.");
        }

        // Допоміжний метод для обрізання довгих рядків у таблиці
        static string Truncate(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Length <= maxLength ? text : text.Substring(0, maxLength - 3) + "...";
        }

        // 3: Знайти книгу за номером
        static void FindById()
        {
            Console.Write("Номер книги для пошуку: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Номер має бути цілим числом.");
                return;
            }

            int index = IndexOfId(id);
            if (index == -1)
            {
                Console.WriteLine($"Книги №{id} у списку немає.");
                return;
            }

            Console.WriteLine($"\nЗнайдено в позиції {index}:");
            Console.WriteLine($"  Інвентарний номер : {ids[index]}");
            Console.WriteLine($"  Назва             : {titles[index]}");
            Console.WriteLine($"  Автор             : {authors[index]}");
            Console.WriteLine($"  Рік видання       : {publishYears[index]}");
            Console.WriteLine($"  Жанр              : {genres[index]}");
            Console.WriteLine($"  Примірників       : {copyCounts[index]}");
            Console.WriteLine($"  Стан              : {(isIssued[index] ? "Видана на руки" : "В наявності у бібліотеці")}");
        }

        // Дія 4: Видалити книгу за номером (зсувом уліво)
        static void DeleteById()
        {
            Console.Write("Номер книги для видалення: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Номер має бути цілим числом.");
                return;
            }

            int index = IndexOfId(id);
            if (index == -1)
            {
                Console.WriteLine($"Книги №{id} у списку немає.");
                return;
            }

            Console.WriteLine($"Видалено книгу №{ids[index]} (\"{titles[index]}\", автор: {authors[index]}).");

            // Зсув уліво
            for (int i = index; i < count - 1; i++)
            {
                ids[i] = ids[i + 1];
                titles[i] = titles[i + 1];
                authors[i] = authors[i + 1];
                publishYears[i] = publishYears[i + 1];
                genres[i] = genres[i + 1];
                copyCounts[i] = copyCounts[i + 1];
                isIssued[i] = isIssued[i + 1];
            }

            count--; 

            // Очищуємо вивільнену останню комірку 
            ids[count] = 0;
            titles[count] = "";
            authors[count] = "";
            publishYears[count] = 0;
            genres[count] = "";
            copyCounts[count] = 0;
            isIssued[count] = false;

            Console.WriteLine($"Зайнято комірок: {count} зі {Capacity}.");
        }
    }
}