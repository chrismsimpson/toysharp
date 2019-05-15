using System;

namespace Toy.AST {

    public class Node {

        public Kind Kind { get; private set; }

        public Location Location { get; private set; }

        public Node(Kind kind, Location location) {

            this.Kind = kind;
            this.Location = location;
        }
    }
}