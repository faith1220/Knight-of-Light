using UnityEngine;

namespace PlayerStates
{
    public class PlayerStateMachine
    {
        public PlayerState currenetState { get; private set; }

        public void Init(PlayerState _startState)
        {
            currenetState = _startState;
            currenetState.Enter();
        }

        public void ChangeState(PlayerState _newState)
        {
            Debug.Log("�л�Ϊ"+_newState.ToString()+"״̬");
            currenetState.Exit();
            currenetState = _newState;
            currenetState.Enter();
        }
    }
}

