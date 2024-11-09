using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public float groundCheckDistance;//检测地面的距离
    public LayerMask whatIsGround;//检测地面的图层

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
