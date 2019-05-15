using System;
using System.Collections.Generic;

namespace Toy {

    public class PrototypeAST {

        public Location Location { get; private set; }
        public String Name { get; private set; }
        public IEnumerable<ExprAST> Arguments { get; private set; }

        public PrototypeAST(Location location, String name, IEnumerable<ExprAST> arguments) {

            this.Location = location;
            this.Name = name;
            this.Arguments = arguments;
        }
    }
}