using System;

namespace MLIR {
    
    public class Node {

        public Kind Kind { get; private set; }

        public Location Location { get; private set; }

        public Node(Kind kind, Location location) {

            this.Kind = kind;
            this.Location = location;
        }

        protected internal virtual Node VisitChildren(Visitor visitor)
        {
            return visitor.Visit(this);
        }

        protected internal virtual Node Accept(Visitor visitor)
        {
            return visitor.VisitExtension(this);
        }
    }
}