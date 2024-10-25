/* 
----- Funktioner som underlättar för mig men som inte nödvändigtvis har med spelet att göra -----
 */

namespace lunaris;

public class Tools
{
    /* --- FUNKTIONER --- */
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
        foreach (var letter in message)
        {
            Write(letter);
            Thread.Sleep(10);
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

}