/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

// Для дополнительной задачи
// using Lib1.AdditionalTask;

namespace Lib1
{
    /// <summary>
    /// Предоставляет набор статических методов для обработки ввода пользователя,
    /// включая ввод данных, фильтрацию, сортировку, выполнение задач и вывод данных.
    /// </summary>
    public abstract class Menu
    {
        /// <summary>
        /// Позволяет пользователю ввести данные с клавиатуры или из файла.
        /// Настраивает соответствующий режим ввода и обрабатывает JSON-данные.
        /// </summary>
        /// <param name="dataBase">Ссылка на базу данных, которая будет обновлена введёнными данными.</param>
        /// <param name="json">Ссылка на JSON-объект, используемый для парсинга и обновления данных.</param>
        public static void EnterData(ref DataBase dataBase, ref JsonObject? json)
        {
            Console.WriteLine("1. Установить режим ввода с клавиатуры");
            Console.WriteLine("2. Ввести путь и считать данные из файла");

            Console.Write("Выберите метод ввода данных: ");
            string? input = Console.ReadLine();
            string inputText = "";
            switch (input)
            {
                case "1":
                    FileManager.SetStandardInput();
                    Console.WriteLine("Введите данные через консоль (Ctrl + V)");
                    Console.WriteLine("Для завершения ввода вызовите EOF");
                    string? curr = Console.ReadLine();
                    while (curr != null)
                    {
                        inputText += curr;
                        curr = Console.ReadLine();
                    }

                    break;
                case "2":
                    Console.WriteLine("Введите относительный путь к файлу...");
                    try
                    {
                        string? path = Console.ReadLine();
                        StreamReader reader = FileManager.GetFileReader(path);
                        Console.SetIn(reader);
                    }
                    catch (FileNotFoundException)
                    {
                        json = null;
                        FileManager.SetStandardInput();
                        ConsoleManager.WriteWarn("Файл не найден");
                        return;
                    }
                    catch (IOException)
                    {
                        json = null;
                        FileManager.SetStandardInput();
                        ConsoleManager.WriteWarn("Ошибка ввода/вывода при чтении файла");
                        return;
                    }
                    catch (AccessViolationException)
                    {
                        FileManager.SetStandardInput();
                        ConsoleManager.WriteWarn("Нет доступа");
                        return;
                    }
                    catch (Exception)
                    {
                        json = null;
                        FileManager.SetStandardInput();
                        ConsoleManager.WriteWarn("Непредвиденная ошибка с файлом. Ввод сброшен");
                        return;
                    }

                    break;
                default:
                    Console.WriteLine("Неверный ввод.");
                    return;
            }

            try
            {
                if (json == null)
                {
                    return;
                }

                JsonParser.ReadJson(json, inputText);
                dataBase.Update(json);
            }
            catch (FormatException)
            {
                ConsoleManager.WriteWarn("Некорректный формат JSON");
            }
            catch (ArgumentException)
            {
                ConsoleManager.WriteWarn("Некорректный аргумент");
            }
            catch (Exception)
            {
                ConsoleManager.WriteWarn("Файл некорректный. Непредвиденная ошибка");
            }
            finally
            {
                FileManager.SetStandardInput();
            }
        }

        /// <summary>
        /// Применяет фильтрацию данных в базе на основе ввода пользователя.
        /// </summary>
        /// <param name="dataBase">Ссылка на базу данных, к которой применяется фильтрация.</param>
        public static void FilterData(ref DataBase dataBase)
        {
            dataBase.WriteFilters();
            Console.WriteLine("Введите аргумент по которому будет происходить фильтрация");
            string? by = Console.ReadLine();
            Console.WriteLine("Введите через пробел доступные значения для этого аргумента");
            string? possibleArgs = Console.ReadLine();

            try
            {
                dataBase.FilterBy(by, possibleArgs);
                Console.WriteLine("Фильтрация произведена");
            }
            catch (RankException)
            {
                ConsoleManager.WriteWarn("Фильтры не установлены");
            }
            catch (ArgumentOutOfRangeException)
            {
                dataBase.ResetFilters();
                ConsoleManager.WriteWarn("ВСЕ ФИЛЬТРЫ СБРОШЕНЫ");
            }
            catch (ArgumentNullException)
            {
                dataBase.ResetFilters();
                ConsoleManager.WriteWarn("ВСЕ ФИЛЬТРЫ СБРОШЕНЫ");
            }
            catch (NullReferenceException)
            {
                dataBase.ResetFilters();
                ConsoleManager.WriteWarn("ВСЕ ФИЛЬТРЫ СБРОШЕНЫ");
            }
            catch (Exception)
            {
                ConsoleManager.WriteWarn("Непредвиденная ошибка. Что вы сделали?");
                dataBase.ResetFilters();
                ConsoleManager.WriteWarn("ВСЕ ФИЛЬТРЫ СБРОШЕНЫ");
            }
        }

