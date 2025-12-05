using iktraktar.Models;
using iktraktar.Models;
using System;
using System.Security.Cryptography.X509Certificates;

namespace iktraktar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();
            storage.Add(new Product(1, "Ceruza", 100));
            storage.Add(new Product(2, "Toll", 50));
            storage.Add(new Product(3, "Füzet", 80));

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Raktár rendszer - menü");
                Console.WriteLine("----------------------");
                Console.WriteLine("1 - Teljes lista");
                Console.WriteLine("2 - Keresés ID alapján");
                Console.WriteLine("3 - Keresés név részlet alapján");
                Console.WriteLine("4 - Készlet növelése");
                Console.WriteLine("5 - Készlet csökkentése");
                Console.WriteLine("0 - Kilépés");
                Console.Write("Válassz menüpontot: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.WriteLine("Érvénytelen választás.");
                    continue;
                }

                if (choice == "0")
                {
                    Console.WriteLine("Viszlát!");
                    break;
                }

                if (choice == "1")
                {
                    // Formázott listázás
                    Console.WriteLine("ID | Név | Készlet");
                    Console.WriteLine("-------------------------");
                    foreach (var p in storage.FindAll("")) // FindAll("") visszaad minden terméket, ha a megvalósítás engedi
                    {
                        Console.WriteLine($"{p.Id} | {p.Name} | {p.Quantity}");
                    }
                    continue;
                }
                while (true)
                {

                    Console.WriteLine("2 - Keresés ID alapján");
                    Console.WriteLine("3 - Keresés név részlet alapján");
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Console.WriteLine("Teljes lista:");
                        PrintTable(storage.FindAll(string.Empty));
                    }
                    else if (input == "2")
                    {
                        Console.Write("Add meg az ID-t: ");
                        string idInput = Console.ReadLine();
                        if (int.TryParse(idInput, out int keresettId))
                        {
                            var found = storage.FindById(keresettId);
                            if (found != null)
                            {
                                Console.WriteLine("Keresett termék:");
                                PrintTable(new List<Product> { found });
                            }
                            else
                            {
                                Console.WriteLine("Nem található ilyen ID.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Érvénytelen ID.");
                        }
                    }
                    else if (input == "3")
                    {
                        Console.Write("Add meg a név részletet: ");
                        string namePart = Console.ReadLine();
                        var results = storage.FindAll(namePart);
                        if (results.Any())
                        {
                            Console.WriteLine("Találatok:");
                            PrintTable(results);
                        }
                        else
                        {
                            Console.WriteLine("Nincs találat.");
                        }
                    }
                    else if (input == "0")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Érvénytelen választás.");
                    }
                }

                if (choice == "4" || choice == "5")
                {
                    bool isIncrease = choice == "4";
                    Console.Write(isIncrease ? "Növelés - Add meg a termék ID-ját: " : "Csökkentés - Add meg a termék ID-ját: ");
                    string? idInput = Console.ReadLine();

                    if (!int.TryParse(idInput, out int id))
                    {
                        Console.WriteLine("Érvénytelen ID.");
                        continue;
                    }

                    Console.Write("Add meg a mennyiséget: ");
                    string? qtyInput = Console.ReadLine();

                    if (!int.TryParse(qtyInput, out int amount) || amount < 0)
                    {
                        Console.WriteLine("Érvénytelen mennyiség (nem lehet negatív).");
                        continue;
                    }

                    var product = storage.FindById(id);
                    if (product == null)
                    {
                        Console.WriteLine("Nincs ilyen termék!");
                        continue;
                    }

                    if (isIncrease)
                    {
                        storage.IncreaseQuantity(id, amount);
                    }
                    else
                    {
                        storage.DecreaseQuantity(id, amount);
                    }

                    // Kiírás a minta szerint:
                    Console.WriteLine($"Termék: {id} #{product.Id} {product.Name} | Új mennyiség: {product.Quantity}");
                    continue;
                }

                Console.WriteLine("Ismeretlen menüpont.");
            }
            


        }
        public static void PrintTable(IEnumerable<Product> list)
            {
                Console.WriteLine("ID | Név        | Készlet");
                Console.WriteLine("----------------------------");
                foreach (var item in list)
                {
                    if (item is Product product)
                    {
                        Console.WriteLine($"{product.Id,-2} | {product.Name,-10} | {product.Quantity}");
                    }
                }
            }
    }
}

    /**
     * 
     * 
     * 
     ** 

    Feladatok

    CsapatTag 1 - Raktárkezelő funkciók bővítése []
     - IncreaseQuantity(int id, int amount)
     - DecreaseQuantity(int id, int amount)
     + Konzol menü kialakítása 
        4 - Készlet növelése
        5 - Készlet csökkentése

        Mintha:
            Termék: 2
            #2 Toll | Új mennyiség: 51


    CsapatTag 2 - Kereső és listázó funkciók []
     - 1 - Keresés ID alapján
     - 2 - Keresés név részlet alapján
     - 3 - Teljes lista formázott megjelenítése 
        A keresések során az ISearchable-t kell használni.

        Táblázatos listázás:
        ID | Név        | Készlet
        ----------------------------
        1  | Ceruza     | 100
        2  | Toll       | 50
*/

    
        
    



    /*   CsapatTag 3 - Rendelés kezelése (új modul)
        - Új osztályok: OrderItem, Order
        - OrderItem (Tulajdonságok: Product, Quantity + Konstruktor) 
        - Order : IIdentifiable (Rendelési itemek listája)
        + Funkciók:
           6 - Rendelés létrehozása termékek és mennyiségek hozzáadásával (csak létező, rendelkezésre álló termékekből)
           7 - Rendelés összegzése, kiírás file-ba

       CsapatTag 4 - Rendelés feldolgozása
        - ProcessOrder függvény, egy Order-t és a Storage-et várja paraméterben
           > Ellenőrzi, hogy mindenből rendelkezésre áll a készlet, ha igen, akkor csökkenti a raktár mennyiségeket ha nem hibát ír ki.
        + Funkciók:
           8 - Rendelés feldolgozása

       Minta:
           Rendelés feldolgozva #4
               Levont készlet: #2 Toll (-3), #3 Füzet (-1)

       CsapatTag 5 - Export/Import
        - Storage filebe mentése (json/csv/xml) és betöltése (Csak a List<Product> -ot kell lementeni)
        + Funciók:
           9 - Raktár betöltése
           10 - Raktár mentése  

       */


   

