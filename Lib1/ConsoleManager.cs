/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

namespace Lib1
{
    /// <summary>
    /// Управляет выводом в консоль с поддержкой цветного текста.
    /// </summary>
    public static class ConsoleManager
    {
        /// <summary>
        /// Выводит объект в консоль указанным цветом.
        /// </summary>
        /// <param name="obj">Объект для вывода.</param>
        /// <param name="color">Цвет текста.</param>
        private static void WriteColorLine(object obj, ConsoleColor color)
        {
            ConsoleColor beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(obj.ToString());
            Console.ForegroundColor = beforeColor;
        }

        /// <summary>
        /// Выводит предупреждающее сообщение темно-красным цветом.
        /// </summary>
        /// <param name="obj">Сообщение для вывода.</param>
        public static void WriteWarn(object obj)
        {
            WriteColorLine(obj, ConsoleColor.DarkRed);
        }

        /// <summary>
        /// Выводит сообщение зелёным цветом.
        /// </summary>
        /// <param name="obj">Сообщение для вывода.</param>
        public static void WriteGreen(object obj)
        {
            WriteColorLine(obj, ConsoleColor.Green);
        }
    }
}