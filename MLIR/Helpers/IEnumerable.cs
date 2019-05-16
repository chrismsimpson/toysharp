using System;
using System.Collections.Generic;
using System.Linq;

namespace MLIR {

    public static partial class IEnumerableHelpers {

        public static bool IsEmpty<T>(this IEnumerable<T> e) {

            return e == null || e.Count() == 0;
        }
    }
}