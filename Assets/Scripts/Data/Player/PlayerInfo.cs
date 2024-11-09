using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("角色名")]
    public string playerName;
    [Header("等级")]
    public int lev;
    [Header("金钱")]
    public int money;
    [Header("经验")]
    public int exp;

    [Header("移动速度")]
    public float spd;
    [Header("生命")]
    public float hp;
}
