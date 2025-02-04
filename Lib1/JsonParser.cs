using System.Text;

namespace Lib1
{
    /// <summary>
    /// Предоставляет методы для записи и чтения JSON-данных.
    /// Данный класс содержит статические методы для вывода JSON-представления объектов в консоль
    /// и для чтения JSON-данных из строки или стандартного ввода.
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Выводит представление JSON-объекта в консоль.
        /// </summary>
        /// <param name="jsonObject">
        /// Объект, реализующий функциональность JSON, который необходимо вывести.
        /// </param>
        /// <remarks>
        /// Метод выводит фигурные скобки и строковое представление JSON-объекта, 
        /// форматируя его для отображения в консоли.
        /// </remarks>
        public static void WriteJson(JsonObject jsonObject)
        {
            Console.WriteLine("{");
            Console.WriteLine(jsonObject.ToString());
            Console.WriteLine();
            Console.WriteLine("}");
        }

        /// <summary>
        /// Выводит строковое представление базы данных в консоль.
        /// </summary>
        /// <param name="dataBase">
        /// Объект базы данных, данные которого будут выведены.
        /// </param>
        /// <remarks>
        /// Метод использует метод <see cref="DataBase.ToString"/> для получения строкового представления базы данных.
        /// </remarks>
        public static void WriteJson(DataBase dataBase)
        {
            string toOutput = dataBase.ToString();
            Console.WriteLine(toOutput);
        }

        /// <summary>
        /// Читает JSON-данные и заполняет указанный JSON-объект.
        /// </summary>
        /// <param name="jsonObject">
        /// Объект, который будет заполнен данными, полученными из JSON.
        /// </param>
        /// <param name="strings">
        /// Необязательная строка с данными JSON. Если строка не пустая, данные берутся из неё, 
        /// иначе происходит чтение данных из стандартного ввода.
        /// </param>
        /// <remarks>
        /// Метод обрабатывает входной JSON, удаляя лишние символы и извлекая ключ и оставшуюся часть JSON,
        /// после чего вызывает метод <see cref="JsonObject.SetField(string, string)"/> для установки значения поля.
        /// </remarks>
        public static void ReadJson(JsonObject jsonObject, string strings = "")
        {
            string json;
            if (strings != "")
            {
                json = strings;
            }
            else
            {
                StringBuilder jsonBuilder = new();

                string? line;
                while ((line = Console.ReadLine()) != null)
                {
                    jsonBuilder.AppendLine(line);
                }

                json = jsonBuilder.ToString();
            }

            json = json.Trim().Trim('}').Trim('{').Trim();
            (string Key, string RemainingJson) processedJson = JsonRasParser.ExtractKeyAndRemaining(json);
            jsonObject.SetField(processedJson.Key, processedJson.RemainingJson);
        }
    }
}