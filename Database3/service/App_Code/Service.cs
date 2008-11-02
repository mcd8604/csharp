using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Axel.Database;

[WebService(Namespace = "http://www.cs.rit.edu/~ats/db")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service: System.Web.Services.WebService, IModel {
  /// <summary> self-initialize: allocate a <c>LocalDB</c> as <c>Application["db"]</c>. </summary>
  public Service () {
    Application.Lock();
    try {
      if (Application["db"] == null) Application["db"] = new LocalDB();
    } finally {
      Application.UnLock();
    }
  }

  public int Count {
    [WebMethod]
    get { return ((IModel)Application["db"]).Count; }
  }

  [WebMethod]
  public string[][] Search (string[] keys) {
    return ((IModel)Application["db"]).Search(keys);
  }

  [WebMethod]
  public bool Enter (string[] tuple) {
    return ((IModel)Application["db"]).Enter(tuple);
  }

  [WebMethod]
  public bool Remove (string[] keys) {
    return ((IModel)Application["db"]).Remove(keys);
  }
}