namespace lunaris;

class Program
{
    static void Main(string[] args)
    {
        //Initiera klasser för tillgång till metoder
        Inventory invy = new Inventory();
        GameFlow flow = new();

        //Lagra samtliga objekt som finns i spelet
        List<Item> allItems = new();
        allItems = invy.getAllItems();


        Item specificItem = allItems[10];
        flow.wantToAdd(specificItem, invy);
        Item anotherItem = allItems[17];
        flow.wantToAdd(anotherItem, invy);
        flow.wantToAdd(allItems[18], invy);
        flow.wantToAdd(allItems[9], invy);
        invy.viewInventory();

        string final = invy.distinctItemCounter();
        WriteLine(final);
    }
}
