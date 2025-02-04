/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

using System.Text;

namespace Lib1
{
    /// <summary>
    /// Класс для парсинга JSON-строк.
    /// </summary>
    internal static class JsonRasParser
    {
        /// <summary>
        /// Извлекает ключ и остаток JSON-строки.
        /// </summary>
        /// <param name="json">Строка JSON с парой "ключ":значение.</param>
        /// <returns>Кортеж с ключом и оставшейся частью строки.</returns>
        /// <exception cref="FormatException">Неверный формат JSON.</exception>
        public static (string Key, string RemainingJson) ExtractKeyAndRemaining(string json)
        {
            json = json.Trim().Trim('}').Trim('{').Trim();
            int index = 0;
            SkipWhitespaceAndCheckEnd(json, ref index);

            if (json[index] == '"')
            {
                string key = ParseString(json, ref index);
                SkipWhitespaceAndCheckEnd(json, ref index);
                if (json[index] != ':')
                {
                    throw new FormatException($"Expected ':' at position {index}");
                }

                index++;
                string remainingJson = json.Substring(index).Trim();
                return (key, remainingJson);
            }

            throw new FormatException($"Expected '\"' at position {index}");
        }

        /// <summary>
        /// Парсит JSON-строку в словарь.
        /// </summary>
        /// <param name="json">Строка JSON с парами ключ-значение.</param>
        /// <returns>Словарь с разобранными данными.</returns>
        /// <exception cref="FormatException">Неверный формат JSON.</exception>
        public static Dictionary<string, object> RealParseJson(string json)
        {
            json = "\"Vals\":" + json;
            Dictionary<string, object> result = new();
            int index = 0;

            while (index < json.Length)
            {
                if (!SkipWhitespaceAndCheckEnd(json, ref index))
                {
                    break;
                }

                if (json[index] == '"')
                {
                    string key = ParseString(json, ref index);
                    if (!SkipWhitespaceAndCheckEnd(json, ref index))
                    {
                        break;
                    }

                    if (json[index] != ':')
                    {
                        throw new FormatException($"Expected ':' at position {index}");
                    }

                    index++;
                    if (!SkipWhitespaceAndCheckEnd(json, ref index))
                    {
                        break;
                    }

                    object value = ParseValue(json, ref index);
                    result[key] = value;
                    if (!SkipWhitespaceAndCheckEnd(json, ref index))
                    {
                        break;
                    }

                    if (json[index] == ',')
                    {
                        index++;
                    }
                }
                else
                {
                    index++;
                }
            }

            return result;
        }

        /// <summary>
        /// Парсит строку, заключённую в двойные кавычки.
        /// </summary>
        /// <param name="json">JSON-строка.</param>
        /// <param name="index">
        /// Позиция начала строки; после парсинга указывает на символ после закрывающей кавычки.
        /// </param>
        /// <returns>Извлечённое строковое значение.</returns>
        private static string ParseString(string json, ref int index)
        {
            StringBuilder sb = new();
            index++; // пропускаем открывающую кавычку

            while (index < json.Length && json[index] != '"')
            {
                sb.Append(json[index]);
                index++;
            }

            index++; // пропускаем закрывающую кавычку
            return sb.ToString();
        }

        /// <summary>
        /// Парсит значение JSON.
        /// </summary>
        /// <param name="json">JSON-строка.</param>
        /// <param name="index">Позиция начала значения.</param>
        /// <returns>Распознанное значение.</returns>
        /// <exception cref="FormatException">Неверный формат JSON.</exception>
        private static object ParseValue(string json, ref int index)
        {
            if (json[index] == '"')
            {
                return ParseString(json, ref index);
            }

            if (char.IsDigit(json[index]) || json[index] == '-')
            {
                return ParseNumber(json, ref index);
            }

            if (json[index] == '{')
            {
                return ParseObject(json, ref index);
            }

            if (json[index] == '[')
            {
                return ParseArray(json, ref index);
            }

