using System;
using System.Collections.Generic;

namespace Toy.AST {

    public class CallExpression : Node {

        public String Callee { get; private set; }
        public IEnumerable<Node> Arguments { get; private set; }

        public CallExpression(Location location, String callee, IEnumerable<Node> arguments)
            : base(Kind.CallExpression, location) {

            this.Callee = callee;
            this.Arguments = arguments;
        }
    }
}