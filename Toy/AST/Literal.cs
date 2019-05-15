using System;
using System.Collections.Generic;

namespace Toy.AST {

    public class Literal : Node {

        public IEnumerable<Node> Values { get; private set; }
        public IEnumerable<long> Dims { get; private set; }

        public Literal(Location location, IEnumerable<Node> values, IEnumerable<long> dims)
            : base(Kind.Literal, location) {

            this.Values = values;
            this.Dims = dims;
        }
    }
}