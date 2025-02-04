// using SkiaSharp;
// using System;
//
//
// namespace Lib1.AdditionalTask
// {
//     public class AdditionalTask
//     {
//         public AdditionalTask(DataBase dataBase)
//         {
//             // 1. Запрос у пользователя путей
//             // Путь до JSON-файла с аспектами
//             Console.Write(
//                 "Введите путь до JSON-файла с аспектами (например, ./BoH/ru/elements/_aspects.json): ");
//
//             string jsonPath = Console.ReadLine()?.Trim();
//
//             // Пути до директорий с изображениями (разделитель можно использовать ; или ,)
//             Console.Write("Введите пути до директорий с изображениями (разделитель ; или ,): ");
//             string dirsInput = Console.ReadLine();
//
//             List<string> imageDirectories = new List<string>();
//             if (!string.IsNullOrWhiteSpace(dirsInput))
//             {
//                 foreach (string d in dirsInput.Split(new char[] { ';', ',' },
//                              StringSplitOptions.RemoveEmptyEntries))
//                 {
//                     imageDirectories.Add(d.Trim());
//                 }
//             }
//
//             /// TODO сделать
//             // JsonObject jsonObject = new();
//             // JsonParser.ReadJson(jsonObject, "");
//
//
//             Ability ability = new Ability("", "", "", new Dictionary<string, object>(),
//                 null, null, null, null, null, null, null, null);
//
//             // 4. Настройка холста (Canvas)
//             // Размер холста выбираем так, чтобы от НЕ белого пикселя до границы было не менее 50 пикселей
//             int canvasWidth = 1000;
//             int canvasHeight = 800;
//             using (SKSurface? surface = SKSurface.Create(new SKImageInfo(canvasWidth, canvasHeight)))
//             {
//                 SKCanvas canvas = surface.Canvas;
//                 // Заполняем фон белым
//                 canvas.Clear(SKColors.White);
//
//                 int margin = 50; // 50 пикселей от границы
//                 SKRect outerRect = new SKRect(margin, margin, canvasWidth - margin, canvasHeight - margin);
//
//                 // 4.1. Рисуем внешнюю рамку со скруглениями
//                 using (SKPaint framePaint = new SKPaint())
//                 {
//                     framePaint.Style = SKPaintStyle.Stroke;
//                     framePaint.Color = SKColors.Black;
//                     framePaint.StrokeWidth = 4;
//                     canvas.DrawRoundRect(outerRect, 20, 20, framePaint);
//                 }
//
//                 // 5. Разметка элементов
//                 // Разобьём внешний прямоугольник на две зоны:
//                 // • Верхняя зона (примерно 350 пикселей по высоте): изображение способности слева и текст (заголовок, разделитель, описание) справа.
//                 // • Нижняя зона: внутренняя рамка с аспектами.
//
//                 // 5.1. Верхняя зона
//                 float topAreaHeight = 350;
//                 // Зона изображения способности – зададим прямоугольник фиксированного размера
//                 SKRect abilityImageRect = new SKRect(outerRect.Left + 20, outerRect.Top + 20,
//                     outerRect.Left + 20 + 300, outerRect.Top + 20 + 300);
//
//                 // Пытаемся загрузить изображение способности по алгоритму А (ищем файл "ability1.png" в указанных папках)
//                 SKBitmap abilityBitmap = LoadImage.Load(ability.Id, imageDirectories);
//                 if (abilityBitmap != null)
//                 {
//                     // Если требуется масштабировать изображение, размер на холсте не должен быть меньше 50% от оригинала
//                     float scaleX = abilityImageRect.Width / abilityBitmap.Width;
//                     float scaleY = abilityImageRect.Height / abilityBitmap.Height;
//                     float scale = Math.Min(scaleX, scaleY);
//                     if (scale < 0.5f)
//                     {
//                         scale = 0.5f;
//                     }
//
//                     float drawWidth = abilityBitmap.Width * scale;
//                     float drawHeight = abilityBitmap.Height * scale;
//                     SKRect destRect = new SKRect(abilityImageRect.Left, abilityImageRect.Top,
//                         abilityImageRect.Left + drawWidth, abilityImageRect.Top + drawHeight);
//                     canvas.DrawBitmap(abilityBitmap, destRect);
//                 }
//                 else
//                 {
//                     // Если изображение не найдено, рисуем рамку с сообщением
//                     using (SKPaint errorPaint = new SKPaint())
//                     {
//                         errorPaint.Style = SKPaintStyle.Stroke;
//                         errorPaint.Color = SKColors.Red;
//                         errorPaint.StrokeWidth = 2;
//                         canvas.DrawRect(abilityImageRect, errorPaint);
//
//                         errorPaint.Style = SKPaintStyle.Fill;
//                         errorPaint.TextSize = 20;
//                         canvas.DrawText("Нет изображения", abilityImageRect.Left + 10, abilityImageRect.MidY,
//                             errorPaint);
//                     }
//                 }
//
//                 // Зона для заголовка и описания – располагаем её справа от изображения
//                 SKRect textAreaRect = new SKRect(abilityImageRect.Right + 20, abilityImageRect.Top,
//                     outerRect.Right - 20, abilityImageRect.Bottom);
//
//                 // 5.2. Выбор шрифта – разрешены Times New Roman, HSE Sans, HSE Slab
//                 SKTypeface typeface = SKTypeface.FromFamilyName("Times New Roman");
//                 if (typeface == null || !typeface.FamilyName.Contains("Times"))
//                 {
//                     typeface = SKTypeface.FromFamilyName("HSE Sans");
//                 }
//
//                 if (typeface == null)
//                 {
//                     typeface = SKTypeface.FromFamilyName("HSE Sans");
//                 }
//
//                 // Рисуем заголовок, разделительную линию и описание
//                 using (SKPaint textPaint = new SKPaint())
//                 {
//                     textPaint.IsAntialias = true;
//                     textPaint.Typeface = typeface;
//                     textPaint.Color = SKColors.Black;
//
//                     // Заголовок – размер шрифта не менее 10, выбираем 30
//                     textPaint.TextSize = 30;
//                     float titleX = textAreaRect.Left;
//                     float titleY = textAreaRect.Top + textPaint.TextSize;
//                     canvas.DrawText(ability.Label, titleX, titleY, textPaint);
//
//                     // Разделительная линия (примерно через 10 пикселей ниже заголовка)
//                     float dividerY = titleY + 10;
//                     canvas.DrawLine(textAreaRect.Left, dividerY, textAreaRect.Right, dividerY, textPaint);
//
//                     // Описание – уменьшаем размер шрифта, но не меньше 10 (выберем 20)
//                     textPaint.TextSize = 20;
//                     float descriptionX = textAreaRect.Left;
//                     float descriptionY = dividerY + 30;
//                     canvas.DrawText(ability.Description, descriptionX, descriptionY, textPaint);
//                 }
//
//                 // 5.3. Нижняя зона – внутренняя рамка для аспектов
//                 SKRect aspectsFrameRect = new SKRect(outerRect.Left + 20, abilityImageRect.Bottom + 20,
//                     outerRect.Right - 20, outerRect.Bottom - 20);
//                 using (SKPaint innerFramePaint = new SKPaint())
//                 {
//                     innerFramePaint.Style = SKPaintStyle.Stroke;
//                     innerFramePaint.Color = SKColors.Black;
//                     innerFramePaint.StrokeWidth = 2;
//                     canvas.DrawRoundRect(aspectsFrameRect, 15, 15, innerFramePaint);
//                 }
//
//                 // Отрисовка иконок аспектов с количеством и разделителем (♦)
//                 // Располагаем их горизонтально по центру внутренней рамки.
//                 int aspectCount = 0;
//                 if (aspectCount > 0)
//                 {
//                     // Предположим, что для каждого аспекта иконка рисуется в квадрате не менее 50x50 пикселей.
//                     float iconSize = 50;
//                     float spacing = 10; // Отступ между иконкой и текстом (количество)
//                     float countTextApproxWidth = 20; // примерная ширина текста с количеством
//                     float separatorWidth = 20; // ширина для символа ♦
//                     float eachAspectWidth = iconSize + spacing + countTextApproxWidth;
//                     float totalWidth = (eachAspectWidth * aspectCount) + (separatorWidth * (aspectCount - 1));
//                     float startX = aspectsFrameRect.Left + ((aspectsFrameRect.Width - totalWidth) / 2);
//                     float currentX = startX;
//                     float iconY = aspectsFrameRect.Top + ((aspectsFrameRect.Height - iconSize) / 2);
//
//                     for (int i = 0; i < aspectCount; i++)
//                     {
//                         object? aspect = ability.Aspects.Keys.ToArray();
//                         // Если в JSON для данного аспекта установлено noartneeded, то можно не пытаться загрузить изображение.
//                         // bool noArtNeeded = aspectsNoArtNeeded.TryGetValue(aspect.Id, out bool val) && val;
//                         SKBitmap aspectBitmap = true ? null : LoadImage.Load("rank", imageDirectories);
//
//                         SKRect iconRect = new SKRect(currentX, iconY, currentX + iconSize, iconY + iconSize);
//                         if (aspectBitmap != null)
//                         {
//                             // Масштабируем изображение, но не меньше 50% от исходного размера
//                             float scaleX = iconRect.Width / aspectBitmap.Width;
//                             float scaleY = iconRect.Height / aspectBitmap.Height;
//                             float scale = Math.Min(scaleX, scaleY);
//                             if (scale < 0.5f)
//                             {
//                                 scale = 0.5f;
//                             }
//
//                             float drawWidth = aspectBitmap.Width * scale;
//                             float drawHeight = aspectBitmap.Height * scale;
//                             SKRect destRect = new SKRect(iconRect.Left, iconRect.Top, iconRect.Left + drawWidth,
//                                 iconRect.Top + drawHeight);
//                             canvas.DrawBitmap(aspectBitmap, destRect);
//                         }
//                         else
//                         {
//                             // Если изображение не найдено или noartneeded==true, рисуем рамку с вопросительным знаком
//                             using (SKPaint phPaint = new SKPaint())
//                             {
//                                 phPaint.Style = SKPaintStyle.Stroke;
//                                 phPaint.Color = SKColors.Gray;
//                                 phPaint.StrokeWidth = 2;
//                                 canvas.DrawRect(iconRect, phPaint);
//
//                                 phPaint.Style = SKPaintStyle.Fill;
//                                 phPaint.TextSize = 20;
//                                 // Центрируем знак "?" внутри iconRect
//                                 SKRect textBounds = new SKRect();
//                                 string qm = "?";
//                                 phPaint.MeasureText(qm, ref textBounds);
//                                 float textX = iconRect.MidX - textBounds.MidX;
//                                 float textY = iconRect.MidY - textBounds.MidY;
//                                 canvas.DrawText(qm, textX, textY, phPaint);
//                             }
//                         }
//
//                         // Рисуем количество аспекта рядом с иконкой
//                         string countText = 5.ToString();
//                         float countX = iconRect.Right + spacing;
//                         using (SKPaint countPaint = new SKPaint())
//                         {
//                             countPaint.IsAntialias = true;
//                             countPaint.Typeface = typeface;
//                             countPaint.TextSize = 20;
//                             countPaint.Color = SKColors.Black;
//                             canvas.DrawText(countText, countX, iconRect.MidY + 10, countPaint);
//                         }
//
//                         // Обновляем текущую позицию по горизонтали
//                         currentX += eachAspectWidth;
//                         // Если это не последний аспект – рисуем разделитель ♦
//                         if (i < aspectCount - 1)
//                         {
//                             string separator = "♦";
//                             using (SKPaint sepPaint = new SKPaint())
//                             {
//                                 sepPaint.IsAntialias = true;
//                                 sepPaint.Typeface = typeface;
//                                 sepPaint.TextSize = 20;
//                                 sepPaint.Color = SKColors.Black;
//                                 SKRect sepBounds = new SKRect();
//                                 float sepWidth = sepPaint.MeasureText(separator, ref sepBounds);
//                                 float sepX = currentX + ((separatorWidth - sepWidth) / 2);
//                                 float sepY = aspectsFrameRect.MidY + 10;
//                                 canvas.DrawText(separator, sepX, sepY, sepPaint);
//                             }
//
//                             currentX += separatorWidth;
//                         }
//                     }
//                 }
//
//                 // 6. Сохраняем изображение в файл
//                 using (SKImage? image = surface.Snapshot())
//                 using (SKData? data = image.Encode(SKEncodedImageFormat.Png, 100))
//                 using (FileStream stream = File.OpenWrite("result.png"))
//                 {
//                     data.SaveTo(stream);
//                 }
//             }
//
//             Console.WriteLine("Изображение сгенерировано и сохранено как result.png");
//         }
//     }
// }