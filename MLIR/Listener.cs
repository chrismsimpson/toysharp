using System;
using System.Collections.Generic;
using System.Linq;

namespace MLIR {

    public enum Direction {

        Enter,
        Exit
    }

    public class Listener {

        public Stack<Kind> DescentStack { get; private set; }
        public Stack<Context> AscentStack { get; private set; }

        public Listener() {

            this.DescentStack = new Stack<Kind>();
            this.AscentStack = new Stack<Context>();
        }

        public void Enter(Kind kind) {

            this.DescentStack.Push(kind);
        }

        public void Exit(Node argument) {

            var kind = this.DescentStack.Pop();

            this.AscentStack.Push(new Context(kind, Direction.Exit, argument));
            this.AscentStack.Push(new Context(kind, Direction.Enter, argument));
        }

        public void Listen() {

            while (this.AscentStack.Count() != 0) {

                var context = this.AscentStack.Pop();

                switch (context.Kind) {

                case Kind.Function:

                    if (context.Direction == Direction.Exit) {

                    }

                    break;

                case Kind.Region:

                    if (context.Direction == Direction.Exit) {
                        
                    }

                    break;
                
                case Kind.Operation:

                    if (context.Direction == Direction.Exit) {
                        
                    }

                    break;

                case Kind.Block:

                    if (context.Direction == Direction.Exit) {
                        
                    }

                    break;
                }
            }
        }
    }
}