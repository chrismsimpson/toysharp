using System;

namespace MLIR {

    public class Parser {

        private Listener Listener { get; set; }

        public Parser(Listener listener) {

            this.Listener = listener;
        }
    }
}