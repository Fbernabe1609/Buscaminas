using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private TextMeshProUGUI endGameText;

    [SerializeField] private int easyWidth,
        easyHeight,
        easyBombsNumber,
        mediumWidth,
        mediumHeight,
        mediumBombsNumber,
        hardWidth,
        hardHeight,
        hardBombsNumber;

    public void StartEasy()
    {
        Generator generator = gameObject.AddComponent<Generator>();
        generator.SetGenerator(easyWidth, easyHeight, easyBombsNumber, endGameCanvas, endGameText, cell);
    }

    public void StartNormal()
    {
        Generator generator = gameObject.AddComponent<Generator>();
        generator.SetGenerator(mediumWidth, mediumHeight, mediumBombsNumber, endGameCanvas, endGameText, cell);
    }

    public void StartHard()
    {
        Generator generator = gameObject.AddComponent<Generator>();
        generator.SetGenerator(hardWidth, hardHeight, hardBombsNumber, endGameCanvas, endGameText, cell);
    }
}