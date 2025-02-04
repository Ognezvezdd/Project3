/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

using Lib1;
using System.Text;

namespace Project3
{
    /// <summary>
    /// Основной класс программы, содержащий точку входа.
    /// Этот класс управляет настройкой ввода/вывода, обработкой меню и вызовом соответствующих задач.
    /// </summary>
    internal abstract class Program
    {
        /// <summary>
        /// Главная точка входа в приложение.
        /// Инициализирует менеджер файлов, создаёт объекты базы данных и JSON объекта, затем запускает цикл обработки пользовательского меню.
        /// </summary>
        private static void Main()
        {
            // Настройка стандартного ввода и вывода.
            FileManager.SetStandardInput();
            FileManager.SetStandardOutput();
            Console.OutputEncoding = Encoding.UTF8;


            DataBase dataBase = new();
            JsonObject ourJsonObject = new();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Ввести данные (консоль/файл)");
                Console.WriteLine("2. Отфильтровать данные");
                Console.WriteLine("3. Отсортировать данные");
                Console.WriteLine("3.5. Сбросить все фильтры и сортировки (вернуть к исходнику)");
                Console.WriteLine("4. Основная задача индивидуального варианта");
                Console.WriteLine("5. Дополнительная задача индивидуального варианта");
                Console.WriteLine("6. Вывести данные (консоль/файл)");
                Console.WriteLine("7. Выход");

                Console.Write("Введите номер пункта: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Menu.EnterData(ref dataBase, ref ourJsonObject!);
                        break;

                    case "2":
                        if (!FileManager.CheckValidFile(ourJsonObject))
                        {
                            ConsoleManager.WriteWarn("Файл не считан");
                            break;
                        }

                        Menu.FilterData(ref dataBase);
                        break;

                    case "3":
                        if (!FileManager.CheckValidFile(ourJsonObject))
                        {
                            ConsoleManager.WriteWarn("Файл не считан");
                            break;
                        }

                        Menu.SortData(ref dataBase);
                        break;

                    case "3.5":
                        if (!FileManager.CheckValidFile(ourJsonObject))
                        {
                            ConsoleManager.WriteWarn("Файл не считан");
                            break;
                        }

                        dataBase.ResetFilters();
                        Console.WriteLine("Фильтры сброшены!");
                        break;

                    case "4":
                        if (!FileManager.CheckValidFile(ourJsonObject))
                        {
                            ConsoleManager.WriteWarn("Файл не считан");
                            break;
                        }

                        Menu.MainTask(ref dataBase);
                        break;

                    case "5":
                        if (!FileManager.CheckValidFile(ourJsonObject))
                        {
                            ConsoleManager.WriteWarn("Файл не считан");
                            break;
                        }

                        Menu.TaskTwo(dataBase);
                        break;

                    case "6":
                        if (!FileManager.CheckValidFile(ourJsonObject))
                        {
                            ConsoleManager.WriteWarn("Файл не считан");
                            break;
                        }

                        Menu.DisplayData(dataBase);
                        break;

                    case "7":
                        exit = true;
                        Console.WriteLine("Выход из программы...");
                        break;

                    default:
                        ConsoleManager.WriteWarn("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}