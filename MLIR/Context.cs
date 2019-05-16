using System;

namespace MLIR {

    public class Context {

        public Kind Kind { get; set; }

        public Direction Direction { get; set; }

        public object Argument { get; set; }

        public Context(Kind kind, Direction direction, object argument) {
            
            this.Kind = kind;
            this.Direction = direction;
            this.Argument = argument;
        }

        public void RegisterDialect() {

        }
    }
}