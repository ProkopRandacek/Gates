using System;
using System.Collections.Generic;

public class And : Gate
{
    protected override void PostStart()
    {
        AddPut(PutType.In);
        AddPut(PutType.In);
        AddPut(PutType.Out);
        Name = "AND";
    }

    public new List<bool> Evaluate(List<bool> inputs)
    {
        if (inputs.Count != 2)
            throw new Exception($"And gate called with {inputs.Count} inputs instead of 2");
        return new List<bool> {inputs[0] && inputs[1]};
    }
}