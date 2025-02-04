/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

namespace Lib1
{
    /// <summary>
    /// Организует фильтрацию и сортировку списка способностей.
    /// </summary>
    public static class DataBaseOrganize
    {
        /// <summary>
        /// Сбрасывает фильтры, возвращая исходный набор данных.
        /// </summary>
        /// <param name="data">Ссылка на текущий список способностей.</param>
        /// <param name="zeroData">Исходный список способностей.</param>
        /// <param name="filters">Ссылка на словарь фильтров.</param>
        public static void ResetFilters(ref List<Ability> data, List<Ability> zeroData,
            ref Dictionary<string, List<string>> filters)
        {
            data = zeroData;
            filters = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Фильтрует список способностей по указанному атрибуту и аргументам.
        /// </summary>
        /// <param name="by">Атрибут для фильтрации (например, "id", "label").</param>
        /// <param name="args">Аргументы фильтрации, разделённые пробелами.</param>
        /// <param name="data">Ссылка на список способностей для фильтрации.</param>
        /// <exception cref="RankException">Выбрасывается при пустом вводе.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается при неверном атрибуте фильтрации.</exception>
        public static void FilterBy(string? by, string? args, ref List<Ability> data)
        {
            if (string.IsNullOrWhiteSpace(by) || string.IsNullOrWhiteSpace(args))
            {
                ConsoleManager.WriteWarn("Вы ничего не ввели");
                throw new RankException();
            }

            if (!new List<string>
                {
                    "id",
                    "label",
                    "description",
                    "inherits",
                    "lifetime",
                    "icon",
                    "decayto",
                    "resaturate",
                    "noartneeded"
                }.Contains(by))
            {
                ConsoleManager.WriteWarn($"Неверный атрибут фильтрации \"{by}\"");
                throw new ArgumentOutOfRangeException();
            }

            if (string.IsNullOrWhiteSpace(by) || string.IsNullOrWhiteSpace(args))
            {
                ConsoleManager.WriteWarn("Пустой ввод");
                throw new ArgumentNullException();
            }

            HashSet<string> filterValues = args.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(arg => arg.Trim())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            data = data.Where(ability =>
            {
                return by.ToLower() switch
                {
                    "id" => filterValues.Contains(ability.Id),
                    "label" => filterValues.Contains(ability.Label),
                    "description" => filterValues.Any(val =>
                        ability.Description.Contains(val, StringComparison.OrdinalIgnoreCase)),
                    "inherits" => ability.Inherits == null || filterValues.Contains(ability.Inherits),
                    "icon" => ability.Icon == null || filterValues.Contains(ability.Icon),
                    "decayto" => ability.Decayto == null || filterValues.Contains(ability.Decayto),
                    "resaturate" => ability.Resaturate == null ||
                                    filterValues.Contains(ability.Resaturate.ToString() ?? string.Empty),
                    "noartneeded" => ability.Noartneeded == null ||
                                     filterValues.Contains(ability.Noartneeded.ToString() ?? string.Empty),
                    "lifetime" => ability.Lifetime == null ||
                                  filterValues.Contains(ability.Lifetime.ToString() ?? string.Empty),
                    _ => throw new AggregateException()
                };
            }).ToList();
        }

        /// <summary>
        /// Сортирует список способностей по указанному атрибуту в заданном порядке.
        /// </summary>
        /// <param name="by">Атрибут для сортировки (например, "id", "label").</param>
        /// <param name="ascending">Порядок сортировки: "UP" для возрастания, "DOWN" для убывания.</param>
        /// <param name="data">Ссылка на список способностей для сортировки.</param>
        /// <exception cref="RankException">Выбрасывается при пустом вводе.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается при неверном порядке сортировки или недопустимом атрибуте.
        /// </exception>
        public static void SortBy(string? by, string? ascending, ref List<Ability> data)
        {
            if (string.IsNullOrWhiteSpace(by) || string.IsNullOrWhiteSpace(ascending))
            {
                ConsoleManager.WriteWarn("Вы ничего не ввели");
                throw new RankException();
            }

            if (ascending != "UP" && ascending != "DOWN")
            {
                ConsoleManager.WriteWarn("Неверный ввод. Введите в следующий раз UP or DOWN");
                throw new ArgumentOutOfRangeException();
            }

            if (!new List<string>
                {
                    "id",
                    "label",
                    "description",
                    "inherits",
                    "lifetime",
                    "icon",
                    "decayto",
                    "resaturate",
                    "noartneeded"
                }.Contains(by))
            {
                ConsoleManager.WriteWarn($"Неверный атрибут сортировки \"{by}\"");
                throw new ArgumentOutOfRangeException();
            }

            data.Sort((p1, p2) =>
            {
                IComparable? val1 = p1.GetObjField(by) as IComparable;
                IComparable? val2 = p2.GetObjField(by) as IComparable;

                if (val1 == null && val2 == null)
                {
                    return 0;
                }

                if (val1 == null)
                {
                    return -1;
                }

                if (val2 == null)
                {
                    return 1;
                }

                return ascending == "UP" ? val1.CompareTo(val2) : val2.CompareTo(val1);
            });
        }
    }
}