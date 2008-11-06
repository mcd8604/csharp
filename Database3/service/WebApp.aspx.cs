using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Axel.Database;
using System.Text;

public partial class WebApp : System.Web.UI.Page
{

    protected void Page_Init(object sender, EventArgs eventArgs)
    {
        /*new Switcher(
              new Enable(isEnabled =>
              {
                  btn_toggleDB.Enabled = btn_search.Enabled = btn_enter.Enabled = btn_remove.Enabled = isEnabled;
              }),
              new IAccess[]{
                  new Access(() => lbl_currentDB.Text, s => { lbl_currentDB.Text = s; }),
                  new Access(() => lbl_recordCount.Text, s => { lbl_recordCount.Text = s; }),
                  new Access(() => txt_name.Text, s => { txt_name.Text = s; }),
                  new Access(() => txt_phone.Text, s => { txt_phone.Text = s; }),
                  new Access(() => txt_room.Text, s => { txt_room.Text = s; })},
                  new SetClick[]{
              new SetClick((EventHandler e) => { btn_toggleDB.Click += new EventHandler(e); }),
              new SetClick((EventHandler e) => { btn_search.Click += new EventHandler(e); }),
              new SetClick((EventHandler e) => { btn_enter.Click += new EventHandler(e); }),
              new SetClick((EventHandler e) => { btn_remove.Click += new EventHandler(e); }),
                  },
              new WorkQueue(),
              "local", new LocalDB(), "remote", new RemoteDB());*/

        if ( Application["activeDB"] == null )
            Application["activeDB"] = new LocalDB();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_currentDB.Text = Application["activeDB"].GetType().Name;
    }


    protected void btn_toggleDB_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("oneDbWarn", "<script>alert('Only one database choice exists!')</script>");
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        // Search the model on keys

        string[] keys = GetTuple();
        string[][] tuples = ((IModel)Application["activeDB"]).Search(keys);

        // Display the array elements

        StringBuilder nameBuilder = new StringBuilder();
        StringBuilder phoneBuilder = new StringBuilder();
        StringBuilder roomBuilder = new StringBuilder();

        //all tuples of length 3
        if (tuples.Length != 3) return;

        for (int i = 0; i < tuples[0].Length; i++)
        {
            //don't append final newline
            if (i + 1 == tuples[0].Length)
            {
                nameBuilder.Append(tuples[0][i]);
                phoneBuilder.Append(tuples[1][i]);
                roomBuilder.Append(tuples[2][i]);
            }
            else
            {
                nameBuilder.AppendLine(tuples[0][i]);
                phoneBuilder.AppendLine(tuples[1][i]);
                roomBuilder.AppendLine(tuples[2][i]);
            }
        }

        txt_name.Text = nameBuilder.ToString();
        txt_phone.Text = phoneBuilder.ToString();
        txt_room.Text = roomBuilder.ToString();
    }
    protected void btn_enter_Click(object sender, EventArgs e)
    {
        string[] tuple = GetTuple();
        ((IModel)Application["activeDB"]).Enter(tuple);
        lbl_recordCount.Text = ((IModel)Application["activeDB"]).Count.ToString();
    }
    protected void btn_remove_Click(object sender, EventArgs e)
    {
        string[] keys = GetTuple();
        ((IModel)Application["activeDB"]).Remove(keys);
        lbl_recordCount.Text = ((IModel)Application["activeDB"]).Count.ToString();

        //clear output text boxes
        txt_name.Text = string.Empty;
        txt_phone.Text = string.Empty;
        txt_room.Text = string.Empty;
    }

    /// <summary>
    /// Form 3-tuple from the first line of each of the textboxs, replacing empty strings with null
    /// </summary>
    /// <returns>String array (tuple) with null representing empty textboxes</returns>
    private string[] GetTuple()
    {
        return new string[] { txt_name.Text.Length > 0 ? 
                                    txt_name.Text.IndexOfAny( new char[] {'\r', '\n'} ) >= 0 ? txt_name.Text.Substring(0, txt_name.Text.IndexOfAny(new char[] {'\r','\n'})) : txt_name.Text
                                : null, 
                            txt_phone.Text.Length > 0 ? 
                                txt_phone.Text.IndexOfAny( new char[] {'\r', '\n'} ) >= 0 ? txt_phone.Text.Substring(0, txt_phone.Text.IndexOfAny(new char[] {'\r','\n'})) : txt_phone.Text
                            : null, 
                            txt_room.Text.Length > 0 ? 
                                txt_room.Text.IndexOfAny( new char[] {'\r', '\n'} ) >= 0 ? txt_room.Text.Substring(0, txt_room.Text.IndexOfAny(new char[] {'\r','\n'})) : txt_room.Text 
                            : null
        };


    }

    /// <summary>
    /// Returns the first line of a text box or null if no such line exists
    /// </summary>
    /// <param name="tb">The textbox to query</param>
    /// <returns>First line of textbox or null</returns>
/*    private string GetTextboxFirstLine( TextBox tb ) {
        if ( tb.Text.Length <= 0 ) {
            return null;
        }
        else {
            int newlineLoc = tb.Text.IndexOfAny(new char[]{'\r','\n'});
            if ( newlineLoc <= 0 )
                return tb.Text;

        }*/
}
