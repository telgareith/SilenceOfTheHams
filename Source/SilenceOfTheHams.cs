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
        //Settings = GetSettings<Settings>();
    }

    public static Modbase Instance
    {
        get;
        private set;
    }

    //public override void DoSettingsWindowContents(Rect inRect) => Settings.DoSettingsWindowContents(inRect);
    public override string SettingsCategory()
    {
        return "SilenceOfTheHams";
    }
    //public static Settings Settings { get; private set; }
}
