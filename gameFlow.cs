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
            tools.TypeLine("Du lyckas utröna att en grotta ligger västerut, Norra strand nordväst, och brallblötan (står det verkligen så?) till nordöst. \n", true);
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
    public void northwestTrailDesc()
    {
        tools.TypeLine("Du går på en trevlig och bred stig, alternativt en trevlig och smal väg beroende på hur man ser på det. Den tycks fortsätta i all oändlighet.", true);
        tools.TypeLine("Periodvis växer det hallon längsmed vägen.", true);
        tools.TypeLine("Dina fötter känns lite trötta, så du stannar och plockar några att mumsa på för att få lite energi.", true);
        tools.TypeLine("Du börjar fundera på om du vill fortsätta på den här vägen, eller om du vill vända tillbaka. \n", true);


        tools.TypeLine("Vad vill du göra?", true);
        WriteLine("1. Följ vägen nordväst");
        WriteLine("2. Följ vägen sydöst");
    }

    //Drakglänta
    public void dragonClearingDesc(int part)
    {
        if (part == 1)
        {
            tools.TypeLine("Du kommer till stort, stort fält med mjukt, högt gräs. Du går genom höggräset och drar av frön från rajstjälkarna eftersom det är fysiskt omöjligt att inte göra det när man går genom gräs.", true);
            tools.TypeLine("Det härliga vädret och den fantastiska lägdan till trots har du ett stort problem. \n", true);
            tools.TypeLine("En enorm drake är ute och sträcker på sina vingar högt över fältet. Det finns inte en chans att du kan slåss mot den; ditt enda hopp är att undgå att bli upptäckt.", true);
            tools.TypeLine("Du stelnar till och tänker. Du känner dig osäker på hur drakar fungerar. \n", true);
            tools.TypeLine("Om du kryper långsamt och försiktigt över fältet kanske draken inte lägger märke till dina rörelser, men den kanske kan känna doften av dig, och du syns nog uppifrån. \n Om du springer syns inte lika mycket av din kroppsyta uppifrån, och du spenderar mindre tid totalt på fältet, vilket kan innebära mindre tid att bli upptäckt av draken på. Men det är kanske lättare att lägga märke till någon som springer än någon som kryper?", true);
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
            tools.TypeLine("Det tar evigheter att krypa över fältet. Flera gånger är du bergsäker på att draken sett dig, och det har stuckit obehagligt längsmed ryggraden när du hört dess vingslag precis ovanför dig utan att kunna se odjuret.", true);
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
            tools.TypeLine("Det tar evigheter att krypa över fältet. Flera gånger är du bergsäker på att draken sett dig, och det har stuckit obehagligt längsmed ryggraden när du hört dess vingslag precis ovanför dig utan att kunna se odjuret.", true);
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
            tools.TypeLine("I brist på andra idéer får du utgå från att båten styrs verbalt.", true);
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
            tools.TypeLine("'Får jag komma förbi, tack och snälla? Du sitter lite i vägen', frågar du med tunn röst.", true);
            tools.TypeLine("'Endast den värdige får mig passera oskadd', svarar sfinxen med en röst som tycks vibrera. 'Skänk mig det rätta svaret på min gåta, och jag bedömer dig värdig.'", true);
            tools.TypeLine("'Jag är inte så bra på gåtor', säger du. 'Måste jag?'", true);
            tools.TypeLine("Sfinxens pupiller vidgas lätt som av förväntan. 'Du svarar efter eget gottfinnande. Men att ej svara är i sig själv ett svar.'", true);
            tools.TypeLine("'Vad händer om jag svarar fel?' undrar du.", true);
            tools.TypeLine("'Jag kräver ditt liv som lön' svarar hon enkelt.", true);
            WriteLine();
            tools.TypeLine("Detta var ju högst beklagligt. Du verkar inte ha så mycket till val.", true);
            tools.TypeLine("'Nej, jag undviker gärna att dö. Låt höra gåtan', säger du.", true);
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
            tools.TypeLine("'Du har så rätt', säger hon till sist. Anar du besvikelse i hennes röst? 'Du får lov att passera mig, oskadd, var gång din väg bär dig förbi mig.'", true);
            tools.TypeLine("Och med det fäller hon ut sina vingar (de når nästan till träden på vardera sida), tar ett par kraftiga slag, och försvinner upp i luften. \n", true);
            tools.TypeLine("Stigen delar sig norrut och åt sydöst.", true);
        }
        else if (part == 1 && firstTime == false)
        {
            //Om man besegrade sfinxen innan
            tools.TypeLine("Du kommer fram till en bekant plats och en bekant figur. Sfinxen sitter precis där hon satt förut, och det rycker lätt i ormen som är hennes svans.", true);
            tools.TypeLine("'Välkommen åter, vandrare! Vägen är föga vältrafikerad och jag finner mig själv utled på denna enformighet. Vill du höra en gåta?' \n", true);
            tools.TypeLine("Innan du hinner svara tar hon ton. \n", true);
            tools.TypeLine($"'{randomRiddle.Question}'", true);
            tools.TypeLine("Du stirrar stumt på henne.", true);
            tools.TypeLine($"'{randomRiddle.Answer}'", true);
            tools.TypeLine("Hon slänger ut med sina lejonarmar, höjer ögonbrynen och ler brett med öppen mun. \n", true);
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
            tools.TypeLine("Du går längsmed älvkanten bland glesa träd och kommer fram till en tältplats. Det gulgröna tältet ser ut att vara välanvänt och står stadigt uppställt.", true);
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
            tools.TypeLine("Strandkanten till sjön prys av benknotor. Här och där kan du se delar av någon gammal rostig rustning. Du försöker att inte tänka på det för mycket. \n", true);
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
            tools.TypeLine("'Har du min pärla?' frågar den med mullrande röst.", true);
            tools.TypeLine("Vidundrets ögon börjar lysa när det tittar på dig, och du känner dig plötsligt obehagligt naken, alldeles avklädd.", true);
        }

        //Om man har pärlan i inventory:
        else if (pearlPossession && part == 1)
        {
            WriteLine();
            tools.TypeLine("'Jag ser att du har min pärla på din person', dånar besten.", true);
            tools.TypeLine("'Är du George Skörwe?' piper du tillbaka.", true);
            tools.TypeLine("'Jag är en George Skörwe, ty det är inte ett namn utan en art', svarar George Skörwen.", true);
            tools.TypeLine("'Jag förlorade min pärla för länge sedan, och mången vandrare har funnit sin väg hit utan att ha min pärla med sig', fortsätter den. 'Jag vill ha den tillbaka.'", true);
            tools.TypeLine("'Rättvisa är mig kär och ära sätter jag högt, ty jag är en George Skörwe; jag föreslår ett byte. Återlämna mig min dyrbara pärla, och du skall i gengäld få ett föremål av valfri art. Vad sägs?'", true);
            WriteLine();
            tools.TypeLine("Du funderar ett slag. Vad vill du göra?", true);
            WriteLine("1. Gå med på ett byte.");
            WriteLine("2. Tacka nej.");
        }
        else if (pearlPossession && part == 2)
        {
            WriteLine();
            tools.TypeLine("George Skörwen ser tillfreds ut. 'Vad skall ditt föremål vara av för natur?'", true);
            WriteLine("1. Magic artefact");
            WriteLine("2. Wearables");
            WriteLine("3. Instrument");
            WriteLine("4. Academia");
        }
        else if (pearlPossession && part == 3)
        {
            WriteLine();
            tools.TypeLine("George Skörwen håller i sin dyrbara pärla med gigantiska händer.", true);
            tools.TypeLine("Den ser rört på dig. 'Du har min eviga tacksamhet!'", true);
            tools.TypeLine("Anar du tårar i monstrets ögon? 'Tack själv', svarar du. \n", true);
            tools.TypeLine("Och med det dyker George Skörwen bakåt och försvinner ner i djupet, för att aldrig kräva en vilsen vandrares liv igen. \n", true);
        }
        else if (!pearlPossession)
        {
            //Om man inte har pärlan i inventory
            WriteLine();
            tools.TypeLine("'Ännu en själ som frivilligt vandrar till sitt slut', bullrar besten djupt.", true);
            tools.TypeLine("'Vad menar du?', piper du tillbaka.", true);
            tools.TypeLine("Monstrets ögon smalnar till strimmor. \n 'Jag förlorade min pärla för länge sedan, och ingen jag mött har bringat den tillbaka till mig. Den enda nytta ni tjänar mig är att fylla min mage.' \n", true);
        }

    }


}