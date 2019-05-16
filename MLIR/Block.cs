using System;

namespace MLIR {

    public class Block : Node {

        public Block(Location location)
            : base(Kind.Block, location) {

        }
    }
}