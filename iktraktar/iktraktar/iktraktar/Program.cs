
using iktraktar.Models;

namespace iktraktar
{
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

    CsapatTag 3 - Rendelés kezelése (új modul)
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

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Storage storage = new Storage();

            storage.Add(new Product(1, "Ceruza", 100));
            storage.Add(new Product(2, "Toll", 50));
            storage.Add(new Product(3, "Füzet", 80));

            Console.WriteLine("Raktár rendszer - fejlesztési alap");



            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Main Menu ===");
                Console.WriteLine("8. Order Processing.");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                if (choice == "3")
                {
                    break;
                }
                if(choice == "8")
                {
                    Order order = new Order();
                    order.AddItem(storage.FindById(2), 3);
                    order.AddItem(storage.FindById(3), 1);
                    Storage.ProcessOrder(order, storage);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
                }
            }
        }
    }
}
