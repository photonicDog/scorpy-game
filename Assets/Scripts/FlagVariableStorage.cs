using Flags;
using Yarn;

public class FlagVariableStorage : Yarn.Unity.VariableStorageBehaviour {
    
    public FlagManager _flagManager;
    
    // Store a value into a variable
    public override void SetValue(string variableName, Yarn.Value value) {
        _flagManager.SetFlag(variableName, value);
    }

    // Return a value, given a variable name
    public override Yarn.Value GetValue(string variableName) {
        return new Value(_flagManager.GetFlag(variableName));
    }

    // Return to the original state
    public override void ResetToDefaults () {
        _flagManager.Reset();
    }
}