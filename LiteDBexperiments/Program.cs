using LiteDB;

using var database = new LiteDatabase("customers.db");
var customers = database.GetCollection<BsonDocument>("customers");

if (customers.Count() == 0)
{
    var seedCustomers = new[]
    {
        new BsonDocument { ["name"] = "Alex Morgan", ["adress"] = "101 Maple Street", ["phone"] = "555-0101", ["email"] = "alex.morgan@example.com" },
        new BsonDocument { ["name"] = "Jamie Lee", ["adress"] = "202 Cedar Avenue", ["phone"] = "555-0102", ["email"] = "jamie.lee@example.com" },
        new BsonDocument { ["name"] = "Taylor Kim", ["adress"] = "303 Birch Road", ["phone"] = "555-0103", ["email"] = "taylor.kim@example.com" },
        new BsonDocument { ["name"] = "Jordan Patel", ["adress"] = "404 Oak Lane", ["phone"] = "555-0104", ["email"] = "jordan.patel@example.com" },
        new BsonDocument { ["name"] = "Casey Rivera", ["adress"] = "505 Pine Drive", ["phone"] = "555-0105", ["email"] = "casey.rivera@example.com" },
        new BsonDocument { ["name"] = "Riley Chen", ["adress"] = "606 Elm Court", ["phone"] = "555-0106", ["email"] = "riley.chen@example.com" },
        new BsonDocument { ["name"] = "Avery Brooks", ["adress"] = "707 Willow Way", ["phone"] = "555-0107", ["email"] = "avery.brooks@example.com" },
        new BsonDocument { ["name"] = "Morgan Diaz", ["adress"] = "808 Ash Boulevard", ["phone"] = "555-0108", ["email"] = "morgan.diaz@example.com" },
        new BsonDocument { ["name"] = "Quinn Foster", ["adress"] = "909 Spruce Place", ["phone"] = "555-0109", ["email"] = "quinn.foster@example.com" },
        new BsonDocument { ["name"] = "Drew Nguyen", ["adress"] = "110 Aspen Terrace", ["phone"] = "555-0110", ["email"] = "drew.nguyen@example.com" }
    };

    customers.InsertBulk(seedCustomers);
}

while (true)
{
    Console.WriteLine("Customers");
    Console.WriteLine("1 - Count");
    Console.WriteLine("2 - List");
    Console.WriteLine("3 - Delete last");
    Console.WriteLine("4 - Close app");
    Console.Write("Select an option: ");

    var option = Console.ReadLine();
    if (option is null)
    {
        break;
    }

    switch (option)
    {
        case "1":
            Console.WriteLine($"Customers in database: {customers.Count()}");
            break;
        case "2":
            foreach (var customer in customers.FindAll())
            {
                Console.WriteLine(
                    $"{customer["name"].AsString} | {customer["adress"].AsString} | " +
                    $"{customer["phone"].AsString} | {customer["email"].AsString}");
            }
            break;
        case "3":
            var lastCustomer = customers.Find(Query.All("_id", Query.Descending)).FirstOrDefault();
            if (lastCustomer is null)
            {
                Console.WriteLine("No customers to delete.");
            }
            else
            {
                customers.Delete(lastCustomer["_id"]);
                Console.WriteLine($"Deleted customer: {lastCustomer["name"].AsString}");
            }
            break;
        case "4":
            return;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}
