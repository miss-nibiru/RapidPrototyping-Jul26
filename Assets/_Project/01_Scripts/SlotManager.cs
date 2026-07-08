using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance;

    [Header("SlotOptions")]
    [SerializeField] private GameObject responseOne;
    [SerializeField] private GameObject responseTwo;
    [SerializeField] private GameObject responseThree;
    [SerializeField] private GameObject responseFour;
    
    [Header("Slots")]
    [SerializeField] private GameObject wordSlotOne;
    [SerializeField] private GameObject wordSlotTwo;
    
    public void CheckEmailRequirements()
    {
        //check both slots to see what it needs
    }
}
