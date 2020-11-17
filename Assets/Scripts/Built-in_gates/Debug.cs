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
    
    public override void Evaluate()
    {
        for (int i = 0; i < 3; i++)
            Outputs[i].value = Inputs[i].value;
    }
}