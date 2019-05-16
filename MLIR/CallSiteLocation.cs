using System;

namespace MLIR {

    public class CallSiteLocation : Location {
        
        public CallSiteLocation()
            : base (LocationKind.CallSite) {

        }
    }
}