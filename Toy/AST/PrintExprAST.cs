using System;

namespace Toy.AST {

    public class PrintExpression : Node {

        public String Argument { get; private set; }

        public PrintExpression(Location location, String argument)
            : base(Kind.PrintExpression, location) {

            this.Argument = argument;
        }
    }
}