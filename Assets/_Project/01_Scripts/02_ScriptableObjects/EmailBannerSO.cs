using System;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "EmailBanner", menuName = "Scriptable Objects/EmailBanner")]
public class EmailBannerSO : ScriptableObject
{
    public string senderName;
    public string subject;
    public string previewText;
    public string contentsText;
    public float bannerDuration = 5f;   
    public float timePenalty = 10f;     
}


