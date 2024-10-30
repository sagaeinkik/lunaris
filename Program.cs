/* MAIN: 
Här ligger själva "rummen" och anropar alla funktioner
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
    private static SceneDirection ctrl = new();

    //Bools för att hålla reda på om man redan stött på varelserna
    private static bool trollDead = false;
    private static bool sphinxCleared = false;
    private static bool seamonsterVisited = false;

    static void Main(string[] args)
    {
        /* //Visa meny!
        flow.intro();
        //Sätt first Time till true så man "vaknar upp"
        bool firstTime = true;
        //Första "rummet"
        campFireScene(firstTime); */
        insideLunaris();


    }
    /* --- "RUMMEN" ---- */

    //Lägret
    public static void campFireScene(bool firstTime)
    {
        Clear();
        tools.printTitle("Lägret");

        //Text att visa om det är första gången man är i rummet
        if (firstTime)
        {
            tools.TypeLine("Ditt äventyr börjar, som så många andras, med att du vaknar upp i en liten skogsglänta helt utan att minnas hur du hamnade där. Dina fickor är helt tomma. \n ", true);
            tools.TypeLine("Lyckligtvis är du en sån där lättsam person som inte ser några konstigheter alls i din situation. Du vet att det finns en trollkarl i Lunaris som säkert kan hjälpa dig.", true);
            tools.TypeLine("Han gillar dock inte att man kommer tomhänt. \n", true);
        }
        firstTime = false;
        //Beskrivning av scen
        flow.campFireDesc(1);

        //Kolla om item är ägt, fråga om lägg till:
        Item mungiga = invy.getItem(10);
        if (!flow.isOwned(mungiga, invy))
        {
            tools.TypeLine($"I kanten av gläntan ser du något som ligger och glittrar i morgonsolen. En {mungiga.name}! {mungiga.description} \n", true);
            flow.wantToAdd(mungiga, invy);
        }

        //Beskrivning av valmöjligheterna 
        flow.campFireDesc(2);
        //Anropa funktion som faktiskt gör något när man väljer 
        ctrl.campFireControls(invy);

    }

    //Skogsglänta
    public static void forestClearing()
    {
        tools.printTitle("Skogsgläntan");
        flow.forestClearingDesc(1);

        //Kolla om boots finns i inventory, annars erbjud att lägga till
        Item boots = invy.getItem(8);
        if (!flow.isOwned(boots, invy))
        {
            tools.TypeLine($"När du sänker blicken till skyltens bas tappar du hakan. {boots.name}!", true);
            tools.TypeLine($"{boots.description}", true);
            flow.wantToAdd(boots, invy);
        }
        //Fortsättning av scenbeskrivning
        flow.forestClearingDesc(2);

        //Kontroller av val
        ctrl.forestClearingCtrl(invy);
    }

    //Lilla södergläntan
    public static void southClearing()
    {
        tools.printTitle("Lilla södergläntan");

        flow.southClearingDesc(1);
        //Lägg till
        Item scroll = invy.getItem(16);
        if (!flow.isOwned(scroll, invy))
        {
            tools.TypeLine($"Något fångar ditt öga i det höga gräset. Det är något ihoprullat. Du tar upp föremålet och tittar närmre på det. \n", true);
            tools.TypeLine($"Det är {scroll.name}. {scroll.description}", true);
            flow.wantToAdd(scroll, invy);
        }
        //Riktningar
        flow.southClearingDesc(2);
        //Valkontroller
        ctrl.southClearingCtrl(invy);

    }

    //Skogsstig väst
    public static void westTrail()
    {
        tools.printTitle("Västra skogsstigen");

        flow.westTrailDesc(1);
        flow.westTrailDesc(2);

        ctrl.westTrailCtrl(invy);
    }

    //Trolltrakten
    public static void trollTrakten()
    {

        tools.printTitle("Trolltrakten");


        //Om trollet ej är dött:
        if (!trollDead)
        {
            //Anropa spelkontrollerna för att kullerbytta, kasta sten osv
            ctrl.aliveTrollCtrl(invy);
        }
        else
        {
            //Funktion med besegrat troll 
            trollDefeated();
        }
    }

    //Döda troll
    public static void trollDefeated()
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
        //Lägg till
        Item luta = invy.getItem(13);
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
        //Spelkontroller
        ctrl.trolltraktCtrl(invy);
    }

    //Grottmynning
    public static void caveEntrance()
    {
        tools.printTitle("Grottmynningen");

        //Del 1
        flow.caveEntranceDesc(1);

        //Lägg till
        Item skuggkappa = invy.getItem(5);
        if (!flow.isOwned(skuggkappa, invy))
        {
            tools.TypeLine($"Du stannar till precis under portvalvet, där någonting fångar din blick ur ögonvrån. En {skuggkappa.name} ligger på den steniga marken.", true);
            tools.TypeLine($"{skuggkappa.description}", true);
            flow.wantToAdd(skuggkappa, invy);
        }
        // Del 2
        flow.caveEntranceDesc(2);
        //Spelkontroller 
        ctrl.caveCtrl(invy);
    }

    //Trollsal
    public static void trollHall()
    {
        tools.printTitle("Trollsalen");
        //Rumbeskrivning
        flow.trollCaveDesc(1);

        //Lägg till
        Item trollspegel = invy.getItem(0);
        if (!flow.isOwned(trollspegel, invy))
        {
            tools.TypeLine($"Bland högarna av skatter hittar du plötsligt en vacker {trollspegel.name}. {trollspegel.description}", true);
            flow.wantToAdd(trollspegel, invy);
        }
        //Valbeskrivning
        flow.trollCaveDesc(2);
        //Spelkontroller
        ctrl.trollHallCtrl(invy);
    }

    //Östra skogsstigen
    public static void eastTrail()
    {
        tools.printTitle("Östra skogsstigen");

        flow.eastTrailDesc();

        ctrl.eastTrailCtrl(invy);
    }

    //Halvöstra strand
    public static void halfEastBeach()
    {
        tools.printTitle("Halvöstra strand");

        flow.halfEastBeachDesc(1);

        //lägg till
        Item klykring = invy.getItem(4);
        if (!flow.isOwned(klykring, invy))
        {
            tools.TypeLine($"Ett av träden har så långa och låga grenar att det nästan nuddar vattnet. På en av kvistarna ser du något underligt. Det ser ut som en... {klykring.name}", true);
            tools.TypeLine($"{klykring.description}", true);
            flow.wantToAdd(klykring, invy);
            WriteLine();
        }
        //Spelkontroll
        ctrl.halfEastBeachCtrl(invy);
    }

    //Nordvästra skogsstigen
    public static void northwestTrail()
    {
        tools.printTitle("Nordvästra leden");

        flow.northwestTrailDesc();

        ctrl.nwTrailCtrl(invy);
    }

    public static void dragonClearing()
    {
        tools.printTitle("Drakgläntan");

        //Beskriv scen
        flow.dragonClearingDesc(1);

        //Lägg till
        Item drakfjall = invy.getItem(19);
        if (!flow.isOwned(drakfjall, invy))
        {
            WriteLine();
            tools.TypeLine($"Medan du funderar på vad du ska göra blänker någonting till i gräset. Ett {drakfjall.name}! \n {drakfjall.description} \n", true);
            flow.wantToAdd(drakfjall, invy);
        }
        //Beskrivning av val
        flow.dragonClearingDesc(2);
        //Spelkontroller
        ctrl.dragonClearingCtrl(invy);
    }

    //Trädrandälvkant
    public static void treeLineRiver()
    {
        tools.printTitle("Trädrandälvkant");

        flow.treelineRiverDesc(1);

        //Lägg till
        Item bestiarium = invy.getItem(18);
        if (!flow.isOwned(bestiarium, invy))
        {
            tools.TypeLine($"Vid älvkanten har någon glömt ett föremål. Du tar upp det och tittar närmre på det. '{bestiarium.name}' står det. \n {bestiarium.description}", true);
            flow.wantToAdd(bestiarium, invy);
            WriteLine();
        }
        //Valbesk
        flow.treelineRiverDesc(2);
        //Spelkontroller
        ctrl.treelineriverCtrl(invy);

    }
    //Älvbanken
    public static void riverBank(string direction)
    {

        tools.printTitle("Älvbanken");

        //Beskrivning med riktning
        flow.riverBankDesc(direction);

        ctrl.riverBankCtrl(invy);
    }

    //Ombord på båt
    public static void inBoat()
    {

        tools.printTitle("Ombord på båt");

        flow.inBoatDesc(1);

        //Lägg till
        Item pumps = invy.getItem(9);
        if (!flow.isOwned(pumps, invy))
        {
            tools.TypeLine($"Medan du river runt bland grejerna på båten drar du undan en presenning och blottlägger ett förvånande föremål. {pumps.name}? ", true);
            tools.TypeLine($"{pumps.description} ", true);
            flow.wantToAdd(pumps, invy);
        }

        flow.inBoatDesc(2);

        ctrl.boatCtrl(invy);

    }

    //Norra strand
    public static void northShore()
    {
        tools.printTitle("Norra strand");

        flow.northShoreDesc(1);

        //Lägg till
        Item silvertejp = invy.getItem(2);
        if (!flow.isOwned(silvertejp, invy))
        {
            tools.TypeLine($"Där båten lägger an vid stranden sparkar du av misstag till någonting. Det verkar vara {silvertejp.name}...", true);
            tools.TypeLine($"{silvertejp.description} ", true);
            flow.wantToAdd(silvertejp, invy);
        }

        flow.northShoreDesc(2);

        ctrl.northShoreCtrl(invy);
    }

    //Bro: Vässia
    public static void bridgeWest(string direction)
    {
        tools.printTitle("Bro: vässia");

        flow.bridgeWestDesc(direction);
        //Spelkontroller, skicka med bool för om man klarar sfinxens gåta (till när man ska österut)
        ctrl.bridgeWestCtrl(invy, sphinxCleared);
    }

    //Lilla nordergläntan
    public static void northClearing()
    {
        tools.printTitle("Lilla nordergläntan");

        flow.northClearingDesc(1);

        //Lägg till
        Item draktrumma = invy.getItem(11);
        if (!flow.isOwned(draktrumma, invy))
        {
            tools.TypeLine("Bland de brända och förvridna liken av träd hittar du ett besynnerligt föremål.", true);
            tools.TypeLine($"En {draktrumma.name}! {draktrumma.description}", true);
            flow.wantToAdd(draktrumma, invy);
        }

        flow.northClearingDesc(2);
        //Spelkontroller
        ctrl.northClearingCtrl(invy);
    }

    //Sfinxens gåta
    public static void sphinxChallenge()
    {
        tools.printTitle("Bro: össia");

        //Beskrivning 1
        flow.sphinxChallengeDesc(1);
        //Själva gåtan
        flow.sphinxChallengeDesc(2);

        //Spelkontroller till gåtan
        ctrl.sphinxRiddleCtrl();
    }

    //Bro: össia (sfinxen besegrad)
    public static void bridgeEast(bool firstTime)
    {
        //Slå om bool till true
        sphinxCleared = true;

        tools.printTitle("Bro: össia");

        //Beskrivning baserat på om man vart där förr eller ej
        flow.bridgeEastDesc(1, firstTime);

        Item botanikerboken = invy.getItem(17);
        if (!flow.isOwned(botanikerboken, invy))
        {
            WriteLine();
            tools.TypeLine($"På stigen, strax till höger om sfinxens sittplats, ligger det kvar någonting. Du ser snabbt vad det är. {botanikerboken.name}!", true);
            tools.TypeLine($"{botanikerboken.description}", true);
            flow.wantToAdd(botanikerboken, invy);
        }

        //Valbesk
        flow.bridgeEastDesc(2);
        //Spelkontroller
        ctrl.bridgeEastCtrl(invy);
    }

    //Tämliga tältet
    public static void tentScene()
    {
        tools.printTitle("Tämliga tältet");
        //Scenbesk
        flow.tentSceneDesc(1);
        //Lägg till
        Item featherboa = invy.getItem(6);
        if (!flow.isOwned(featherboa, invy))
        {
            tools.TypeLine("Nyfikenheten tar överhanden och du petar in huvudet i tältöppningen. Vad du ser där får dig att dra efter andan.", true);
            tools.TypeLine($"En {featherboa.name}! {featherboa.description}", true);
            flow.wantToAdd(featherboa, invy);
        }
        else
        {
            //Alternativ text 
            tools.TypeLine("Du slår den tanken ur hågen. Det vore ju en jättekonstig sak att göra.", true);
            tools.TypeLine("Du bestämmer dig för att låta tältet vara ett av livets stora mysterium. ", true);
        }
        //Valbesk
        flow.tentSceneDesc(2);
        //Spelkontroll
        ctrl.tentCtrl(invy);
    }

    //Storslätta
    public static void storSlatt()
    {
        tools.printTitle("Storslätta");

        //Scenbesk
        flow.storSlattDesc(1);
        flow.storSlattDesc(2);
        //Spelkontroll
        ctrl.storSlattCtrl(invy);
    }

    //Bråddjupa brallblötan
    public static void brallBlotan()
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

            //Om man har pärlan, svara om man vill byta
            if (pearlOwned)
            {
                //Spelkontroll
                ctrl.wannaTrade(invy);
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
            //Spelkontroller
            ctrl.brallblotCtrl(invy);
        }
    }
    //Byte med Skörwe
    public static void tradeWithGeorge()
    {
        //Visa texten
        flow.georgeSkoerweDesc(true, 2);
        ctrl.tradingWithGeorge(invy);

        //Kör text efter man är klar med bytet
        flow.georgeSkoerweDesc(true, 3);

        //Slå om besökt-variabeln
        seamonsterVisited = true;

        //Visa val att gå
        flow.brallblotanDesc(2);

        ctrl.brallblotCtrl(invy);
    }

    //Förtvivlans fält
    public static void despairField()
    {
        tools.printTitle("Förtvivlans fält");

        flow.despairFieldDesc(1);

        //Lägg till
        Item pearl = invy.getItem(20);
        if (!flow.isOwned(pearl, invy))
        {
            tools.TypeLine("Allt runtomkring dig är bara en stor tomhet av gult gräs. Du går i all oändlighet, och plötsligt halkar du till och störtar som JAS 39-Gripen på Långholmen i Augusti 1993.", true);
            tools.TypeLine("Mäkta förargad över att nu sitta på arslet efter att ha stått ståtlig ser du dig omkring på marken efter vad som bringade dig på fall.", true);
            tools.TypeLine($"{pearl.name}? {pearl.description} \n", true);
            flow.wantToAdd(pearl, invy);
        }

        flow.despairFieldDesc(2);

        ctrl.despairFieldCtrl(invy);

    }

    public static void spurten()
    {
        tools.printTitle("Spurten");

        flow.spurtenDesc(1);

        Item moonstone = invy.getItem(1);

        if (!flow.isOwned(moonstone, invy))
        {
            tools.TypeLine("Du råkar sparka till ett föremål så det flyger. Du skriker till och sjunker ner på knä; din tå ömmar som om den vore bruten.", true);
            tools.TypeLine("Visste du förresten att Viggo Mortensen bröt sin tå när han sparkade till Uruk hai-hjälmen i Sagan om de två Tornen? \n", true);
            tools.TypeLine($"Det du sparkade till är dock ingen uruk-hjälm. Det är en {moonstone.name}.", true);
            tools.TypeLine($"{moonstone.description} \n", true);
            flow.wantToAdd(moonstone, invy);
        }

        flow.spurtenDesc(2);

        ctrl.spurtCtrl(invy);
    }
    //Tidsslöseri 1
    public static void timeWaste1()
    {
        tools.printTitle("Ödemarkerna");
        //Scenbesk
        flow.wasteOfTimeDesc(1, 1);
        //Valbesk
        flow.wasteOfTimeDesc(1, 2);
        //spelktrl
        ctrl.tw1Ctrl(invy);

    }
    //Tidsslöseri 2
    public static void timeWaste2()
    {
        tools.printTitle("Ödemarkerna");
        //scenbesk
        flow.wasteOfTimeDesc(2, 1);
        //valbesk
        flow.wasteOfTimeDesc(2, 2);
        //Spelktrl
        ctrl.tw2Ctrl(invy);
    }
    //Tidsslöseri 3
    public static void timeWaste3()
    {
        tools.printTitle("Ödemarkerna");
        //scenbesk
        flow.wasteOfTimeDesc(3, 1);
        //valbesk
        flow.wasteOfTimeDesc(3, 2);
        //Spelktrl
        ctrl.tw3Ctrl(invy);
    }
    //Utanför lunaris
    public static void outsideLunaris(string direction)
    {
        tools.printTitle("Utanför trollkarlstornet Lunaris");

        flow.outsideLunarisDesc(1, direction);

        flow.outsideLunarisDesc(2, direction);

        ctrl.outsideLunarisCtrl(invy);
    }

    //Hemliga hörnet
    public static void secretCorner()
    {
        tools.printTitle("Hemliga hörnet");

        flow.secretCornerDesc(1);
        //Lägg till
        Item silverlyra = invy.getItem(12);
        if (!flow.isOwned(silverlyra, invy))
        {
            tools.TypeLine("Du ser något vackert stå lutat mot ett träd nära stranden.", true);
            tools.TypeLine($"Det ser ut att vara en {silverlyra.name}.", true);
            tools.TypeLine($"{silverlyra.description} \n", true);
            flow.wantToAdd(silverlyra, invy);
        }

        flow.secretCornerDesc(2);
        ctrl.secretCornerCtrl(invy);
    }

    //TROLLKARLSTORNET!!
    public static void insideLunaris()
    {
        tools.printTitle("Inuti trollkarlstornet Lunaris");
        flow.insideLunaris();

        //Om man äger pärla, skicka till tjuvslut på en gång.
        if (flow.isOwned(invy.getItem(20), invy))
        {
            flow.thiefEnding();
        }

        //Kolla vilken kategori man plockat på sig flest av
        string mostOwned = invy.distinctItemCounter();

        switch (mostOwned)
        {
            case "Inga items!":
                //Dålig ending
                flow.badEnding();
                break;
            default:
                break;
        }



    }

}

