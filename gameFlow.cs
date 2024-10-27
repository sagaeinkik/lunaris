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
        else if (beast == "georgeSkörwe")
        {
            tools.TypeLine("George Skörwen har inga öron, men de två utstickande flikarna på odjurets huvud läggs platt bakåt i en handling som avslöjar dess sinnesstämning, inte helt olikt en tjurig fuxmärr.", true);
            tools.TypeLine("Samtidigt fäller besten ut den krage av hudflikar som tidigare legat avslappnat mot dess hals, mycket olikt ovan nämnda fuxmärr.", true);
            tools.TypeLine("George Skörwen ryter så det skär i dina öron innan den blixtsnabbt virar sin svans runt din vrist och drar ner dit i sjön.", true);
            tools.TypeLine("När du dras ner i djupet lämnar luften dina lungor i ett spår av bubblor efter dig. Det dröjer inte länge förrän mörkret i Bråddjupa Brallblötan omsluter dig.", true);
            WriteLine("\n \n");

        }

        tools.printMessage(true, true, ConsoleColor.DarkRed, "G A M E   O V E R ");
        tools.printMessage(true, true, ConsoleColor.DarkRed, "Du dog! Bättre lycka nästa gång.");

        //Credits
        tools.gameCredits();
        //avsluta
        Environment.Exit(0);
    }

    //Läger
    public void campFireDesc(int part)
    {
        if (part == 1)
        {
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
            tools.TypeLine("I mitten av gläntan står en urblekt skylt med pilar i olika riktningar. Texten ser ut att ha varit med om sju svåra år och är därmed svårtydd.", true);
            tools.TypeLine("Du lyckas utröna att en grotta ligger västerut, Norra strand nordväst, och Bråddjupa Brallblötan (står det verkligen så?) till nordöst.", true);
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
            WriteLine("6. Gå söderut (till lägret)");
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
    public void westTrailDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du har gått ganska långt på den här skogsleden. Det är ganska trivsamt att vandra med fågelkvitter som ljudspår, speciellt i den grönskan som omger dig.", true);
            tools.TypeLine("Ändå börjar du fråga dig själv om du verkligen vill fortsätta vidare på den här vägen, eller om du har ändrat dig. \n", true);
        }
        else if (part == 2)
        {
            tools.TypeLine("Vad vill du göra? \n", true);
            WriteLine("1. Gå västerut");
            WriteLine("2. Gå österut");
        }
    }

    //Trolltrakten
    public void trollTraktenDesc(bool trollDead, int part)
    {
        //Om troll lever
        if (!trollDead)
        {
            tools.TypeLine("Du ser något på stigen längre fram. Det ser ut som en stor sten, med det lilla undantaget att det rör sig...  \n", true);
            tools.TypeLine("Du stannar tvärt när du ser att det är ett troll. Det har en gråaktig ton, stripigt hår ner till hakan, och så grov hy att det närmast liknar en klipphäll.", true);
            tools.TypeLine("Trollet byggt i stort sett som Röjar-Ralf, bredaxlat med gigantiska underarmar och händer, litet huvud, och orimligt korta små ben. \n Det stirrar på dig med små grisögon innan det plötsligt rusar framåt och sträcker ut sina väldiga armar för att hugga tag i dig.", true);
            WriteLine();
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Gör en kullerbytta västerut för att undvika trollet");
            WriteLine("2. Plocka upp stenen vid dina fötter och kasta den på trollet");
            WriteLine("3. Stå på dig och gör dig stor; avskräck skenattacken");
            WriteLine("4. Gör en kullerbytta österut för att undvika trollet");
        }

        else if (trollDead && part == 1)
        {
            // Troll är dödd han 
            tools.TypeLine("Du når en bekant plats i skogen. Bredvid stigen ligger kadavret av trollet, med den blodiga stenen strax intill.", true);
            tools.TypeLine("Du tänker på hur imponerade alla hemma ska bli när de hör att du numera kan titulera dig själv Trolldräpare. \n", true);

        }
        else if (trollDead && part == 2)
        {
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Kullerbytta västerut; inte för att det behövs, utan för att det var kul");
            WriteLine("2. Kullerbytta österut, av samma anledning");
        }
    }

    //Besegra troll
    public void killTrollDesc()
    {
        tools.TypeLine("Du böjer dig ner kvickt som attan för att plocka upp stenen samtidigt som du dyker undan från trollets väg. När du rätat lite på dig busvisslar du, och trollet vänder sig dumt mot dig.", true);
        tools.TypeLine("Du sular iväg stenen så hårt du förmår och lyckas kasta den med en träffsäkerhet som skulle göra New York Jets' quarterback Zach Wilson alldeles grön av avund.", true);
        tools.TypeLine("Seriöst, snubben har riktiga problem med att kasta under press. Och han spelar i NFL! \n", true);
        tools.TypeLine("I alla fall, stenen träffar sitt mål rakt mellan trollets ögon, där det måste ha funnits en svag punkt. Trollet vacklar till innan det rasar ihop i en hög. Nu ser det verkligen ut som en sten.", true);
        tools.TypeLine("När trollet kollapsar ramlar ett föremål ut ur... ja, nånstans ifrån. \n", true);
    }

    //Grottmynning
    public void caveEntranceDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Efter att ha gått och gått i vad som känns som evigheter finner du dig själv ståendes vid solid bergvägg av svart sten.", true);
            tools.TypeLine("'Vilken platt och hög vägg', tänker du.", true);
            tools.TypeLine("Stigen fortsätter stadigt hela vägen fram till väggen, och när du kommer närmre svingar en stenport upp helt på eget bevåg.", true);
            tools.TypeLine("Den måste vara magisk! \n", true);

        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Följ stigen norr, in i berget");
            WriteLine("2. Gå österut, ut i solen.");
        }
    }

    //Trollgrotta
    public void trollCaveDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du går varsamt längre in i grottan och hör hur den väldiga stenporten skrapar bakom dig. Du vänder dig om precis lagom till att se den slå igen bakom dig, vilket försänker hela din omgivning i totalt mörker. \n", true);
            tools.TypeLine("Innan du hinner drabbas av total panik börjar art deco-inspirerade mönster framträda i självlysande färg på väggarna. ", true);
            tools.TypeLine("Någon har haft lite för mycket fritid, verkar det som. Den självlysande färgen är stark nog för att lysa upp vägen framför dina fötter.", true);
            tools.TypeLine("Du följer den steniga vägen längre in i berget, uppför breda trappor, tills du kommer till en gigantisk sal mitt i berget. \n", true);
            tools.TypeLine("Du kan inte räkna hur många pelare som finns i salen, men det är minst en, kanske till och med två.", true);
            tools.TypeLine("Det du kan se mellan sagda stenpelare är kopiösa högar med sällsynta och dyrbara värdesaker.", true);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra nu?", true);
            WriteLine("1. Gå söderut (tillbaka)");
        }
    }

    //Skoggstig österut
    public void eastTrailDesc()
    {
        tools.TypeLine("Att kalla det för stig är generöst; det är mer en vag skymt av färdväg genom tätvuxen och snårig skog. Du mosar dig igenom kvistar och löv, men fastnar frustrerande nog ganska konstant. ", true);
        tools.TypeLine("Du tänkte kanske att stigen hade trampats upp av skogens djur, men du inser ganska snabbt att om djur ligger bakom detta rör det sig snarare om ett lämmeltåg än en stadig älg.", true);
        tools.TypeLine("Det tar inte särskilt lång tid innan du blir svettig, varm och irriterad, och du börjar ifrågasätta hur klokt beslut det var att försöka ta sig in den här vägen. \n", true);

        tools.TypeLine("Vad vill du göra nu?", true);
        WriteLine("1. Gå västerut");
        WriteLine("1. Gå österut");
    }

    //Halvöstra strand
    public void halfEastBeachDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du hör älvens brus innan du ser den, men plötsligt viker skogen undan och du finner dig själv ståendes på en smal strandkant.", true);
            tools.TypeLine("Träden trängs nästan hela vägen fram till älvkanten, som kommer tvärt och plötsligt. Du slår vad om att det finns fin öring i dessa vatten.", true);
            tools.TypeLine("På andra sidan älven anar du ett tält bland träden. \n", true);
        }
        else if (part == 2)
        {
            tools.TypeLine("Vad vill du göra nu?", true);
            WriteLine("1. Gå västerut");

        }
    }
}