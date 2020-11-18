public class BoardGate: Gate
{
    protected override void PostStart()
    {
        Name = "";
    }
    
    public override void Evaluate()
    {
        // TODO
    }

    public void AddInput()
    {
        AddPut(PutType.Out);
    }

    public void AddOutput()
    {
        AddPut(PutType.In);
    }

    public void RemoveInput()
    {
        RemovePut(PutType.Out);
    }
    public void RemoveOutput()
    {
        RemovePut(PutType.In);
    }
}