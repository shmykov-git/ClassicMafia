
// what player a thinks about player b in round r 
// может быть замечен
public class Act
{
    public int r;
    public Player pA; 
    public Player pB;
    public int count;
    public required double force; // (0, 1) probability of act
    public required ActType type;
    public required ActAbout about;
    public List<Act> linkedActs = new(); // why he thinks so
    public string? text;
}

