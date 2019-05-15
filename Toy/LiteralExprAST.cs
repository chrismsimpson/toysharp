using System;
using System.Collections.Generic;

namespace Toy {

    public class LiteralExprAST : ExprAST {

        public IEnumerable<ExprAST> Values { get; private set; }
        public IEnumerable<long> Dims { get; private set; }

        public LiteralExprAST(Location location, IEnumerable<ExprAST> values, IEnumerable<long> dims)
            : base(ExprASTKind.Literal, location) {

            this.Values = values;
            this.Dims = dims;
        }
    }
}