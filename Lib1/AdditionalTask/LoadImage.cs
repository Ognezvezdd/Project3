// using SkiaSharp;
//
// namespace Lib1.AdditionalTask
// {
//     internal static class LoadImage
//     {
//         public static SKBitmap Load(string id, List<string> directories)
//         {
//             foreach (string dir in directories)
//             {
//                 // Собираем полный путь
//                 string path = Path.Combine(dir, id + ".png");
//                 if (File.Exists(path))
//                 {
//                     return SKBitmap.Decode(path);
//                 }
//             }
//
//             return null;
//         }
//     }
// }