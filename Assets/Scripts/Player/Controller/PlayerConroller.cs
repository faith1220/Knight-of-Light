using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    protected PlayerInfo playerData;
    protected PlayerCheck playerCheck;

    public Rigidbody2D rb;
    public Animator animator;

    #region 角色属性
    public float spd
    {
        protected set
        {
            playerData.spd = value;
        }
        get
        {
            return Mathf.Clamp(playerData.spd,0,10f);
        }
    }

    public float LeapForce
    {
        get
        {
            return 5;
        }
    }

    #endregion

    #region 检测属性
    public bool isGround
    {
        get
        {
            return Physics2D.Raycast(transform.position,Vector2.down,playerCheck.groundCheckDistance,playerCheck.whatIsGround);
        }
    }

    public bool isLeap
    {
        get
        {
            return rb.linearVelocity.y> 0;
        }
    }
    public bool isFall
    {
        get
        {
            bool isFall = rb.linearVelocity.y < 0;
            rb.gravityScale = isFall ? 2 : 1;
            return isFall;
        }
    }
    #endregion

    protected virtual void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化属性
    /// </summary>
    private void Init()
    {
        _ = playerData != null ? playerData : playerData = GetComponent<PlayerInfo>();
        _ = playerCheck != null ? playerCheck : playerCheck = GetComponent<PlayerCheck>();

        _ = rb != null ? rb : rb = GetComponent<Rigidbody2D>();
    }

}
