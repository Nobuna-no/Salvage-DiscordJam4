using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "ScriptableObjects/BulletScriptableObject", order = 1)]
public class BulletSO : ScriptableObject
{
    public int Dammage;
    public float Speed;
    public float Life;
    public float Radius;
}