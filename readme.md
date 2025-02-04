# Проект 1, модуль №3

> **Внимание**: Дополнительная задача (отрисовка интерфейса с помощью SkiaSharp и пр.) **не выполнена**.

## Описание

Данный проект — это консольное приложение, работающее с JSON-данными из игры *Book of Hours* (вариант №4).  
Цель проекта — реализовать **справочную систему**, позволяющую:

1. Читать данные из файла/консоли (JSON) и сохранять данные в файл/консоль (с помощью перенаправления потоков).
2. Фильтровать и сортировать эти данные по выбранным полям.
3. Организовать «обозреватель xtriggers»:
    - Пользователь вводит `id` способности.
    - Программа выводит всю информацию по объекту (ID, Label, Description, xtriggers и т. д.) в формате таблицы с учётом
      переноса строк.

## Структура решения

1. **Библиотека классов** (DLL), где находятся:
    - **Интерфейс `IJSONObject`**
        - `IEnumerable<string> GetAllFields()` — возвращает список имён всех полей.
        - `string GetField(string fieldName)` — возвращает значение поля (строка). Если нет поля, возвращает `null`.
        - `void SetField(string fieldName, string value)` — меняет значение поля. Если нет такого поля, генерирует
          `KeyNotFoundException`.
    - **Классы-модели** для представления данных JSON.
        - Вариант №4: класс, описывающий «Возможности» (например, `Ability`).
        - Все классы реализуют `IJSONObject` и содержат поля для `id`, `label`, `description`, `xtriggers` и т. д.
    - **Статический класс `JsonParser`**
        - `ReadJson()` — читает JSON-данные из `Console.In`, парсит и создаёт объекты моделей.
        - `WriteJson()` — сериализует объекты в JSON и выводит результат в `Console.Out`.
        - Запрещено использовать готовые библиотеки для парсинга (`System.Text.Json`, `Newtonsoft.Json` и т. п.).
          Разрешены регулярные выражения или конечные автоматы.

2. **Консольное приложение**:
    - Запрашивает действия через текстовое меню:
        1. **Ввести данные** (из консоли или файла)
            - При вводе из файла используем `Console.SetIn()`.
        2. **Отфильтровать данные**
            - Пользователь выбирает поле и перечисляет значения, по которым остаются объекты.
        3. **Отсортировать данные**
            - Пользователь выбирает поле и направление (возрастание/убывание).
        4. **Обозреватель xtriggers**
            - Запрашивает у пользователя `id` способности и выводит всю информацию, включая список `xtriggers`, в виде
              таблицы.
        5. **(Дополнительная задача — не выполнена)**
        6. **Вывести данные** (консоль/файл)
            - При записи в файл используем `Console.SetOut()`.
        7. **Выход**
    - Реализован цикл, позволяющий выполнять эти действия несколько раз без завершения программы.

## Использование

При запуске консоли вам будет предложено меню:

	1.	Ввести данные (консоль/файл)
	2.	Отфильтровать данные
	3.	Отсортировать данные
	4.	Обозреватель xtriggers
	5.	Доп. задача (не выполнена)
	6.	Вывести данные (консоль/файл)
	7.	Выход

### Ввод данных

- Выбираете источник (консоль или файл). Если файл, введите путь к JSON-файлу. Программа перенаправит `Console.In` и
  попытается прочитать данные.

### Фильтрация

- Выбираете поле из списка `GetAllFields()` объектов.
- Вводите набор значений (например, целые числа, строки).
- Приложение оставляет объекты, у которых значение поля совпадает с указанными.

### Сортировка

- Выбираете поле и направление (ASC или DESC).
- Программа сортирует коллекцию по нужному полю.

### Обозреватель xtriggers

- Введите `id` способности.
- Приложение находит соответствующий объект и выводит:
    - ID, Label, Description.
    - Триггеры (`xtriggers`) в виде ключей и значений (объекты/массивы).
    - Формат вывода — таблица с ограниченной шириной, переносами строк по пробелам и/или разрывом слов с дефисом.

### Вывод данных

- Можно вывести на консоль или в файл. При выборе файла вводится путь, и вызывается `Console.SetOut()`.

### Выход

- Завершение работы приложения.

## Пример запуска

## По дополнительным вопросам пишите на почту

### `mekapoguzov@edu.hse.ru`

## Лицензия

Проект создан в учебных целях (Проект 1, модуль №3). Данные из игры *Book of Hours* предоставлены *Weather Factory*
только для обучения и не могут использоваться в коммерческих проектах.

---

Автор: *Максим Евгеньевич*  
Контакты: *`mekapoguzov@edu.hse.ru`*