using System;

namespace Toy {

    public struct Location {

        public String File { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
    }

    public static partial class LocationHelpers {

        public static Location MoveTo(this Location location, int line, int column) {

            return new Location { File = location.File, Line = line, Column = column };
        }
    }
}