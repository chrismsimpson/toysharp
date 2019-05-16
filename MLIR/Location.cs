using System;

namespace MLIR {

    public class Location {

        public LocationKind Kind { get; private set; }
        
        public Location(LocationKind kind) {

            this.Kind = kind;
        }
    }
}