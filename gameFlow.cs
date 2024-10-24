/* 
----- Kontroller, WriteLines, annan köttig kod ------
 */
using System;
using System.Linq;
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
                //Lämna
                tools.printMessage(true, true, ConsoleColor.Yellow, "Du lämnar kvar föremålet där du hittade det.");
                break;
        }
    }
    //Kontroll om item redan finns i inventory, avgör scenen: 
    public bool isOwned(Inventory inv, Item item)
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
        WriteLine("Du kan kolla din inventory när som helst under spelets gång genom att skriva 'i' eller 'inventory' och trycka enter. \n \n");
        WriteLine("Tryck enter för att komma tillbaka till menyn.");
        //Tillbaka till startmenyn
        ReadKey();
        intro();
    }

    //Beskrivning av rum 1
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

}