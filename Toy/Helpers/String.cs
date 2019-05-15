using System;
using System.Collections.Generic;
using System.Linq;

namespace Toy {

    public static partial class StringHelpers {

        public static bool IsEmpty(this String str) {

            return (str == null || str.Length == 0);
        }

        public static String DropFirst(this String str) {

            var sub = str.Skip(1).ToArray();

            return new String(sub);
        }
    }
}