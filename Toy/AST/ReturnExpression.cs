using System;

namespace Toy.AST {

    public class ReturnExpression : Node {

        public Node Expression { get; private set; }

        public ReturnExpression(Location location, Node expression)
            : base(Kind.ReturnExpression, location) {

            this.Expression = expression;
        }
    }
}