/* 
----- Funktioner som underlättar för mig men som inte nödvändigtvis har med spelet att göra -----
 */

namespace lunaris;

public class Tools
{
    /* --- FUNKTIONER --- */
    //Återanvänd funktion från tidigare moment för att lägga till lite färg
    public void printMessage(bool newLine, ConsoleColor color, string message)
    {
        //Kolla om meddelandet ska vara på egen rad eller inte
        if (newLine == true)
        {
            //Ändra färgen
            ForegroundColor = color;
            //Skriv ut meddelandet till skärmen
            WriteLine(message);
            //Nollställ färgen
            ResetColor();
        }
        else
        {
            ForegroundColor = color;
            //Samma rad
            Write(message);
            ResetColor();
        }
    }

}