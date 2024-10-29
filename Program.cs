/* MAIN: 
Här ligger själva spelet och handlingarna som påverkar det (valen man gör). 
 */

using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace lunaris;

class Program
{
    //Initiera klasser för tillgång till metoder från samtliga funktioner
    private static Tools tools = new();
    private static GameFlow flow = new();
    private static Inventory invy = new();
    private static List<Item> allItems = new();

    private static bool trollDead = false;
    private static bool sphinxCleared = false;
    private static bool seamonsterVisited = false;

    //Listor för riktningar så man slipper skriva ut dem i varje riktningsCommand
    private static List<string> west = new List<string> { "väst", "gå väst", "v", "gå v", "västerut", "gå västerut", "west", "go west", "w", "go w" };
    private static List<string> northwest = new List<string> { "nordväst", "gå nordväst", "nv", "gå nv", "nw", "go nw", "northwest", "north west", "go northwest", "go north west" };
    private static List<string> north = new List<string> { "norr", "norrut", "gå norr", "gå norrut", "north", "go north", "n", "gå n", "go n" };
    private static List<string> northeast = new List<string> { "nordöst", "gå nordöst", "nordost", "gå nordost", "no", "nö", "gå nö", "ne", "go ne", "northeast", "north east", "go northeast", "go north east" };
    private static List<string> east = new List<string> { "öst", "gå öst", "österut", "gå österut", "ost", "gå ost", "ö", "gå ö", "e", "go e", "east", "go east" };
    private static List<string> southeast = new List<string> { "sydöst", "gå sydöst", "sydost", "gå sydost", "so", "sö", "gå sö", "sw", "go sw", "southeast", "south east", "go southeast", "go south east" };
    private static List<string> south = new List<string> { "syd", "gå syd", "söderut", "gå söderut", "s", "gå s", "go s", "south", "go south" };
    private static List<string> southwest = new List<string> { "sydväst", "gå sydväst", "sv", "gå sv", "sw", "go sw", "southwest", "south west", "go southwest", "go south west" };

    static void Main(string[] args)
    {
        //Visa meny!
        flow.intro();
        //Sätt first Time till true så man "vaknar upp"
        bool firstTime = true;
        //Första "rummet"
        campFireScene(firstTime);


    }

