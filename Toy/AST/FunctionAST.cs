using System;
using System.Collections.Generic;

namespace Toy.AST {

    public class Function {

        public Prototype Prototype { get; private set; }
        public IEnumerable<Node> Body { get; private set; }

        public Function(Prototype prototype, IEnumerable<Node> body) {

            this.Prototype = prototype;
            this.Body = body;
        }
    }
}