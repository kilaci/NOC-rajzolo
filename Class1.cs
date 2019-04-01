using System;

public class halozatielemek
{
    public int ID;
    public String nev;

    public halozatielemek(int p1, string p2)
    {
        ID = p1;
        nev = p2;

    }

}
public class vonal
{
    public int ID;
    public String nev;
    public int Obj1;
    public int Obj2;
    public vonal(int p1, string p2, int p3, int p4)
    {
        ID = p1;
        nev = p2;
        Obj1 = p3;
        Obj2 = p4;
    }

}