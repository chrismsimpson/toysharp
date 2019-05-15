using System;
using System.Collections.Generic;

namespace Toy.AST {

    public class Prototype {

        public Location Location { get; private set; }
        public String Name { get; private set; }
        public IEnumerable<Node> Arguments { get; private set; }

        public Prototype(Location location, String name, IEnumerable<Node> arguments) {

            this.Location = location;
            this.Name = name;
            this.Arguments = arguments;
        }
    }
}