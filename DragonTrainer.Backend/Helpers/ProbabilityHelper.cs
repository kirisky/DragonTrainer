namespace DragonTrainer.Backend.Helpers
{
    public class ProbabilityHelper
    {
       public static string SureThing { get; set; } = "Sure thing";
       public static string PieceOfCake { get; set; } = "Piece of cake";
       public static string WalkInThePark { get; set; } = "Walk in the park";
       public static string QuiteLikely { get; set; } = "Quite likely";
       public static string Hmmm { get; set; } = "Hmmm....";
       public static string RatherDetrimetal { get; set; } = "Rather detrimental";
       public static string Risky { get; set; } = "Risky";
       public static string Gamble { get; set; } = "Gamble";
       public static string PlayWithFire { get; set; } = "Playing with fire";
       public static string SuicideMission { get; set; } = "Suicide mission";

        // factors for Missions Difficulties
       public static int SureThingFactor { get; set; } = 0;
       public static int PieceOfCakeFactor { get; set; } = 0;
       public static int WalkInTheParkFactor { get; set; } = 0;
       public static int QuiteLikelyFactor { get; set; } = 20;
       public static int HmmmFactor { get; set; } = 40;
       public static int RatherDetrimetalFactor { get; set; } = 50;
       public static int RiskyFactor { get; set; } = 70;
       public static int GambleFactor { get; set; } = 80;
       public static int PlayWithFireFactor { get; set; } = 100;
       public static int SuicideMissionFactor { get; set; } = 150;
    }
}