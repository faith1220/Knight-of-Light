using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public float groundCheckDistance;//������ľ���
    public LayerMask whatIsGround;//�������ͼ��

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
