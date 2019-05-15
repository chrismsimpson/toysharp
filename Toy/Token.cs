using System;

namespace Toy {

    public enum Token {
        
        Semicolon = ';',
        ParenthesOpen = '(',
        ParenthesClose = ')',
        BracketOpen = '{',
        BracketClose = '}',
        SquareBracketOpen = '[',
        SquareBracketClose = ']',

        EndOfFile = -1,

        // commands
        Return = -2,
        Var = -3,
        Def = -4,

        // primary
        Identifier = -5,
        Number = -6
    }
}