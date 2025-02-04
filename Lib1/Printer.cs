/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

using System.Collections;
using System.Globalization;

namespace Lib1
{
    /// <summary>
    /// Предоставляет методы для форматированного вывода информации.
    /// </summary>
    public static class Printer
    {
        /// <summary>
        /// Печатает строку, разбивая её на части, если визуальная длина превышает maxLength.
        /// </summary>
        /// <param name="text">Выводимый текст.</param>
        /// <param name="maxLength">Максимальная визуальная длина строки.</param>
        private static void PrintWrappedLine(string text, int maxLength)
        {
            text = text.Trim();
            int val = GetStringVisualWidth(text);
            if (val > maxLength && !(GetStringVisualWidth(text) == maxLength + 1 && text[^1] == '-'))
            {
                string firstPart = text[..CutStringVisualWidth(text, maxLength)];

                if (firstPart[^1] != ' ')
                {
                    firstPart += "-";
                }

                string secondPart = text[CutStringVisualWidth(text, maxLength)..];
                PrintWrappedLine(firstPart.Trim(), maxLength);
                PrintWrappedLine(secondPart.Trim(), maxLength);
            }
            else
            {
                Console.Write("\u2502 " + text);
                int spaceCount = maxLength + 1 - GetStringVisualWidth(text);
                Console.WriteLine(new string(' ', Math.Max(spaceCount, 0)) + " \u2502\u2591");
            }
        }

        /// <summary>
        /// Возвращает строковое представление объекта.
        /// </summary>
        /// <param name="obj">Объект для преобразования.</param>
        /// <returns>Строковое представление объекта.</returns>
        private static string GetString(object obj)
        {
            string res = "";
            switch (obj)
            {
                case int s:
                    return s.ToString();
                case double s:
                    return s.ToString(CultureInfo.InvariantCulture);
                case string s:
                    return s;
                case bool s:
                    return s.ToString();
                case IList list:
                    res = "";
                    foreach (object variable in list)
                    {
                        res += GetString(variable!) + ", ";
                    }

                    return res.Length >= 2 ? res[..^2] : res;
                case IDictionary dict:
                    {
                        res = "";
                        foreach (DictionaryEntry entry in dict)
                        {
                            string keyString = GetString(entry.Key).Trim();
                            string valueString = GetString(entry.Value!).Trim();
                            res += $"{keyString}: {valueString}; ";
                        }

                        return res.Length >= 2 ? res[..^2] : res;
                    }
                default:
                    return obj.ToString() ?? res;
            }
        }

        /// <summary>
        /// Печатает элемент словаря (ключ-значение) в формате.
        /// </summary>
        /// <param name="key">Ключ элемента.</param>
        /// <param name="value">Значение элемента.</param>
        /// <param name="totalWidth">Общая ширина форматированной строки.</param>
        private static void PrintDictItem(string key, object value, int totalWidth)
        {
            Console.WriteLine($"\u251c{new string('\u2500', totalWidth - 3)}\u2524\u2591");
            Console.WriteLine($"{new string('.', totalWidth)}");
            Console.WriteLine($"\u251c{new string('\u2500', totalWidth - 3)}\u2524\u2591");

            PrintWrappedLine(key, totalWidth - 6);
            Console.WriteLine($"\u2502 {new string('\u2193', totalWidth - 5)} \u2502\u2591");

            PrintWrappedLine(GetString(value).Trim('"'), totalWidth - 6);
        }

        /// <summary>
        /// Определяет индекс, до которого визуальная ширина строки не превышает maxVal.
        /// </summary>
        /// <param name="input">Входная строка.</param>
        /// <param name="maxVal">Максимальная визуальная ширина.</param>
        /// <returns>Индекс разреза строки.</returns>
        private static int CutStringVisualWidth(string input, int maxVal)
        {
            int i = 0;
            int width = 0;
            foreach (char c in input)
            {
                if (width >= maxVal)
                {
                    return i - 1;
                }

                width += IsWideCharacter(c) ? 2 : 1;
                i++;
            }

            return i;
        }

        /// <summary>
        /// Вычисляет визуальную ширину строки с учетом широких символов.
        /// </summary>
        /// <param name="input">Входная строка.</param>
        /// <returns>Визуальная ширина строки.</returns>
        private static int GetStringVisualWidth(string input)
        {
            int width = 0;
            foreach (char c in input)
            {
                width += IsWideCharacter(c) ? 2 : 1;
            }

            return width;
        }

        /// <summary>
        /// Определяет, является ли символ широким.
        /// </summary>
        /// <param name="c">Символ для проверки.</param>
        /// <returns>True, если символ широкий, иначе false.</returns>
        private static bool IsWideCharacter(char c)
        {
            return c is (>= (char)0x1100 and <= (char)0x115F) or
                (>= (char)0x2E80 and <= (char)0xA4CF) or
                (>= (char)0xAC00 and <= (char)0xD7A3) or
                (>= (char)0xF900 and <= (char)0xFAFF) or
                (>= (char)0xFE10 and <= (char)0xFE6F) or
                (>= (char)0xFF00 and <= (char)0xFF60) or
                (>= (char)0xFFE0 and <= (char)0xFFE6);
        }

        /// <summary>
        /// Печатает полное описание способности.
        /// </summary>
        /// <param name="ability">Способность для печати.</param>
        public static void Print(Ability ability)
        {
            string id = ability.Id.Trim('"').Trim();
            string label = ability.Label.Trim('"').Trim();
            string desc = ability.Description.Trim('"').Trim();
            Dictionary<string, object> triggers = ability.Xtriggers;

            int lenStr = GetStringVisualWidth(
                $"\u2502 \u2551 ID: {id} {new string(' ', Math.Max(4 - id.Length, 0))} \u2551 \u2551 Label: {label} \u2551 \u2502\u2591"
            );

            Console.WriteLine($"\u250c{new string('\u2500', lenStr - 3)}\u2510");
            Console.WriteLine(
                $"\u2502 \u2554{new string('\u2550', GetStringVisualWidth(id) + 7)}\u2557 \u2554{new string('\u2550', GetStringVisualWidth(label) + 9)}\u2557 \u2502\u2591");
            Console.WriteLine(
                $"\u2502 \u2551 ID: {id} {new string(' ', Math.Max(4 - GetStringVisualWidth(id), 0))} \u2551 \u2551 Label: {label} \u2551 \u2502\u2591");
            Console.WriteLine(
                $"\u2502 \u2560{new string('\u2550', GetStringVisualWidth(id) + 7)}\u255d \u255a{new string('\u2550', GetStringVisualWidth(label) + 9)}\u2563 \u2502\u2591");
            Console.WriteLine($"\u255e\u2550\u2569{new string('\u2550', lenStr - 7)}\u2569\u2550\u2561\u2591");
            Console.WriteLine($"\u2502 Description:{new string(' ', lenStr - 16)}\u2502\u2591");

            PrintWrappedLine(desc, lenStr - 6);

            Console.WriteLine($"\u255e{new string('\u2550', lenStr - 3)}\u2561\u2591");
            Console.WriteLine($"\u2502 Triggers:{new string(' ', lenStr - 13)}\u2502\u2591");

            foreach (string key in triggers.Keys)
            {
                PrintDictItem(key, triggers[key], lenStr);
            }

            Console.WriteLine($"\u251c{new string('\u2500', lenStr - 3)}\u2524\u2591");
            Console.WriteLine(new string('.', lenStr));
            Console.WriteLine($"\u2514{new string('\u2500', lenStr - 3)}\u2518\u2591");
            Console.WriteLine($" {new string('\u2591', lenStr - 1)}");
        }
    }
}