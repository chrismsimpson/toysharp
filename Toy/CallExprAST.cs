using System;
using System.Collections.Generic;

namespace Toy {

    public class CallExprAST : ExprAST {

        public String Callee { get; private set; }
        public IEnumerable<ExprAST> Arguments { get; private set; }

        public CallExprAST(Location location, String callee, IEnumerable<ExprAST> arguments)
            : base(ExprASTKind.Call, location) {

            this.Callee = callee;
            this.Arguments = arguments;
        }
    }
}