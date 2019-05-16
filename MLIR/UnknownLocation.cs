using System;

namespace MLIR {

    public class UnknownLocation : Location {
        
        public UnknownLocation()
            : base(LocationKind.Unknown) {

        }
    }
}