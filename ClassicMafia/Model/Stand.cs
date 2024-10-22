
/// what player a think about player b in round r 
public class Stand
{
    public required (int r, int a, int b) id;
    public required double force; // (-1, 1)
    public required Motive motive;
    public List<(int r, int a, int b)> explanations = new(); // why he thinks so
    public string? text;
}

