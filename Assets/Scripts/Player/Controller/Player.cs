using PlayerStates;
using UnityEngine;

public class Player : PlayerConroller
{
    #region States

    public PlayerStateMachine StateMachine { private set; get; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();
        
    }

    private void Update()
    {
        if (isGround)
        {
            Debug.Log("��⵽����");
        }
        else
            Debug.Log("δ��⵽����");
        if(isFall)
        {
            Debug.Log("�����½�");
        }
    }
}
