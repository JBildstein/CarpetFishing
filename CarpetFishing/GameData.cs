using System;

namespace CarpetFishing
{
    /// <summary>
    /// Container for data to customize a carpet fishing game
    /// </summary>
    public abstract class GameData
    {
        /// <summary>
        /// Argument string. Usually one character.
        /// <para>Do NOT use "h", "n", "t" or "g"</para>
        /// </summary>
        public abstract string Tag { get; }
        /// <summary>
        /// Short description. Shown in the help
        /// </summary>
        public abstract string Description { get; }
        /// <summary>
        /// Title of this type. Shown in the header and help
        /// </summary>
        public abstract string Title { get; }
        /// <summary>
        /// The message that shows when the program starts to run
        /// <para>Output if set to "Foo":</para>
        /// <para>Foo</para>
        /// <para>{<see cref="Time"/>}: 5m  -  {<see cref="Grid"/>}: X:5 Y:5</para>
        /// </summary>
        public abstract string Start { get; }
        /// <summary>
        /// The description of the wait time after the <see cref="Start"/> message
        /// <para>Output if set to "Foo":</para>
        /// <para>{<see cref="Start"/>}</para>
        /// <para>Foo: 5m  -  {<see cref="Grid"/>}: X:5 Y:5</para>
        /// </summary>
        public abstract string Time { get; }
        /// <summary>
        /// The description of the grid size after the <see cref="Start"/> message
        /// <para>Output if set to "Foo":</para>
        /// <para>{<see cref="Start"/>}</para>
        /// <para>{<see cref="Time"/>}: 5m  -  Foo: X:5 Y:5</para>
        /// </summary>
        public abstract string Grid { get; }
        /// <summary>
        /// The message to write for getting the grid size
        /// <para>Output if set to "Foo":</para>
        /// <para>Foo (x,y):</para>
        /// </summary>
        public abstract string GetGrid { get; }
        /// <summary>
        /// The message to write for getting the wait time
        /// <para>Output if set to "Foo":</para>
        /// <para>Foo (in minutes):</para>
        /// </summary>
        /// </summary>
        public abstract string GetTime { get; }
        /// <summary>
        /// The string that is shown as progress. It is followed by a spinner.
        /// </summary>
        public abstract string Progress { get; }
        /// <summary>
        /// Set to true to hide the "How To" part in the help
        /// </summary>
        public abstract bool OmitHowTo { get; }
        /// <summary>
        /// The help text of this data type. Can be null.
        /// </summary>
        public virtual string Help
        {
            get { return null; }
        }

        /// <summary>
        /// If overridden, initiates the data type with some user given arguments
        /// </summary>
        /// <param name="args">The arguments passed by the user</param>
        public virtual void Init(string[] args)
        {
            /* No extra arguments to handle by default */
        }
        /// <summary>
        /// Get the string that is shown for a result.
        /// Make sure you include the coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the result</param>
        /// <param name="y">The Y coordinate of the result</param>
        /// <returns>The string to show</returns>
        public abstract string GetResultString(int x, int y);
        /// <summary>
        /// If not overridden, uses Console.Beep() to notify of a result.
        /// <para>This is only called if the -b argument is set</para>
        /// </summary>
        public virtual void Notification()
        {
            Console.Beep(600, 400);
        }

        protected GameData()
        {
            if (Tag == null) throw new ArgumentNullException("Tag must not be null");
            if (Tag == "h") throw new FormatException("Tag 'h' is reserved for help");
            if (Tag == "b") throw new FormatException("Tag 'b' is reserved for beep");
            if (Tag == "t") throw new FormatException("Tag 't' is reserved for time");
            if (Tag == "g") throw new FormatException("Tag 'g' is reserved for grid");
        }
    }
}
