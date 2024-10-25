using System.Reflection.Emit;

namespace lunaris;

class Program
{
    //Initiera klasser för tillgång till metoder från samtliga funktioner
    private static Tools tools = new();
    private static GameFlow flow = new();
    private static Inventory invy = new();
    private static List<Item> allItems = new();

    private bool trollDead = false;
    private bool sphinxCleared = false;
    private bool seamonsterVisited = false;
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

    //Lägret
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
        bool itemOwned = flow.isOwned(mungiga, invy);
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
                case "gå norrut":
                case "norr":
                case "north":
                case "go north":
                    Clear();
                    WriteLine("Du gick norrut!");
                    forestClearing();
                    validChoice = true;
                    break;
                case "i":
                case "inventory":
                    Clear();
                    invy.viewInventory();
                    flow.campFireDesc(2);
                    break;
                default:
                    tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen med ett enda ord.");
                    break;
            }
        }

    }

    //Skogsglänta
    static void forestClearing()
    {
        //Lagra boots
        Item boots = invy.getItem(8);
        tools.printMessage(true, false, ConsoleColor.Green, "Skogsgläntan");
        flow.forestClearingDesc(1);

        //Kolla om boots finns i inventory, annars erbjud att lägga till
        if (!flow.isOwned(boots, invy))
        {
            tools.TypeLine($"När du sänker blicken till skyltens bas tappar du hakan. {boots.name}!", true);
            tools.TypeLine($"{boots.description}", true);
            flow.wantToAdd(boots, invy);
        }
        //Fortsättning av scenbeskrivning
        flow.forestClearingDesc(2);

        //INPUT
        //Knapp till while-loop
        bool validChoice = false;
        //Switchsats
        while (!validChoice)
        {
            string userChoice = ReadLine()!.ToLower();
            switch (userChoice)
            {
                case "1":
                case "sydväst":
                case "gå sydväst":
                case "sv":
                case "gå sv":
                case "sw":
                case "go sw":
                case "southwest":
                case "south west":
                case "go southwest":
                case "go south west":
                    Clear();
                    WriteLine("Du gick Sydväst!");
                    southClearing();
                    validChoice = true;
                    break;
                case "2":
                case "väst":
                case "gå väst":
                case "västerut":
                case "gå västerut":
                case "west":
                case "go west":
                case "w":
                case "v":
                    Clear();
                    WriteLine("Du gick väst!");
                    validChoice = true;
                    break;
                case "3":
                case "nordväst":
                case "gå nordväst":
                case "nv":
                case "gå nv":
                case "northwest":
                case "north west":
                case "go northwest":
                case "go north west":
                case "go nw":
                    Clear();
                    WriteLine("Du gick nordväst!");
                    validChoice = true;
                    break;
                case "4":
                case "nordöst":
                case "gå nordöst":
                case "nö":
                case "gå nö":
                case "northeast":
                case "north east":
                case "go northeast":
                case "go north east":
                case "go ne":
                    Clear();
                    WriteLine("Du gick nordöst!");
                    validChoice = true;
                    break;
                case "5":
                case "öst":
                case "gå öst":
                case "österut":
                case "gå österut":
                case "east":
                case "go east":
                case "e":
                case "ö":
                    Clear();
                    WriteLine("Du gick österut!");
                    validChoice = true;
                    break;
                case "6":
                case "syd":
                case "gå syd":
                case "söderut":
                case "gå söderut":
                case "south":
                case "go south":
                case "s":
                    Clear();
                    WriteLine("Du gick söderut");
                    campFireScene(false);
                    validChoice = true;
                    break;
                case "i":
                case "inventory":
                    Clear();
                    invy.viewInventory();
                    flow.forestClearingDesc(2);
                    break;
                default:
                    tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen på svenska eller engelska!");
                    break;
            }
        }
    }

    //Lilla södergläntan
    static void southClearing()
    {
        //Lagra scroll
        Item scroll = invy.getItem(16);
        tools.printMessage(true, false, ConsoleColor.Green, "Lilla södergläntan");
        flow.southClearingDesc(1);
        if (!flow.isOwned(scroll, invy))
        {
            tools.TypeLine($"Något fångar ditt öga i det höga gräset. Det är något ihoprullat. Du tar upp föremålet och tittar närmre på det. \n", true);
            tools.TypeLine($"Det är {scroll.name}. {scroll.description}", true);
            flow.wantToAdd(scroll, invy);
        }
        flow.southClearingDesc(2);
        //INPUT
        //Knapp till while-loop
        bool validChoice = false;
        //Switchsats
        while (!validChoice)
        {
            string userChoice = ReadLine()!.ToLower();
            switch (userChoice)
            {
                case "1":
                case "nordöst":
                case "gå nordöst":
                case "nö":
                case "gå nö":
                case "northeast":
                case "north east":
                case "go northeast":
                case "go north east":
                case "go ne":
                    Clear();
                    forestClearing();
                    southClearing();
                    validChoice = true;
                    break;
                case "i":
                case "inventory":
                    Clear();
                    invy.viewInventory();
                    flow.southClearingDesc(2);
                    break;
                default:
                    tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen på svenska eller engelska!");
                    break;
            }
        }
    }

    //Skogsstig väst
    static void westTrail()
    {

    }

    //Trolltrakten
    static void trollTrakten()
    {

    }

    //Grottmynning

    //Trollgrotta
}
