using UnityEngine;

namespace PlayerStates
{
    public class PlayerState
    {
        protected PlayerStateMachine playerStateMachine;
        protected Player player;
        protected Animator animator;
        protected Rigidbody2D rb;

        public PlayerState(PlayerStateMachine playerStateMachine,Player player)
        {
            this.playerStateMachine = playerStateMachine;
            this.player = player;

            animator = player.animator;
            rb = player.rb;
        }

        public void Enter()
        {

        }

        public void Update()
        {

        }
        public void Exit()
        {

        }
    }

}

