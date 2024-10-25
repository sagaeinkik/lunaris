/* 
----- Kontroller, WriteLines, texten som ska visas i varje "scen", annan köttig kod som har med spelet att göra ------
 */
using System;
using System.Linq;
using System.Reflection.Emit;
namespace lunaris;

public class GameFlow
{

    Tools tools = new();
    Program mainGame = new();

    /* SPELPÅVERKANDE METODER */

    //Metod med kontrollfrågor om man vill lägga till item i inventory
    public void wantToAdd(Item item, Inventory inventory)
    {
        string result;
        //Fråga om man vill lägga till
        tools.printMessage(false, true, ConsoleColor.Gray, $"Vill du lägga till {item.name} i din inventarie? [y / n] ");
        //Lagra användarinput
        string userChoice = ReadLine()!.ToLower();

        switch (userChoice)
        {
            case "y":
            case "yes":
            case "ja":
            case "add":
                //Lägg till i inventory, lagra resultat i variabel
                result = inventory.addToInventory(item);
                //kolla om det gick eller ej; skriv ut till skärm
                if (result.Contains("stolt ägare"))
                {
                    tools.printMessage(true, true, ConsoleColor.Green, result);
                }
                else
                {
                    tools.printMessage(true, true, ConsoleColor.Red, result);
                }
                break;

            case "n":
            case "no":
            case "nej":
            case "leave":
                //Lämna
                tools.printMessage(true, true, ConsoleColor.Yellow, "Du lämnar kvar föremålet där du hittade det.");
                break;

            default:
                tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen yes / no, ja / nej, y / no");
                wantToAdd(item, inventory);
                break;
        }
        WriteLine();
    }

