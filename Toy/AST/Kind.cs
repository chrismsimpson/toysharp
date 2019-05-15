using System;

namespace Toy.AST {

    public enum Kind {
        
        VariableDeclaration,
        ReturnExpression,
        Number,
        Literal,
        Variable,
        BinaryExpression,
        CallExpression,
        PrintExpression, // builtin
        IfStatement,
        ForStatement
    }
}