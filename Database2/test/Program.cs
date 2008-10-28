using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Axel.Database {
  /// <summary> tests a flat database with string arrays. </summary>
  static class Program {
    /// <summary> process using console i/o. </summary>
    public static void Main () {
      Test(new DB<string>(), Console.In, Console.Out);
    }

    /// <summary>
    ///   process set, get, and remove commands until end of file.
    ///   This cannot make or deal with null in a tuple.
    /// </summary>
    public static void Test (DB<string> db, TextReader input, TextWriter output) {
      // spaces separate command words
      Regex spaces = new Regex(@"\s+");

      // until end of file
      string line;
      while ((line = input.ReadLine()) != null)
        try {
          // separate into words, if any
          string[] words = spaces.Split(line.Trim());
          int keyPos, valPos;
          Regex key;

          // dispatch on first letter of first word
          switch (words[0][0]) {

          // add word...
          case 'a':
            string[] tail = new string[words.Length - 1];
            Array.Copy(words, 1, tail, 0, tail.Length);
            output.WriteLine("\t" + db.Add(tuple => {
              if (tail.Length != tuple.Length)
                return false;
              for (int n = 0; n < tail.Length; ++n)
                if (tail[n] == null && tuple[n] != null
                    || tail != null && !tail[n].Equals(tuple[n]))
                  return false;
              return true;
            }, tail));
            continue;

          // extract value-position key-position pattern // select valPos where pattern ~ keyPos
          case 'e':
            valPos = int.Parse(words[1]); // non-negative?
            keyPos = int.Parse(words[2]); // non-negative?
            key = new Regex(words[3]);
            object[] values = db.Extract(
              tuple =>
                tuple.Length > keyPos
                  && tuple.Length > valPos
                  && tuple[keyPos] != null // cannot match null
                  && key.IsMatch(tuple[keyPos]),
              tuple => tuple[valPos]);

            foreach (object value in values)
              output.WriteLine("\t" + value);
            continue;

          // delete key-position pattern
          case 'd':
            keyPos = int.Parse(words[1]);
            key = new Regex(words[2]);
            output.WriteLine("\t" + db.Remove(
              tuple =>
                tuple.Length > keyPos
                  && tuple[keyPos] != null // cannot match null
                  && key.IsMatch(tuple[keyPos])
            ));
            continue;
          }
        } catch {
          output.WriteLine("add word...\n"
            + "extract value-position key-position pattern\n"
            + "delete key-position pattern\n");
        }
    }
  }
}
