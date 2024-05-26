using UnityEngine;

[CreateAssetMenu(fileName = "ChaserData",menuName = "Data/Enemy Data/Chaser Data")]
public class Chaser_Data : D_EntityData
{
    [Header("PlayerDetected State")] 
    public float DetectedStateTime = 0.5f;
    
    [Header("Charge State")] 
    public float ChargeSpeed = 7;
}
