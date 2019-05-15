using System;
using System.IO;

namespace Toy {

    public class LexerBuffer : Lexer {

        private TextReader Reader { get; set; }

        public LexerBuffer(String filename)
            : base(filename) {

            this.Reader = File.OpenText(filename);
        }

        /// PUBLIC 

        public String ReadNextLine() {

            return this.Reader.ReadLine();
        }
    }
}