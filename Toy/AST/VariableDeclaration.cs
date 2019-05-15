using System;

namespace Toy.AST {

    public class VariableDeclaration : Node {

        public String Name { get; private set; }
        public VarType VarType { get; private set; }
        public Node InitialValue { get; private set; }

        public VariableDeclaration(Location location, String name, VarType type, Node initialValue)
            : base(Kind.VariableDeclaration, location) {

            this.Name = name;
            this.VarType = type;
            this.InitialValue = initialValue;
        }
    }
}