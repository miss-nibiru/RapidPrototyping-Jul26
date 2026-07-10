using System.Collections.Generic;
using _Project._01_Scripts._02_ScriptableObjects;
using UnityEngine;

namespace _Project._01_Scripts._00_VisualScripts
{
    public class EmailBank : MonoBehaviour
    {
        public static EmailBank Instance { get; private set; }

        public EmailBannerPanel BannerPrefabRef;
        public Queue<GameObject> SpawnedBanners = new Queue<GameObject>();
        public Transform SpawnPoint;
        
        public List<EmailBannerSO>  Banners;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            Banners.AddRange(Resources.LoadAll<EmailBannerSO>("Bannertypes"));
        }
        
       
        
        public void ClearBanners()
        {
           //this would be used for when you lose the game or need to reset it at the end
        }
    }
}