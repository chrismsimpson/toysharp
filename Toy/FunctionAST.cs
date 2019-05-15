using System;
using System.Collections.Generic;

namespace Toy {

    public class FunctionAST {

        public PrototypeAST Prototype { get; private set; }
        public IEnumerable<ExprAST> Body { get; private set; }

        public FunctionAST(PrototypeAST prototype, IEnumerable<ExprAST> body) {

            this.Prototype = prototype;
            this.Body = body;
        }
    }
}