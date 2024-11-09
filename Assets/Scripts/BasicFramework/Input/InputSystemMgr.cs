public class InputSystemMgr : BaseManager<InputSystemMgr>
{
    public InputSystem_Actions inputActions;

    public InputSystemMgr()
    {
        if(inputActions==null)
            inputActions = new InputSystem_Actions();
        
    }       

    public void SwitchInputSystem(bool isOpen)
    {
        if(isOpen)
            inputActions.Enable();
        else
            inputActions.Disable();
    }

}
