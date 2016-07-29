using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ListData
{
    public int BirthYear;
    public int DeathYear;
}

public class Globals
{
    private static readonly Globals instance = new Globals();

    public List<ListData> TheList = new List<ListData>();

    private Globals()
    {

    }

    public static Globals Instance
    {
        get
        {
            return instance;
        }
    }
}
