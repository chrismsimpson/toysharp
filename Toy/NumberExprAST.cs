using System;

namespace Toy {

    public class NumberExprAST : ExprAST {

        public Double Value { get; private set; }

        public NumberExprAST(Location location, Double value)
            : base(ExprASTKind.Num, location) {

            this.Value = value;
        }
    }
}