        /// <summary>
        /// Сортирует данные в базе на основе ввода пользователя.
        /// </summary>
        /// <param name="dataBase">Ссылка на базу данных, которую необходимо отсортировать.</param>
        public static void SortData(ref DataBase dataBase)
        {
            Console.WriteLine("Сортировка данных...");
            Console.WriteLine("Введите аргумент по которому будет происходить сортировка");
            string? arg = Console.ReadLine();
            Console.WriteLine("Выберите возрастающая (UP) или убывающая (DOWN) сортировка");
            string? asc = Console.ReadLine();

            try
            {
                dataBase.SortBy(arg, asc);
                Console.WriteLine("Данные отсортированы успешно");
            }
            catch (RankException)
            {
                ConsoleManager.WriteWarn("Фильтры не установлены");
            }
            catch (ArgumentOutOfRangeException)
            {
                dataBase.ResetFilters();
                ConsoleManager.WriteWarn("ВСЕ ФИЛЬТРЫ СБРОШЕНЫ");
            }
            catch (ArgumentNullException)
            {
                dataBase.ResetFilters();
                ConsoleManager.WriteWarn("ВСЕ ФИЛЬТРЫ СБРОШЕНЫ");
            }
            catch (NullReferenceException)
            {
                dataBase.ResetFilters();
                ConsoleManager.WriteWarn("ВСЕ ФИЛЬТРЫ СБРОШЕНЫ");
            }
            catch (Exception)
            {
                ConsoleManager.WriteWarn("Непредвиденная ошибка. Что вы сделали?");
                dataBase.ResetFilters();
                ConsoleManager.WriteWarn("ВСЕ ФИЛЬТРЫ СБРОШЕНЫ");
            }
        }

        /// <summary>
        /// Выполняет основную задачу индивидуального варианта,
        /// получая возможность по идентификатору и выводя её.
        /// </summary>
        /// <param name="database">Ссылка на базу данных, из которой будет получена возможность.</param>
        public static void MainTask(ref DataBase database)
        {
            Console.WriteLine("Введите id возможности");
            Ability x;
            try
            {
                x = database.GetById(Console.ReadLine());
            }
            catch (KeyNotFoundException)
            {
                ConsoleManager.WriteWarn("Несуществующий id");
                return;
            }
            catch (Exception)
            {
                ConsoleManager.WriteWarn("Непредвиденная ошибка. Что вы сделали?");
                return;
            }

            try
            {
                Printer.Print(x);
            }
            catch (InvalidOperationException)
            {
                ConsoleManager.WriteWarn("Ошибка при выводе: неверная операция");
            }
            catch (Exception)
            {
                ConsoleManager.WriteWarn("Неизвестная Ошибка при выводе. Введите корректный JSON при следующем вызове");
            }
        }

        /// <summary>
        /// Выполняет дополнительную задачу индивидуального варианта.
        /// </summary>
        public static void TaskTwo(DataBase database)
        {
            ConsoleManager.WriteGreen("Метод не реализован. Я не усепл(");
            // Для дополнительной задачи
            // AdditionalTask.AdditionalTask start = new(database);
        }

        /// <summary>
        /// Выводит данные базы пользователю, либо на консоль, либо в файл.
        /// </summary>
        /// <param name="dataBase">База данных, данные которой необходимо вывести.</param>
        public static void DisplayData(DataBase dataBase)
        {
            Console.WriteLine("1. Вывести данные в консоль");
            Console.WriteLine("2. Сохранить данные в файл");
            Console.Write("Выберите метод вывода данных: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Данные для вывода в консоль...");
                    JsonParser.WriteJson(dataBase);
                    break;
                case "2":
                    bool isCorrect = FileManager.SetOutputToFile();
                    if (isCorrect)
                    {
                        JsonParser.WriteJson(dataBase);
                    }
                    else
                    {
                        ConsoleManager.WriteWarn("Ошибка вывода");
                    }

                    FileManager.SetStandardOutput();
                    break;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }
}