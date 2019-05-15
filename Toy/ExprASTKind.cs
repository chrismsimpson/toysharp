using System;

namespace Toy {

    public enum ExprASTKind {
        
        VarDecl,
        Return,
        Num,
        Literal,
        Var,
        BinOp,
        Call,
        Print, // builtin
        If,
        For,
    }
}