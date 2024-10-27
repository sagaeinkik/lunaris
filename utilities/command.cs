/* KOMMANDOKLASS: För att slippa skriva en miljon switchsatser.
Kopplad till tools-funktionen dirHandler */

public class Command
{
    //För lista med acceptabla input, ex gå norr/gå norrut/n
    public List<string> Aliases { get; set; }

    //Den funktion som körs när kommandot känns igen, dvs kallar på nästa scen
    public Action Execute { get; set; }

    //Bool för att ange om loopen ska avslutas i ett scenario
    public bool EndsLoop { get; set; }

    //Constructor
    public Command(List<string> alias, Action exec, bool endsLoop = false)
    {
        Aliases = alias;
        Execute = exec;
        EndsLoop = endsLoop;
    }
}