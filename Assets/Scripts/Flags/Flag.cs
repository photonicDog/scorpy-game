using System;

namespace Flags {
    [Serializable]
    public class Flag<T> : IFlag<T> {
        public string ID;
        public T value;

        public Flag(string ID, T value) {
            this.ID = ID;
            this.value = value;
        }
        
        public T ReadFlag() {
            return value;
        }

        public void SetFlag(T obj) {
            value = obj;
        }
    }
}