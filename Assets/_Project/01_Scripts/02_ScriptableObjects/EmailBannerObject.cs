using UnityEngine;

[CreateAssetMenu(fileName = "EmailBanner", menuName = "Scriptable Objects/EmailBanner")]
public class EmailBannerObject : ScriptableObject
{
    [Header("Email Info")]
    public string senderName;
    public string subject;
    public string previewText;

    [Header("Timing")]
    public float bannerDuration = 5f;   

    [Header("Rewards / Penalties")]
    public float timeBonus = 8f;        
    public float timePenalty = 10f;     
}

