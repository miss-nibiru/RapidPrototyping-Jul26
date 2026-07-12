using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BrowserOptions", menuName = "Scriptable Objects/Browser Options")]
public class BrowserOptions : ScriptableObject
{
    public List<ComputerFilesData> browserPages = new();
}