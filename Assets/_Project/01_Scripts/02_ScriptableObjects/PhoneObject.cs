
using UnityEngine;

[CreateAssetMenu(fileName = "PhoneCallData", menuName = "Scriptable Objects/PhoneCall")]
public class PhoneObject : ScriptableObject
{
    public string callerName;
    public float ringDuration = 5f;      
    public float timeBonus = 8f;        
    public float timePenalty = 10f;      
}
