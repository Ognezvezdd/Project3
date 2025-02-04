/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

using System.Text;

namespace Lib1
{
    /// <summary>
    /// Класс для работы с файлами и путями. Пути считаются относительными базовому каталогу приложения.
    /// </summary>
    public static class FileManager
    {
        private const string DefaultInputPath = "./BoH/loc_ru/elements/abilities.json";

        /// <summary>
        /// Перенаправляет вывод в файл, путь к которому указывает пользователь.
        /// </summary>
        public static bool SetOutputToFile()
        {
            try
            {
                Console.WriteLine("Введите относительный путь для сохранения файла...");
                string? path = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException();
                }

                StreamWriter writer = GetFileWriter(path);
                Console.SetOut(writer);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                SetStandardOutput();
                ConsoleManager.WriteWarn("Нет доступа к файлу для записи.");
                return false;
            }
            catch (IOException)
            {
                SetStandardOutput();
                ConsoleManager.WriteWarn("Ошибка ввода/вывода при создании файла или файл уже существует.");
                return false;
            }
            catch (ArgumentNullException)
            {
                SetStandardOutput();
                ConsoleManager.WriteWarn("Введите корректный путь");
                return false;
            }
            catch (Exception)
            {
                SetStandardOutput();
                ConsoleManager.WriteWarn("Произошла непредвиденная ошибка при создании файла.");
                return false;
            }
        }

        /// <summary>
        /// Перенаправляет стандартный ввод.
        /// </summary>
        public static void SetStandardInput()
        {
            Stream inputStream = Console.OpenStandardInput();
            Console.SetIn(new StreamReader(inputStream));
        }

        /// <summary>
        /// Перенаправляет стандартный вывод.
        /// </summary>
        public static void SetStandardOutput()
        {
            Console.Out.Close(); // Без этой строчки не гарантируется полный вывод в файл. Часть данных остается в буфере
            Stream outputStream = Console.OpenStandardOutput();
            Console.SetOut(new StreamWriter(outputStream) { AutoFlush = true });
            Console.OutputEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// Проверяет, соответствует ли JSON объект требуемому формату.
        /// </summary>
        /// <param name="jsonObject">Объект JsonObject.</param>
        /// <returns>True, если JSON валиден; иначе false.</returns>
        public static bool CheckValidFile(JsonObject jsonObject)
        {
            try
            {
                Dictionary<string, object> data = JsonRasParser.RealParseJson(jsonObject.GetField("elements"));
                List<object> unused = (List<object>)data["Vals"];
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Возвращает StreamReader для файла по относительному пути. Если путь пустой, используется путь по умолчанию.
        /// </summary>
        /// <param name="relativePath">Относительный путь к файлу.</param>
        /// <returns>StreamReader для чтения файла.</returns>
        /// <exception cref="FileNotFoundException">Если файл не найден по сформированному абсолютному пути.</exception>
        public static StreamReader GetFileReader(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                relativePath = DefaultInputPath;
                ConsoleManager.WriteWarn("Пустой ввод заменен стандартным путем к файлу");
            }

            string fullPath = GetAbsolutePath(relativePath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"Файл не найден по пути: {fullPath}");
            }

            return new StreamReader(fullPath);
        }

        /// <summary>
        /// Возвращает StreamWriter для файла по относительному пути. Выбрасывает исключение, если файл уже существует.
        /// </summary>
        /// <param name="relativePath">Относительный путь к файлу.</param>
        /// <returns>StreamWriter для записи в файл.</returns>
        /// <exception cref="ArgumentException">Если путь пустой.</exception>
        /// <exception cref="IOException">Если файл уже существует.</exception>
        private static StreamWriter GetFileWriter(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                throw new ArgumentException("Путь не может быть пустым.", nameof(relativePath));
            }

            string fullPath = GetAbsolutePath(relativePath);
            if (File.Exists(fullPath))
            {
                throw new IOException($"Файл уже существует по пути: {fullPath}");
            }

            return new StreamWriter(fullPath);
        }

        /// <summary>
        /// Преобразует относительный путь в абсолютный относительно базового каталога приложения.
        /// </summary>
        /// <param name="relativePath">Относительный путь.</param>
        /// <returns>Абсолютный путь.</returns>
        private static string GetAbsolutePath(string relativePath)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDir, relativePath);
        }
    }
}