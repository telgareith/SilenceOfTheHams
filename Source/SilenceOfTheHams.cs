global using System;
global using System.Collections.Generic;
global using UnityEngine;
global using Verse;
global using Verse.AI;
global using RimWorld;

namespace SilenceOfTheHams;

public class Modbase : Mod
{
    public Modbase(ModContentPack content) : base(content)
    {
        Instance = this;
    }

    public static Modbase Instance
    {
        get;
        private set;
    }
}
