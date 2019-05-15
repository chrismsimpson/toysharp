using System;
using System.Collections.Generic;

namespace Toy {

    public enum VType {
        Float,
        Int
    }

    public struct VarType {

        public VType VType { get; set; }
        public IEnumerable<int> Shape { get; set; }
    }
}