/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

namespace Lib1
{
    /// <summary>
    /// Реализация IJsonObject.
    /// </summary>
    public class JsonObject : IJsonObject
    {
        private readonly Dictionary<string, string> _fields = new();

        /// <summary>
        /// Возвращает список всех полей.
        /// </summary>
        public IEnumerable<string> GetAllFields()
        {
            return
            [
                "id",
                "aspects",
                "label",
                "desc",
                "inherits",
                "icon",
                "decayto",
                "xtriggers",
                "xexts",
                "lifetime",
                "resaturate",
                "noartneeded"
            ];
        }

        /// <summary>
        /// Возвращает значение указанного поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        public string GetField(string fieldName)
        {
            return _fields.GetValueOrDefault(fieldName) ?? string.Empty;
        }

        /// <summary>
        /// Устанавливает значение указанного поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Новое значение.</param>
        public void SetField(string fieldName, string value)
        {
            _fields[fieldName] = value;
        }

        /// <summary>
        /// Возвращает строковое представление объекта в формате JSON.
        /// </summary>
        public override string ToString()
        {
            return $"{string.Join(", ", _fields.Select(a => $"{a.Key}: {a.Value}"))}";
        }
    }
}