using iktraktar.Models;
using iktRaktar.Models;
using System;

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
                Console.WriteLine("4 - Készlet növelése");
                Console.WriteLine("5 - Készlet csökkentése");
                Console.WriteLine("6 - Rendelés létrehozása termékek és mennyiségek hozzáadásával (csak létező, rendelkezésre álló termékekből)");
                Console.WriteLine("7 - Rendelés összegzése, kiírás file-ba");
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


                if (choice == "6")
                {

                    Order? currentOrder = new Order();
                    Console.WriteLine("Új rendelés létrehozva!");
                    while (true)
                    {
                        Console.Write("Add meg a termék ID-ját (0 = kilép): ");
                        int pid = int.Parse(Console.ReadLine()!);
                        if (pid == 0) break;
                        var product = storage.FindById(pid);
                        if (product == null)
                        {
                            Console.WriteLine("Nincs ilyen termék!");
                            continue;
                        }
                        Console.Write("Mennyiség: ");
                        int qty = int.Parse(Console.ReadLine()!);
                        if (currentOrder.AddItem(product, qty))
                            Console.WriteLine("Tétel hozzáadva!");
                        else
                            Console.WriteLine("Nem sikerült (nincs készlet vagy hibás adat).");
                    }
                    break;

                }

                if (choice == "7")
                {
                    Order? currentOrder = new Order();
                    if (currentOrder == null || currentOrder.Items.Count == 0)
                    {
                        Console.WriteLine("Nincs aktív rendelés!");
                    }
                    else
                    {
                        string fileName = $"order_{currentOrder.Id}.txt";
                        currentOrder.SaveToFile(fileName);
                        Console.WriteLine($"Rendelés mentve ide: {fileName}");
                    }
                    break;
                }
                Console.WriteLine("Ismeretlen menüpont.");
            }
        }
    }
}