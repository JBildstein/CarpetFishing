using System;

namespace CarpetFishing.GameTypes
{
    /// <summary>
    /// Data for a data collection tool cover
    /// </summary>
    public class DataCollectionData : GameData
    {
        public override string Tag
        {
            get { return "d"; }
        }
        public override string Description
        {
            get { return "Data collection tool"; }
        }
        public override string Title
        {
            get { return "the Data Collecting Tool"; }
        }
        public override string Start
        {
            get { return "Data collection tool setup:"; }
        }
        public override string Time
        {
            get { return "Report interval"; }
        }
        public override string Grid
        {
            get { return "Collection area"; }
        }
        public override string GetGrid
        {
            get { return "Choose the size of your collecting area"; }
        }
        public override string GetTime
        {
            get { return "Choose the approximate time for reports"; }
        }
        public override string Progress
        {
            get { return "Collecting data"; }
        }
        public override bool OmitHowTo
        {
            get { return true; }
        }

        Random rd = new Random();
        static string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public override string GetResultString(int x, int y)
        {
            int length = rd.Next(16, 24);
            char[] outChars = new char[length];
            for (int i = 0; i < length; i++) { outChars[i] = chars[rd.Next(chars.Length - 1)]; }
            string data = new string(outChars);

            return string.Format("Data update: {0}{1}Data coordinates: X:{2} Y:{3}", data, Environment.NewLine, x, y);
        }
    }
}
