using System;

namespace MLIR {

    public class Attribute {
        
        public AttributeKind Kind { get; set; }

        public Attribute(AttributeKind kind) {

            this.Kind = kind;
        }
    }
}