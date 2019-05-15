using System;

namespace MLIR {

    public class Dialect {

        private String Name { get; set; }
        private Context Context { get; set; }
        private bool AllowUnknownOperations { get; set; }

        protected Dialect(String name, Context context) {

            this.Name = name;
            this.Context = context;
            this.AllowUnknownOperations = false;

            context.RegisterDialect();
        }
    }
}