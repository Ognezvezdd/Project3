/*
 * Группа: БПИ 246-2
 * Студент: Капогузов Максим
 * Вариант: 4
 */

namespace Lib1
{
    /// <summary>
    /// Интерфейс для JSON объектов.
    /// </summary>
    internal interface IJsonObject
    {
        /// <summary>
        /// Возвращает имена всех полей объекта.
        /// </summary>
        /// <returns>Имена полей.</returns>
        IEnumerable<string> GetAllFields();

        /// <summary>
        /// Возвращает значение поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Значение поля или null.</returns>
        string GetField(string fieldName);

        /// <summary>
        /// Устанавливает значение поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Новое значение.</param>
        void SetField(string fieldName, string value);
    }
}