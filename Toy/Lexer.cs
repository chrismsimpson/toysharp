using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toy {
    
    public abstract class Lexer {
        
        public Token CurrentToken { get; private set; }

        public Location LastLocation { get; private set; }

        public int CurrentLineNumber { get; private set; }
        public int CurrentColumn { get; private set; }

        public String Identifier { get; private set; }
        public Double Value { get; private set; }
        
        private String Filename;

        private Token LastToken;

        private String CurrentLineBuffer = "\n";

        public Lexer(String filename) {

            this.Filename = filename;
        }

        /// Public

        public virtual String ReadNextLine()
        {
            throw new NotImplementedException();
        }

        public Token GetNextToken() {

            this.CurrentToken = this.getToken();

            return this.CurrentToken;
        }

        public void Consume(Token token) {

            if (token != this.CurrentToken) {

                throw new Exception("Token mismatch");
            }

            this.GetNextToken();
        }

        /// Private

        private Token getTokenFromNextChar() {

            // The current line buffer should not be empty unless it is the end of file.

            if (this.CurrentLineBuffer.IsEmpty())
            {
                return Token.EndOfFile;
            }

            ++this.CurrentColumn;
            
            var nextChar = this.CurrentLineBuffer.First();

            this.CurrentLineBuffer = this.CurrentLineBuffer.DropFirst();

            if (this.CurrentLineBuffer.IsEmpty()) {

                this.CurrentLineBuffer = this.ReadNextLine();
            }

            if (nextChar == '\n') {
                
                ++this.CurrentLineNumber;

                this.CurrentColumn = 0;
            }

            return (Token) nextChar;
        }

        private Token getToken() {

            // Skip any whitespace.

            while (this.LastToken.IsSpace()) {

                this.LastToken = this.getTokenFromNextChar();
            }

            this.LastLocation.Line = this.CurrentLineNumber;
            this.LastLocation.Column = this.CurrentColumn;

            if (this.LastToken.IsAlpha()) {

                this.Identifier = this.LastToken.ToRawString();

                while ((this.LastToken = this.getTokenFromNextChar()).IsAlphaNumeric() || this.LastToken == Token.Underscore)
                {
                    this.Identifier += this.LastToken.ToRawString();
                }

                if (this.Identifier == "return") {

                    return Token.Return;
                }
                else if (this.Identifier == "def") {

                    return Token.Def;
                }

                return Token.Identifier;
            }

            if (this.LastToken.IsDigit() || this.LastToken == Token.Period) {

                var numberString = "";

                do {

                    numberString += this.LastToken.ToRawString();

                    this.LastToken = this.getTokenFromNextChar();
                }
                while (this.LastToken.IsDigit() || this.LastToken == Token.Period);

                Double v;

                if (Double.TryParse(numberString, out v)) {

                    this.Value = v;
                }

                return Token.Number;
            }

            if (this.LastToken == Token.Hash) {

                do {

                    this.LastToken = this.getTokenFromNextChar();
                }
                while (this.LastToken != Token.EndOfFile && this.LastToken != Token.NewLine && this.LastToken != Token.CR);

                if (this.LastToken != Token.EndOfFile) {

                    return this.getToken();
                }
            }

            if (this.LastToken == Token.EndOfFile) {

                return Token.EndOfFile;
            }

            var t = this.LastToken;
            
            this.LastToken = this.getTokenFromNextChar();

            return t;
        }
    }
}