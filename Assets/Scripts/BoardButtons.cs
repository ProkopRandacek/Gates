using UnityEngine;

public enum ButtonTyper { AddInput, AddOutput, RemoveInput, RemoveOutput }

public class BoardButtons : MonoBehaviour
{
    public BoardGate boardInput;
    public BoardGate boardOutput;
    public ButtonTyper buttonType;
    
    private void OnMouseDown()
    {
        switch (buttonType)
        {
            case ButtonTyper.AddInput:
                boardInput.AddInput();
                break;
            case ButtonTyper.AddOutput:
                boardOutput.AddOutput();
                break;
            case ButtonTyper.RemoveInput:
                boardInput.RemoveInput();
                break;
            case ButtonTyper.RemoveOutput:
                boardOutput.RemoveOutput();
                break;
        }
    }
}
