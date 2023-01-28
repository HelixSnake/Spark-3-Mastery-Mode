using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace FloatChargeShot
{
    public class FloatChargeShotMod : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
        }
        public static bool DifficultyIsNotMastery()
        {
            return Save.GetCurrentSave().CombatDificulty != 5;
        }
    }
}