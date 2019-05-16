using System;
using System.Collections.Generic;

namespace Toy.AST {

    public class Module {

        public IEnumerable<Function> Functions { get; private set; }

        public Module(IEnumerable<Function> functions) {

            this.Functions = functions;
        }
    }
}