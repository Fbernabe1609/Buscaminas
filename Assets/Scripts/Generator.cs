using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
    internal static Generator Instance;

    private int width, height, bombsNumber, cellsWithoutBombs;
    private GameObject endGameCanvas, cell;
    private TextMeshProUGUI endGameText;

    private GameObject[][] map;
    private const float cellSize = 1;
    private bool isWinner = true;
    private int clicks = 0;

    public void SetGenerator(int width, int height, int bombsNumber, GameObject endGameCanvas,
        TextMeshProUGUI endGameText, GameObject cell)
    {
        this.width = width;
        this.height = height;
        this.bombsNumber = bombsNumber;
        this.endGameCanvas = endGameCanvas;
        this.endGameText = endGameText;
        this.cell = cell;
        cellsWithoutBombs = width * height - bombsNumber;
        Init();
    }

    private void Init()
    {
        Instance = this;
        GenerateBombs();
        InitBoard();
        PlaceBombs();
    }

    public void SetIsWinner(bool isWinner)
    {
        this.isWinner = isWinner;
    }

    public bool IsWinner
    {
        get { return isWinner; }
    }

    public int Width
    {
        get { return width; }
    }

    public int Height
    {
        get { return height; }
    }

    public int BombsNumber
    {
        get { return bombsNumber; }
    }

    public int Clicks
    {
        get { return clicks; }
        set { clicks = value; }
    }

    private void InitBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j] = Instantiate(cell, new Vector3(i, j, 0), Quaternion.identity);
                map[i][j].GetComponent<Cell>().X = i;
                map[i][j].GetComponent<Cell>().Y = j;
            }
        }

        if (Camera.main != null)
        {
            Camera.main.transform.position = new Vector3(((float)width / 2) - (cellSize / 2),
                ((float)height / 2) - (cellSize / 2), -10);
        }
    }

    private void GenerateBombs()
    {
        map = new GameObject[width][];
        for (int i = 0; i < width; i++)
        {
            map[i] = new GameObject[height];
        }
    }

    private void PlaceBombs()
    {
        int currentX, currentY;
        for (int i = 0; i < bombsNumber; i++)
        {
            currentX = Random.Range(0, width);
            currentY = Random.Range(0, height);
            if (!map[currentX][currentY].GetComponent<Cell>().IsBomb)
            {
                map[currentX][currentY].GetComponent<Cell>().IsBomb = true;
            }
            else
            {
                i--;
            }
        }
    }

    public int CalculateBombsAround(int x, int y)
    {
        int bombsAround = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height && !(i == x && j == y))
                {
                    if (map[i][j].GetComponent<Cell>().IsBomb) bombsAround++;
                }
            }
        }

        return bombsAround;
    }

    public void EndGame()
    {
        string text = isWinner ? "Has ganado." : "Has perdido.";
        StartCoroutine(ShowEndGameText(text));
    }

    public void CheckWinner()
    {
        clicks++;
        if (cellsWithoutBombs == clicks)
        {
            isWinner = true;
            EndGame();
        }
    }

    IEnumerator ShowEndGameText(string text)
    {
        endGameText.text = text;
        endGameCanvas.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}