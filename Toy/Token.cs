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
        
        Space = ' ',

        Period = '.',

        Underscore = '_',

        LessThan = '<',
        GreaterThan = '<',
        Equals = '=',

        Hash = '#',
        NewLine = '\n',
        CR = '\r',
        Comma = ',',

        Plus = '+',
        Minus = '-',
        Asterisk = '*',
        Slash = '/',

        EndOfFile = -1,

        // commands
        Return = -2,
        Var = -3,
        Def = -4,

        // primary
        Identifier = -5,
        Number = -6
    }

    public static partial class TokenHelpers {

        public static bool IsSpace(this Token token) {

            return (token == Token.Space);
        }

        public static bool IsAlpha(this Token token) {

            var c = (char) token;

            return Char.IsLetter(c);
        }

        public static bool IsAlphaNumeric(this Token token) {

            var c = (char) token;

            return Char.IsLetterOrDigit(c);
        }

        public static String ToRawString(this Token token) {

            var c = (char) token;

            return new String(c, 1);
        }

        public static bool IsDigit(this Token token) {

            var c = (char) token;

            return Char.IsDigit(c);
        }

        public static bool IsAscii(this Token token) {

            var c = (char) token;

            return c < 128;
        }
    }
}