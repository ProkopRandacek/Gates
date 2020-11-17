public class Not : Gate
{
    protected override void PostStart()
    {
        AddPut(PutType.In);
        AddPut(PutType.Out);
        
        Name = "NOT";
    }
    
    public override void Evaluate()
    {
        Outputs[0].value = !Inputs[0].value;
    }
}