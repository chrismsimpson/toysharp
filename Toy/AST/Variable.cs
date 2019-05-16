using System;

namespace Toy.AST {

    public class Variable : Node {

        public String Name { get; private set; }

        public Variable(Location location, String name)
            : base(Kind.Variable, location) {

            this.Name = name;
        }
    }
}