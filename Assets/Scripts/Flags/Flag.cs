using System;

namespace Flags {
    [Serializable]
    public class Flag<T> : IFlag<T> {
        public string ID;
        public T value;
        public delegate bool FlagDelegate(T value);
        public FlagDelegate Delegate;

        public Flag(string ID, T value) {
            this.ID = ID;
            this.value = value;
        }
        
        public T ReadFlag() {
            return value;
        }

        public void SetFlag(T obj) {
            value = obj;
            Delegate?.Invoke(value);
        }
    }
}