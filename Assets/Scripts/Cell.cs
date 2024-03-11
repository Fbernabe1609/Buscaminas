using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private TMP_Text cellText;
    private int x, y;
    private bool isBomb;

    private void Start()
    {
        cellText = GetComponentInChildren<TMP_Text>();
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }
    public bool IsBomb
    {
        get { return isBomb; }
        set { isBomb = value; }
    }

    private void SetText(string text)
    {
        cellText.text = text;
    }

    public void OnMouseDown()
    {
        if (!Generator.Instance.IsWinner)
        {
            return;
        }
        if (isBomb)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            Generator.Instance.SetIsWinner(false);
            Generator.Instance.EndGame();
        }
        else
        {
            SetText(Generator.Instance.CalculateBombsAround(x, y).ToString());
            Generator.Instance.CheckWinner();
        }
    }
}
