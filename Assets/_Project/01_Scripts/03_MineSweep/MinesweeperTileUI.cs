using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// this contorls the tiles of the mine sweeper mini game
/// </summary>
[RequireComponent(typeof(Image))]
public class MinesweeperTileUI : MonoBehaviour,
    IPointerClickHandler
{
    [SerializeField] private Image tileImage;

    private MinesweeperGame game;
    private Cell cell;

    public Cell Cell => cell;

    private void Awake()
    {
        if (tileImage == null)
        {
            tileImage = GetComponent<Image>();
        }
    }

    public void Initialize(MinesweeperGame owner, Cell assignedCell)
    {
        game = owner;
        cell = assignedCell;
    }

    public void SetSprite(Sprite sprite)
    {
        tileImage.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked Minesweeper tile: {cell?.position}");
        if (game == null || cell == null)
        {
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            game.RevealCell(cell);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            game.ToggleFlag(cell);
        }
    }
}