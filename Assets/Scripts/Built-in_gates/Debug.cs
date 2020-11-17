using System;
using System.Collections.Generic;

public class Debug : Gate
{
    protected override void PostStart()
    {
        AddPut(PutType.In);
        AddPut(PutType.In);
        AddPut(PutType.In);
        AddPut(PutType.Out);
        AddPut(PutType.Out);
        AddPut(PutType.Out);
        
        Name = "DBG";
    }
    
    public new List<bool> Evaluate(List<bool> inputs)
    {
        if (inputs.Count != 3)
            throw new Exception($"Debug gate called with {inputs.Count} inputs instead of 3");
        return inputs;
    }
}