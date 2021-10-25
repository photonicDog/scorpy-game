using System;

namespace Flags {
    public interface IFlag<T> {
        public T ReadFlag();
        public void SetFlag(T obj);
    }
}