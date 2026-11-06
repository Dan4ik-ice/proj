using System.Security.Cryptography.X509Certificates;

namespace TestForAll
{
    internal class Program
    {
        public class ShoppingItems
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int? Quantity { get; set; }
            public bool? IsBought { get; set; }

            public ShoppingItems(int id, string? name, int? quantity, bool? isBought)
            {
                Id = id;
                Name = name;
                Quantity = quantity;
                IsBought = isBought;
            }
        }
        public class ShoppingList
        {
            List<ShoppingItems> items = new List<ShoppingItems>();

            private int nextId = 1;
            public void AddItem(string name, int quantity)
            {
                var item = new ShoppingItems(nextId++, name, quantity, false);
                items.Add(item);
            }
            public bool RemoveItem(int id)
            {
                int index = items.FindIndex(item => item.Id == id);

                if (index == -1)
                {
                    return false;
                }
                items.RemoveAt(index);
                return true;
            }
            public void ShowAllItems()
            {
                foreach (var item in items)
                {
                    Console.WriteLine(
                        $"ID: {item.Id}, Название: {item.Name}, Количество: {item.Quantity}, Куплен: {item.IsBought}");
                }



            }
            
            public List<ShoppingItems> Search(string query)
            {
                        return items
               .Where(item => item.Name != null &&
                              item.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
               .ToList();
            }
        }


            public static void HandleAddItem(ShoppingList shopping)
            {

                Console.Write("Название товара: ");
                string name = Console.ReadLine() ?? "Без названия";

                Console.WriteLine("Количество:");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Введите целое число больше нуля.");
                    return;
                }
                shopping.AddItem(name, quantity);
                Console.WriteLine("Товар добавлен");
            }
            public static void HandleRemoveItem(ShoppingList shopping)
            {
                Console.WriteLine("Введите id товара который надо удалить");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Введите корректный id:");
                    return;
                }
                if (shopping.RemoveItem(id))
                {
                    Console.WriteLine("товар удален");
                }
                else
                {
                    Console.WriteLine("товар с таким id не найден.");
                }
            }
            public static void HandleFindItem(ShoppingList shopping)
            {
                Console.WriteLine("Ввеите название предмета который вы хотите найти");
                string query = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(query))
            {
                Console.WriteLine("запрос не должен быть пустым");
                return;
            }
            List<ShoppingItems> founItems = shopping.Search(query);
            if(founItems.Count == 0)
            {
                Console.WriteLine("Товары не найдены");
                return;
            }
            foreach (var item in founItems)
            {
                Console.WriteLine(
                    $"ID: {item.Id}, Название: {item.Name}, Количество: {item.Quantity}, Куплен: {item.IsBought}");
            }
        }

            static void Main(string[] args)
            {
                ShoppingList shopping = new ShoppingList();

                while (true)
                {
                    Console.WriteLine("\n1.Добавить Товар");
                    Console.WriteLine("2. Удалить товар");
                    Console.WriteLine("3. Показать все товары");
                    Console.WriteLine("4. Найти товар");
                    Console.WriteLine("5. Выйти");

                    string? choise = Console.ReadLine();
                    switch (choise)
                    {
                        case "1": HandleAddItem(shopping); break;
                        case "2": HandleRemoveItem(shopping); break;
                        case "3": shopping.ShowAllItems(); break;
                        case "4": HandleFindItem(shopping); break;
                        case "5": return;
                    default: Console.WriteLine("Введите корректное значение!"); break;
                    }

                }
                //shopping.HandleAddItem(shopping);
                //shopping.ShowAllItems();

                //shopping.HandleRemoveItem(shopping);
                //shopping.ShowAllItems();


            }
        }
    }

