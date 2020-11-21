using System;

public class BoardGate: Gate
{
    protected override void PostStart()
    {
        Name = "";
    }
    
    public override void Evaluate()
    {
        if (Outputs.Count != Inputs.Count)
        {
            throw new Exception("Wrong amounts of {In, Out}puts in BoardGate");
        }

        for (int i = 0; i < Outputs.Count; i++)
            Outputs[i].value = Inputs[i].value;
    }

    private void Update()
    {
        Evaluate();
    }

    public void AddInput()
    {
        AddPut(PutType.Out);
        AddPut(PutType.In, "", true);
    }

    public void AddOutput()
    {
        AddPut(PutType.In);
        AddPut(PutType.Out, "", true);
    }

    public void RemoveInput()
    {
        RemovePut(PutType.Out);
        RemovePut(PutType.In);
    }
    public void RemoveOutput()
    {
        RemovePut(PutType.Out);
        RemovePut(PutType.In);
    }
}