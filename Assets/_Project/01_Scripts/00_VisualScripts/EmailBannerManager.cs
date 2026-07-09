using System.Collections.Generic;
using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class EmailBannerManager : MonoBehaviour
    {
        public static EmailBannerManager Instance { get; private set; }
    
    
        [Header("Email UI")]
        [SerializeField] private Transform emailListParent;
        [SerializeField] private GameObject emailListItemPrefab;
    
        [SerializeField] private List<string> testEmailSenders = new();

        private readonly List<EmailListItem> _emailItems = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            foreach (string sender in testEmailSenders) 
                CreateEmailItem(sender);
        }

   
        private void CreateEmailItem(string senderName)
        {
            if (emailListItemPrefab == null || emailListParent == null)
                return;

            GameObject obj = Instantiate(emailListItemPrefab, emailListParent);
            EmailListItem item = obj.GetComponent<EmailListItem>();

            item.Initialize(senderName);
            _emailItems.Add(item);
        }
    

        public void OnEmailClicked(string senderName)
        {
            Debug.Log("Email clicked: " + senderName);
            // Tomorrow: open EmailWindow, apply timers, etc.
        }

        private void Update()
        {
            // TEMP: fade all items slowly over time
            float t = Mathf.PingPong(Time.time * 0.1f, 1f);

            foreach (var item in _emailItems)
                item.UpdateFlagColor(t);
        }
    }
}