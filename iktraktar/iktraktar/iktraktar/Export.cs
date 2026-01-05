using System.Text.Json;

using System.IO;

using System.Collections.Generic;

namespace iktraktar.Models

{

    internal static class Export

    {

        // Save

        public static void SaveToJson(string filePath, List<Product> products)

        {

            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(filePath, json);

        }

        // Load

        public static List<Product> LoadFromJson(string filePath)

        {

            if (!File.Exists(filePath))

                return new List<Product>();

            var json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();

        }

    }

}


