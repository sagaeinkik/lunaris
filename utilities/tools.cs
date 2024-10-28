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
        Thread.Sleep(200);
        //Om <br> används, gör ny rad
        if (br)
        {
            WriteLine();
        }
    }
    public void gameCredits()
    {
        string credits = "Skapat av Saga Einarsdotter Kikajon för kursen Programmering i C# .NET på Mittuniversitetet 2024.";
        printMessage(true, false, ConsoleColor.DarkGray, credits);
    }

    //METOD FÖR TITEL I RUMMET
    public void printTitle(string title)
    {
        printMessage(true, false, ConsoleColor.Green, title);
    }

}