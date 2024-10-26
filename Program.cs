/* MAIN: 
Här ligger själva spelet och handlingarna som påverkar det (valen man gör). 
Det är i sort sett ett nätverk av switch-satser.
 */

using System.Reflection.Emit;

namespace lunaris;

class Program
{
    //Initiera klasser för tillgång till metoder från samtliga funktioner
    private static Tools tools = new();
    private static GameFlow flow = new();
    private static Inventory invy = new();
    private static List<Item> allItems = new();

    private static bool trollDead = false;
    private bool sphinxCleared = false;
    private bool seamonsterVisited = false;
    static void Main(string[] args)
    {
        //Lagra samtliga objekt som finns i spelet
        allItems = invy.getAllItems();

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
        tools.printMessage(true, false, ConsoleColor.Green, "Lägret");
        //Hitta item
        Item mungiga = invy.getItem(10);

        Clear();
        //Text att visa om det är första gången man är i rummet
        if (firstTime)
        {
            tools.TypeLine("Ditt äventyr börjar, som så många andras, med att du vaknar upp i en liten skogsglänta helt utan att minnas hur du hamnade där. \n Lyckligtvis är du en sån där lättsam person som inte ser några konstigheter alls i din situation.", true);
        }
        firstTime = false;
        //Beskrivning av scen
        flow.campFireDesc(1);

        //Kolla om item är ägt, fråga om lägg till:
        if (!flow.isOwned(mungiga, invy))
        {
            tools.TypeLine($"I kanten av gläntan ser du något som ligger och glittrar i morgonsolen. En {mungiga.name}! {mungiga.description} \n", true);
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
                case "n":
                    Clear();
                    WriteLine("Du gick norrut!");
                    //Gå till skogsglänta
                    forestClearing();
                    validChoice = true;
                    break;
                case "i":
                case "inventory":
                    Clear();
                    invy.viewInventory();
                    //Visa vägval igen
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
                    //Gå till Lilla södergläntan
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
                    //Gå till västra skogsstigen
                    westTrail();
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
                    //Visa vägval igen
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
                    WriteLine("Du gick nordöst!");
                    //Gå till skogsgläntan
                    forestClearing();
                    validChoice = true;
                    break;
                case "i":
                case "inventory":
                    Clear();
                    invy.viewInventory();
                    //Visa vägval igen
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
        tools.printMessage(true, false, ConsoleColor.Green, "Västra skogsstigen");
        flow.westTrailDesc(1);
        flow.westTrailDesc(2);

        //ANVÄNDARVAL
        //Knapp till while-loop
        bool validChoice = false;
        //Switchsats
        while (!validChoice)
        {
            string userChoice = ReadLine()!.ToLower();
            switch (userChoice)
            {
                case "1":
                case "väst":
                case "gå väst":
                case "v":
                case "gå v":
                case "västerut":
                case "gå västerut":
                case "fortsätt västerut":
                case "go west":
                case "go w":
                case "w":
                    Clear();
                    WriteLine("Du gick västerut!");
                    trollTrakten();
                    validChoice = true;
                    break;
                case "2":
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
                    forestClearing();
                    validChoice = true;
                    break;
                case "i":
                case "inventory":
                    Clear();
                    invy.viewInventory();
                    flow.westTrailDesc(2);
                    break;
                default:
                    tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen på svenska eller engelska!");
                    break;
            }
        }
    }

    //Trolltrakten
    static void trollTrakten()
    {
        tools.printMessage(true, false, ConsoleColor.Green, "Trolltrakten");


        //Om trollet ej är dött:
        if (!trollDead)
        {
            //Skicka med false för trollDead
            flow.trollTraktenDesc(false, 0);

            //ANVÄNDARVAL
            //Knapp till while-loop
            bool validChoice = false;
            //Switchsats
            while (!validChoice)
            {
                string userChoice = ReadLine()!.ToLower();
                switch (userChoice)
                {
                    case "1":
                    case "väst":
                    case "gå väst":
                    case "kullerbytta väst":
                    case "kullerbytta västerut":
                    case "kullerbytta v":
                    case "v":
                    case "gå v":
                    case "västerut":
                    case "gå västerut":
                    case "fortsätt västerut":
                    case "go west":
                    case "go w":
                    case "w":
                        Clear();
                        WriteLine("Du kullerbyttade västerut förbi trollet!");
                        caveEntrance();
                        validChoice = true;
                        break;
                    case "2":
                    case "plocka upp sten":
                    case "kasta sten":
                    case "kasta":
                    case "sten":
                    case "pick up rock":
                    case "throw rock":
                    case "throw":
                    case "rock":
                        Clear();
                        WriteLine("Du kastade sten!");
                        trollDefeated();
                        validChoice = true;
                        break;
                    case "3":
                    case "stå på dig":
                    case "stand your ground":
                    case "stå":
                    case "stand":
                    case "stor":
                        Clear();
                        WriteLine("Du står kvar!");
                        flow.gameOver("troll");
                        validChoice = true;
                        break;
                    case "4":
                    case "öst":
                    case "gå öst":
                    case "österut":
                    case "gå österut":
                    case "kullerbytta öst":
                    case "kullerbytta österut":
                    case "kullerbytta ö":
                    case "east":
                    case "go east":
                    case "e":
                    case "ö":
                        Clear();
                        WriteLine("Du kullerbyttade österut!");
                        westTrail();
                        validChoice = true;
                        break;
                    case "i":
                    case "inventory":
                        tools.TypeLine("Det är inte läge att kolla det nu! Du har ett argt troll att hålla koll på!", true);
                        break;
                    default:
                        tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen på svenska eller engelska!");
                        break;
                }
            }

        }
        else
        {
            //Funktion med besegrat troll 
            trollDefeated();
        }
    }

    //Döda troll
    static void trollDefeated()
    {
        //Om trollet lever: detta är resultat av att kasta sten i trolltrakten
        if (!trollDead)
        {
            //Visa text om hur trollet dör
            flow.killTrollDesc();

        }
        else
        {
            //Beskriv rummet med dött troll
            flow.trollTraktenDesc(true, 1);
        }
        //Lutan
        Item luta = invy.getItem(13);
        //Kolla om lutan ägs 
        if (!flow.isOwned(luta, invy))
        {
            tools.TypeLine("Föremålet som trollet tappade i sitt dödsögonblick ligger kvar på stigen.", true);
            tools.TypeLine($"En {luta.name}! {luta.description}", true);
            flow.wantToAdd(luta, invy);

        }
        //Ge alternativ för riktning
        flow.trollTraktenDesc(true, 2);

        //Slå om trollDead till true 
        trollDead = true;

        //ANVÄNDARVAL
        //Knapp till while-loop
        bool validChoice = false;
        //Switchsats
        while (!validChoice)
        {
            string userChoice = ReadLine()!.ToLower();
            switch (userChoice)
            {
                case "1":
                case "väst":
                case "gå väst":
                case "kullerbytta väst":
                case "kullerbytta västerut":
                case "kullerbytta v":
                case "v":
                case "gå v":
                case "västerut":
                case "gå västerut":
                case "fortsätt västerut":
                case "go west":
                case "go w":
                case "w":
                    Clear();
                    WriteLine("Du gick västerut!");
                    caveEntrance();
                    validChoice = true;
                    break;
                case "2":
                case "öst":
                case "gå öst":
                case "österut":
                case "gå österut":
                case "kullerbytta öst":
                case "kullerbytta österut":
                case "kullerbytta ö":
                case "east":
                case "go east":
                case "e":
                case "ö":
                    Clear();
                    WriteLine("Du gick österut!");
                    westTrail();
                    validChoice = true;
                    break;
                case "i":
                case "inventory":
                    Clear();
                    invy.viewInventory();
                    //Val av riktning
                    flow.trollTraktenDesc(true, 2);
                    break;
                default:
                    tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen på svenska eller engelska!");
                    break;
            }
        }
    }
    //Grottmynning
    static void caveEntrance()
    {
        tools.printMessage(true, false, ConsoleColor.Green, "Grottmynningen");
        Item skuggkappa = invy.getItem(5);

        //Del 1
        flow.caveEntranceDesc(1);

        if (!flow.isOwned(skuggkappa, invy))
        {
            tools.TypeLine($"Du stannar till precis under portvalvet, där någonting fångar din blick ur ögonvrån. En {skuggkappa.name} ligger på den steniga marken.", true);
            tools.TypeLine($"{skuggkappa.description}", true);
            flow.wantToAdd(skuggkappa, invy);
        }
        // Del 2
        flow.caveEntranceDesc(2);

        //ANVÄNDARVAL
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
                case "n":
                    Clear();
                    WriteLine("Du gick norrut!");
                    trollCave();
                    validChoice = true;
                    break;
                case "2":
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
                    trollTrakten();
                    validChoice = true;
                    break;
                case "i":
                case "inventory":
                    Clear();
                    invy.viewInventory();
                    //Val av riktning
                    flow.caveEntranceDesc(2);
                    break;
                default:
                    tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen på svenska eller engelska!");
                    break;
            }
        }


    }

    //Trollgrotta
    static void trollCave()
    {
        tools.printMessage(true, false, ConsoleColor.Green, "Trollgrottan");
        Item trollspegel = invy.getItem(0);

        flow.trollCaveDesc(1);
        if (!flow.isOwned(trollspegel, invy))
        {
            tools.TypeLine($"Bland högarna av skatter hittar du plötsligt en vacker {trollspegel.name}. {trollspegel.description}", true);
            flow.wantToAdd(trollspegel, invy);
        }
        flow.trollCaveDesc(2);

        //ANVÄNDARVAL
        //Knapp till while-loop
        bool validChoice = false;
        //Switchsats
        while (!validChoice)
        {
            string userChoice = ReadLine()!.ToLower();
            switch (userChoice)
            {
                case "1":
                case "syd":
                case "gå syd":
                case "söderut":
                case "gå söderut":
                case "south":
                case "go south":
                case "s":
                    Clear();
                    WriteLine("Du gick söderut!");
                    caveEntrance();
                    validChoice = true;
                    break;

                case "i":
                case "inventory":
                    Clear();
                    invy.viewInventory();
                    //Val av riktning
                    flow.trollCaveDesc(2);
                    break;
                default:
                    tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen på svenska eller engelska!");
                    break;
            }
        }

    }
}
