//GÅTA

namespace lunaris;

public class Riddle
{
    public string Question { get; set; }
    public string Answer { get; set; }
    //Constructor
    public Riddle(string q, string a)
    {
        Question = q;
        Answer = a;
    }
}