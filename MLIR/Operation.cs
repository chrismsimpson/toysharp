using System;

namespace MLIR {

    public class Operation : Node {
        
        public Operation(Location location)
            : base(Kind.Operation, location) {

        }
    }
}