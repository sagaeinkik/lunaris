namespace lunaris;

public class SceneDirection
{

    Tools tools = new();
    GameFlow flow = new();

    //RIKTNINGAR
    private static List<string> west = new List<string> { "väst", "gå väst", "v", "gå v", "västerut", "gå västerut", "west", "go west", "w", "go w" };
    private static List<string> northwest = new List<string> { "nordväst", "gå nordväst", "nv", "gå nv", "nw", "go nw", "northwest", "north west", "go northwest", "go north west" };
    private static List<string> north = new List<string> { "norr", "norrut", "gå norr", "gå norrut", "north", "go north", "n", "gå n", "go n" };
    private static List<string> northeast = new List<string> { "nordöst", "gå nordöst", "nordost", "gå nordost", "no", "nö", "gå nö", "ne", "go ne", "northeast", "north east", "go northeast", "go north east" };
    private static List<string> east = new List<string> { "öst", "gå öst", "österut", "gå österut", "ost", "gå ost", "ö", "gå ö", "e", "go e", "east", "go east" };
    private static List<string> southeast = new List<string> { "sydöst", "gå sydöst", "sydost", "gå sydost", "so", "sö", "gå sö", "sw", "go sw", "southeast", "south east", "go southeast", "go south east" };
    private static List<string> south = new List<string> { "syd", "gå syd", "söderut", "gå söderut", "s", "gå s", "go s", "south", "go south" };
    private static List<string> southwest = new List<string> { "sydväst", "gå sydväst", "sv", "gå sv", "sw", "go sw", "southwest", "south west", "go southwest", "go south west" };


