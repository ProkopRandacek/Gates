public class And : Gate
{
    protected override void PostStart()
    {
        AddPut(PutType.In);
        AddPut(PutType.In);
        AddPut(PutType.Out);
        Name = "AND";
    }

    public override void Evaluate()
    {
        Outputs[0].value = Inputs[0].value && Inputs[1].value;
    }
    
    private void Update()
    {
        Evaluate();
    }
}