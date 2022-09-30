using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
public class MasteryMod : MelonMod
{
    public override void OnApplicationStart()
    {
        base.OnApplicationStart();
    }
    public static bool DifficultyIsMastery()
    {
        return Save.GetCurrentSave().CombatDificulty == 5;
    }
}
