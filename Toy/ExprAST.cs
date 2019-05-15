using System;

namespace Toy {

    public class ExprAST {

        public ExprASTKind Kind { get; private set; }

        public Location Location { get; private set; }

        public ExprAST(ExprASTKind kind, Location location) {

            this.Kind = kind;
            this.Location = location;
        }
    }
}