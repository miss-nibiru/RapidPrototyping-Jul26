using UnityEngine;

namespace _Project._01_Scripts._02_ScriptableObjects
{
    public class PhoneWindow : MonoBehaviour
    {
        [SerializeField] private GameObject phonePanel;
        [SerializeField] private ResponceType  correctPhoneResponceType;
    
        public void OpenPhoneWindow()
        {
            phonePanel.SetActive(true);
        }
    }
}
