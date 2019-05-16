using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LLVMSharp;

using NamedAttributes = System.Collections.Generic.Dictionary<MLIR.Identifier, MLIR.Attribute>;

namespace MLIR {

    public class Function : Node {

        public Module Module { get; private set; }
        
        public Location Location { get; set; }

        public Identifier Identifier { get; private set; }

        private FunctionType functionType;

        public FunctionType FunctionType {

            get {

                return this.functionType;
            }
            set {

                this.functionType = value;
            }
        }

        public NamedAttributes Attributes { get; private set; }

        public IEnumerable<NamedAttributes> ArgumentAttributes { get; private set; }

        public Region Body { get; private set; }

        public Function(Location location, Module module, NamedAttributes attributes) : this(location, module, attributes, null) { }

        public Function(Location location, Module module, NamedAttributes attributes, IEnumerable<NamedAttributes> argumentAttributes)
            : base(Kind.Function, location) {

            this.Location = location;
            this.Module = module;

            this.Attributes = attributes;
            this.ArgumentAttributes = argumentAttributes;
        }

        /// Public

        public void AddEntryBlock() {

        }

        /// Unlink this function from its module and delete it.

        public void Erase() {

        }

        public bool IsExternal() {

            return this.IsEmpty();
        }

        public IEnumerable<Block> Blocks {

            get {

                return this.Body.Blocks;
            }
        }

        public bool IsEmpty() {

            return this.Body.IsEmpty();
        }

        protected internal override Node Accept(Visitor visitor)
        {
            return visitor.VisitFunction(this);
        }
    }
}