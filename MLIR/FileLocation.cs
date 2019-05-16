using System;

namespace MLIR {

    public class FileLocation : Location {
        
        public FileLocation()
            : base(LocationKind.File) {

        }
    }
}