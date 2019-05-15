using System;

namespace Toy {

    public class BinaryExprAST : ExprAST {

        public Char Op { get; private set; }
        public ExprAST LHS { get; private set; }
        public ExprAST RHS { get; private set; }

        public BinaryExprAST(Location location, Char op, ExprAST lhs, ExprAST rhs)
            : base(ExprASTKind.BinOp, location) {

            this.Op = op;
            this.LHS = lhs;
            this.RHS = rhs;
        }
    }
}