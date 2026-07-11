using UnityEngine;

[CreateAssetMenu(fileName = "WordOptions", menuName = "Scriptable Objects/WordOptions")]
public class WordOptions : ScriptableObject
{
    public string responce;
    public ResponceType responceType;
    public Color responceColor;
    public Sprite responceBackground;
}
