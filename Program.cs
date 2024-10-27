/* MAIN: 
Här ligger själva spelet och handlingarna som påverkar det (valen man gör). 
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

    }

    //Lägret
    static void campFireScene(bool firstTime)
    {
        //Hitta item
        Item mungiga = invy.getItem(10);

        Clear();
        tools.printMessage(true, false, ConsoleColor.Green, "Lägret");
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

        //VALMÖJLIGHETER

        //Skapa ny lista med acceptabla inputs och funktion som ska köras
        var inputs = new List<Command> {
            //Norrut
            new Command(new List<string>{ "1", "norr", "norrut", "gå norr", "gå norrut", "north", "go north", "n", "gå n", "go n" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick norrut!");
                //Gå till skogsglänta
                forestClearing();
            }),

            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                flow.campFireDesc(2);
            }, false)
        };

        //anropa dirHandler med dessa kommandon
        tools.dirHandler(inputs);

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


        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Sydväst till lilla södergläntan
            new Command(new List<string>{ "1", "sydväst", "gå sydväst", "sv", "gå sv", "sw", "go sw", "southwest", "south west", "go southwest", "go south west" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick sydväst!");
                //Gå till Lilla södergläntan
                southClearing();
            }),
            //Västerut till västra skogsstigen
            new Command(new List<string>{ "2", "väst", "gå väst", "v", "gå v", "västerut", "gå västerut", "west", "go west", "w", "go w" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till Västra skogsstigen
                westTrail();
            }),
            //Nordväst, till skogsstig nv
            new Command(new List<string>{ "3", "nordväst", "gå nordväst", "nv", "gå nv", "nw", "go nw", "northwest", "north west", "go northwest", "go north west" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick nordväst!");
                //Gå till 
            }),
            //Nordöst, till Bro vässia
            new Command(new List<string>{ "4", "nordöst", "gå nordöst", "nö", "gå nö", "ne", "go ne", "northeast", "north east", "go northeast", "go north east" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick nordöst!");
                //Gå till 
            }),
            //ÖSterut till östra skogsstigen
            new Command(new List<string>{ "5", "öst", "gå öst", "österut", "gå österut", "ö", "gå ö", "e", "go e", "east", "go east" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick österut!");
                //Gå till östra skogsstigen
                eastTrail();
            }),
            //Söderut till glänta
            new Command(new List<string>{ "6", "syd", "gå syd", "söderut", "gå söderut", "s", "gå s", "go s", "south", "go south" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick söderut!");
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.forestClearingDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
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

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Nordöst tillbaka till glänta
            new Command(new List<string>{ "1", "nordöst", "gå nordöst", "nö", "gå nö", "northeast", "go northeast", "north east", "go north east", "ne", "go ne" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick nordöst!");
                //Gå till skogsglänta
                forestClearing();
            }),

            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.southClearingDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);

    }

    //Skogsstig väst
    static void westTrail()
    {
        tools.printMessage(true, false, ConsoleColor.Green, "Västra skogsstigen");
        flow.westTrailDesc(1);
        flow.westTrailDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut
            new Command(new List<string>{ "1", "väst", "gå väst", "v", "gå v", "västerut", "gå västerut", "west", "go west", "w", "go w" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till trolltrakten
                trollTrakten();
            }),
            //Österut
            new Command(new List<string>{ "2", "öst", "gå öst", "österut", "gå österut", "ö", "gå ö", "e", "go e", "east", "go east" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick österut!");
                //Gå till glänta
                forestClearing();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.westTrailDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
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
            //VALMÖJLIGHETER
            var inputs = new List<Command> {
            //Kullerbytta västerut
            new Command(new List<string>{ "1", "väst", "gå väst", "v", "gå v", "västerut", "gå västerut", "west", "go west", "w", "go w", "kullerbytta väst", "kullerbytta västerut"}, () => {
                //Töm skärm
                Clear();
                WriteLine("Du kullerbyttade västerut förbi trollet!");
                //Gå till grottmynning
                caveEntrance();
            }),
            //Kasta sten
            new Command(new List<string>{ "2", "plocka upp sten", "kasta sten", "kasta", "sten", "pick up rock", "throw rock", "throw", "rock"}, () => {
                //Töm skärm
                Clear();
                WriteLine("Du valde att kasta sten!");
                //Gå till trollDefeated
                trollDefeated();
            }),
            //Stå på dig
            new Command(new List<string>{ "3", "stå på dig", "stand your ground", "stå", "stand", "stor"}, () => {
                //Töm skärm
                Clear();
                WriteLine("Du står kvar på plats!");
                //Gå till Game over;
                flow.gameOver("troll");
            }),
            //Kullerbytta västerut
            new Command(new List<string>{ "4", "öst", "gå öst", "ö", "gå ö", "österut", "gå österut", "east", "go east", "e", "go e", "kullerbytta öst", "kullerbytta österut"}, () => {
                //Töm skärm
                Clear();
                WriteLine("Du kullerbyttade österut förbi trollet!");
                //Gå till Skogsstig väst
                westTrail();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                tools.TypeLine("Det är inte läge att kolla det nu! Du har ett argt troll att hålla koll på!", true);
            }, false)
        };

            //anropa dirHandler med kommandon
            tools.dirHandler(inputs);


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
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut till grottmynning
            new Command(new List<string>{ "1", "väst", "gå väst", "v", "gå v", "västerut", "gå västerut", "west", "go west", "w", "go w", "kullerbytta väst", "kullerbytta västerut" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du kullerbyttade västerut!");
                //Gå till grottmynning
                caveEntrance();
            }),
            //Österut till västra skogsstigen
            new Command(new List<string>{ "2", "öst", "gå öst", "ö", "gå ö", "österut", "gå österut", "east", "go east", "e", "go e", "kullerbytta öst", "kullerbytta österut" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du kullerbyttade österut!");
                //Gå till skogsstig
                westTrail();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.trollTraktenDesc(true, 2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
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

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //In i berget
            new Command(new List<string>{ "1", "norr", "norrut", "gå norr", "gå norrut", "north", "go north", "n", "gå n", "go n", }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick norrut in i berget!");
                //Gå till trollsalen
                trollHall();
            }),
            //Österut, till trolltrakten
            new Command(new List<string>{ "2", "öst", "gå öst", "ö", "gå ö", "österut", "gå österut", "east", "go east", "e", "go e", }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick österut!");
                //Gå till trolltrakten
                trollTrakten();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.caveEntranceDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Trollgrotta
    static void trollHall()
    {
        tools.printMessage(true, false, ConsoleColor.Green, "Trollsalen");
        Item trollspegel = invy.getItem(0);

        flow.trollCaveDesc(1);
        if (!flow.isOwned(trollspegel, invy))
        {
            tools.TypeLine($"Bland högarna av skatter hittar du plötsligt en vacker {trollspegel.name}. {trollspegel.description}", true);
            flow.wantToAdd(trollspegel, invy);
        }
        flow.trollCaveDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Söderut
            new Command(new List<string>{ "1", "syd", "gå syd", "söderut", "gå söderut", "s", "gå s", "go s", "south", "go south" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick tillbaka söderut!");
                //Gå till grottmynning
                caveEntrance();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.trollCaveDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Östra skogsstigen
    static void eastTrail()
    {
        tools.printMessage(true, false, ConsoleColor.Green, "Östra skogsstigen");

        flow.eastTrailDesc();

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut till glänta
            new Command(new List<string>{ "1", "väst", "gå väst", "v", "gå v", "västerut", "gå västerut", "west", "go west", "w", "go w" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till skogsglänta
                forestClearing();
            }),
            //Österut till Halvöstra strand
            new Command(new List<string>{ "2", "öst", "gå öst", "ö", "gå ö", "österut", "gå österut", "east", "go east", "e", "go e" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick österut!");
                //Gå till stranden
                halfEastBeach();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                tools.TypeLine("Vad vill du göra nu?", true);
                     WriteLine("1. Gå västerut");
                     WriteLine("1. Gå österut");
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Halvöstra strand
    static void halfEastBeach()
    {
        tools.printMessage(true, false, ConsoleColor.Green, "Halvöstra strand");

        flow.halfEastBeachDesc(1);

        Item klykring = invy.getItem(4);
        if (!flow.isOwned(klykring, invy))
        {
            tools.TypeLine($"Ett av träden har så långa och låga grenar att det nästan nuddar vattnet. På en av kvistarna ser du något underligt. Det ser ut som en... {klykring.name}", true);
            tools.TypeLine($"{klykring.description}", true);
            flow.wantToAdd(klykring, invy);
            WriteLine();
        }

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut
            new Command(new List<string>{ "1", "väst", "gå väst", "v", "gå v", "västerut", "gå västerut", "west", "go west", "w", "go w" }, () => {
                //Töm skärm
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till skogsstig östra
                eastTrail();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.westTrailDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

}
