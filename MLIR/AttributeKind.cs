using System;

namespace MLIR {

    public enum AttributeKind {

        Unit,
        Bool,
        Integer,
        Float,
        String,
        Type,
        Array,
        AffineMap,
        IntegerSet,
        Function,

        SplatElements,
        DenseIntElements,
        DenseFPElements,
        OpaqueElements,
        SparseElements,

        FIRST_ELEMENTS_ATTR = SplatElements,
        LAST_ELEMENTS_ATTR = SparseElements,

        FIRST_KIND = Unit,
        LAST_KIND = SparseElements,
    }
}