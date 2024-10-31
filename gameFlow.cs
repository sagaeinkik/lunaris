/* 
----- Kontroller, WriteLines, texten som ska visas i varje "scen", annan köttig kod som har med spelet att göra ------

OBS: Jag är medveten om att else if(part == 2) är överflödigt. Jag har valt att skriva så för att själv enklare hålla reda på delarna och anrop.
 */
using System;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
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
                tools.printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen yes / no, ja / nej, y / n");
                wantToAdd(item, inventory);
                break;
        }
        WriteLine();
    }

    //Kontroll om item redan finns i inventory, avgör scenen: 
    public bool isOwned(Item item, Inventory inv)
    {
        bool ownership = inv.userInventory.Contains(item);
        return ownership;
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
        WriteLine("Lunaris är ett textbaserat spel där du ska samla på dig föremål, ta dig förbi hinder och besöka trollkarlen för att gå i mål.");
        WriteLine("Det finns fyra kategorier av föremål, och baserat på vilken kategori du har plockat på dig flest föremål av kommer du att få ett av sex olika slut.");
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
            tools.TypeLine("George Skörwen ryter så det skär i dina öron innan den blixtsnabbt virar sin svans runt din vrist och drar ner dig i sjön.", true);
            tools.TypeLine("När du dras ner i djupet lämnar luften dina lungor i ett spår av bubblor efter dig. Det dröjer inte länge förrän mörkret i Bråddjupa Brallblötan kväver dig.", true);
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
            WriteLine();
            tools.TypeLine("Vad vill du göra nu?", true);
            WriteLine("1. Gå norrut \n \n");
            tools.printMessage(true, true, ConsoleColor.Gray, "Tips! När du blir ombedd att välja riktning kan du alltid skriva 'i' eller 'inventory' för att kolla din inventarie.");
        }

    }

    //Skogsglänta
    public void forestClearingDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du kommer fram till en glänta i skogen som liknar ett mossklätt torg, med vägar att följa i flera olika riktningar. ", true);
            tools.TypeLine("I mitten av gläntan står en urblekt skylt med pilar i olika riktningar. Texten ser ut att ha varit med om sju svåra år och är därmed svårtydd.", true);
            tools.TypeLine("Du lyckas utröna att en grotta ligger västerut, Norra strand nordväst, och Lunaris till nordöst. \n", true);
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
        tools.TypeLine("I alla fall, stenen träffar sitt mål rakt mellan trollets ögon, där det måste ha funnits en svag punkt. Trollet vacklar till innan det rasar ihop i en hög. Nu ser det verkligen ut som en stenbumling.", true);
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
        tools.TypeLine("Du tänkte kanske att stigen hade trampats upp av skogens djur, men du inser ganska snabbt att om djur ligger bakom detta rör det sig snarare om ett lämmeltåg än en stadig älg.\n ", true);
        tools.TypeLine("Det tar inte särskilt lång tid innan du blir svettig, varm och irriterad, och du börjar ifrågasätta hur klokt beslut det var att försöka ta sig in den här vägen. \n", true);

        tools.TypeLine("Vad vill du göra nu?", true);
        WriteLine("1. Gå västerut");
        WriteLine("2. Gå österut");
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
    public void northwestTrailDesc(string direction)
    {
        if (direction == "nw")
        {
            tools.TypeLine("Du hatar den här biten. Det tar evigheter att krypa, och det skrämmer slag på dig att höra draken bakom dig.", true);
            tools.TypeLine("Det sticker och ilar i hela ryggen av skräck och ditt hjärta hamrar på som Meshuggah-trummisen Tomas Haake.", true);
            tools.TypeLine("Till din stora lättnad kommer du så småningom till slutet på fältet, där du vågar resa dig. Draken syns inte till. \n", true);
        }
        tools.TypeLine("Nu går du på en trevlig och bred stig, alternativt en trevlig och smal väg beroende på hur man ser på det. Den tycks fortsätta i all oändlighet.", true);
        tools.TypeLine("Periodvis växer det hallon längsmed vägen.", true);
        tools.TypeLine("Dina fötter känns lite trötta, så du stannar och plockar några att mumsa på för att få lite energi.", true);
        tools.TypeLine("Du börjar fundera på om du vill fortsätta på den här vägen, eller om du vill vända tillbaka. \n", true);


        tools.TypeLine("Vad vill du göra?", true);
        WriteLine("1. Följ vägen nordväst");
        WriteLine("2. Följ vägen sydöst");
    }

    //Drakglänta: bool på om man sett draken innan
    public void dragonClearingDesc(int part, bool firstTime)
    {
        if (part == 1)
        {
            tools.TypeLine("Du kommer till stort, stort fält med mjukt, högt gräs. Du går genom höggräset och drar av frön från rajstjälkarna eftersom det är fysiskt omöjligt att inte göra det när man går genom gräs.", true);

            //Om det är första gången: 
            if (firstTime)
            {
                tools.TypeLine("Det härliga vädret och den fantastiska lägdan till trots har du ett stort problem, vilket du bara inser först när du är halvvägs ut på fältet. \n", true);
                tools.TypeLine("En enorm drake är ute och sträcker på sina vingar högt över fältet. Det finns inte en chans att du kan slåss mot den; ditt enda hopp är att undgå att bli upptäckt.", true);
                tools.TypeLine("Du stelnar till och tänker. Du känner dig osäker på hur drakar fungerar. \n", true);
                tools.TypeLine("Om du kryper långsamt och försiktigt tar det lång tid att korsa fältet, du syns tydligare uppifrån, men du kanske inte drar till dig lika mycket uppmärksamhet.", true);
                tools.TypeLine("Om du springer spenderar du inte lika lång tid på fältet, så draken får inte lika lång tid på sig att upptäcka dig, och du syns inte lika väl uppifrån, men du kan dra till dig mer uppmärksamhet.", true);
            }
            else
            {
                tools.TypeLine("Du tvärnitar när du ser draken flaxa omkring uppe i luften, spanandes över fältet efter något att grilla och äta.", true);
                tools.TypeLine("Just fan. Det var ju den också. Du hade faktiskt hunnit glömma den. \n", true);
                tools.TypeLine("Du suckar, stålsätter dig, och försöker att inte drabbas av paniken som stiger i halsen på dig.", true);
            }
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Kryp västerut");
            WriteLine("2. Spring västerut");
            WriteLine("3. Kryp norrut");
            WriteLine("4. Spring norrut");
            WriteLine("5. Kryp sydöst");
            WriteLine("6. Spring sydöst");
        }
    }

    //Trädkantälvrand
    public void treelineRiverDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Det tar evigheter att krypa över fältet. Flera gånger är du bergsäker på att draken har sett dig, och det har stuckit obehagligt längsmed ryggraden när du hört dess vingslag precis ovanför dig utan att kunna se odjuret.", true);
            tools.TypeLine("Men till din stora förvåning kommer du så småningom till slutet på fältet, där du vågar resa dig. Draken syns inte till. \n", true);
            tools.TypeLine("Nu finner du dig själv stående vid älvens strandkant. \n", true);
        }
        else if (part == 2)
        {
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Gå nordöst längsmed älvkanten");
            WriteLine("2. Gå österut, tillbaka mot draken");
        }
    }

    //Älvbank: Direction är riktning användaren kommer ifrån
    public void riverBankDesc(string direction)
    {
        if (direction == "sv")
        {
            //Om man kom från sydväst, via älvkanten
            tools.TypeLine("Du följer strandkanten, nervös över att draken ska bredda sitt flygområde, men du tröstar dig med att om det skulle skita sig kan du alltid slänga dig ner i älven.", true);

        }
        else if (direction == "s")
        {
            //Om man kom söderifrån, över drakgläntan
            tools.TypeLine("Det tar evigheter att krypa över fältet. Flera gånger är du bergsäker på att draken har sett dig, och det har stuckit obehagligt längsmed ryggraden när du hört dess vingslag precis ovanför dig utan att kunna se odjuret.", true);
            tools.TypeLine("Men till din stora förvåning kommer du så småningom till slutet på fältet, där du vågar resa dig. Draken syns inte till. \n", true);
        }
        else if (direction == "n")
        {
            //Om man kom norrifrån, från båt
            tools.TypeLine("Sakta men säkert glider båten mot älvkanten och anlägger sig själv vid stranden.", true);
            tools.TypeLine("Du stiger ur båten och vänder dig om. \n", true);

        }

        tools.TypeLine("Framför dig ligger en båt och guppar nära strandkanten. Den är inte fastlåst på något vis. \n", true);
        tools.TypeLine("Vad vill du göra?", true);
        WriteLine("1. Kliv ner i båten");
        WriteLine("2. Gå söderut, tillbaka mot draken");
    }

    //Till båt
    public void inBoatDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du stiger ner i båten, varpå den på eget bevåg börjar ta sig ut på älven. Du uppskattar den otroliga snitsigheten med en båt som självror.", true);
            tools.TypeLine("Inget gott varar dock för evigt. Båten stannar till halvvägs ut på älven. Du börjar rota runt i båten efter någon form av redskap som kan användas för att driva båten fram mot strandkanten. ", true);
            tools.TypeLine("Kanske ett par åror, eller en Mercury Verado V12 7.6L som producerar 600hk, med cirkapris på 870'000 kronor. \n", true);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Du finner inget av det du letar efter. I brist på andra idéer får du utgå från att båten styrs verbalt.", true);
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Säg åt båten att ta dig norrut");
            WriteLine("2. Säg åt båten att ta dig söderut");
        }
    }

    //Norra strand
    public void northShoreDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du kliver ur båten på en liten isolerad strandsnutt. Den är omgiven av skog och verkar inte gå att ta sig till på något annat vis än den väg du just kom.", true);
            tools.TypeLine("Eller ja, såvida du inte skärmflyger hit eller råkar vara en gibbonapa.", true);
            tools.TypeLine("Den lilla stranden och den tillhörande gräsplätten är i vilket fall alldeles förtjusande och skulle vara en förträfflig plats för en picknick. \n", true);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Kliv ner i båten");
        }
    }

    //Bro vässia
    public void bridgeWestDesc(string dir)
    {
        if (dir == "sw")
        {
            //Om man kommer från skogsgläntan
            tools.TypeLine("Du följer skogsvägen nordösterut och efter bara några kilometer börjar du höra älvbruset i bakgrunden.", true);
            tools.TypeLine("Träden börjar glesna och inom kort finner du dig själv ståendes vid en välvd stenbo över älven.", true);
            tools.TypeLine("Från stenbron ser du en stig löpa nordvästerut, in i tätare skogen igen. \n", true);
        }
        else if (dir == "nw")
        {
            //Om man kommer från lilla nordergläntan
            tools.TypeLine("Du går tillbaka på den lilla stigen tills du kommer tillbaka till stenbron.", true);
            tools.TypeLine("Du blir nästan lite stolt över dig själv som har kommit hit, för du har just inget lokalsinne att tala om, och du har gått vilse inne på ICA mer än en gång. \n", true);
        }
        else if (dir == "e")
        {
            //om man kommer från bron: össia
            tools.TypeLine("Du styr dina steg över bron medan du håller ett krampaktigt tag om stenräcket. Älven dånar under dig och du är nog lite rädd för höjder ändå.", true);
            tools.TypeLine("Men innan du vet ordet av befinner du dig på solid mark och sänker därmed risken för att plötsligt bli blöt avsevärt. \n", true);
        }

        tools.TypeLine("Vad vill du göra?", true);
        WriteLine("1. Följ nordvästra stigen");
        WriteLine("2. Gå österut över bron");
        WriteLine("3. Följ skogsvägen sydväst");
    }

    //Lilla nordergläntan
    public void northClearingDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du följer den nordvästra stigen in i skogen. Skogen visar sig från sin alla bästa sida. Ljuset filtreras genom lövverket på ett sagolikt sätt.", true);
            tools.TypeLine("Vemodet infinner sig snabbt när du kommer fram till stigens slut; det som såg ut som en glänta verkar snarare vara ett ärr i skogen, ett bevis på något fasansfullt.", true);
            tools.TypeLine("Träd ligger avbrutna och brända, marken är svedd och svart, som att skogen har blivit massakrerad. \n", true);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Du vill helst inte spendera mer tid än du måste här. Vad vill du göra?", true);
            WriteLine("1. Följ stigen tillbaka sydöst");
        }
    }

    //Sfinx
    public void sphinxChallengeDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du knatar med ostadiga ben över stenbron och försöker att inte titta ner på vattnet som forsar under dina fötter. \n Du försöker framförallt att inte tänka på hur snabbt du skulle svepas med av strömmen om du ramlade i.", true);
            tools.TypeLine("Den lättnad du känner när du tagit dig förbi detta vattenhinder byts snabbt ut till oförstående och sedan fasa. Det sitter en sfinx framför dig på stigen. \n", true);
            tools.TypeLine("Sfinxen har ett kvinnoansikte som skulle ha kunnat kallas för vackert om det inte vore för den obehagligt breda munnen och de gula ögonen med små, runda pupiller.", true);
            tools.TypeLine("Hon hade sina gigantiska örnvingar infällda mot kroppen innan du kom, men nu när hon ser dig fäller hon ut dem lätt. Hennes kropp är som ett lejons, och svansen är en bitsk huggorm som spänner blicken i dig.", true);
            tools.TypeLine("Ord kan inte beskriva hur ofantligt massiv hon är. Du har sällan känt dig så liten.", true);
            WriteLine();
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Får jag komma förbi, tack och snälla? Du sitter lite i vägen'");
            tools.TypeLine(", frågar du med tunn röst. \n", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Endast den värdige får mig passera oskadd'");
            tools.TypeLine(", svarar sfinxen med en röst som tycks vibrera. ", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Skänk mig det rätta svaret på min gåta, och jag bedömer dig värdig.' \n");
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag är inte så bra på gåtor'");
            tools.TypeLine(", säger du. ", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Måste jag?' \n");
            tools.TypeLine("Sfinxens pupiller vidgas lätt som av förväntan. ", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Du svarar efter eget gottfinnande. Men att ej svara är i sig själv ett svar.' \n");
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Vad händer om jag svarar fel?'");
            tools.TypeLine(" undrar du. \n", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag kräver ditt liv som lön'");
            tools.TypeLine(", svarar hon enkelt. \n", false);
            WriteLine();
            tools.TypeLine("Detta var ju högst beklagligt. Du verkar inte ha så mycket till val.", true);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Nej, jag undviker gärna att dö. Låt höra gåtan'");
            tools.TypeLine(", säger du. \n", false);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad faller en mot, \n som snubblar över trädrot?", true);
            /* tools.TypeLine("Jag ligger stilla, vad än sker, \n vad än i världen händer. \n Jag går alltjämt upp och ner, \n men blott ett steg i sänder", true); */
        }
    }
    //Bro: össia
    public void bridgeEastDesc(int part, bool firstTime = false)
    {
        Riddle randomRiddle = tools.generateRiddle();

        if (part == 1 && firstTime == true)
        {
            //Om man preciiis har "besegrat" sfinxen 
            tools.TypeLine("Sfinxen sitter till en början orörlig med ett oläsligt ansiktsuttryck. Du håller andan med hjärtat hamrandes hårt i halsgropen. \n", true);
            tools.TypeLine("Varje sekund känns som en livstid. \n", true);
            tools.TypeLine("...", true);
            WriteLine();
            tools.printMessage(false, true, ConsoleColor.Cyan, "'Du har så rätt'");
            tools.TypeLine(", säger hon till sist. Anar du besvikelse i hennes röst? ", false);
            tools.printMessage(false, true, ConsoleColor.Cyan, "'Du får lov att passera mig, oskadd, var gång din väg bär dig förbi mig.'");
            WriteLine();
            tools.TypeLine("Och med det fäller hon ut sina vingar (de når nästan till träden på vardera sida), tar ett par kraftiga slag, och försvinner upp i luften. \n", true);
            tools.TypeLine("Stigen delar sig norrut och åt sydöst.", true);
        }
        else if (part == 1 && firstTime == false)
        {
            //Om man besegrade sfinxen innan
            tools.TypeLine("Du kommer fram till en bekant plats och en bekant figur. Sfinxen sitter precis där hon satt förut, och det rycker lätt i ormen som är hennes svans.", true);
            tools.printMessage(true, true, ConsoleColor.Cyan, "'Välkommen åter, vandrare! Vägen är föga vältrafikerad och jag finner mig själv utled på denna enformighet. Vill du höra en gåta?' \n");
            tools.TypeLine("Innan du hinner svara tar hon ton. \n", true);
            tools.printMessage(true, true, ConsoleColor.DarkCyan, $"'{randomRiddle.Question}'");
            tools.TypeLine("Du stirrar stumt på henne.", true);
            tools.printMessage(true, true, ConsoleColor.DarkCyan, $"'{randomRiddle.Answer}' \n");
            tools.TypeLine("Hon slänger ut med sina lejonarmar, höjer ögonbrynen och ler brett med öppen mun.", true);
            tools.TypeLine("Du pressar fram ett ansträngt leende och skyndar dig förbi. \n", true);
        }
        else if (part == 2)
        {
            //Val
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Gå norrut");
            WriteLine("2. Gå västerut över bron");
            WriteLine("3. Gå sydöst");
        }
    }

    //Tämliga tältet
    public void tentSceneDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du går längsmed älvkanten bland glesa träd och kommer fram till en tältplats.", true);
            tools.TypeLine("Det gulgröna tältet ser ut att vara välanvänt och står stadigt uppställt, men ändå övergivet. Du ser inte tältets ägare någonstans. \n \n Du känner en överväldigande nyfikenhet på vems tält det är, och varför det står här.", true);
            tools.TypeLine("Du tvekar litegrann. Ska du våga gå in? \n", true);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Gå tillbaka nordväst");
        }
    }

    //Storslätta
    public void storSlattDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du kommer fram till den största slätten du nånsin sett.", true);
            tools.TypeLine("Åt alla riktningar framför dig är det bara vidöppna, böljande fält med gulgrönt gräs.", true);
            tools.TypeLine("Det hade varit en episk plats för ett stort slag. \n", true);
            tools.TypeLine("Du ser en gammal träskylt som är mer grå än brun, blekt av solen och alla år som gått. Den har en pil som pekar norrut. ", true);
            tools.TypeLine("Inristat på skylten är en teckning av ett monster. Ovanför monstret står det 'VARNING' med stora bokstäver, och längst ner står det 'Bråddjupa Brallblötan'. \n", true);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vart vill du gå?", true);
            WriteLine("1. Västerut");
            WriteLine("2. Norrut");
            WriteLine("3. Österut");
            WriteLine("4. Söderut");
        }
    }
    //Bråddjupa brallblötan
    public void brallblotanDesc(int part)
    {
        if (part == 1)
        {
            //Om detta är första gången man är i brallblötan, dvs första gången man träffar george
            tools.TypeLine("Ju längre norrut du går desto tätare växer sig ett mörker runtomkring.", true);
            tools.TypeLine("Slätten byts så sakteliga ut mot döda, förvridna, svarta träd med nakna, vassa grenar. \n ", true);
            tools.TypeLine("Mellan träden ser du en stor, svart sjö. Bråddjupa Brallblötan.", true);
            tools.TypeLine("Strandkanten till sjön pryds av benknotor. Här och där kan du se delar av någon gammal rostig rustning. Du försöker att inte tänka på det för mycket. \n", true);
        }
        else if (part == 2)
        {
            //Del 2 
            WriteLine();
            tools.TypeLine("Vad vill du göra nu?", true);
            WriteLine("1. Gå söderut");

        }
    }

    //George Skörwen!
    public void georgeSkoerweDesc(bool pearlPossession, int part)
    {
        if (part == 0)
        {
            WriteLine();
            tools.TypeLine("Någonting rör sig under sjöns blanka yta. Det kommer närmare, mycket snabbt!", true);
            tools.TypeLine("Sedan är det som att ytan exploderar. Ur djupet av sjön stiger vad som ser ut som en tjock stam upp med hisnande fart.", true);
            tools.TypeLine("Framför dig tornar ett gigantiskt vidunder upp sig; ändå är det bara huvud, hals och en bit av ryggen som syns. \n Odjurets huvud beskrivs bäst som en blandning mellan en hund och en ödla, med en krage av hudflikar som ligger avslappnat bakåt, och kalla gråa kattögon. \n", true);
            tools.TypeLine("Den spänner blicken i dig. Sedan blir allt lugnt och tyst, nästan som att odjuret väntar på något.", true);
            WriteLine();
            tools.TypeLine("Slutligen öppnar besten sitt gap, och börjar tala.", true);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Har du min pärla?'");
            tools.TypeLine(" frågar den med mullrande röst. \n", false);
            tools.TypeLine("Vidundrets ögon börjar lysa när det tittar på dig, och du känner dig plötsligt obehagligt naken, alldeles avklädd.", true);
        }

        //Om man har pärlan i inventory:
        else if (pearlPossession && part == 1)
        {
            WriteLine();

            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag ser att du har min pärla på din person'");
            tools.TypeLine(", dånar besten. \n", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Är du George Skörwe?'");
            tools.TypeLine(" piper du tillbaka. \n", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag är en George Skörwe, inte ensam i mitt slag'");
            tools.TypeLine(", svarar George Skörwen. \n", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag förlorade min pärla för länge sedan, och mången vandrare har funnit sin väg hit utan att ha min pärla med sig'");
            tools.TypeLine(", fortsätter den. ", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag vill ha den tillbaka.' \n");
            tools.printMessage(true, true, ConsoleColor.DarkCyan, "''Rättvisa är mig kär och ära sätter jag högt, ty jag är en George Skörwe; jag föreslår ett byte. Återlämna mig min dyrbara pärla, och du skall i gengäld få ett föremål av valfri art. Vad sägs?'");
            WriteLine();
            tools.TypeLine("Du funderar ett slag. Går du med på ett byte? [ y / n ]", true);
        }
        else if (pearlPossession && part == 2)
        {
            WriteLine();
            tools.TypeLine("George Skörwen ser tillfreds ut. ", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Vad skall ditt föremål vara av för natur?' \n");
            WriteLine("1. Magic artefact");
            WriteLine("2. Wearables");
            WriteLine("3. Instrument");
            WriteLine("4. Academia");
        }
        else if (pearlPossession && part == 3)
        {
            WriteLine();
            tools.TypeLine("George Skörwen håller i sin dyrbara pärla med gigantiska, fjälliga reptilhänder.", true);
            tools.TypeLine("Den ser rört på dig. ", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Du har min eviga tacksamhet!' \n");
            tools.TypeLine("Anar du tårar i monstrets ögon? ", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Tack själv'");
            tools.TypeLine(", svarar du. \n ", false);
            tools.TypeLine("Och med det dyker George Skörwen bakåt och försvinner ner i djupet, för att aldrig kräva en vilsen vandrares liv igen. \n", true);
        }
        else if (!pearlPossession)
        {
            //Om man inte har pärlan i inventory
            WriteLine();
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Ännu en själ som frivilligt vandrar till sitt slut'");
            tools.TypeLine(", bullrar besten djupt. \n", false);
            tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Vad menar du?'");
            tools.TypeLine(", piper du tillbaka. \n", false);
            tools.TypeLine("Monstrets ögon smalnar till strimmor.", true);
            tools.printMessage(true, true, ConsoleColor.DarkCyan, "'Jag förlorade min pärla för länge sedan, och ingen jag mött har bringat den tillbaka till mig. Den enda nytta ni tjänar mig är att fylla min mage.'");

        }

    }

    //Förtvivlans fält
    public void despairFieldDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du går och går och går. Du börjar nästan känna det som att du är på självaste världens kant. \n", true);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Gå västerut");
            WriteLine("2. Gå norrut");
        }
    }

    //Spurten!! 
    public void spurtenDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du går på ett fält som löper längsmed älven.", true);
            tools.TypeLine("Långt nordväst kan du skönja något som sticker upp i horisonten, men vad det är kan du inte se. ", true);
            tools.TypeLine("Solen står lägre på himlen nu, och du börjar bli trött. \n", true);

        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra?", true);
            WriteLine("1. Gå nordväst");
            WriteLine("2. Gå norrut");
            WriteLine("3. Gå österut");
        }
    }

    //Tidsslöseri 1
    public void wasteOfTimeDesc(int version, int part)
    {
        if (version == 1)
        {
            //Tidsslöseri 1
            if (part == 1)
            {
                tools.TypeLine("Du går längsmed kanten på världen. Här finns absolut ingenting.", true);
                tools.TypeLine("Du måste ha gått i timmar, eller så känns det bara som att tiden står still. \n", true);
            }
            else if (part == 2)
            {
                //Valbeskrivningar
                tools.TypeLine("Var vill du gå?", true);
                WriteLine("1. Västerut");
                WriteLine("2. Söderut");
            }
        }
        else if (version == 2)
        {
            //Tidsslöseri 2
            if (part == 1)
            {
                tools.TypeLine("Här finns ingenting heller. Dina fötter värker. Du börjar bli törstig.", true);
                tools.TypeLine("Landskapet är totalt öde. \n", true);
            }
            else if (part == 2)
            {
                //Valbeskrivningar
                tools.TypeLine("Var vill du gå?", true);
                WriteLine("1. Västerut");
                WriteLine("2. Österut");
            }

        }
        else if (version == 3)
        {
            //Tidsslöseri 3
            if (part == 1)
            {
                tools.TypeLine("Här är allt tomt, kargt, stort och ogästvänligt. Finns det något här att finna?", true);
                tools.TypeLine("Du ser ingenting annat än öppna ytor och någon enstaka kråkfågel. \n", true);
            }
            else if (part == 2)
            {
                //Valbeskrivningar
                tools.TypeLine("Var vill du gå?", true);
                WriteLine("1. Västerut");
                WriteLine("2. Österut");
                WriteLine("3. Söderut");
            }

        }
    }

    //Utsidan av lunaris
    public void outsideLunarisDesc(int part, string direction)
    {
        //Part 1 baserat på om man kommer österifrån/söderifrån
        if (part == 1)
        {
            //Om man kommer från tidsslöseri 3 eller spurten
            if (direction == "e" || direction == "se")
            {
                tools.TypeLine("Du ser ett torn i fjärran. Ju närmre det kommer desto större blir det (lustigt hur det där funkar).", true);
            }
            else if (direction == "w")
            {
                //Om man kommer från hemliga hörnet
                tools.TypeLine("Du går tillbaka till mot Lunaris. Eftersom du kom västerifrån måste du gå runt alla träd för att komma till framsidan av tornet.", true);
                tools.TypeLine("Det orkar du inte, så du försöker gå igenom den lilla skogen. Det är i stort sett bottenlöst och du får kämpa dig upp ur sumphål efter sumphål som slukat dig till midjan.", true);
                tools.TypeLine("Det måste ju vara en trollformel för att skydda tornet från anfall? \n", true);
                tools.TypeLine("Du tar dig äntligen ut från träden, täckt av dy. Din heder lämnade du kvar i gyttjan. \n", true);
            }

            //Gemensam beskrivning
            tools.TypeLine("Tornet är fullkomligt massivt och när du står vid foten av det ser det ut att nå till himlen.", true);
            tools.TypeLine("Det är byggt av trä och sten och ser faktiskt exakt ut så som man tänker sig att ett trollkarlstorn ska se ut. \n", true);
        }


        //Valbeskrivning
        if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra nu?", true);
            WriteLine("1. Gå in i tornet");
            WriteLine("2. Gå västerut");
            WriteLine("3. Gå österut");
            WriteLine("4. Gå sydöst");
        }
    }

    //Hemliga hörnet
    public void secretCornerDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du känner en dragning till den plätt som syns bakom Lunaris, bredvid älvkanten. Du kan inte förklara varför.", true);
            tools.TypeLine("Platsen känns som en fridfull fristad. \n", true);
        }
        else if (part == 2)
        {
            WriteLine();
            tools.TypeLine("Vad vill du göra nu?", true);
            WriteLine("1. Gå österut");
        }
    }

    //Inne i Lunaris
    public void insideLunaris()
    {
        //Gå in i tornet
        tools.TypeLine("Du trycker upp dörren till tornet och kliver in. \n Hallen är mycket stor och fylld med tavlor, böcker, kartor och förtrollade ting.", true);
        tools.TypeLine("En underlig mackapär med kugghjul hänger fritt i luften och tickar. \n", true);
        tools.TypeLine("Hallen delar sig i flera andra rum, men du känner på dig att du inte ska snoka. Istället vänder du dig mot den lååånga spiraltrappan, vars steg är klädda i safirfärgad matta. \n", true);
        tools.TypeLine("När du går uppför trappan passerar du våning efter våning.", true);
        tools.TypeLine("Våning 2 verkar vara ett växthus med magiskt förstärkt ljus.", true);
        tools.TypeLine("Våning 3, 5 och 7 är alla bibliotek.", true);
        tools.TypeLine("Våning 4 och 6 verkar vara laboratorier.", true);
        tools.TypeLine("Våning 8 verkar vara en ädelstensamling med glasskåp, och våning 9 ett förråd.", true);
        tools.TypeLine("Våning 10 är helt tom sånär som på ett badkar mitt i det cirkulära rummet.", true);
        tools.TypeLine("Du tappar räkningen medan du klättrar, men det känns mycket hemtrevligare än du väntade dig. Gott om sittplatser och plantor. \n", true);
        tools.TypeLine("Till sist kommer du upp till trollkarlens studierum, eller kontor, du är lite osäker på vad man ska kalla det.", true);
        tools.TypeLine("Du drar ett djupt andetag och knackar på dörren. \n \n", true);
        tools.printMessage(true, true, ConsoleColor.DarkGray, "Tryck på nån tangent för att fortsätta...");
        ReadKey();
        Clear();
        //Träffa trollkarlen
        WriteLine();
        tools.printTitle("Trollkarlens studierum");
        tools.TypeLine("Dörren svänger upp av sig själv. Rummet är enormt och runt med fönster åt nästan alla håll. Det är fyllt med spännande krimskrams. ", true);
        tools.TypeLine("Du ser ett mäktigt teleskop som står ute på en slags stenterrass som löper runt hela utsidan. ", true);
        tools.TypeLine("Men du ser inte trollkarlen till en början, även om du hör hans rotande någonstans i rummet. Det visar sig komma från bakom skrivbordet. \n", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Öh... ursäkta om jag stör. Jag vaknade upp i skogen utan att minnas hur jag hamnade där. Finns det något du kan göra?'");
        tools.TypeLine(" undrar du försynt. \n \n", false);
        tools.TypeLine("Rotandet slutar tvärt och trollkarlen reser sig med ryggen mot dig. Han har på sig en glittrig, skir rock ovanpå läderbyxor.", true);
        tools.TypeLine("Du begriper inte riktigt vad som försiggår med hans kropp, förrän han vänder sig om och du med nöd och näppe lyckas undvika att utbrista något olämpligt.", true);
        tools.TypeLine("Ditt ansikte måste dock ha förrått dig, för trollkarlen får en irriterad uppsyn (imponerande bedrift, faktiskt).", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Det var en trollformel som slog slint. Jag försöker leta upp en motformel!'");
        tools.TypeLine(" bräker han fram. \n \n ", false);
        tools.TypeLine("Har du någonsin sett en damaskus-get? Bockarna har häpnadsväckande konvexa nosryggar, långa halsar och groteska underbett.", true);
        tools.TypeLine("Ta en sådan gets huvud och sätt det på en lindorms kropp, smäll på lite glesa fjädrar och ge den människoarmar och ben, så har du trollkarlen. \n", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Hur mycket av ditt minne är borta? Några bestående men?'");
        tools.TypeLine(" brölar trollkarlen. \n", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag minns allt utom hur jag hamnade i gläntan'");
        tools.TypeLine(" svarar du. \n", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Då tycker jag faktiskt inte att det är värt att lägga tid på att gräva i det där. Låt det vara en konstig grej.'");
        tools.TypeLine(" Trollkarlen plirar mot dig med sina horisontella pupiller. Han är faktiskt jätteful. ", false);
        tools.printMessage(true, true, ConsoleColor.DarkCyan, "'Har du tagit med dig några föremål att visa mig?'");
        WriteLine();
        tools.printMessage(true, true, ConsoleColor.DarkGray, "Tryck på nån tangent för att fortsätta...");
        ReadKey();
        Clear();
        //Visa föremål
        WriteLine();
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Man kan lära sig mycket om en person baserat på innehållet i dess fickor, både vart hen kom ifrån och vad hen ska bli");
        tools.TypeLine(", säger trollkarlen brummigt och gestikulerar mot sitt skrivbord. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Kom fram här och visa mig vad du har plockat på dig på din färd.' \n");
        tools.TypeLine("Du går långsamt fram till skrivbordet. \n", true);

    }

    /* --- VARIANTER PÅ SLUT --- */

    //Tjuvslut
    public void thiefEnding()
    {
        WriteLine();
        tools.TypeLine("När du lägger upp George Skörwens Pärla på skrivbordet mörknar trollkarlens blick (ännu en imponerande bedrift).", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Vad ska det här föreställa!?'");
        tools.TypeLine(", frågar han anklagande, som man gör till en hund med kattsand på nosen efter att ha varit och kalasat i kattlådan. \n ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Det är en pärla'");
        tools.TypeLine(", svarar du osäkert. Hans getögon kanske gör det svårt för honom? \n", false);
        tools.printMessage(true, true, ConsoleColor.DarkCyan, "'Ja, det ser jag väl. Det är George Skörwens Pärla. Den är inte din.' \n");
        tools.TypeLine("Trollkarlen smäller handflatorna i bordet och stirrar ner dig. Han är ännu fulare framifrån, för då ser han skelögd ut till råge på allt.", true);
        tools.printMessage(true, true, ConsoleColor.DarkCyan, "'Vet du hur många som har fått sätta livet till för att George Skörwens Pärla har varit på vift? Vet du hur många lik som pryder Bråddjupa Brallblötans stränder?' \n");
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Och här går du runt med den i fickan och bryr dig inte alls om all blodspillan du har orsakat. Jag borde gott offra dig till George Skörwen som straff'");
        tools.TypeLine(", fräser han irriterat. Sedan är det som att han hajar till. \n \n ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Det var faktiskt inte en helt dum idé'");
        tools.TypeLine(", säger han, mer för sig själv än till dig. \n ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jo det var det, det var en jättedum idé. Jag tycker inte alls om den'");
        tools.TypeLine(", skyndar du dig att säga, i hopp om att tanken inte ska hinna slå rot i hans huvud. \n ", false);
        tools.TypeLine("Men trollkarlen viftar bort dina protester.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jo, så får det bli!'");
        tools.TypeLine(", bestämmer han. \n", false);
        tools.TypeLine("Han sträcker ut en hand mot dig och plötsligt känner du hur hela din kropp binds av rep som dyker upp ur tomma intet. Du kan inte röra en fena, inte ens öppna munnen för att prata. Du är en repmumie. ", true);
        tools.printMessage(true, true, ConsoleColor.DarkCyan, "'Imorgon vid gryningen återförenar jag George Skörwen med sin pärla, och ditt liv ska bli det sista som krävs av odjuret, som straff för de liv du inte skonade på grund av din girighet.' \n");
        tools.printMessage(true, true, ConsoleColor.DarkCyan, "'Vi skulle kunna ha gjort det redan ikväll, men klockan börjar bli middagsdags och jag har en torskgratäng i ugnen, så jag känner inte för det.' \n");
        tools.TypeLine("Och med det öppnar trollkarlen en lucka i golvet med hjälp av magi och puttar ner dig.", true);
        tools.TypeLine("Det är en lång rutschkana med loopar och svängar, och till sist spottar den ut dig på ett hårt, kallt stengolv i en fuktig fängelsehåla.", true);
        tools.TypeLine("Där sitter du till gryningen och förbannar dig själv för att du inte tog vägen förbi Bråddjupa Brallblötan efter att du plockade på dig pärlan.", true);
        WriteLine("\n ");
        tools.TypeLine("Nåväl. Det är lätt att vara efterklok. \n \n", true);
        tools.printMessage(true, true, ConsoleColor.Blue, "*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
        tools.TypeLine("Du fick tjuvens slut!", true);
        tools.gameCredits();

    }

    //Dåligt slut
    public void badEnding()
    {
        WriteLine();
        tools.TypeLine("Du vänder ut och in på dina tomma fickor. Du plockade ju aldrig upp några föremål på färden hit.", true);
        tools.TypeLine("...", true);
        tools.TypeLine("Trollkarlen ser besviket på dig (också en bedrift). ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag tycker det är oförskämt att komma hit och kräva min tid alldeles tomhänt.' \n");
        tools.printMessage(true, true, ConsoleColor.DarkCyan, "'Jag tycker faktiskt inte att det är mycket begärt. Jag vill liksom inte ta dina föremål ifrån dig, jag vill bara analysera litegrann. Det är ju hela min grej.' \n");
        tools.TypeLine("Han fäller frustrerat ut med händerna.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Hela den här tiden som vi har stått och pratat nu hade jag kunnat lägga på att fixa mitt utseende. Vilket totalt slöseri!' \n \n");
        tools.TypeLine("I ren irritation viftar han med handen mot dig och du känner med ens helt bisarra saker hända med din kropp, som att du mosas ihop av en högtryckspress och tänjs ut på samma gång.", true);
        tools.TypeLine("Det sträcker och drar från alla håll och åt alla kanter, och plötsligt ligger du på marken och tittar upp. \n Du tror att du har öron. Det känns som att du har det. \n", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Vad har du gjort med mig!?' ");
        tools.TypeLine("utbrister du förskräckt. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag känner mig som en jakttrofé monterad på en träsköld!' \n");
        tools.TypeLine("Trollkarlen har inga ögonbryn, för han har damaskusgetansikte, men han lyckas ändå se fantastiskt förvånad ut. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Hur visste du det? Såg du din reflektion nånstans eller var det bara en lyckosam gissning?' \n");
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'ÄR jag en jakttrofé monterad på en träsköld!?' ");
        tools.TypeLine("skriker du. \n ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Naturligtvis. Slösar du min tid så tänker jag slösa din'");
        tools.TypeLine(", svarar han oberört och börjar hänga upp dig på väggen. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Här kan du gott hänga och tänka igenom saker och ting. Mors!' \n \n");
        tools.TypeLine("Trollkarlen spurtar ut ur rummet och lämnar dig där.", true);
        WriteLine("\n");
        tools.TypeLine("Och där hänger du. Dag ut och dag in. Du inser snabbt att det är lönlöst att böna och be trollkarlen om någonting.", true);
        tools.TypeLine("Så småningom byter du taktik och börjar irritera trollkarlen så mycket och ofta du kan.", true);
        tools.TypeLine("En konstig form av vänskap utvecklas mellan er. Du saknar honom när han inte är hemma, och han tycker resten av tornet är för tyst utan ditt kackel. \n", true);
        tools.TypeLine("Det finns väl värre öden, antar du.", true);
        WriteLine("\n");
        tools.printMessage(true, true, ConsoleColor.Blue, "*~*~*~*~*~*~*~*~*~*~*~*~*");
        WriteLine("Du fick den Tomhäntes slut!");

        tools.gameCredits();
    }

    //Lärlingsslut
    public void apprenticeEnd(bool fiveMatching)
    {
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Du tycks ha en preferens för det magiska'");
        tools.TypeLine(", säger trollkarlen. \n", false);
        tools.TypeLine("Du rycker på axlarna.", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, " 'Jag tycker det är spännande, antar jag.' \n");
        tools.TypeLine("Trollkarlen gör en fundersam min (verkligen beundransvärd bedrift med tanke på getansiktet). Han börjar spatsera medan han tänker.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Det råkar så vara'");
        tools.TypeLine(", säger han, ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'att min förra hjälpreda var ute och skulle samla in drakfjäll åt mig, men då själva draken dök upp tappade han fullkomligt huvudet. Han sprang i panik och blev bränd till en kolbit av draken. Jag fick aldrig fjällen jag behövde.' \n \n");
        tools.TypeLine("Trollkarlen gnider sitt absurda underbett innan han verkar bestämma sig.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag skulle kunna dra nytta av att ha en ny hjälpreda, och i utbyte erbjuder jag dig mat, boende och mitt mentorskap.' \n ");
        tools.TypeLine("Du blinkar chockat.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'..Mentorskap? Menar du som din lärling?'");
        tools.TypeLine(" frågar du dumt. \n", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Javisst. Du har en böjelse för magi; jag råkar vara trollkarl. Du behöver en lärare; jag behöver en lärling. Vad tycker du?'");
        tools.TypeLine(" svarar han. \n \n", false);
        tools.TypeLine("Du tvekar lite. Du vet inte hur klokt det är att tacka ja till att bli upplärd i magi av en trollkarl som just nu ser ut som en damaskus-get i ansiktet.", true);
        tools.TypeLine("Men å andra sidan har du inte så mycket bättre för dig.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Ja, varför inte? Ingen minns en fegis'");
        tools.TypeLine(" säger du och sträcker fram handen för att skaka hans. \n", false);
        WriteLine("\n");
        tools.TypeLine("Du sätts i hård skola nästan med en gång. I början tror du att han bara ville ha en husa, och ångrade ditt val mer än en gång.", true);
        tools.TypeLine("Men det dröjer faktiskt inte särskilt länge innan han börjar lära dig grunderna inom magi.", true);
        tools.TypeLine("Du övar flitigt varje dag, och innan du vet ordet av kan du åstadkomma fantastiska trollerikonster du bara drömt om tidigare.", true);
        tools.TypeLine("Du kan få saker att sväva, ändra färg och form, du till och med dra hundratals ihopknutna näsdukar ur din handflata! \n", true);
        tools.TypeLine("Du känner dig tillfreds med hur saker och ting artade sig.", true);
        WriteLine("\n");
        tools.printMessage(true, true, ConsoleColor.Blue, "*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
        WriteLine("Du fick Trollkarlens lärlings slut!");

        //Kolla om man har alla fem; visa meddelande då
        if (fiveMatching)
        {
            tools.congratulations("Magic Artefact");
        }
        tools.gameCredits();
    }

    //Bardslut
    public void bardEnd(bool fiveMatching)
    {
        WriteLine();
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Du tycks ha en preferens för det musikaliska'");
        tools.TypeLine(", säger trollkarlen. \n", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Vem älskar inte musik?'");
        tools.TypeLine(", svarar du. \n", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Kan du spela en truddelutt för mig?'");
        tools.TypeLine(", frågar han, och gestikulerar mot en One-Man Band-rigg. \n", false);
        tools.TypeLine("Du stelnar lite. Du har aldrig i ditt liv spelat på ett sånt.", true);
        tools.TypeLine("Men du är inget om inte ofantligt begåvad (och ödmjuk), så du går fram till riggen, sätter allt på plats, och börjar spela. \n", true);
        tools.TypeLine("Det du åstadkommer är rentav häpnadsväckande.", true);
        tools.TypeLine("Du börjar undra om riggen är magiskt betingad, för aldrig i ditt liv har du lyckats producera sådana ljud via instrument.", true);
        tools.TypeLine("Det är en fullkomlig amalgamation av alla de sämsta delarna hos varje instrument. Trummorna låter slappa och spelas i otakt.", true);
        tools.TypeLine("Gitarren klingar helt falskt. Munspelet låter väl som vilket munspel som helst om man inte kan spela munspel.", true);
        tools.TypeLine("Cymbalerna ska vi inte ens tala om. \n", true);

        tools.TypeLine("Trollkarlen applåderar glatt med lysande ögon (om nu en damaskus-get kan ha den blicken).", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Det där lät rent förjävligt'");
        tools.TypeLine(", säger han lyckligt. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Du behöver en replokal! Det råkar så vara att jag har en ljudisolerad källarvåning, den användes förr till att tortera fångar. Nu har jag inte tid med sånt, så du får gärna använda den för att finslipa dina förmågor.' \n");
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Menar du verkligen det?'");
        tools.TypeLine(", frågar du hoppfullt. \n", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Absolut! Sådär kan du inte gå runt och låta. Jag gör samhället en tjänst, tro mig.'");
        tools.TypeLine(" svarar han. \n \n", false);
        tools.TypeLine("Och så visar han dig vägen ner i källaren, där det mycket riktigt finns en hel våning där du kan trumma och humma bäst du vill.", true);
        tools.TypeLine("Du spenderar timmar, som blir till veckor, som blir till månader i din replokal.", true);
        tools.TypeLine("Ibland kommer trollkarlen för att lyssna. Du blir stadigt bättre och bemästrar snart de flesta instrument du tar dig an. \n", true);
        tools.TypeLine("Till sist är du redo att ge dig av.", true);
        tools.TypeLine("Du tackar trollkarlen så mycket innan du spelar din väg genom världen, och sprider glädje och musik var du än går.", true);
        tools.TypeLine("Du blir välkänd som en av de bästa trubadurer världen någonsin har skådat, och du spelar för trollkarlen på hans födelsedagar.", true);
        tools.TypeLine("När du slutligen dör - gammal, lycklig och med musik i hjärtat - blir du ihågkommen under hundratals år framöver.", true);


        WriteLine("\n");
        tools.printMessage(true, true, ConsoleColor.Blue, "*~*~*~*~*~*~*~*~*~*~*");
        WriteLine("Du fick Bardens slut!");

        //Kolla om man har alla fem; visa meddelande då
        if (fiveMatching)
        {
            tools.congratulations("Instrument");
        }
        tools.gameCredits();
    }

    //Vagabondslut
    public void vagabondEnd(bool fiveMatching)
    {
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Du tycks ha en preferens för klädnader'");
        tools.TypeLine(", säger trollkarlen. \n", false);
        tools.TypeLine("Du lägger huvudet lite på sned. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag gillar att de har olika bakgrund. Att klä sig i dem är som att bära små historier från sina resor.' \n");
        tools.TypeLine("Någonting glittrar till i trollkarlens skelögda getögon.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Det gillar jag att höra'");
        tools.TypeLine(", säger trollkarlen.", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag har något åt dig.' \n \n");
        tools.TypeLine("Han dyker ner bakom skrivbordet igen. Du lutar dig lätt framåt över bordet för att se vad han gör.", true);
        tools.TypeLine("Han slänger upp locket på en stor kista och river ut ting efter ting som han vårdslöst slänger över sin axel på jakt efter något särskilt.", true);
        tools.TypeLine("En pälsmössa med öronlappar. Ett svart stearinljus. En tom fågelbur. En uppdragbar råtta. En bok om det ockulta. Du funderar lätt nervöst på om dessa objekt är relaterade till varandra. \n", true);

        tools.TypeLine("Plötsligt håller han triumferande upp det han sökt. En karta?", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Här ska du få se!'");
        tools.TypeLine(" bräker han lyckligt. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Det här är en magisk karta över de säkraste färdvägarna på kontinenten. Vägarna förändras enligt rådande säkerhetsläge, så att du alltid kan resa utan att oroa dig för att råka illa ut.' \n");
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Imponerande'");
        tools.TypeLine(", svarar du. Trollkarlen sticker kartan i din hand. Du börjar protestera. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Inte kan jag ta den!' \n");
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jovisst kan du det! Jag har överfört den kartan till mitt medvetande, så jag har allt här uppe'");
        tools.TypeLine(", säger han och knackar sig lätt i tinningen. Du vet inte om du litar på det, men du tar emot kartan ändå. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'En sak till! Med detta sigill får du billigare öl på alla de bästa pubarna i varje halvstor stad.' \n");
        tools.TypeLine("Han höjer en hand, och någonting bränner till på din handled. En symbol dyker upp där det brände.", true);
        tools.TypeLine("Du stryker med tummen över den, men symbolen smetas inte ens ut. En magisk tatuering? \n", true);

        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Hur ska jag kunna tacka dig?'");
        tools.TypeLine(" frågar du. \n", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Kom på besök nån gång ibland.'");
        tools.TypeLine(" svarar han. \n \n", false);

        tools.TypeLine("Och med det ger du dig av för att påbörja en ny resa.", true);
        tools.TypeLine("Du reser överallt du kan ta dig. Ibland sysslar du med byteshandel, ibland agerar du köpman.", true);
        tools.TypeLine("Du samlar på kläder och historier, klär din kropp i minnena från alla dina äventyr, och blir igenkänd i världens alla hörn som ett bekant ansikte.", true);
        tools.TypeLine("Du stannar aldrig länge på ett ställe, för din ande kan inte förankras, och du hör inte till ett enda ställe.", true);
        tools.TypeLine("Världen är ditt hem.", true);

        WriteLine("\n");
        tools.printMessage(true, true, ConsoleColor.Blue, "*~*~*~*~*~*~*~*~*~*~*~*~*");
        WriteLine("Du fick Vagabondens slut!");

        //Kolla om man har alla fem; visa meddelande då
        if (fiveMatching)
        {
            tools.congratulations("Wearables");
        }
        tools.gameCredits();
    }

    //Kvaserslut
    public void kvaserEnd(bool fiveMatching)
    {
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Du tycks ha en preferens för det akademiska'");
        tools.TypeLine(", säger trollkarlen. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Är du kunskapstörstande?' \n");
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag vill lära mig allt om allt'");
        tools.TypeLine(", bekräftar du. \n", false);
        tools.TypeLine("Trollkarlen ler, vilket ser fantastiskt ocharmigt ut med tanke på get-underbettet.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'I såna fall har jag någon jag vill att du ska träffa'");
        tools.TypeLine(", säger han. Han går till väggen och drar i ett snöre som sitter monterat på väggen, och när han gör det hörs en ringklocka någonstans. \n", false);
        tools.TypeLine("Du ser frågande på honom. Han säger ingenting. \n", true);
        tools.TypeLine("Efter några minuters spänd väntan har det inte hänt någonting. Han ringer igen.", true);
        tools.TypeLine("Efter några till minuter blir du obekväm av tystnaden, börjar vagga på fötterna och göra den där fingerknäpp-till-klapp-grejen med händerna medan du visslar svagt.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag vet inte vad problemet är, han brukar alltid infinna sig snabbt när man ringer efter honom'");
        tools.TypeLine(", mumlar trollkarlen urskuldrande. \n Han går fram till dörren, sticker ut huvudet, och skriker för full hals ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'KVASER!!!!' \n");
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Kvaser?'");
        tools.TypeLine(" börjar du fråga, men tystnar tvärt. \n \n", false);

        tools.TypeLine("In i rummet kommer självaste Kvaser. Luften tycks spraka runt honom. Du står och betraktar en livs levande gud.", true);
        tools.TypeLine("Född av salivet från två gudasläkten, självaste gestaltningen av vishet och fred.", true);
        tools.TypeLine("Kvaser, den allvetande som kan svara på varje fråga som ställs honom, som är klokare än någon annan, som står för förening och harmoni.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Tjenare'");
        tools.TypeLine(", hälsar han. \n \n", false);

        WriteLine();
        tools.printMessage(true, true, ConsoleColor.DarkGray, "Tryck på nån tangent för att fortsätta...");
        ReadKey();
        Clear();
        WriteLine();

        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Vad tog dig sån tid? Det är ju pinsamt att stå här och vänta'");
        tools.TypeLine(", förbarmar sig trollkarlen. Kvaser ser totalt oberörd ut. \n", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag var lite upptagen med annat, men du kan förlita dig på att jag släppte allt så snart jag kunde för att komma till dig, min vän'");
        tools.TypeLine(", svarar Kvaser milt. Sedan tittar han på dig. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Vem är du?' \n");
        tools.TypeLine("Du höjer ett ögonbryn. Borde han inte redan veta det?", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Naturligtvis vet jag det'");
        tools.TypeLine(", säger han som om han kunde läsa dina tankar. ", false);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag frågade mest för att vara artig. Du kom till trollkarlen för att få hjälp, och trollkarlen vill att jag ska ta dig an som lärling.' \n");
        tools.TypeLine("Trollkarlen nickar entusiastiskt och Kvaser ler.", true);
        tools.printMessage(false, true, ConsoleColor.DarkCyan, "'Jag ska lära dig allt jag kan'");
        tools.TypeLine(", nickar Kvaser. \n \n", false);

        tools.TypeLine("Det visar sig att Kvaser kan ganska mycket, så att lära sig allt han kan är inte helt realistiskt.", true);
        tools.TypeLine("Men han lär dig om trolldrycker, om alkemi, om astronomi, om biologi, om kemi, om alla andra läror som slutar på i.", true);
        tools.TypeLine("Under hans mentorskap blir du snart sedd som en vis och lärd person som besitter stor kunskap om allting.", true);
        tools.TypeLine("Folk kommer till dig för att få hjälp med nästan vad som helst; bota deras husdjur från sjukdomar, råda dem om kärleksproblem, investera deras pengar.", true);
        tools.TypeLine("Med Kvaser som vägledare löser du tvister, förlöser barn och filosoferar om livet. \n", true);
        tools.TypeLine("Trist, det som hände med Kvaser senare.", true);

        tools.printMessage(true, true, ConsoleColor.DarkGray, "(Du hade ingen roll i det)");

        WriteLine("\n \n");
        tools.printMessage(true, true, ConsoleColor.Blue, "*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~");
        WriteLine("Du fick Kvasers lärlings slut!");

        //Kolla om man har alla fem; visa meddelande då
        if (fiveMatching)
        {
            tools.congratulations("Academia");
        }
        tools.gameCredits();
    }


}