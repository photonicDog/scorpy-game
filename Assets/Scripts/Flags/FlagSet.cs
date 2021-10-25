using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Yarn;

namespace Flags {
    [CreateAssetMenu(fileName = "Flag Set", menuName = "Glittertools/FlagSet", order = 0)]
    public class FlagSet : SerializedScriptableObject {
        private List<Flag<Yarn.Value>> yarnFlags;
        public List<DefaultVariable> defaultFlags;

        public void OnEnable() {
            yarnFlags = new List<Flag<Value>>();
            ResetToDefaults();
        }

        public void ResetToDefaults() {
            yarnFlags.Clear();

            // For each default variable that's been defined, parse the
            // string that the user typed in in Unity and store the
            // variable
            foreach (var variable in defaultFlags) {
                
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
                        // We don't support assigning default variables from
                        // other variables yet
                        Debug.LogErrorFormat("Can't set variable {0} to {1}: You can't " +
                                             "set a default variable to be another variable, because it " +
                                             "may not have been initialised yet.", variable.name, variable.value);
                        continue;

                    case Yarn.Value.Type.Null:
                        value = null;
                        break;

                    default:
                        throw new System.ArgumentOutOfRangeException ();

                }

                var v = new Yarn.Value(value);

                SetFlag("$" + variable.name, v);
            }
        }
        
        public void SetFlag(string id, Yarn.Value v) {
            if (yarnFlags.Exists(a=>a.ID == id))
                yarnFlags.Find(a => a.ID == id).SetFlag(v);
            else
                yarnFlags.Add(new Flag<Value>(id, v));
        }
        public Yarn.Value GetFlag(string id) {
            if (yarnFlags.Exists(a=>a.ID == id))
            {
                return yarnFlags.Find(a => a.ID == id).ReadFlag();
            }
            
            throw new Exception("Flag " + id + " does not exist!");
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