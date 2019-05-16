using System;

namespace Toy.AST {

    public class PrintExpression : Node {

        public Node Argument { get; private set; }

        public PrintExpression(Location location, Node argument)
            : base(Kind.PrintExpression, location) {

            this.Argument = argument;
        }
    }
}