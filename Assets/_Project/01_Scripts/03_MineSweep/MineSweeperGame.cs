using System.Collections;
using UnityEngine;

public class MinesweeperGame : MonoBehaviour
{
    [Header("Board Settings")]
    [SerializeField] private int width = 16;
    [SerializeField] private int height = 16;
    [SerializeField] private int mineCount = 32;

    [Header("Board References")]
    [SerializeField] private Transform boardParent;
    [SerializeField] private MinesweeperTileUI tilePrefab;

    [Header("Tile Sprites")]
    [SerializeField] private Sprite tileUnknown;
    [SerializeField] private Sprite tileEmpty;
    [SerializeField] private Sprite tileMine;
    [SerializeField] private Sprite tileExploded;
    [SerializeField] private Sprite tileFlag;
    [SerializeField] private Sprite tileNum1;
    [SerializeField] private Sprite tileNum2;
    [SerializeField] private Sprite tileNum3;
    [SerializeField] private Sprite tileNum4;
    [SerializeField] private Sprite tileNum5;
    [SerializeField] private Sprite tileNum6;
    [SerializeField] private Sprite tileNum7;
    [SerializeField] private Sprite tileNum8;

    private CellGrid grid;
    private MinesweeperTileUI[,] tiles;

    private bool gameOver;
    private bool minesGenerated;

    private void OnValidate()
    {
        width = Mathf.Max(1, width);
        height = Mathf.Max(1, height);
        mineCount = Mathf.Clamp(mineCount, 0, width * height - 1);
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        StopAllCoroutines();

        ClearBoard();

        gameOver = false;
        minesGenerated = false;

        grid = new CellGrid(width, height);
        tiles = new MinesweeperTileUI[width, height];

        CreateBoard();
        DrawBoard();
    }

    private void CreateBoard()
    {
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                MinesweeperTileUI tile =
                    Instantiate(tilePrefab, boardParent);

                Cell cell = grid[x, y];

                tile.Initialize(this, cell);
                tiles[x, y] = tile;
            }
        }
    }

    private void ClearBoard()
    {
        if (boardParent == null)
        {
            return;
        }

        for (int i = boardParent.childCount - 1; i >= 0; i--)
        {
            Destroy(boardParent.GetChild(i).gameObject);
        }
    }

    public void RevealCell(Cell cell)
    {
        if (gameOver)
        {
            return;
        }

        if (cell.revealed || cell.flagged)
        {
            return;
        }

        if (!minesGenerated)
        {
            grid.GenerateMines(cell, mineCount);
            grid.GenerateNumbers();
            minesGenerated = true;
        }

        if (cell.type == Cell.Type.Mine)
        {
            Explode(cell);
        }
        else if (cell.type == Cell.Type.Empty)
        {
            StartCoroutine(FloodReveal(cell));
        }
        else
        {
            cell.revealed = true;
            CheckWinCondition();
        }

        DrawBoard();
    }

    public void ToggleFlag(Cell cell)
    {
        if (gameOver || cell.revealed)
        {
            return;
        }

        cell.flagged = !cell.flagged;
        DrawBoard();
    }

    private IEnumerator FloodReveal(Cell cell)
    {
        if (gameOver || cell.revealed || cell.flagged)
        {
            yield break;
        }

        if (cell.type == Cell.Type.Mine)
        {
            yield break;
        }

        cell.revealed = true;
        DrawBoard();

        yield return null;

        if (cell.type == Cell.Type.Empty)
        {
            for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
            {
                for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
                {
                    if (adjacentX == 0 && adjacentY == 0)
                    {
                        continue;
                    }

                    int x = cell.position.x + adjacentX;
                    int y = cell.position.y + adjacentY;

                    if (grid.TryGetCell(x, y, out Cell adjacentCell))
                    {
                        StartCoroutine(FloodReveal(adjacentCell));
                    }
                }
            }
        }

        CheckWinCondition();
    }

    private void Explode(Cell explodedCell)
    {
        gameOver = true;

        explodedCell.exploded = true;
        explodedCell.revealed = true;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.revealed = true;
                }
            }
        }

        DrawBoard();
    }

    private void CheckWinCondition()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];

                if (cell.type != Cell.Type.Mine && !cell.revealed)
                {
                    return;
                }
            }
        }

        gameOver = true;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.flagged = true;
                }
            }
        }

        DrawBoard();
    }

    private void DrawBoard()
    {
        if (grid == null || tiles == null)
        {
            return;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];
                MinesweeperTileUI tile = tiles[x, y];

                tile.SetSprite(GetSprite(cell));
            }
        }
    }

    private Sprite GetSprite(Cell cell)
    {
        if (cell.revealed)
        {
            return GetRevealedSprite(cell);
        }

        if (cell.flagged)
        {
            return tileFlag;
        }

        return tileUnknown;
    }

    private Sprite GetRevealedSprite(Cell cell)
    {
        switch (cell.type)
        {
            case Cell.Type.Empty:
                return tileEmpty;

            case Cell.Type.Mine:
                return cell.exploded ? tileExploded : tileMine;

            case Cell.Type.Number:
                return GetNumberSprite(cell.number);

            default:
                return tileUnknown;
        }
    }

    private Sprite GetNumberSprite(int number)
    {
        switch (number)
        {
            case 1: return tileNum1;
            case 2: return tileNum2;
            case 3: return tileNum3;
            case 4: return tileNum4;
            case 5: return tileNum5;
            case 6: return tileNum6;
            case 7: return tileNum7;
            case 8: return tileNum8;
            default: return tileEmpty;
        }
    }
}