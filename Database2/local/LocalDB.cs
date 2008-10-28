using System;
using System.Collections.Generic;
using System.Text;

namespace Axel.Database {
  /// <summary> wraps and synchronizes <c>DB</c> as <c>IModel</c>. </summary>
  public class LocalDB: DB<string>, IModel {
    /// <summary> number of tuples, delegate to <c>list</c>. </summary>
    public int Count { get { return list.Count; } }
    /// <returns> what was found. </returns>>
    public string[][] Search (string[] keys) {
      DB<string>.Match match = Matcher(keys);
      string[][] result = new string[keys.Length][];

      lock (this)
        for (int n = 0; n < keys.Length; ++n) {
          object[] report = Extract(match, tuple => tuple[n]);
          result[n] = new string[report.Length];
          report.CopyTo(result[n], 0);
        }
      return result;
    }
    /// <returns> true if something was added (and not replaced). </returns>
    public bool Enter (string[] tuple) {
      Simplify(tuple);
      foreach (string t in tuple)
        if (t == null) return false;
      lock(this)
        return !Add(Matcher(tuple), tuple);
    }
    /// <returns> true if something was removed. </returns>
    public bool Remove (string[] keys) {
      lock(this)
        return Remove(Matcher(keys)) > 0;
    }
    /// <summary> turn invisible elements into null. </summary>
    protected static void Simplify (string[] tuple) {
      for (int n = 0; n < tuple.Length; ++n)
        if (tuple[n] != null
            && (tuple[n] = tuple[n].Trim()).Length == 0)
          tuple[n] = null;
    }
    /// <summary> match non-empty keys exactly. </summary>
    protected static DB<string>.Match Matcher (string[] keys) {
      Simplify(keys);
      return tuple => {
        for (int n = 0; n < keys.Length; ++n)
          if (tuple.Length <= n
              || keys[n] != null && keys[n] != tuple[n])
            return false;
        return true;
      };
    }
  }
}