    //LÄGERKONTROLLER
    public void campFireControls(Inventory invy)
    {
        //VALMÖJLIGHETER

        //Skapa ny lista med acceptabla inputs och funktion som ska köras
        var inputs = new List<Command> {
            //Norrut
            new Command(new List<string>{ "1" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du gick norrut!");
                //Gå till skogsglänta
                Program.forestClearing();
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

    //Skogsgläntan
    public void forestClearingCtrl(Inventory invy)
    {

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Sydväst till lilla södergläntan
            new Command(new List<string>{ "1" }.Concat(southwest).ToList(), () => {
                Clear();
                WriteLine("Du gick sydväst!");
                //Gå till Lilla södergläntan
                Program.southClearing();
            }),
            //Västerut till västra skogsstigen
            new Command(new List<string>{ "2" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till Västra skogsstigen
                Program.westTrail();
            }),
            //Nordväst, till skogsstig nv
            new Command(new List<string>{ "3" }.Concat(northwest).ToList(), () => {
                Clear();
                WriteLine("Du gick nordväst!");
                //Gå till nordvästra skogsleden
                Program.northwestTrail();
            }),
            //Nordöst, till Bro vässia
            new Command(new List<string>{ "4" }.Concat(northeast).ToList(), () => {
                Clear();
                WriteLine("Du gick nordöst!");
                //Gå till bro vässia
                Program.bridgeWest("sw");
            }),
            //ÖSterut till östra skogsstigen
            new Command(new List<string>{ "5" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till östra skogsstigen
                Program.eastTrail();
            }),
            //Söderut till glänta
            new Command(new List<string>{ "6" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick söderut!");
                Program.campFireScene(false);
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
    public void southClearingCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Nordöst tillbaka till glänta
            new Command(new List<string>{ "1" }.Concat(northeast).ToList(), () => {
                Clear();
                WriteLine("Du gick nordöst!");
                //Gå till skogsglänta
                Program.forestClearing();
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
    //Västra skogsstigen
    public void westTrailCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut
            new Command(new List<string>{ "1" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till trolltrakten
                Program.trollTrakten();
            }),
            //Österut
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till glänta
                Program.forestClearing();
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

    //Trolltrakten (troll lever)
    public void aliveTrollCtrl(Inventory invy)
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
                Program.caveEntrance();
            }),
            //Kasta sten
            new Command(new List<string>{ "2", "plocka upp sten", "kasta sten", "kasta", "sten", "pick up rock", "throw rock", "throw", "rock"}, () => {
                Clear();
                WriteLine("Du valde att kasta sten!");
                //Gå till trollDefeated
                Program.trollDefeated();
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
                Program.westTrail();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                tools.TypeLine("Det är inte läge att kolla det nu! Du har ett argt troll att hålla koll på!", true);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);

    }

    //Trolltrakten (troll besegrat) 
    public void trolltraktCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut till grottmynning
            new Command(new List<string>{ "1", "kullerbytta väst", "kullerbytta västerut" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du kullerbyttade västerut!");
                //Gå till grottmynning
                Program.caveEntrance();
            }),
            //Österut till västra skogsstigen
            new Command(new List<string>{ "2", "kullerbytta öst", "kullerbytta österut" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du kullerbyttade österut!");
                //Gå till skogsstig
                Program.westTrail();
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
    public void caveCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //In i berget
            new Command(new List<string>{ "1" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du gick norrut in i berget!");
                //Gå till trollsalen
                Program.trollHall();
            }),
            //Österut, till trolltrakten
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till trolltrakten
                Program.trollTrakten();
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

    //Trollsalen
    public void trollHallCtrl(Inventory invy)
    {

        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Söderut
            new Command(new List<string>{ "1" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick tillbaka söderut!");
                //Gå till grottmynning
                Program.caveEntrance();
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

    //ÖStra skogsstigen
    public void eastTrailCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut till glänta
            new Command(new List<string>{ "1" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till skogsglänta
                Program.forestClearing();
            }),
            //Österut till Halvöstra strand
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till stranden
                Program.halfEastBeach();
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
    public void halfEastBeachCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut
            new Command(new List<string>{ "1" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till skogsstig östra
                Program.eastTrail();
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

    //Nordvästra leden
    public void nwTrailCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Nordväst mot drakglänta
            new Command(new List<string>{ "1" }.Concat(northwest).ToList(), () => {
                Clear();
                WriteLine("Du gick nordväst");
                //Gå till drakglänta
                Program.dragonClearing();
            }),
            //Sydöst mot skogsglänta
            new Command(new List<string>{ "2"  }.Concat(southeast).ToList(), () => {
                Clear();
                WriteLine("Du gick sydöst!");
                //Gå till skogsglänta
                Program.forestClearing();
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

    //Drakgläntan
    public void dragonClearingCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Kryp västerut
            new Command(new List<string>{ "1", "kryp västerut", "kryp väst", "kryp v", "crawl west", "crawl w"}, () => {
                Clear();
                WriteLine("Du kröp västerut!");
                //Gå till trädrantälvkant
                Program.treeLineRiver();
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
                Program.riverBank("s");
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
                Program.northwestTrail();
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
    public void treelineriverCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Nordöst
            new Command(new List<string>{ "1", }, () => {
                Clear();
                WriteLine("Du gick nordöst!");
                //Gå till älvbanken
                Program.riverBank("sw");
            }),
            //Österut
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till drakglänta
                Program.dragonClearing();
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

    //Älvbank
    public void riverBankCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Kliv i båten, ro norr
            new Command(new List<string>{ "1", "kliv ner i båten", "kliv i båt", "kliv i båten", "båt", "båten", "boat", "the boat", "get in boat", "get in the boat" }, () => {
                Clear();
                WriteLine("Du steg ner i båten!");
                //Gå till båtscen
                Program.inBoat();
            }),
            //Söderut
            new Command(new List<string>{ "2" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick söderut");

                Program.dragonClearing();
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
    public void boatCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Norrut mot norra strand
            new Command(new List<string>{ "1" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du kommenderar båten med tvekan i rösten. Till din lättnad börjar den röra sig lätt norrut.");
                //Gå till norra strand
                Program.northShore();
            }),
            //Söderut mot älvkanten
            new Command(new List<string>{ "2"  }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du kommenderar båten med tvekan i rösten. Till din lättnad börjar den röra sig lätt söderut.");
                //Gå till älvkant
                Program.riverBank("n");
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
    public void northShoreCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Riktning
            new Command(new List<string>{ "1","kliv ner i båten", "kliv i båt", "kliv i båten", "båt", "båten", "boat", "the boat", "get in boat", "get in the boat" }, () => {
                Clear();
                WriteLine("Du klev tillbaka ner i båten! ");
                //Gå till båt
                Program.inBoat();
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

    //Bro vässia
    public void bridgeWestCtrl(Inventory invy, bool sphinxCleared)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Lilla nordergläntan
            new Command(new List<string>{ "1"  }.Concat(northwest).ToList(), () => {
                Clear();
                WriteLine("Du gick nordväst!");
                //Gå till norderglänta
                Program.northClearing();
            }),
            //Bro össia
            new Command(new List<string>{ "2" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut över bron!");
                //Baserat på om sfinxen är besegrad eller ej: 
                if(sphinxCleared == false) {
                    Program.sphinxChallenge();
                } else {
                    //Bro össia; inte första gången man är där
                    Program.bridgeEast(false);
                }
            }),
            //Gläntan
            new Command(new List<string>{ "3" }.Concat(southwest).ToList(), () => {
                Clear();
                WriteLine("Du gick sydväst!");
                //Gå till skogsgläntan
                Program.forestClearing();
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
    public void northClearingCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Sydöst
            new Command(new List<string>{ "1" }.Concat(southeast).ToList(), () => {
                Clear();
                WriteLine("Du gick sydöst!");
                //Gå till bro: vässia
                Program.bridgeWest("nw");
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
    public void sphinxRiddleCtrl()
    {
        //Håll reda på försök
        int wrongGuesses = 0;

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
                    validAnswer = true;
                    Clear();
                    Program.bridgeEast(true);
                    break;
                /* SVAR TILL ANDRA GÅTAN
                case "trappan":
                case "trappa":
                case "stairs":
                    Clear();
                    Program.bridgeEast();
                    break; */
                default:
                    wrongGuesses++;
                    //Om man misslyckats tre gånger dör man
                    if (wrongGuesses == 3)
                    {
                        Clear();
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
                    tools.TypeLine("Sfinxen slickar sig hoppfullt om läpparna. ", false);
                    tools.printMessage(false, true, ConsoleColor.DarkCyan, $"'Du har {3 - wrongGuesses} {grammatik} kvar'");
                    tools.TypeLine(", informerar hon dig. \n", false);
                    break;
            }
        }
    }

    //Bro össia
    public void bridgeEastCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Norrut, till storslätta
            new Command(new List<string>{ "1" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du gick norrut!");
                //Gå till storslätta
                Program.storSlatt();
            }),
            //Västerut till vässia
            new Command(new List<string>{ "2" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut över bron!");
                //Gå till bro vässia, österifrån
                Program.bridgeWest("e");
            }),
            //Sydost till tämliga tältet
            new Command(new List<string>{ "3" }.Concat(southeast).ToList(), () => {
                Clear();
                WriteLine("Du gick sydöst!");
                //Gå till tämliga tältet
                Program.tentScene();
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
    public void tentCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Bro össia
            new Command(new List<string>{ "1" }.Concat(northwest).ToList(), () => {
                Clear();
                WriteLine("Du gick tillbaka nordvästerut!");
                //Gå till bro össia; ej första gången man är där
                Program.bridgeEast(false);
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
    public void storSlattCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut, Spurten
            new Command(new List<string>{ "1" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till Spurten
                Program.spurten();
            }),
            //Norrut, bråddjupa brallblötan
            new Command(new List<string>{ "2" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du gick norrut!");
                //Gå till brallblötan
                Program.brallBlotan();
            }),
            //Österut, Förtvivlans fält
            new Command(new List<string>{ "3" }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till förtvivlans fält
                Program.despairField();
            }),
            //Söderut, bro össia
            new Command(new List<string>{ "4" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick söderut!");
                //Gå till bro össia
                Program.bridgeEast(false);
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

    //George frågar om man vill byta
    public void wannaTrade(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
                //Byt med George Skörwen
                 new Command(new List<string>{ "1", "ja", "y", "yes", "byt", "trade" }, () => {
                    Clear();
                    WriteLine("Du gick med på att byta med George Skörwen!");
                    //Gå till bytesfunktion
                    Program.tradeWithGeorge();
                 }),
                 //Neka ett byte
                 new Command(new List<string>{ "2", "nej", "n", "no", "dont", "don't", "don't trade", "dont trade", "byt ej", "byt inte" }, () => {
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
                    tools.TypeLine("Du funderar ett slag. Går du med på ett byte? [ y / n ]", true);
                }, false)
                };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Själva bytet
    public void tradingWithGeorge(Inventory invy)
    {

        //Index på pärlan
        Item pearl = invy.getItem(20);
        int index = Array.FindIndex(invy.userInventory, i => i.name == pearl.name);

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
    }

    //Bråddjupa brallblötan
    public void brallblotCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var choice = new List<Command> {
            //Söderut mot storslätta
            new Command(new List<string>{ "1" }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick söderut!");
                //Gå till storslätta
                Program.storSlatt();
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

    //Förtvivlans fält
    public void despairFieldCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Norrut, till tidsslöseri 1
            new Command(new List<string>{ "1" }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du gick norrut!");
                //Gå till tidsslöseri 1
                Program.timeWaste1();
            }),
            new Command(new List<string>{ "2" }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till storslätta
                Program.storSlatt();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.despairFieldDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Spurten
    public void spurtCtrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Nordväst: till tornet
            new Command(new List<string>{ "1", }.Concat(northwest).ToList(), () => {
                Clear();
                WriteLine("Du gick nordväst!");
                //Gå till
            }),
            //Norrut: tidsslöseri 3
            new Command(new List<string>{ "2", }.Concat(north).ToList(), () => {
                Clear();
                WriteLine("Du gick norrut!");
                //Gå till tw3
                Program.timeWaste3();
            }),
            //Österut: storslätta
            new Command(new List<string>{ "3", }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till storslätta
                Program.storSlatt();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.spurtenDesc(2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Tidsslöseri 1
    public void tw1Ctrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut
            new Command(new List<string>{ "1", }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut");
                //Gå till tw2
                Program.timeWaste2();
            }),
            //Söderut
            new Command(new List<string>{ "1", }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut");
                //Gå till förtvivlans fält
                Program.despairField();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.wasteOfTimeDesc(1, 2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);
    }

    //Tidsslöseri 2
    public void tw2Ctrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut
            new Command(new List<string>{ "1", }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut!");
                //Gå till tw3
                Program.timeWaste3();
            }),
            //Västerut
            new Command(new List<string>{ "2", }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!!");
                //Gå till tw1
                Program.timeWaste1();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                flow.wasteOfTimeDesc(2, 2);
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);

    }

    public void tw3Ctrl(Inventory invy)
    {
        //VALMÖJLIGHETER
        var inputs = new List<Command> {
            //Västerut
            new Command(new List<string>{ "1", }.Concat(west).ToList(), () => {
                Clear();
                WriteLine("Du gick västerut");
                //Gå till 
            }),
            //österut
            new Command(new List<string>{ "2", }.Concat(east).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till tw2
                Program.timeWaste2();
            }),
            //söderut
            new Command(new List<string>{ "3", }.Concat(south).ToList(), () => {
                Clear();
                WriteLine("Du gick österut!");
                //Gå till spurten
                Program.spurten();
            }),
            //Kolla inventory
            new Command(new List<string>{ "i", "inventory" }, () => {
                Clear();
                invy.viewInventory();
                //Visa val igen
                
            }, false)
        };

        //anropa dirHandler med kommandon
        tools.dirHandler(inputs);

    }
}