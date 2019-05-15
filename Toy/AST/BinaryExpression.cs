using System;

namespace Toy.AST {

    public class BinaryExpression : Node {

        public Char Op { get; private set; }
        public Node LHS { get; private set; }
        public Node RHS { get; private set; }

        public BinaryExpression(Location location, Char op, Node lhs, Node rhs)
            : base(Kind.BinaryExpression, location) {

            this.Op = op;
            this.LHS = lhs;
            this.RHS = rhs;
        }
    }
}