using System.Reflection.Emit;

namespace lunaris;

class Program
{
    //Initiera klasser för tillgång till metoder från samtliga funktioner
    private static Tools tools = new();
    private static GameFlow flow = new();
    private static Inventory invy = new();
    private static List<Item> allItems = new();
    static void Main(string[] args)
    {
        //Lagra samtliga objekt som finns i spelet
        allItems = invy.getAllItems();

        //Testkod för att se om den sköter sig
        /* flow.wantToAdd(allItems[18], invy);
        flow.wantToAdd(allItems[9], invy);
        flow.wantToAdd(allItems[19], invy);
        flow.wantToAdd(allItems[2], invy);
        invy.viewInventory();
        WriteLine(flow.isOwned(invy, allItems[2]));
        WriteLine(flow.isOwned(invy, allItems[12]));
        string final = invy.distinctItemCounter();
        WriteLine(final); */

        //Visa meny!
        flow.intro();

        bool firstTime = true;
        //Första "rummet"
        campFireScene(firstTime);

        //Credits
        tools.gameCredits();

    }
    static void campFireScene(bool firstTime)
    {
        //Hitta item
        Item mungiga = invy.getItem(10);

        Clear();
        if (firstTime)
        {
            tools.TypeLine("Ditt äventyr börjar, som så många andras, med att du vaknar upp i en liten skogsglänta helt utan att minnas hur du hamnade där. \n Lyckligtvis är du en sån där lättsam person som inte ser några konstigheter alls i din situation.", true);
        }
        firstTime = false;
        //Beskrivning av scen
        flow.campFireDesc(1);

        //Kolla om item är ägt, fråga om lägg till:
        bool itemOwned = flow.isOwned(invy, mungiga);
        if (!itemOwned)
        {
            tools.TypeLine($"I kanten av gläntan ser du något som ligger och glittrar i morgonsolen. En {mungiga.name}! {mungiga.description}. \n", true);
            flow.wantToAdd(mungiga, invy);
        }
        flow.campFireDesc(2);

        //Knapp till while-loop
        bool validChoice = false;
        //Switchsats
        while (!validChoice)
        {
            string userChoice = ReadLine()!.ToLower();
            switch (userChoice)
            {
                case "1":
                case "norrut":
                case "norr":
                case "north":
                    WriteLine("Du gick norrut!");
                    validChoice = true;
                    break;
                case "i":
                case "inventory":
                    invy.viewInventory();
                    break;
                default:
                    tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen med ett enda ord.");
                    break;
            }
        }

    }
}