    //Kontroll om item redan finns i inventory, avgör scenen: 
    public bool isOwned(Item item, Inventory inv)
    {
        //Kolla om inventory redan innehåller item
        if (inv.userInventory.Contains(item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /* BESKRIVANDE TEXTER TILL SKÄRMEN */
    //INTROTEXT
    public void intro()
    {
        Clear();
        //TITEL
        WriteLine();
        tools.printMessage(false, false, ConsoleColor.Blue, "L ");
        tools.printMessage(false, false, ConsoleColor.DarkCyan, "U ");
        tools.printMessage(false, false, ConsoleColor.Green, "N ");
        tools.printMessage(false, false, ConsoleColor.Yellow, "A ");
        tools.printMessage(false, false, ConsoleColor.DarkYellow, "R ");
        tools.printMessage(false, false, ConsoleColor.Red, "I ");
        tools.printMessage(false, false, ConsoleColor.DarkMagenta, "S \n \n");
        //BESKRIVNING
        WriteLine("Överlev faror, samla items och ta reda på ditt öde i detta lättsmälta äventyr. \n");

        //MENY
        WriteLine("MENY");
        WriteLine("1. Tutorial");
        WriteLine("2. Starta nytt spel");
        WriteLine("X. Avsluta");

        int input = (int)ReadKey(true).Key;

        switch (input)
        {
            //Skicka till tutorial page
            case '1':
                tutorialPage();
                break;
            //Avslutar spelet
            case 88:
                Clear();
                tools.gameCredits();
                Environment.Exit(0);
                break;
            //allt annat, inklusive 2, leder till att spelet börjar
            default:
                break;

        }
    }

    //TUTORIAL
    public void tutorialPage()
    {
        Clear();
        tools.printMessage(false, false, ConsoleColor.Blue, "L ");
        tools.printMessage(false, false, ConsoleColor.DarkCyan, "U ");
        tools.printMessage(false, false, ConsoleColor.Green, "N ");
        tools.printMessage(false, false, ConsoleColor.Yellow, "A ");
        tools.printMessage(false, false, ConsoleColor.DarkYellow, "R ");
        tools.printMessage(false, false, ConsoleColor.Red, "I ");
        tools.printMessage(false, false, ConsoleColor.DarkMagenta, "S \n \n");
        WriteLine("Lunaris är ett textbaserat spel där du ska samla på dig föremål, ta dig förbi hinder och gå i mål.");
        WriteLine("Det finns fyra kategorier av föremål, och baserat på vilken kategori du har plockat på dig flest föremål av kommer du att få ett av fem olika slut.");
        WriteLine("Du har bara utrymme för fem föremål i din inventory, så plocka upp saker med lite omsorg! \n");
        WriteLine("För att navigera runt i spelet kan du antingen skriva in svarsalternativen du får med siffor, eller skriva den riktning du vill gå i, till exempel norr/norrut, väst/västerut, syd/söderut, öst/österut. Spelet är på svenska men klarar av vissa engelska fraser som yes/y, no/n, west/north/east/south, och leave. \n");
        WriteLine("Du kan kolla din inventory under spelets gång genom att skriva 'i' eller 'inventory' när du får frågan om vilken riktning du vill gå. \n \n");
        WriteLine("Tryck enter för att komma tillbaka till menyn.");
        //Tillbaka till startmenyn
        ReadKey();
        intro();
    }

    //Game over
    public void gameOver(string beast)
    {
        if (beast == "sfinx")
        {
            tools.TypeLine("Sfinxen sitter till en början orörlig med ett oläsligt ansiktsuttryck. Du håller andan med hjärtat hamrandes hårt i halsgropen. \n", true);
            tools.TypeLine("Varje sekund känns som en livstid. \n", true);
            tools.TypeLine("...", true);
            tools.TypeLine("'Du suger på det här', säger sfinxen.", true);
            tools.TypeLine("Ett onaturligt brett leende sprider sig över hennes ansikte som ett djupt sår från öra till öra och blottar hennes vassa tänder. Hennes pupiller vidgar sig och täcker hela hennes iris som en bottenlös avgrund.", true);
            tools.TypeLine("Hon kryper ihop och börjar vicka lite på rumpan precis som en katt inför ett språng. Det hade varit lite gulligt om du inte precis skulle dö.", true);
            tools.TypeLine("Med den tanken naglas du fast i marken och möter din skapare.", true);
            WriteLine("\n \n");
        }
        else if (beast == "drake")
        {
            tools.TypeLine("Du drar ett djupt andetag och lägger benen på ryggen över fältet.\n", true);
            tools.TypeLine("Det visar sig oturligt nog att en drake inte fungerar helt olikt en t-rex; dess syn dras till hastiga rörelser.", true);
            tools.TypeLine("Du hinner ta 2-3 steg innan suset från väldiga vingslag hörs i luften strax bakom dig; kraftiga vindar välter nästan omkull dig.", true);
            tools.TypeLine("Du springer frustrerande långsamt, som en knubbsäl genom hummus. Plötslig hetta får ditt hår att krulla ihop sig innan du är omgiven av ett eldklot. \n", true);
            tools.TypeLine("Och så var det med den saken.", true);
            WriteLine("\n \n");
        }
        else if (beast == "troll")
        {
            tools.TypeLine("Du har hört att när älgar och andra blodtörstiga varelser anfaller ska man göra sig stor genom att sträcka händerna upp i luften och skrika.", true);
            tools.TypeLine("Du gör din bästa imitation av en förbannad björnhanne på bakbenen och klämmer ur dig ett imponerande vrål. \n", true);
            tools.TypeLine("Tyvärr var detta helt fel beslut och retar bara upp trollet till vansinne. Det tar två stora steg mot dig och smäller dig över huvudet med en kraftig näve. \n", true);
            tools.TypeLine("Det är tack och godnatt...", true);
            WriteLine("\n \n");

        }

        tools.printMessage(true, true, ConsoleColor.DarkRed, "G A M E   O V E R ");
        tools.printMessage(true, true, ConsoleColor.DarkRed, "Du dog! Bättre lycka nästa gång.");
    }

    //Läger
    public void campFireDesc(int part)
    {
        if (part == 1)
        {
            tools.printMessage(true, false, ConsoleColor.Green, "Lägret");
            tools.TypeLine("Framför dig ser du en lägereld som knappt pyr längre. Du är omgiven av tät skog i alla riktningar, men du ser en stig som leder norrut. \n", true);
        }
        else if (part == 2)
        {
            //Obs: Vet att else if är onödigt här, men jag har det för min skull 
            WriteLine();
            tools.TypeLine("Vad vill du göra nu?", true);
            WriteLine("1. Gå norrut");
        }

    }

    //Skogsglänta
    public void forestClearingDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du kommer fram till en glänta i skogen som liknar ett mossklätt torg, med vägar att följa i flera olika riktningar. ", true);
            tools.TypeLine("I mitten av gläntan står en urblekt skylt med pilar i olika riktningar. Texten är svårtydd, men den pil som pekar nordväst verkar läsa 'Norra strand'.", true);
            tools.TypeLine("Pilen västerut är omöjlig att läsa, men ordet slutar på 'grotta'.", true);
            tools.TypeLine("En pil visar nordöst, men orden på skylten har suddats bort av årens hand. \n", true);

        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("En stig leder sydväst, in i tätskogen. ", true);
            tools.TypeLine("Stigen västerut är bredare och lummig, till och med inbjudande.", true);
            tools.TypeLine("Från nordväst till nordöst är stigen inte så mycket en stig som det är en skogsväg; man skulle säkert kunna köra på den med en Honda CR-V RD1 från 1998 om man så ville.", true);
            tools.TypeLine("Österut ser du en liten liten stig som knappt är värd namnet, det är snarare en öppning i buskaget. \n", true);
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Ta sydvästra stigen");
            WriteLine("2. Gå västerut");
            WriteLine("3. Följ vägen nordväst");
            WriteLine("4. Följ vägen nordöst");
            WriteLine("5. Mosa dig in på östra stigen");
            WriteLine("6. Gå tillbaka söderut (till lägret)");
        }
    }

    //Lilla södergläntan
    public void southClearingDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du följer stigen till en väldigt liten glänta, knappt mer än en studsmatta i diameter.", true);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Gå nordöst (tillbaka till större Skogsgläntan)");
        }
    }

    //Skogsstig väst
    public void westTrailDesc()
    {

    }

    //Trolltrakten
    public void trollTraktenDesc()
    {

    }

    //Besegra troll

    //Grottmynning

    //Trollgrotta

}