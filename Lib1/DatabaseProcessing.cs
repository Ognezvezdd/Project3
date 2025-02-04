/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

namespace Lib1
{
    /// <summary>
    /// Обрабатывает JSON-данные и преобразует их в список объектов Ability.
    /// </summary>
    public static class DatabaseProcessing
    {
        /// <summary>
        /// Преобразует список объектов в список способностей.
        /// </summary>
        /// <param name="data">Список объектов, содержащих данные способностей.</param>
        /// <returns>Список объектов типа <see cref="Ability"/>.</returns>
        private static List<Ability> AbilityProcessing(List<object> data)
        {
            List<Ability> arr = [];
            foreach (Dictionary<string, object> value in data)
            {
                string id = (string)value.GetValueOrDefault("id", "NotValidID");
                string label = (string)value.GetValueOrDefault("label", "NotValidLABEL");
                string desc = (string)value.GetValueOrDefault("desc", "NotValidDESC");
                Dictionary<string, object> triggers;
                if (value.ContainsKey("xtriggers"))
                {
                    triggers = (Dictionary<string, object>)value["xtriggers"];
                }
                else
                {
                    triggers = new Dictionary<string, object>();
                }

                Ability curr = new(
                    id,
                    label,
                    desc,
                    triggers,
                    value!.GetValueOrDefault("xexts", null) as Dictionary<string, object>,
                    value!.GetValueOrDefault("aspects", null) as Dictionary<string, object>,
                    value!.GetValueOrDefault("inherits", null) as string,
                    value!.GetValueOrDefault("lifetime", null) as int?,
                    value!.GetValueOrDefault("icon", null) as string,
                    value!.GetValueOrDefault("decayto", null) as string,
                    value!.GetValueOrDefault("resaturate", null) as bool?,
                    value!.GetValueOrDefault("noartneeded", null) as bool?
                );

                arr.Add(curr);
            }

            return arr;
        }

        /// <summary>
        /// Обрабатывает JSON-объект и возвращает список способностей.
        /// </summary>
        /// <param name="json">JSON-объект с данными.</param>
        /// <returns>Список объектов типа <see cref="Ability"/>.</returns>
        internal static List<Ability> Processing(JsonObject json)
        {
            Dictionary<string, object> data = JsonRasParser.RealParseJson(json.GetField("elements"));
            List<object> realData = (List<object>)data["Vals"];
            return AbilityProcessing(realData);
        }
    }
}