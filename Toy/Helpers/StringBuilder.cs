using System;
using System.Text;

namespace Toy {

    public static partial class StringBuilderHelpers {

        public static StringBuilder Append(this StringBuilder builder, Token token) {

            var c = (Char) token;

            return builder.Append(c);
        }
    }
}