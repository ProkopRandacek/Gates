using System;
using System.Collections.Generic;

public class Not : Gate
{
    private void Awake()
    {
        AddPut(PutType.In);
        AddPut(PutType.Out);
        
        Name = "NOT";
    }
    
    public new List<bool> Evaluate(List<bool> inputs)
    {
        if (inputs.Count != 1)
            throw new Exception($"Not gate called with {inputs.Count} inputs instead of 1");
        return new List<bool> {!inputs[0]};
    }
}