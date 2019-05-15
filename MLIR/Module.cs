using System;

namespace MLIR {

    public class Module {

        public Context Context { get; private set; }

        public Module(Context context) {

            this.Context = context;
        }
    }
}
