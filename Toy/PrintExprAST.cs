using System;

namespace Toy {

    public class PrintExprAST : ExprAST {

        public String Argument { get; private set; }

        public PrintExprAST(Location location, String argument)
            : base(ExprASTKind.Print, location) {

            this.Argument = argument;
        }
    }
}