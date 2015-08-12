using System;

namespace CarpetFishing.GameTypes
{
    /// <summary>
    /// Data for classical carpet fishing
    /// </summary>
    public class FishingData : GameData
    {
        public override string Tag
        {
            get { return "f"; }
        }
        public override string Description
        {
            get { return "Classical carpet fishing"; }
        }
        public override string Title
        {
            get { return "Carpet Fishing"; }
        }
        public override string Start
        {
            get { return "Setting up fishing rod and starting to fish. Choose your grid and enjoy!"; }
        }
        public override string Time
        {
            get { return "Fish catching interval"; }
        }
        public override string Grid
        {
            get { return "Pond grid size"; }
        }
        public override string GetGrid
        {
            get { return "Choose the size of your pond/carpet grid size"; }
        }
        public override string GetTime
        {
            get { return "Choose the approximate time to wait between catching fishes"; }
        }
        public override string Progress
        {
            get { return "Fishing"; }
        }
        public override bool OmitHowTo
        {
            get { return false; }
        }

        Random rd = new Random();

        public override string GetResultString(int x, int y)
        {
            int f = rd.Next(Fishes.Length - 1);
            return string.Format("A fish ({0}) was seen at X:{1} Y:{2}", Fishes[f], x, y);
        }

        private static string[] Fishes = new string[] { "Basa", "Flounder", "Hake",
            "Scup", "Smelt", "Rainbow Trout", "Hardshell Clam", "Cuttlefish", "Coho Salmon", "Skate",
            "Anchovy", "Herring", "Lingcod", "Moi", "Orange Roughy", "Atlantic Ocean Perch",
            "Lake Victoria Perch", "Yellow Perch", "Rockfish", "Pink Salmon", "Blue Marlin",
            "Sea Urchin", "Atlantic Mackerel", "Black Sea Bass", "European Sea Bass",
            "Hybrid Striped Bass", "Bream", "Cod", "Drum", "Haddock", "Hoki", "Alaska Pollock",
            "Snapper", "Tilapia", "Turbot", "Walleye", "Lake Whitefish", "Wolffish", "Cockle",
            "Crayfish", "Bay Scallop", "Chinese White Shrimp", "Sablefish", "Atlantic Salmon",
            "Chinook Salmon", "Chum Salmon", "American Shad", "Arctic Char", "Carp", "Catfish",
            "Dory", "Grouper", "Halibut", "Monkfish", "Escolar", "Croaker", "Eel",
            "Pompano", "Dover Sole", "Sturgeon", "Tilefish", "Wahoo", "Yellowtail", "Abalone",
            "Conch", "Octopus", "Kingklip", "Mahimahi", "Opah", "Mako Shark", "Swordfish",
            "Squid", "Barramundi", "Cusk", "Dogfish", "Albacore Tuna", "Yellowfin Tuna", "Sea Scallop",
            "Barracuda", "Chilean Sea Bass", "Cobia", "Mullet", "Sockeye Salmon", "Bluefin Tuna" };
    }
}
