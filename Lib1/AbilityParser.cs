/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

using System.Globalization;
using System.Text;

namespace Lib1
{
    public static class AbilityParser
    {
        /// <summary>
        /// Возвращает объект в строку в формате JSON.
        /// Для строк добавляются кавычки (если isNeedToAddQuotes == true),
        /// для чисел и булевых значений кавычки не используются.
        /// </summary>
        /// <param name="obj">Объект для обработки.</param>
        /// <param name="isNeedToAddQuotes">
        /// Флаг, указывающий, нужно ли оборачивать строки в кавычки.
        /// Используется при обработке строковых значений.
        /// </param>
        /// <returns>Обработанное представление объекта.</returns>
        public static string Parser(object? obj, bool isNeedToAddQuotes = true)
        {
            if (obj == null)
            {
                return "";
            }
            StringBuilder res = new();

            switch (obj)
            {
                case int i:
                    res.Append(i);
                    break;

                case long l:
                    res.Append(l);
                    break;

                case double d:
                    res.Append(d.ToString(CultureInfo.InvariantCulture));
                    break;

                case bool b:
                    res.Append(b.ToString().ToLower());
                    break;

                case string str:
                    string escaped = str.Replace("\"", "\\\"");
                    if (isNeedToAddQuotes)
                    {
                        res.Append($"\"{escaped}\"");
                    }
                    else
                    {
                        res.Append(escaped);
                    }

                    break;

                case Dictionary<string, object> dict:
                    res.Append("{");
                    res.Append("\n");
                    res.Append(string.Join(", ", dict.Select(kv =>
                        $"\"{Parser(kv.Key, false)}\": {Parser(kv.Value)}")));
                    res.Append("\n");
                    res.Append("}");
                    break;

                case List<object> list:
                    res.Append("[");
                    res.Append("\n");
                    res.Append(string.Join(", ", list.Select(item => Parser(item))));
                    res.Append("\n");
                    res.Append("]");
                    break;

                default:
                    string objStr = obj.ToString() ?? "";
                    string escapedObj = objStr.Replace("\"", "\\\"");
                    if (isNeedToAddQuotes)
                    {
                        res.Append($"\"{escapedObj}\"");
                    }
                    else
                    {
                        res.Append(escapedObj);
                    }

                    break;
            }

            return res.ToString();
        }
    }
}