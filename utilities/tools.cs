/* 
----- Funktioner som underlättar för mig men som inte nödvändigtvis har med spelet att göra -----
 */

namespace lunaris;

public class Tools
{
    /* --- FUNKTIONER --- */
    //Funktion för att slippa skriva switch-satser om och om igen
    public void dirHandler(List<Command> commands)
    {
        //Av/på-knapp till input
        bool validChoice = false;

        //loop som körs tills man skrivit in accepterat input
        while (!validChoice)
        {
            string userChoice = Console.ReadLine()!.ToLower();
            //Flagga för att köra felmeddelande om inmatning
            bool foundCommand = false;

            //Loopa igenom alla commands
            foreach (var command in commands)
            {
                //Kolla om kommandot finns i alias-listan:
                if (command.Aliases.Contains(userChoice))
                {
                    //Kalla på funktionen som anges
                    command.Execute();
                    //Bryt loopen om så anges
                    if (command.EndsLoop) validChoice = true;

                    //Flagga att vi hittat ett kommando
                    foundCommand = true;
                    break; //Bryter foreach-loopen ifall ifall
                }
            }
            //Hittades inte kommandot i listan över alias, visa felmeddelandet 
            if (!foundCommand)
            {
                printMessage(true, false, ConsoleColor.Yellow, "Förstod inte inmatningen. Testa skriva antingen alternativets siffra, eller riktningen på svenska eller engelska!");
            }
        }
    }

    //Återanvänd funktion från tidigare moment för att lägga till lite färg
    public void printMessage(bool newLine, bool typed, ConsoleColor color, string message)
    {
        //Kolla om meddelandet ska vara på egen rad eller inte
        if (newLine == true && typed == false)
        {
            //Ändra färgen
            ForegroundColor = color;
            //Ingen skrivmaskin, egen rad
            WriteLine(message);
            //Nollställ färgen
            ResetColor();
        }
        else if (newLine == true && typed == true)
        {
            ForegroundColor = color;
            //Skrivmaskin på egen rad
            TypeLine(message, true);
            ResetColor();
        }
        else if (newLine == false && typed == true)
        {
            ForegroundColor = color;
            //Ingen linebreak, skrivmaskin
            TypeLine(message, false);
            ResetColor();
        }
        else
        {
            ForegroundColor = color;
            //Ingen linebreak, ingen skrivmaskin
            Write(message);
            ResetColor();
        }
    }

    //METOD FÖR SKRIVMASKINSEFFEKT
    public void TypeLine(String message, bool br)
    {
        //Loopa igenom sträng
        foreach (var letter in message)
        {
            Write(letter);
            //Fördröjning
            Thread.Sleep(15);
        }
        Thread.Sleep(150);
        //Om <br> används, gör ny rad
        if (br)
        {
            WriteLine();
        }
    }
    public void gameCredits(bool gameFinished = false)
    {
        WriteLine();
        printMessage(true, true, ConsoleColor.Green, "T H E  E N D ");
        WriteLine($"Tack för att du spelade!");
        string credits = "Skapat av Saga Einarsdotter Kikajon för kursen Programmering i C# .NET på Mittuniversitetet 2024.";
        printMessage(true, false, ConsoleColor.DarkGray, credits);

        if (gameFinished)
        {
            printMessage(true, false, ConsoleColor.DarkGray, "\t (Tillägnas David, som har peppat, stöttat, och lyssnat på mitt gnäll.)");

        }

        //Avsluta, för den gör inte alltid det självmant. Vet ej varför
        Environment.Exit(0);
    }

    //METOD FÖR TITEL I RUMMET
    public void printTitle(string title)
    {
        printMessage(true, false, ConsoleColor.Green, title);
    }

    //Metod för att slumpa fram en gåta
    public Riddle generateRiddle()
    {
        //Lista med dumma gåtor
        List<Riddle> riddle = new List<Riddle>();
        riddle.Add(new Riddle("Hur många grönsaker finns det på Finlandsfärjorna?", "En per-silja!"));
        riddle.Add(new Riddle("Vad berättar korna för varandra när ljuset är släckt i fjöset?", "Spenande historier!"));
        riddle.Add(new Riddle("I vilket slag föll Karl XVII?", "Hans sista!"));
        riddle.Add(new Riddle("Hur är vädret i Tjernobyl?", "Strålande!"));
        riddle.Add(new Riddle("Vilket grundämne flyter sämst?", "Zink!"));
        riddle.Add(new Riddle("Vad händer om man korsar en flod och en öken?", "Man blir våt och törstig!"));
        riddle.Add(new Riddle("En frukt- och grönsakshandlare är 54 år gammal, 175 cm lång och har 43 i skonummer. Vad väger han?", "Frukt och grönsaker!"));
        riddle.Add(new Riddle("Varför kastar rovfåglarna längtansfyllda blickar in genom sovrumsfönstret?", "Det ligger örngott i sängen!"));
        riddle.Add(new Riddle("När levde världens största narr?", "Mellan sin födsel och sin död!"));
        riddle.Add(new Riddle("Heter den en rak kurva eller ett rak kurva?", "Det finns inga raka kurvor!"));
        riddle.Add(new Riddle("Vilken fågel är bäst på IT?", "Hacker-spetten!"));
        riddle.Add(new Riddle("Vad behöver man för att lokalisera sin varulv?", "En därulv!"));
        Random rnd = new Random();
        Index index = rnd.Next(4);

        return riddle[index];
    }

    //Grattis i alla färger:
    public void congratulations(string cat)
    {
        WriteLine();
        printMessage(false, false, ConsoleColor.Blue, "G ");
        printMessage(false, false, ConsoleColor.DarkCyan, "R ");
        printMessage(false, false, ConsoleColor.Green, "A ");
        printMessage(false, false, ConsoleColor.Yellow, "T ");
        printMessage(false, false, ConsoleColor.DarkYellow, "T ");
        printMessage(false, false, ConsoleColor.Red, "I ");
        printMessage(false, false, ConsoleColor.DarkMagenta, "S ");
        printMessage(false, false, ConsoleColor.DarkCyan, "! ");
        printMessage(false, false, ConsoleColor.Green, "! \n");

        TypeLine($"Du samlade på dig alla föremål inom kategorin {cat}!", true);
        TypeLine($"Din lilla samlare, där. Bra jobbat!", true);
    }

}