using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator
{
    static List<string> Names = new List<string>()
    {
        "Jonathan", "Joseph", "Jotoro", "Kyle", "Garret", "Steph",
        "Tim", "Puff Ball", "Markus", "Maximus", "Herculeus", "Jimmin",
        "Pluto", "Jacko", "Mario", "Luigi", "Robin", "Chrom",
        "Jeff", "Ilias", "Alex", "Ted", "Azgul", "Browning", "Dani",
        "Cloud", "Brackeys", "Kara", "Toan", "Tyler", "Sigmund", "Morath",
        "Everett"
    };
    public static NameGenerator instance = new NameGenerator();

    public NameGenerator()
    {
        ResetList();
    }

    List<string> NamesToUse = new List<string>();

    public string Get()
    {
        if (NamesToUse.Count < 1) { ResetList(); }

        int i = Random.Range(0, NamesToUse.Count);
        var name = NamesToUse[i];
        NamesToUse.Remove(name);

        return name;
    }

    void ResetList()
    {
        NamesToUse.Clear();
        foreach (var n in Names) NamesToUse.Add(n);
    }
}
