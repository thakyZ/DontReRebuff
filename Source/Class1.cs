using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using RimWorld;

using Verse;

namespace DontReRebuff {
  public class Controller : Mod {
    public Controller(ModContentPack content) : base(content) {
      var harmony = new Harmony("xyz.nekogaming.nekoboinick.rimworld.mod.dontrebuff");
      harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
  }

  [HarmonyPatch(typeof(InteractionWorker_RomanceAttempt), "RandomSelectionWeight", null)]
  public static class InteractionWorker_RomanceAttempt_RandomSelectionWeight {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static bool Prefix(Pawn initiator, Pawn recipient, ref float __result) {
      if (initiator.needs.mood.thoughts.memories.NumMemoriesOfDef(ThoughtDefOf.RebuffedMyRomanceAttempt) > 0) {
        __result = 0f;
        return false;
      }
      return true;
    }
  }
}
