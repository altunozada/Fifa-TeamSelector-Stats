using System.Linq;

internal class Takim:IComparable
{
    public string takimadi;
    public int ovr;

    public Takim(string takimadi, int ovr)
    {
        this.takimadi = takimadi;
        this.ovr = ovr;

    }

    public int CompareTo(object? obj)
    {
        throw new NotImplementedException();
    }
}