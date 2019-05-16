using System;

namespace MLIR {

    public abstract class Visitor {

        protected Visitor() {

        }

        public virtual Node Visit(Node node) {

            if (node == null) {

                return null;
            }

            return node.Accept(this);
        }

        protected internal virtual Node VisitExtension(Node node) {

            return node.VisitChildren(this);
        }

        protected internal virtual Node VisitFunction(Function node) {

            this.Visit(node.Body);

            return node;
        }

        protected internal virtual Node VisitRegion(Region node) {
            
            foreach (var block in node.Blocks) {

                this.Visit(block);
            }

            return node;
        }

        protected internal virtual Node VisitBlock(Block node) {

            return node;
        }

        protected internal virtual Node VisitOperation(Operation node) {

            return node;
        }
    }
}