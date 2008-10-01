using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerryAndMike.Sudoku {
    class ClearableCell : Cell {
        /// <summary>
        /// Create a new clearable cell with all candidates valid.
        /// </summary>
        public ClearableCell( int dimension ) : base( dimension ) { }

        /// <summary>
        /// Clears a digit if it has been set, resets all candidates to true
        /// </summary>
        public void Clear() {
            if ( this.digit.HasValue ) {
                this.digit = null;
                candidates.SetAll( true );
            }
        }

        /// <summary>
        /// Adds <code>digit</code> to the candidate set.
        /// </summary>
        /// <param name="digit">The digit to add, [1,dimension]</param>
        public void AddCandidate( int digit ) {
            candidates.Set( digit - 1, true );
        }

    }
}
