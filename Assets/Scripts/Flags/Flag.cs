using System;
using Yarn;

namespace Flags {
    [Serializable]
    public class Flag : IFlag<Yarn.Value> {
        public string ID;
        public Yarn.Value value;
        public delegate void FlagDelegate(Yarn.Value value);
        public FlagDelegate Delegate;

        public Flag(string ID, Yarn.Value value) {
            this.ID = ID;
            this.value = value;
        }
        
        public Yarn.Value ReadFlag() {
            return value;
        }

        public void SetFlag(Yarn.Value obj) {
            value = obj;
            Delegate?.Invoke(value);
        }

        public void BindToDelegate(FlagDelegate del, bool bind = true) {
            if (bind) Delegate += del;
            else Delegate -= del;
        }

        public static Value DefaultToValue(DefaultVariable variable) {
            object value;

            switch (variable.type) {
                case Yarn.Value.Type.Number:
                    float f = 0.0f;
                    float.TryParse(variable.value, out f);
                    value = f;
                    break;

                case Yarn.Value.Type.String:
                    value = variable.value;
                    break;

                case Yarn.Value.Type.Bool:
                    bool b = false;
                    bool.TryParse(variable.value, out b);
                    value = b;
                    break;

                case Yarn.Value.Type.Variable:
                case Yarn.Value.Type.Null:
                    value = null;
                    break;

                default:
                    throw new System.ArgumentOutOfRangeException ();

            }

            return new Yarn.Value(value);
        }
    }
    
    /// <summary>
    /// A default value to apply when the object wakes up, or when
    /// ResetToDefaults is called.
    /// </summary>
    [System.Serializable]
    public class DefaultVariable
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        /// <remarks>
        /// Do not include the `$` prefix in front of the variable
        /// name. It will be added for you.
        /// </remarks>
        public string name;

        /// <summary>
        /// The value of the variable, as a string.
        /// </summary>
        /// <remarks>
        /// This string will be converted to the appropriate type,
        /// depending on the value of <see cref="type"/>.
        /// </remarks>
        public string value;

        /// <summary>
        /// The type of the variable.
        /// </summary>
        public Yarn.Value.Type type;
    }
}