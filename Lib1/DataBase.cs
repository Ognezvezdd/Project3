/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

namespace Lib1
{
    /// <summary>
    /// Класс для работы с базой данных способностей с поддержкой фильтрации и сортировки.
    /// </summary>
    public class DataBase
    {
        private List<Ability> _zeroData = null!;
        private List<Ability> _data = null!;
        private Dictionary<string, List<string>> _filters = new();

        /// <summary>
        /// Сбрасывает применённые фильтры и восстанавливает исходные данные.
        /// </summary>
        public void ResetFilters()
        {
            DataBaseOrganize.ResetFilters(ref _data, _zeroData, ref _filters);
            _filters = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Выводит текущие фильтры в консоль.
        /// </summary>
        public void WriteFilters()
        {
            if (_filters.Count == 0)
            {
                Console.WriteLine("Фильтры не установлены!");
                return;
            }

            Console.WriteLine("Текущие фильтры:");
            Console.WriteLine(string.Join(";\n", _filters.Select(a => $"{a.Key}: {string.Join("; ", a.Value)}")));
        }

        /// <summary>
        /// Фильтрует данные по заданному критерию.
        /// </summary>
        /// <param name="by">Поле для фильтрации.</param>
        /// <param name="args">Аргументы фильтрации, разделённые пробелом.</param>
        public void FilterBy(string? by, string? args)
        {
            DataBaseOrganize.FilterBy(by, args, ref _data);
            _filters[by!] = args!.Split().ToList();
        }

        /// <summary>
        /// Сортирует данные по указанному полю.
        /// </summary>
        /// <param name="by">Поле для сортировки.</param>
        /// <param name="ascending">Порядок сортировки ("true" для по возрастанию).</param>
        public void SortBy(string? by, string? ascending)
        {
            DataBaseOrganize.SortBy(by, ascending, ref _data);
        }

        /// <summary>
        /// Инициализирует базу данных начальными данными.
        /// </summary>
        /// <param name="data">Начальные данные в формате JsonObject.</param>
        public DataBase(JsonObject? data = null)
        {
            if (data == null)
            {
                return;
            }

            _data = DatabaseProcessing.Processing(data);
            _zeroData = _data;
        }

        /// <summary>
        /// Обновляет базу данных новыми данными.
        /// </summary>
        /// <param name="data">Новые данные в формате JsonObject.</param>
        public void Update(JsonObject data)
        {
            _data = DatabaseProcessing.Processing(data);
            _zeroData = _data;
        }

        /// <summary>
        /// Возвращает способность с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор способности.</param>
        /// <returns>Способность с указанным идентификатором.</returns>
        /// <exception cref="ArgumentNullException">Если данные не инициализированы.</exception>
        /// <exception cref="KeyNotFoundException">Если способность не найдена.</exception>
        public Ability GetById(string? id)
        {
            if (_data == null)
            {
                throw new ArgumentNullException();
            }

            foreach (Ability variable in _data.Where(variable => variable.Id == id))
            {
                return variable;
            }

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Возвращает строковое представление базы данных в формате JSON.
        /// </summary>
        /// <returns>Строка с данными базы.</returns>
        public override string ToString()
        {
            string res = "{\n\"elements\": [\n";
            res += string.Join(", ", _data.Select(ability => ability.ToString()));
            res += "\n]\n}";
            return res;
        }
    }
}