    //Lägret
    static void campFireScene(bool firstTime)
    {
        Clear();
        tools.printTitle("Lägret");

        //Hitta item
        Item mungiga = invy.getItem(10);

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
            new Command(new List<string>{ "1" }.Concat(north).ToList(), () => {
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
        tools.printTitle("Skogsgläntan");
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
            new Command(new List<string>{ "1" }.Concat(southwest).ToList(), () => {
                Clear();
                WriteLine("Du gick sydväst!");
                //Gå till Lilla södergläntan
                southClearing();
            }),
            //Västerut till västra skogsstigen
            new Command(new List<string>{ "2" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till Västra skogsstigen
                westTrail();
            }),
            //Nordväst, till skogsstig nv
            new Command(new List<string>{ "3" }.Concat(northwest).ToList(), () => {
                Clear();
                WriteLine("Du gick nordväst!");
                //Gå till nordvästra skogsleden
                northwestTrail();
            }),
            //Nordöst, till Bro vässia
            new Command(new List<string>{ "4" }.Concat(northeast).ToList(), () => {
                Clear();
                WriteLine("Du gick nordöst!");
                //Gå till bro vässia
                bridgeWest("sw");
            }),
            //ÖSterut till östra skogsstigen
            new Command(new List<string>{ "5" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till östra skogsstigen
                eastTrail();
            }),
            //Söderut till glänta
            new Command(new List<string>{ "6" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick söderut!");
                campFireScene(false);
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
        tools.printTitle("Lilla södergläntan");
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
            new Command(new List<string>{ "1" }.Concat(northeast).ToList(), () => {
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
        tools.printTitle("Västra skogsstigen");

        flow.westTrailDesc(1);
        flow.westTrailDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut
            new Command(new List<string>{ "1" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till trolltrakten
                trollTrakten();
            }),
            //Österut
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
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

        tools.printTitle("Trolltrakten");


        //Om trollet ej är dött:
        if (!trollDead)
        {
            //Skicka med false för trollDead
            flow.trollTraktenDesc(false, 0);
            //VALMÖJLIGHETER
            var inputs = new List<Command> {
            //Kullerbytta västerut
            new Command(new List<string>{ "1", "kullerbytta väst", "kullerbytta västerut"}.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du kullerbyttade västerut förbi trollet!");
                //Gå till grottmynning
                caveEntrance();
            }),
            //Kasta sten
            new Command(new List<string>{ "2", "plocka upp sten", "kasta sten", "kasta", "sten", "pick up rock", "throw rock", "throw", "rock"}, () => {
                Clear();
                WriteLine("Du valde att kasta sten!");
                //Gå till trollDefeated
                trollDefeated();
            }),
            //Stå på dig
            new Command(new List<string>{ "3", "stå på dig", "stand your ground", "stå", "stand", "stor"}, () => {
                Clear();
                WriteLine("Du står kvar på plats!");
                //Gå till Game over;
                flow.gameOver("troll");
            }),
            //Kullerbytta västerut
            new Command(new List<string>{ "4", "kullerbytta öst", "kullerbytta österut"}.Concat(east).ToList(), () => {
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
            new Command(new List<string>{ "1", "kullerbytta väst", "kullerbytta västerut" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du kullerbyttade västerut!");
                //Gå till grottmynning
                caveEntrance();
            }),
            //Österut till västra skogsstigen
            new Command(new List<string>{ "2", "kullerbytta öst", "kullerbytta österut" }.Concat(east).ToList(), () => {
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
        tools.printTitle("Grottmynningen");
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
            new Command(new List<string>{ "1" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du gick norrut in i berget!");
                //Gå till trollsalen
                trollHall();
            }),
            //Österut, till trolltrakten
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
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
        tools.printTitle("Trollsalen");
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
            new Command(new List<string>{ "1" }.Concat(south).ToList(), () => {
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
        tools.printTitle("Östra skogsstigen");

        flow.eastTrailDesc();

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut till glänta
            new Command(new List<string>{ "1" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till skogsglänta
                forestClearing();
            }),
            //Österut till Halvöstra strand
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
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
                     WriteLine("2. Gå österut");
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Halvöstra strand
    static void halfEastBeach()
    {
        tools.printTitle("Halvöstra strand");

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
            new Command(new List<string>{ "1" }.Concat(west).ToList(), () => {
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
                flow.halfEastBeachDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Nordvästra skogsstigen
    static void northwestTrail()
    {
        tools.printTitle("Nordvästra leden");

        flow.northwestTrailDesc();

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Nordväst mot drakglänta
            new Command(new List<string>{ "1" }.Concat(northwest).ToList(), () => {
                Clear();
                WriteLine("Du gick nordväst");
                //Gå till drakglänta
                dragonClearing();
            }),
            //Sydöst mot skogsglänta
            new Command(new List<string>{ "2"  }.Concat(southeast).ToList(), () => {
                Clear();
                WriteLine("Du gick sydöst!");
                //Gå till skogsglänta
                forestClearing();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                tools.TypeLine("Vad vill du göra?", true);
                WriteLine("1. Följ vägen nordväst");
                WriteLine("1. Följ vägen sydöst");
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    static void dragonClearing()
    {
        tools.printTitle("Drakgläntan");

        Item drakfjall = invy.getItem(19);

        flow.dragonClearingDesc(1);

        if (!flow.isOwned(drakfjall, invy))
        {
            WriteLine();
            tools.TypeLine($"Medan du funderar på vad du ska göra blänker någonting till i gräset. Ett {drakfjall.name}! \n {drakfjall.description} \n", true);
            flow.wantToAdd(drakfjall, invy);
        }

        flow.dragonClearingDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Kryp västerut
            new Command(new List<string>{ "1", "kryp västerut", "kryp väst", "kryp v", "crawl west", "crawl w"}, () => {
                Clear();
                WriteLine("Du kröp västerut!");
                //Gå till trädrantälvkant
                treeLineRiver();
            }),
            //Spring västerut
            new Command(new List<string>{ "2", "spring västerut", "spring väst", "spring v", "run west", "run w"}, () => {
                Clear();
                WriteLine("Du sprang västerut!");
                //Gå till game over
                flow.gameOver("drake");
            }),
            //Kryp norrut
            new Command(new List<string>{ "3", "kryp norrut", "kryp norr", "kryp n", "crawl north", "crawl n" }, () => {
                Clear();
                WriteLine("Du kröp norrut!");
                //Gå till älvbanken
                riverBank("s");
            }),
            //Spring norrut
            new Command(new List<string>{ "4", "spring norrut", "spring norr", "spring n", "run north", "run n" }, () => {
                Clear();
                WriteLine("Du sprang norrut!");
                //Gå till game over
                flow.gameOver("drake");
            }),
            //Kryp sydöst
            new Command(new List<string>{ "5", "kryp sydöst", "kryp sö", "kryp sydost", "kryp so", "kryp sydösterut", "crawl southeast", "crawl south east", "crawl se" }, () => {
                Clear();
                WriteLine("Du kröp sydöst!");
                //Gå till nordvästra stigen
                northwestTrail();
            }),
            //Spring Sydöst
            new Command(new List<string>{ "6", "spring sydöst", "spring sö", "spring sydost", "spring so", "spring sydösterut", "run southeast", "run south east", "run se" }, () => {
                Clear();
                WriteLine("Du sprang sydöst!");
                //Gå till game over
                flow.gameOver("drake");
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.dragonClearingDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Trädrandälvkant
    static void treeLineRiver()
    {
        tools.printTitle("Trädrandälvkant");

        Item bestiarium = invy.getItem(18);

        flow.treelineRiverDesc(1);

        //Kolla om bestiarium ägs redan, lägg till annars
        if (!flow.isOwned(bestiarium, invy))
        {
            tools.TypeLine($"Vid älvkanten har någon glömt ett föremål. Du tar upp det och tittar närmre på det. '{bestiarium.name}' står det. \n {bestiarium.description}", true);
            flow.wantToAdd(bestiarium, invy);
            WriteLine();
        }

        flow.treelineRiverDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Nordöst
            new Command(new List<string>{ "1", }, () => {
                Clear();
                WriteLine("Du gick nordöst!");
                //Gå till älvbanken
                riverBank("sw");
            }),
            //Österut
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till drakglänta
                dragonClearing();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.treelineRiverDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);

    }
    //Älvbanken
    static void riverBank(string direction)
    {

        tools.printTitle("Älvbanken");

        //Beskrivning med riktning
        flow.riverBankDesc(direction);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Kliv i båten, ro norr
            new Command(new List<string>{ "1", "kliv ner i båten", "kliv i båt", "kliv i båten", "båt", "båten", "boat", "the boat", "get in boat", "get in the boat" }, () => {
                Clear();
                WriteLine("Du steg ner i båten!");
                //Gå till båtscen
                inBoat();
            }),
            //Söderut
            new Command(new List<string>{ "2" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick söderut");

                dragonClearing();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                tools.TypeLine("Vad vill du göra?", true);
                WriteLine("1. Kliv ner i båten");
                WriteLine("2. Gå söderut, tillbaka mot draken");
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Ombord på båt
    static void inBoat()
    {
        Item pumps = invy.getItem(9);

        tools.printTitle("Ombord på båt");

        flow.inBoatDesc(1);

        if (!flow.isOwned(pumps, invy))
        {
            tools.TypeLine($"Medan du river runt bland grejerna på båten drar du undan en presenning och blottlägger ett förvånande föremål. {pumps.name}? ", true);
            tools.TypeLine($"{pumps.description} ", true);
            flow.wantToAdd(pumps, invy);
        }

        flow.inBoatDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Norrut mot norra strand
            new Command(new List<string>{ "1" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du kommenderar båten med tvekan i rösten. Till din lättnad börjar den röra sig lätt norrut.");
                //Gå till norra strand
                northShore();
            }),
            //Söderut mot älvkanten
            new Command(new List<string>{ "2"  }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du kommenderar båten med tvekan i rösten. Till din lättnad börjar den röra sig lätt söderut.");
                //Gå till älvkant
                riverBank("n");
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.inBoatDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);

    }

    //Norra strand
    static void northShore()
    {
        tools.printTitle("Norra strand");

        Item silvertejp = invy.getItem(2);
        flow.northShoreDesc(1);

        if (!flow.isOwned(silvertejp, invy))
        {
            tools.TypeLine($"Där båten lägger an vid stranden sparkar du av misstag till någonting. Det verkar vara {silvertejp.name}...", true);
            tools.TypeLine($"{silvertejp.description} ", true);
            flow.wantToAdd(silvertejp, invy);
        }

        flow.northShoreDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Riktning
            new Command(new List<string>{ "1","kliv ner i båten", "kliv i båt", "kliv i båten", "båt", "båten", "boat", "the boat", "get in boat", "get in the boat" }, () => {
                Clear();
                WriteLine("Du klev tillbaka ner i båten! ");
                //Gå till båt
                inBoat();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.northShoreDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Bro: Vässia
    static void bridgeWest(string direction)
    {
        tools.printTitle("Bro: vässia");

        flow.bridgeWestDesc(direction);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Lilla nordergläntan
            new Command(new List<string>{ "1"  }.Concat(northwest).ToList(), () => {
                Clear();
                WriteLine("Du gick nordväst!");
                //Gå till norderglänta
                northClearing();
            }),
            //Bro össia
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut över bron!");
                //Baserat på om sfinxen är besegrad eller ej: 
                if(sphinxCleared == false) {
                    sphinxChallenge();
                } else {
                    //Bro össia; inte första gången man är där
                    bridgeEast(false);
                }
            }),
            //Gläntan
            new Command(new List<string>{ "3" }.Concat(southwest).ToList(), () => {
                Clear();
                WriteLine("Du gick sydväst!");
                //Gå till skogsgläntan
                forestClearing();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                tools.TypeLine("Vad vill du göra?", true);
                WriteLine("1. Följ nordvästra stigen");
                WriteLine("2. Gå österut över bron");
                WriteLine("3. Följ skogsvägen sydväst");
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Lilla nordergläntan
    static void northClearing()
    {
        tools.printTitle("Lilla nordergläntan");

        Item draktrumma = invy.getItem(11);

        flow.northClearingDesc(1);

        if (!flow.isOwned(draktrumma, invy))
        {
            tools.TypeLine("Bland de brända och förvridna liken av träd hittar du ett besynnerligt föremål.", true);
            tools.TypeLine($"En {draktrumma.name}! {draktrumma.description}", true);
            flow.wantToAdd(draktrumma, invy);
        }

        flow.northClearingDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Sydöst
            new Command(new List<string>{ "1" }.Concat(southeast).ToList(), () => {
                Clear();
                WriteLine("Du gick sydöst!");
                //Gå till bro: vässia
                bridgeWest("nw");
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.northClearingDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Sfinxens gåta
    static void sphinxChallenge()
    {
        tools.printTitle("Bro: össia");

        //Håll reda på försök
        int wrongGuesses = 0;

        //Beskrivning 1
        flow.sphinxChallengeDesc(1);
        //Själva gåtan
        flow.sphinxChallengeDesc(2);


        //Av/på-knapp till switch
        bool validAnswer = false;

        //Switchsats
        while (!validAnswer)
        {
            //Användarens gissning
            string guess = ReadLine()!.ToLower();

            switch (guess)
            {
                case "mot sin vilja":
                case "sin vilja":
                case "viljan":
                case "vilja":
                    sphinxCleared = true;
                    validAnswer = true;
                    Clear();
                    bridgeEast(true);
                    break;
                /* SVAR TILL ANDRA GÅTAN
                case "trappan":
                case "trappa":
                case "stairs":
                    sphinxCleared = true;
                    Clear();
                    bridgeEast();
                    break; */
                default:
                    wrongGuesses++;
                    //Om man misslyckats tre gånger dör man
                    if (wrongGuesses == 3)
                    {
                        flow.gameOver("sfinx");
                    }
                    string grammatik;
                    //Fixa grammatiken lite för annars blir det fult
                    if (3 - wrongGuesses >= 2)
                    {
                        grammatik = "gissningar";
                    }
                    else
                    {
                        grammatik = "gissning";
                    }
                    //Fel gissat
                    tools.printMessage(false, true, ConsoleColor.Red, "Det är fel! ");
                    tools.TypeLine($"Sfinxen slickar sig hoppfullt om läpparna. 'Du har {3 - wrongGuesses} {grammatik} kvar', informerar hon dig. \n", false);
                    break;
            }
        }
    }

    //Bro: össia (sfinxen besegrad)
    static void bridgeEast(bool firstTime)
    {
        tools.printTitle("Bro: össia");

        Item botanikerboken = invy.getItem(17);
        //Beskrivning baserat på om man vart där förr eller ej
        flow.bridgeEastDesc(1, firstTime);

        if (!flow.isOwned(botanikerboken, invy))
        {
            WriteLine();
            tools.TypeLine($"På stigen, strax till höger om sfinxens sittplats, ligger det kvar någonting. Du ser snabbt vad det är. {botanikerboken.name}!", true);
            tools.TypeLine($"{botanikerboken.description}", true);
            flow.wantToAdd(botanikerboken, invy);
        }
        flow.bridgeEastDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Norrut, till storslätta
            new Command(new List<string>{ "1" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du gick norrut!");
                //Gå till storslätta
                storSlatt();
            }),
            //Västerut till vässia
            new Command(new List<string>{ "2" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut över bron!");
                //Gå till bro vässia, österifrån
                bridgeWest("e");
            }),
            //Sydost till tämliga tältet
            new Command(new List<string>{ "3" }.Concat(southeast).ToList(), () => {
                Clear();
                WriteLine("Du gick sydöst!");
                //Gå till tämliga tältet
                tentScene();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.bridgeEastDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Tämliga tältet
    static void tentScene()
    {
        tools.printTitle("Tämliga tältet");

        Item featherboa = invy.getItem(6);

        flow.tentSceneDesc(1);

        if (!flow.isOwned(featherboa, invy))
        {
            tools.TypeLine("Nyfikenheten tar överhanden och du petar in huvudet i tältöppningen. Vad du ser där får dig att dra efter andan.", true);
            tools.TypeLine($"En {featherboa.name}! {featherboa.description}", true);
            flow.wantToAdd(featherboa, invy);
        }
        else
        {
            tools.TypeLine("Du slår den tanken ur hågen. Det vore ju en jättekonstig sak att göra.", true);
            tools.TypeLine("Du bestämmer dig för att låta tältet vara ett av livets stora mysterium. ", true);
        }
        flow.tentSceneDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Bro össia
            new Command(new List<string>{ "1" }.Concat(northwest).ToList(), () => {
                Clear();
                WriteLine("Du gick tillbaka nordvästerut!");
                //Gå till bro össia; ej första gången man är där
                bridgeEast(false);
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.tentSceneDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Storslätta
    static void storSlatt()
    {
        tools.printTitle("Storslätta");

        flow.storSlattDesc(1);
        flow.storSlattDesc(2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut, Spurten
            new Command(new List<string>{ "1" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till
            }),
            //Norrut, bråddjupa brallblötan
            new Command(new List<string>{ "2" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du gick norrut!");
                //Gå till brallblötan
                brallBlotan();
            }),
            //Österut, Förtvivlans fält
            new Command(new List<string>{ "3" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till
            }),
            //Söderut, bro össia
            new Command(new List<string>{ "4" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick söderut!");
                //Gå till bro össia
                bridgeEast(false);
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.storSlattDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Bråddjupa brallblötan
    static void brallBlotan()
    {
        tools.printTitle("Bråddjupa Brallblötan");

        //Kör första beskrivningen
        flow.brallblotanDesc(1);

        //Om man inte träffat George Skörwen tidigare
        if (!seamonsterVisited)
        {
            //Lagra om man har pärlan
            bool pearlOwned = flow.isOwned(invy.getItem(20), invy);
            //Kör George Skörwens beskrivning
            flow.georgeSkoerweDesc(pearlOwned, 0);
            //Nästa del 
            flow.georgeSkoerweDesc(pearlOwned, 1);

            //Om man har pärlan, visa bytesvalet
            if (pearlOwned)
            {

                //VALMÖJLIGHETER
                var inputs = new List<Command> {
                //Byt med George Skörwen
                 new Command(new List<string>{ "1", "ja", "yes", "byt", "trade" }, () => {
                    Clear();
                    WriteLine("Du gick med på att byta med George Skörwen!");
                    //Gå till bytesfunktion
                    tradeWithGeorge();
                 }),
                 //Neka ett byte
                 new Command(new List<string>{ "2", "nej", "no", "dont", "don't", "don't trade", "dont trade", "byt ej", "byt inte" }, () => {
                    Clear();
                    WriteLine("Du nekade att byta med George Skörwen!");

                    WriteLine("Tryck på enter för att fortsätta.");
                    ReadKey();
                    Clear();
                    //Game over
                    flow.gameOver("georgeSkörwe");
                }),
                //Kolla inventory
                new Command(new List<string>{ "i", "inventory" }, () => {
                    Clear();
                    invy.viewInventory();
                    //Visa val igen
                    WriteLine();
                    tools.TypeLine("Du funderar ett slag. Vad vill du göra?", true);
                    WriteLine("1. Gå med på ett byte.");
                    WriteLine("2. Tacka nej.");
                }, false)
                };

                //anropa dirHandler med kommandon
                tools.dirHandler(inputs);
            }

            //Om man inte har pärlan, dö
            if (!pearlOwned)
            {
                WriteLine("Tryck på enter för att fortsätta.");
                ReadKey();
                Clear();
                flow.gameOver("georgeSkörwe");
            }
        }
        else
        {
            //Man har träffat George Skörwen förut
            flow.brallblotanDesc(2);

            //VALMÖJLIGHETER
            var inputs = new List<Command> {
            //Söderut mot storslätta
            new Command(new List<string>{ "1" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick söderut!");
                //Gå till storslätta
                storSlatt();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.brallblotanDesc(2);
            }, false)
        };

            //anropa dirHandler med kommandon
            tools.dirHandler(inputs);
        }
    }
    //Byte med Skörwe
    static void tradeWithGeorge()
    {


        //Index på pärlan
        Item pearl = invy.getItem(20);
        int index = Array.FindIndex(invy.userInventory, i => i.name == pearl.name);

        //Kör texten där man gått med på byte
        flow.georgeSkoerweDesc(true, 2);

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
             //Magic artefact
             new Command(new List<string>{ "1", "m", "ma", "magic", "magic artefact", "magic artifact", "artifact", "artefact", "artefakt", "magi"}, () => {
                 //Byt pärlan mot Stormrubin
                 invy.userInventory[index] = invy.getItem(3);
                 WriteLine();
                 tools.TypeLine($"I gengäld för pärlan fick du en {invy.getItem(3).name}! {invy.getItem(3).description}", true);
             }, true),
             //Wearable
             new Command(new List<string>{ "2", "wearable", "wearables", "kläder", "clothes", "w"}, () => {
                 //Byt pärlan mot Månställ
                 invy.userInventory[index] = invy.getItem(7);
                 WriteLine();
                 tools.TypeLine($"I gengäld för pärlan fick du ett {invy.getItem(7).name}! {invy.getItem(7).description}", true);
             }, true),
             //Instrument
             new Command(new List<string>{ "3", "i", "instrument" }, () => {
                 //Byt pärlan mot Vindflöjt
                 invy.userInventory[index] = invy.getItem(14);
                 WriteLine();
                 tools.TypeLine($"I gengäld för pärlan fick du en {invy.getItem(14).name}! {invy.getItem(14).description}", true);
             }, true),
             //Instrument
             new Command(new List<string>{ "4", "academia", "a" }, () => {
                 //Byt pärlan mot Grimoire
                 WriteLine();
                 invy.userInventory[index] = invy.getItem(15);
                 tools.TypeLine($"I gengäld för pärlan fick du ett {invy.getItem(15).name}! {invy.getItem(15).description}", true);
             }, true),
             //Kolla inventory
             new Command(new List<string>{ "i", "inventory" }, () => {
                 Clear();
                 invy.viewInventory();
                 //Visa val igen
                flow.georgeSkoerweDesc(true, 2);
             }, false)
         };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);

        //Kör text efter man är klar med bytet
        flow.georgeSkoerweDesc(true, 3);

        //Slå om besökt-variabeln
        seamonsterVisited = true;

        //Visa val att gå
        flow.brallblotanDesc(2);

        //VALMÖJLIGHETER
        var choice = new List<Command> {
            //Söderut mot storslätta
            new Command(new List<string>{ "1" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick söderut!");
                //Gå till storslätta
                storSlatt();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.brallblotanDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(choice);
    }
}

