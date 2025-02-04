/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

using System.Text;

namespace Lib1
{
    /// <summary>
    /// Представляет способность.
    /// </summary>
    public class Ability : IJsonObject
    {
        public string Id { get; private set; }
        public string Label { get; private set; }
        public string Description { get; private set; }
        public Dictionary<string, object> Xtriggers { get; private set; }
        public Dictionary<string, object>? Xexts { get; private set; }
        public Dictionary<string, object>? Aspects { get; private set; }
        public string? Inherits { get; private set; }
        public int? Lifetime { get; private set; }
        public string? Icon { get; private set; }
        public string? Decayto { get; private set; }
        public bool? Resaturate { get; private set; }
        public bool? Noartneeded { get; private set; }

        /// <summary>
        /// Создает новый объект Ability
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="label">Название</param>
        /// <param name="description">Описание</param>
        /// <param name="xtriggers">Триггеры</param>
        /// <param name="xexts">Доп. расширения</param>
        /// <param name="aspects">Аспекты</param>
        /// <param name="inherits">Доп атрибут</param>
        /// <param name="lifetime">Время жизни</param>
        /// <param name="icon">Иконка</param>
        /// <param name="decayto">Доп атрибут</param>
        /// <param name="resaturate">Доп атрибут</param>
        /// <param name="noartneeded">Необходимость иконки</param>
        public Ability(
            string id,
            string label,
            string description,
            Dictionary<string, object> xtriggers,
            Dictionary<string, object>? xexts,
            Dictionary<string, object>? aspects,
            string? inherits,
            int? lifetime,
            string? icon,
            string? decayto,
            bool? resaturate,
            bool? noartneeded
        )
        {
            Id = id;
            Label = label;
            Description = description;
            Xtriggers = xtriggers;
            Xexts = xexts;
            Aspects = aspects;
            Inherits = inherits;
            Lifetime = lifetime;
            Icon = icon;
            Decayto = decayto;
            Resaturate = resaturate;
            Noartneeded = noartneeded;
        }

        /// <summary>
        /// Возвращает список имен полей.
        /// </summary>
        /// <returns>Имена полей.</returns>
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
        /// Возвращает строковое представление указанного поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Строковое представление.</returns>
        /// <remarks>
        /// ДАННЫЙ МЕТОД НЕ ИСПОЛЬЗУЕТСЯ В ПРОГРАММЕ И БЫЛ ДОБАВЛЕН ДЛЯ СОХРАНЕНИЯ СОВМЕСТИМОСТИ
        /// </remarks>
        public string GetField(string fieldName)
        {
            return fieldName switch
            {
                "id" => AbilityParser.Parser(Id),
                "aspects" => AbilityParser.Parser(Aspects),
                "label" => AbilityParser.Parser(Label),
                "desc" => AbilityParser.Parser(Description),
                "inherits" => AbilityParser.Parser(Inherits),
                "icon" => AbilityParser.Parser(Icon),
                "decayto" => AbilityParser.Parser(Decayto),
                "xtriggers" => AbilityParser.Parser(Xtriggers),
                "xexts" => AbilityParser.Parser(Xexts),
                "lifetime" => AbilityParser.Parser(Lifetime),
                "resaturate" => AbilityParser.Parser(Resaturate),
                "noartneeded" => AbilityParser.Parser(Noartneeded),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// Устанавливает значение указанного поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Новое значение.</param>
        /// <remarks>
        /// ДАННЫЙ МЕТОД НЕ ИСПОЛЬЗУЕТСЯ В ПРОГРАММЕ И БЫЛ ДОБАВЛЕН ДЛЯ СОХРАНЕНИЯ СОВМЕСТИМОСТИ
        /// </remarks>
        public void SetField(string fieldName, string value)
        {
            Dictionary<string, object> jsonObject = JsonRasParser.RealParseJson(value);
            object parsedValue = jsonObject["Vals"];

            switch (fieldName)
            {
                case "id":
                    Id = (string)parsedValue;
                    break;
                case "label":
                    Label = (string)parsedValue;
                    break;
                case "desc":
                    Description = (string)parsedValue;
                    break;
                case "xtriggers":
                    Xtriggers = (Dictionary<string, object>)parsedValue;
                    break;
                case "xexts":
                    Xexts = (Dictionary<string, object>?)parsedValue;
                    break;
                case "aspects":
                    Aspects = (Dictionary<string, object>?)parsedValue;
                    break;
                case "inherits":
                    Inherits = (string?)parsedValue;
                    break;
                case "lifetime":
                    Lifetime = Convert.ToInt32(parsedValue);
                    break;
                case "icon":
                    Icon = (string?)parsedValue;
                    break;
                case "decayto":
                    Decayto = (string?)parsedValue;
                    break;
                case "resaturate":
                    Resaturate = Convert.ToBoolean(parsedValue);
                    break;
                case "noartneeded":
                    Noartneeded = Convert.ToBoolean(parsedValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Возвращает значение поля как объект.
        /// </summary>
        /// <param name="field">Имя поля.</param>
        /// <returns>Значение поля.</returns>
        public object? GetObjField(string field)
        {
            return field switch
            {
                "id" => Id,
                "aspects" => Aspects,
                "label" => Label,
                "desc" => Description,
                "inherits" => Inherits,
                "icon" => Icon,
                "decayto" => Decayto,
                "xtriggers" => Xtriggers,
                "xexts" => Xexts,
                "lifetime" => Lifetime,
                "resaturate" => Resaturate,
                "noartneeded" => Noartneeded,
                _ => throw new ArgumentOutOfRangeException()
            };
        }


        /// <summary>
        /// Возвращает JSON-подобное представление способности.
        /// </summary>
        /// <returns>Строка с данными способности.</returns>
        public override string ToString()
        {
            StringBuilder res = new("{\n");

            foreach (string variable in GetAllFields())
            {
                object? obj = GetObjField(variable);

                if ((variable == "label" && (string?)obj == "NotValidLABEL")
                    ||
                    (variable == "desc" && (string?)obj == "NotValidDESC")
                   )
                {
                    continue;
                }

                if (obj == null)
                {
                    continue;
                }

                if (obj.GetType() == typeof(Dictionary<string, object>))
                {
                    Dictionary<string, object> _ = (Dictionary<string, object>)obj;
                    if (_.Count == 0)
                    {
                        continue;
                    }
                }


                res.Append($"\"{variable}\": ");

                res.Append(AbilityParser.Parser(obj));

                res.Append(", ");
            }

            if (res.Length > 1)
            {
                res.Length -= 2;
            }

            res.Append("\n");
            res.Append("}");
            return res.ToString();
        }
    }
}