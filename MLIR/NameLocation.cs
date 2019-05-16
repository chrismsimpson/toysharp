using System;

namespace MLIR {

    public class NameLocation : Location {
        
        public NameLocation()
            : base (LocationKind.Name) {

        }
    }
}