using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Axel.Database;

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

        Session["DBs"] = new IModel[] { new LocalDB(), new RemoteDB() };
        Session["activeDB"] = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //lbl_currentDB.Text = Session["DBs"];//   [0].GetType().Name;
    }


    protected void btn_toggleDB_Click(object sender, EventArgs e)
    {
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

    }
    protected void btn_enter_Click(object sender, EventArgs e)
    {

    }
    protected void btn_remove_Click(object sender, EventArgs e)
    {
        
    }
}
