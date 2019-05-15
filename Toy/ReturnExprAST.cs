using System;

namespace Toy {

    public class ReturnExprAST : ExprAST {

        public ExprAST Expression { get; private set; }

        public ReturnExprAST(Location location, ExprAST expression)
            : base(ExprASTKind.Return, location) {

            this.Expression = expression;
        }
    }
}