            if (IsTrueOrFalse(json, ref index, out bool boolValue))
            {
                return boolValue;
            }

            throw new FormatException($"Unexpected character at position {index}");
        }

        /// <summary>
        /// Парсит JSON-объект.
        /// </summary>
        /// <param name="json">JSON-строка.</param>
        /// <param name="index">Позиция начала объекта.</param>
        /// <returns>Словарь с данными объекта.</returns>
        /// <exception cref="FormatException">Неверный формат JSON.</exception>
        private static Dictionary<string, object> ParseObject(string json, ref int index)
        {
            Dictionary<string, object> obj = new();
            index++; // пропускаем открывающую фигурную скобку

            while (index < json.Length && json[index] != '}')
            {
                if (!SkipWhitespaceAndCheckEnd(json, ref index))
                {
                    break;
                }

                string key = ParseString(json, ref index);
                if (!SkipWhitespaceAndCheckEnd(json, ref index))
                {
                    break;
                }

                if (json[index] != ':')
                {
                    throw new FormatException($"Expected ':' at position {index}");
                }

                index++;
                if (!SkipWhitespaceAndCheckEnd(json, ref index))
                {
                    break;
                }

                object value = ParseValue(json, ref index);
                obj[key] = value;
                if (!SkipWhitespaceAndCheckEnd(json, ref index))
                {
                    break;
                }

                if (json[index] == ',')
                {
                    index++;
                }
            }

            index++; // пропускаем закрывающую фигурную скобку
            return obj;
        }

        /// <summary>
        /// Парсит JSON-массив.
        /// </summary>
        /// <param name="json">JSON-строка.</param>
        /// <param name="index">Позиция начала массива.</param>
        /// <returns>Список элементов массива.</returns>
        private static List<object> ParseArray(string json, ref int index)
        {
            List<object> array = new();
            index++; // пропускаем открывающую квадратную скобку

            while (index < json.Length && json[index] != ']')
            {
                if (!SkipWhitespaceAndCheckEnd(json, ref index))
                {
                    break;
                }

                object value = ParseValue(json, ref index);
                array.Add(value);
                if (!SkipWhitespaceAndCheckEnd(json, ref index))
                {
                    break;
                }

                if (json[index] == ',')
                {
                    index++;
                }
            }

            index++; // пропускаем закрывающую квадратную скобку
            return array;
        }

        /// <summary>
        /// Парсит число из JSON.
        /// </summary>
        /// <param name="json">JSON-строка.</param>
        /// <param name="index">Позиция начала числа.</param>
        /// <returns>Число в формате double.</returns>
        private static double ParseNumber(string json, ref int index)
        {
            int start = index;
            while (index < json.Length && (char.IsDigit(json[index]) || json[index] == '.' || json[index] == '-'))
            {
                index++;
            }

            string numberStr = json.Substring(start, index - start);
            return double.Parse(numberStr);
        }

        /// <summary>
        /// Определяет, начинается ли значение с "true" или "false".
        /// </summary>
        /// <param name="json">JSON-строка.</param>
        /// <param name="index">Позиция проверки.</param>
        /// <param name="value">Выходное логическое значение.</param>
        /// <returns>True, если найдено логическое значение.</returns>
        private static bool IsTrueOrFalse(string json, ref int index, out bool value)
        {
            if (json.Substring(index).StartsWith("true"))
            {
                value = true;
                index += 4;
                return true;
            }

            if (json.Substring(index).StartsWith("false"))
            {
                value = false;
                index += 5;
                return true;
            }

            value = false;
            return false;
        }

        /// <summary>
        /// Пропускает пробельные символы в строке.
        /// </summary>
        /// <param name="json">JSON-строка.</param>
        /// <param name="index">Текущая позиция в строке.</param>
        /// <returns>True, если ещё остались символы.</returns>
        private static bool SkipWhitespaceAndCheckEnd(string json, ref int index)
        {
            while (index < json.Length && char.IsWhiteSpace(json[index]))
            {
                index++;
            }

            return index < json.Length;
        }
    }
}