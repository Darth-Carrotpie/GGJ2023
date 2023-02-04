using System.Collections.Generic;
using System.Linq;

//Naming Convention:
//1. Event names must be unique (thus add prefix of their master class for easier debugging).
//2. Event names must represent of what they do. If after what happened, then what they 'DID' (past tense!)
//3. Master class names which holds event names must define nature of events which they hold.
//3.1. If events don't have a meaningfull origin, place them in their master class by what is their intended impact / target (i.e. UI);

public class EventName {
    public class SampleEventParentFolder {
        public static string SpawnSampleObject() { return "Create_Sample_OBJ"; } //return string has to be unique!
        public static List<string> Get() { return new List<string> { SpawnSampleObject() }; }
    }
    public class UI {
        public static string ShowScoreScreen() { return "UI_ShowScoreScreen"; }
        public static string ScoreScreenShown() { return "UI_ScoreScreenShown"; }
        public static string ModifyLeaves() { return "UI_ModifyLeaves"; }
        public static string ModifyPotatoes() { return "UI_ModifyPotatoes"; }
        public static List<string> Get() { return new List<string> { ShowScoreScreen(), ScoreScreenShown(), ModifyLeaves(), ModifyPotatoes() }; }
    }
    public class Editor {
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { None() }; }
    }
    public class Input {
        public static string TapRegular() { return "Input_TapRegular"; }
        public static string TapUpgrade() { return "Input_TapUpgrade"; }

        public static string StartLevel() { return "Input_StartLevel"; }
        public static string None() { return null; }
        public static List<string> Get() {
            return new List<string> {
                TapRegular(),
                TapUpgrade(),
                StartLevel(),
                None(),
            };
        }
    }
    public class Health {
        public static string HealTree() { return "Health_HealTree"; }
        public static string CurrentPercent() { return "Health_CurrentPercent"; }
        public static string HealthEmpty() { return "Health_IsEmpty"; }
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { HealTree(), CurrentPercent(), HealthEmpty(), None() }; }
    }
    public class Hostiles {
        public static string DamageTrunk() { return "Hostiles_DamageTrunk"; }
        public static string DamageRoots() { return "Hostiles_DamageRoots"; }
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { DamageTrunk(), DamageRoots(), None() }; }
    }
    public class Economy {
        public static string ModifyLeaves() { return "Econ_ModifyLeaves"; }
        public static string ModifyBranches() { return "Econ_ModifyBranches"; }
        public static string ModifyHearts() { return "Econ_ModifyHearts"; }
        public static string ModifyDroplets() { return "Econ_ModifyDroplets"; }

        public static string ConsumeWater() { return "Econ_ConsumeWater"; }
        public static string ConsumeSunshine() { return "Econ_ConsumeSunshine"; }
        public static string UpgradeTrunk() { return "Econ_UpgradeTrunk"; }
        public static string UpgradeRoots() { return "Econ_UpgradeRoots"; }
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { ModifyLeaves(), ModifyBranches(), ModifyHearts(), ModifyDroplets(), UpgradeTrunk(), UpgradeRoots(), ConsumeWater(), ConsumeSunshine(), None() }; }
    }
    public class Environment {
        public static string NextCycle() { return "Environment_NextCycle"; }
        public static string StartChurchDestruction() { return "Environment_StartChurchDestruction"; }
        public static List<string> Get() { return new List<string> { NextCycle(), StartChurchDestruction(), }; }
    }
    public class System {
        public static string GameEnd() { return "System_GameEnd"; }
        public static string PlayerDeath() { return "System_PlayerDeath"; }
        public static string StartGame() { return "System_StartGame"; }
        public static string TurtleCreated() { return "System_TurtleCreated"; }
        public static string TurtleDestroyed() { return "System_TurtleDestroyed"; }
        public static List<string> Get() { return new List<string> { GameEnd(), PlayerDeath(), StartGame(), TurtleCreated(), TurtleDestroyed() }; }
    }
    public class AI {
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { None() }; }
    }

    public static List<string> Get() {
        return new List<string> {}.Concat(UI.Get())
            .Concat(Hostiles.Get())
            .Concat(Health.Get())
            .Concat(Economy.Get())
            .Concat(Environment.Get())
            .Concat(System.Get())
            .Concat(Editor.Get())
            .Concat(Input.Get())
            .Concat(AI.Get())
            .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }
}