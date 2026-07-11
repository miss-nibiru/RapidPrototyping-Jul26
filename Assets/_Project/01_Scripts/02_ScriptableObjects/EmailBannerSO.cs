using UnityEngine;

[CreateAssetMenu(fileName = "EmailBanner", menuName = "Scriptable Objects/EmailBanner")]
public class EmailBannerSO : ScriptableObject
{
    public string senderName;
    public string subject;
    public string previewText;
    public string contentsText;
    
    public ResponceType correctResponseType;   
    
    public float bannerDuration = 5f;
    public bool penalizesTimeWhenMissed = true;
    public float timePenalty = 10f;
    
    public string department;
    public string responseHint;
    
    
}

