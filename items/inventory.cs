/* 
----- HANTERA INVENTORY -------
 */

using System.Text.Json;
using System.Linq;
using System.ComponentModel;

namespace lunaris;

public class Inventory
{
    //Tools-variabel
    Tools tools = new();

    //Lagra alla items man kan ha: 
    private List<Item> allItems = new List<Item>();

    //Skapa en array enligt item-klass, detta är användarens inventory
    public Item[] userInventory = new Item[5];
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
                tools.printMessage(true, false, ConsoleColor.Red, "Filen 'allitems.json' hittades inte.");
            }
        }
        catch (Exception ex)
        {
            // Fånga och skriv ut eventuella fel
            tools.printMessage(true, false, ConsoleColor.Red, $"Ett fel inträffade vid deserialisering: {ex.Message}");
        }
    }

    /* METODER */

    //Visa användarens inventory
    public void viewInventory()
    {
        //Kolla om det finns något i inventoryn
        if (userInventory.Length > 0)
        {
            WriteLine();
            tools.printMessage(true, false, ConsoleColor.Green, "I N V E N T O R Y :");
            WriteLine();
            //Loopa igenom
            foreach (Item item in userInventory)
            {
                //Kontrollera så det inte är ett nullvärde (i och med array)
                if (item != null)
                {
                    tools.printMessage(true, false, ConsoleColor.Blue, $"*-* {item.name} *-*");
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
    //Returnera användarens inventory
    public Item[] returnInvy()
    {
        return userInventory;
    }
    //Fixa fram alla items: tror inte denna behövs
    public List<Item> getAllItems()
    {
        return allItems;
    }

    //Fixa fram specifikt item
    public Item getItem(Index i)
    {
        return allItems[i];
    }

    //Lägg till i inventory
    public string addToInventory(Item item)
    {
        //Kolla om index är mindre än 5
        if (currentIndex < userInventory.Length)
        {
            userInventory[currentIndex] = item; //Tilldela item på det indexet
            currentIndex++; //Öka indexräknarn
            return $"Du är nu stolt ägare till {item.name}!";
        }
        else
        {
            return "Din inventarie är proppfull! Du kan inte bära fler saker. Du kanske skulle ha plockat upp föremål med mer eftertänksamhet?";
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

    //Metod för att se om man har samlat alla 5 objekt inom klass
    public bool checkForFive()
    {
        try
        {
            //Loopa genom klassificeringarna, gruppera dem, räkna igenom grupper och returnera true om antalet är exakt 5 i gruppen
            return userInventory.Where(i => i != null).GroupBy(i => i.classification).Any(g => g.Count() == 5);
        }
        catch
        {
            return false;
        }
    }
}