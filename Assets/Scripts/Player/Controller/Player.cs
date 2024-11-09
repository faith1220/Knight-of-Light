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
            Debug.Log("检测到地面");
        }
        else
            Debug.Log("未检测到地面");
        if(isFall)
        {
            Debug.Log("正在下降");
        }
    }
}
