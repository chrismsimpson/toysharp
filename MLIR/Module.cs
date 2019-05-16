using System;
using System.Collections.Generic;
using System.Linq;

namespace MLIR {

    using Dervied = MLIR.IDenseMapDerived;
    using Key = MLIR.IDenseMapKey;
    using Bucket = MLIR.IDenseMapBucket;
    using Info = MLIR.IDenseMapInfo;
    using KeyInfo = MLIR.IDenseMapKeyInfo;
    using Pair = MLIR.IDenseMapPair;
    using Iterator = DenseMapIterator<MLIR.IDenseMapKey, MLIR.IDenseMapValue, MLIR.IDenseMapKeyInfo, MLIR.IDenseMapBucket, bool>;

    public interface IDenseMapDerived {
        
    }

    public interface IDenseMapValue {
        
    }

    public interface IDenseMapKey {
        
    }

    public interface IDenseMapBucket {

    }

    public interface IDenseMapInfo {
    }

    public interface IDenseMapKeyInfo {

    }

    public interface IDenseMapPair {

    }

    public static partial class IDenseMapBucketHelpers {

        public static Key ToKey(this Bucket bucket) {

            return null;
        }
    }
    
    public class HandleBase {

        public readonly ulong? EpochAddress;
        public ulong EpochAtCreation { get; private set; }

        public HandleBase() {

            this.EpochAddress = null;
            this.EpochAtCreation = ulong.MaxValue;
        }

        public HandleBase(DebugEpochBase parent) {

            this.EpochAddress = parent.Epoch;
            this.EpochAtCreation = parent.Epoch;
        }

        /// Public 

        public bool IsHandleInSync() {

            return this.EpochAddress == EpochAtCreation;
        }
    }

    public interface IDenseMapPair<IDenseMapKey, IDenseMapValue> : IDenseMapPair {

    }

    public struct DenseMapPair<IDenseMapKey, IDenseMapValue> : IDenseMapPair<IDenseMapKey, IDenseMapValue>, IDenseMapPair {

    }

    public interface IDenseMapInfo<IDenseMapKey> : IDenseMapInfo {

    }

    public struct DenseMapInfo<IDenseMapKey> : IDenseMapInfo<IDenseMapKey>, IDenseMapInfo {

        public static bool AreEqual(Bucket bucket, Key key) {

            return DenseMapInfo<Key>.AreEqual(bucket.ToKey(), key);
        }

        public static bool AreEqual(Key key, Bucket bucket) {

            return DenseMapInfo<Key>.AreEqual(key, bucket.ToKey());
        }

        public static bool AreEqual(Key keyA, Key keyB) {

            throw new NotImplementedException();
        }

        public static Key GetEmptyKey() {
            
            return default(Key);
        }

        public static Key GetTombstoneKey() {

            return default(Key);
        }

        public static ulong GetHashCode(Key key) {

            var c = key.GetHashCode();

            return ((ulong) c >> 4) ^ ((ulong) c >> 9);
        }
    }

    public class DenseMapIterator<IDenseMapKey, IDenseMapValue, IDenseMapKeyInfo, IDenseMapBucket, IsConst> : HandleBase
        where IsConst : IEquatable<Boolean> {

        /// Public
        
        public DenseMapIterator(Bucket p, Bucket e, DebugEpochBase epoch, bool noAdvance = false)
            : base(epoch) {

            

            if (this.IsHandleInSync()) {

                throw new Exception("Invalid construction!");
            }

            if (noAdvance) {
                
                return;
            }

            if (Constants.ShouldReverseIterate<IDenseMapKey>()) {

                this.RetreatPastEmptyBuckets();

                return;
            }

            this.AdvancePastEmptyBuckets();
        }

        // Private 

        void AdvancePastEmptyBuckets() {

            throw new NotImplementedException();
        }

        void RetreatPastEmptyBuckets() {

            throw new NotImplementedException();
        }
    }

    public class DebugEpochBase {

        public ulong Epoch { get; private set; }
    }

    public class DenseMapBase<IDenseMapDerived, IDenseMapKey, IDenseMapValue, IDenseMapKeyInfo, IDenseMapBucket> : DebugEpochBase {

        public IEnumerable<Bucket> Buckets { get; set; }
        
        public Iterator Find(Key key) {

            Bucket b = null;

            if (this.LookupBucketFor(key, out b)) {

                return new Iterator();
            }

            // return this.End();
            return null;
        }

        private Iterator makeIterator(Bucket p, Bucket e, DebugEpochBase epoch, bool noAdvance = false) {

            return new Iterator(epoch, noAdvance);
        }

        private bool LookupBucketFor(Key key, out Bucket foundBucket) {

            var buckets = this.Buckets;

            var numberOfBuckets = (ulong) this.Buckets.LongCount();

            if (buckets.IsEmpty()) {

                foundBucket = null;

                return false;
            }

            // FoundTombstone - Keep track of whether we find a tombstone while probing.
            
            Bucket foundTombstone = null;

            var emptyKey = this.GetEmptyKey();
            var tombstoneKey = this.GetTombstoneKey();

            if (DenseMapInfo<Key>.AreEqual(key, emptyKey) || DenseMapInfo<Key>.AreEqual(key, tombstoneKey)) {

                throw new Exception("Empty/Tombstone value shouldn't be inserted into map!");
            }

            var bucketNumber = this.GetHashCode(key) & (numberOfBuckets - 1);

            foreach (var b in buckets) {

                if (DenseMapInfo<Key>.AreEqual(key, b)) {

                    foundBucket = b;

                    return true;
                }

                if (DenseMapInfo<Key>.AreEqual(b, emptyKey)) {

                    foundBucket = foundTombstone ?? b;

                    return false;
                }

                // If this is a tombstone, remember it.  If Val ends up not in the map, we
                // prefer to return it than something that would require more probing.

                if (DenseMapInfo<Key>.AreEqual(b, tombstoneKey) && foundTombstone == null) {

                    foundTombstone = b;
                }
            }

            throw new Exception("Should we be reaching here?");
        }

        private ulong GetHashCode(Key key) {

            return DenseMapInfo<Key>.GetHashCode(key);
        }

        private Key GetEmptyKey() {

            return DenseMapInfo<Key>.GetEmptyKey();
        }

        private Key GetTombstoneKey() {

            return DenseMapInfo<Key>.GetTombstoneKey();
        }
    }

    public class DenseMap<IDenseMapKey, IDenseMapValue, IDenseMapKeyInfo, IDenseMapBucket> : DenseMapBase<DenseMap<IDenseMapKey, IDenseMapValue, IDenseMapKeyInfo, IDenseMapBucket>, IDenseMapKey, IDenseMapValue, IDenseMapKeyInfo, IDenseMapBucket> {

            
    }

    public class Module {

        public Context Context { get; private set; }

        public IList<Function> Functions { get; private set; }

        public DenseMap<Identifier, Function, IDenseMapInfo<Identifier>, IDenseMapPair<Identifier, Function>> SymbolTable { get; private set; }

        public Module(Context context) : this(context, new List<Function>()) { }

        public Module(Context context, IList<Function> functions) {

            this.Context = context;
            this.Functions = functions;

            this.SymbolTable = new DenseMap<Identifier, Function, IDenseMapInfo<Identifier>, IDenseMapPair<Identifier, Function>>();
        }

        /// Public

        public Function GetNamedFunction(String name) {

            throw new NotImplementedException();
        }

        public Function GetNamedFunction(Identifier name) {
    
            this.SymbolTable.find
        }
    }
}
