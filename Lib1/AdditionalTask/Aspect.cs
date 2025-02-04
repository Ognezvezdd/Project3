// namespace Lib1
// {
//     public class Aspect : IJsonObject
//     {
//         public string Id { get; }
//         public string? Icon { get; }
//         public bool Noartneeded { get; }
//
//         public Aspect(string id, string? icon, bool noartneeded)
//         {
//             Id = id;
//             Icon = icon;
//             Noartneeded = noartneeded;
//         }
//
//         public IEnumerable<string> GetAllFields()
//         {
//             return ["id", "icon", "noartneeded"];
//         }
//
//         public string GetField(string fieldName)
//         {
//             throw new NotImplementedException();
//         }
//
//         public void SetField(string fieldName, string value)
//         {
//             throw new NotImplementedException();
//         }
//
//
//         public object? Get(string? fieldName)
//         {
//             return fieldName.ToLower() switch
//             {
//                 "id" => Id,
//                 "icon" => Icon,
//                 "noartneeded" => Noartneeded,
//                 _ => throw new NotImplementedException()
//             };
//         }
//     }
// }