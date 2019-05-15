using System;
using MLIR;

namespace Toy {

    public class ToyDialect : MLIR.Dialect {
        
        public ToyDialect(MLIR.Context context)
            : base("toy", context) {

        }

    }
}