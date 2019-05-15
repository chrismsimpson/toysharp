using System;

namespace Toy {

    public class VarDeclExprAST : ExprAST {

        public String Name { get; private set; }
        public VarType VarType { get; private set; }
        public ExprAST InitialValue { get; private set; }

        public VarDeclExprAST(Location location, String name, VarType type, ExprAST initialValue)
            : base(ExprASTKind.VarDecl, location) {

            this.Name = name;
            this.VarType = type;
            this.InitialValue = initialValue;
        }
    }
}