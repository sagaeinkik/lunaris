/* 
----- Kontroller, WriteLines, annan köttig kod ------
 */

namespace lunaris;

public class GameFlow
{

    Tools tools = new();
    //Metod med kontrollfrågor om man vill lägga till item i inventor
    public void wantToAdd(Item item, Inventory inventory)
    {
        //Fråga om man vill lägga till
        tools.printMessage(false, ConsoleColor.Gray, $"Vill du lägga till {item.name} i din inventarie? [y / n] ");
        //Lagra användarinput
        string userChoice = ReadLine()!.ToLower();

        switch (userChoice)
        {
            case "y":
            case "yes":
            case "ja":
            case "add":
                //Lägg till i inventory
                inventory.addToInventory(item);
                tools.printMessage(true, ConsoleColor.Green, $"Du är nu stolt ägare till {item.name}!");
                break;

            case "n":
            case "no":
            case "nej":
            case "leave":
                //Lämna
                tools.printMessage(true, ConsoleColor.Yellow, "Du lämnar kvar föremålet där du hittade det.");
                break;

            default:
                //Lämna
                tools.printMessage(true, ConsoleColor.Yellow, "Du lämnar kvar föremålet där du hittade det.");
                break;
        }
    }

}