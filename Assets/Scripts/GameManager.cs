using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProを使うために必要な名前空間
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public UnityEngine.UI.Image MoneyIcon;

    public Player PlayerObj;

    void Start()
    {

    }

    void Update()
    {
        MoneyIcon.GetComponent<RectTransform>().Rotate(0, 0.3f, 0);

        // セミコロンを追加
        MoneyText.text = ":" + PlayerObj.Money;
    }
    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene"); // プレイヤーのシーン名に置き換えてください
    }

    public void GoToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene"); // タイトルシーン名に置き換えてください
    }
}
