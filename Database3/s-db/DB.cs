using System;
using System.Collections.Generic;

namespace Axel.Database {
  /// <summary> implements a flat database with object arrays. </summary>
  public class DB<T> {
    /// <summary> delegate, contains the tuples. </summary>
    protected readonly List<T[]> list = new List<T[]>();

    /// <summary> selects a matching tuple. </summary>
    public delegate bool Match (T[] tuple);

    /// <summary> reports information from a matched tuple. </summary>
    public delegate object Report (T[] tuple);

    /// <summary> add (or replace) a tuple. </summary>
    /// <returns> true if a tuple with matching content is replaced. </returns>
    public bool Add (Match match, T[] entry) {
      // remove any equal tuples if necessary
      bool result = 0 < Remove(match);

      // add the new tuple
      list.Add(entry);
      return result;
    }

    /// <summary> report information from matched tuples. </summary>
    public object[] Extract (Match match, Report report) {
        List<T[]> all = new List<T[]>();
        list.ForEach(
        (Action<T[]>)delegate(T[] tuple) {
            if (match(tuple)) all.Add(tuple);
        });

      // construct report
      object[] result = new object[all.Count];
      for (int n = 0; n < result.Length; ++n)
        result[n] = report(all[n]);
      return result;
    }

    /// <summary> remove matching tuples. </summary>
    public int Remove (Match match) {
        List<T[]> matches = new List<T[]>();
        foreach (T[] tuple in list)
        {
            if (match(tuple))
            {
                matches.Add(tuple);
            }
        }
        foreach (T[] tuple in matches)
        {
            list.Remove(tuple);
        }
        return 0;
    }
  }
}
