using System;
using System.Collections.Generic;

namespace MLIR {

    public class Region : Node {

        public IEnumerable<Block> Blocks { get; private set; }

        public Region(Location location, IEnumerable<Block> blocks)
            : base(Kind.Region, location) {

            this.Blocks = blocks;
        }

        /// Public

        public bool IsEmpty() {

            return this.Blocks.IsEmpty();
        }
    }
}