/* 
----- HANTERA INVENTORY -------
 */

using System.Text.Json;
using System.Linq;

namespace lunaris;

public class Inventory
{
    //Tools-variabel
    Tools tools = new();

    //Lagra alla items man kan ha: 
    private List<Item> allItems = new List<Item>();

    //Skapa en array enligt item-klass, detta är användarens inventory
    Item[] userInventory = new Item[5];
    int currentIndex = 0; //Indexräknare för att hålla koll på maxgräns
    public Inventory()
    {
        //Try/catch-block för att se felmeddelanden
        try
        {
            //Kolla om inventory-fil existerar; deserialisera och lagra i tomma listan
            if (File.Exists(@"items/allitems.json"))
            {
                string jsonText = File.ReadAllText(@"items/allitems.json");
                allItems = JsonSerializer.Deserialize<List<Item>>(jsonText)!;
            }
            else
            {
                tools.printMessage(true, ConsoleColor.Red, "Filen 'allitems.json' hittades inte.");
            }
        }
        catch (Exception ex)
        {
            // Fånga och skriv ut eventuella fel
            tools.printMessage(true, ConsoleColor.Red, $"Ett fel inträffade vid deserialisering: {ex.Message}");
        }
    }

    /* METODER */

    //Visa användarens inventory
    public void viewInventory()
    {
        //Kolla om det finns något i inventoryn
        if (userInventory.Length > 0)
        {
            //Loopa igenom
            foreach (Item item in userInventory)
            {
                //Kontrollera så det inte är ett nullvärde (i och med array)
                if (item != null)
                {
                    tools.printMessage(true, ConsoleColor.Blue, $"*-* {item.name} *-*");
                    WriteLine($"{item.description}");
                    WriteLine($"Classification: {item.classification}");
                    WriteLine("-----------------------------------");
                }
                else
                {
                    WriteLine("[Tomt fack]");
                }
            }
        }
        else
        {
            WriteLine("Här var det tomt!");
        }
    }

    //Fixa fram alla items
    public List<Item> getAllItems()
    {
        return allItems;
    }

    //Lägg till i inventory
    public void addToInventory(Item item)
    {
        //Kolla om index är mindre än 5
        if (currentIndex < userInventory.Length)
        {
            userInventory[currentIndex] = item; //Tilldela item på det indexet
            currentIndex++; //Öka indexräknarn
        }
        else
        {
            tools.printMessage(true, ConsoleColor.Red, "Din inventory är proppfull! Du kan inte bära nå mer. Du skulle ha plockat upp saker med lite större eftertänksamhet.");
        }
    }

    //Metod för att kolla vilken klassificering på items man har flest av 
    public string distinctItemCounter()
    {
        try
        {

            var classCount = userInventory.Where(i => i != null).GroupBy(i => i.classification).Select(g => new
            {
                Classification = g.Key,
                Count = g.Count()
            });

            var mostCommon = classCount.OrderByDescending(g => g.Count).FirstOrDefault();

            if (mostCommon != null)
            {
                return mostCommon.Classification!;
            }
            else
            {
                return "Inga items!";
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}