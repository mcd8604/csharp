using System;
using System.Collections.Generic;
using System.Text;
using Axel.Database;
using System.Threading;

namespace TerryAndMike.Database
{
    /// <summary> Identifies databases </summary>
    public enum Databases { ONE, TWO };
    /// <summary> called to handle toggle event. </summary>
    public delegate void ToggleHandler(Databases database);
    /// <summary>Non-blocking controller for two togglable databases</summary>
    public class ToggleController : Controller
    {
        /// <summary> represents the first database. </summary>
        protected readonly IModel db1;
        /// <summary> represents the second database. </summary>
        protected readonly IModel db2;
        /// <summary> connect to first database and view, post current count. </summary>
        /// <param name="db1"> database 1. </param>
        /// <param name="db2"> database 2. </param>
        /// <param name="bg"> for sequential background execution. </param>
        /// <param name="enable"> controls user interaction in view. </param>
        /// <param name="io"> access to current/size/search/enter/remove fields (can be null). </param>
        public ToggleController(IModel db1, IModel db2, WorkQueue bg, Enable enable, params IAccess[] io)
            : base(db1, bg)
        {
            this.db1 = db1; this.db2 = db2;
            Connect(enable, io);
        }
        /// <summary> handle toggle request </summary>
        public void doToggle(object sender, EventArgs e)
        {
            // disable user interface
            enable(false);
            // set up database toggle
            bg.Enqueue(new ThreadStart[] {
            delegate { // db toggle
                if(db1 != null && db2 != null) {
                    db = db == db1 ?  db2 : db1;
                }
            },
            delegate { // clean up after toggle is completed
                for (int n = 2; n < io.Length; ++n)
                  if (io[n] != null) io[n].Text = "";
                doSize(null, null);
                if(DatabaseToggled != null) DatabaseToggled(db == db1 ? Databases.ONE : Databases.TWO );
            }});
        }
        /// <summary>Triggered when the database is toggled</summary>
        public event ToggleHandler DatabaseToggled;
    }
}
