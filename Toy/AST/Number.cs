using System;

namespace Toy.AST {

    public class Number : Node {

        public Double Value { get; private set; }

        public Number(Location location, Double value)
            : base(Kind.Number, location) {

            this.Value = value;
        }
    }
}