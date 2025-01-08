using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ← この行を追加


public class BackgroundScroller : MonoBehaviour // スクリプト名を変更
{
    public Image BackgroundImage;
    public float ScrollSpeed;

    private Material materialInstance;

    void Start()
    {
        // Imageコンポーネントに設定されているマテリアルのインスタンスを作成
        materialInstance = Instantiate(BackgroundImage.material);
        BackgroundImage.material = materialInstance;
    }

    void Update()
    {
        // マテリアルのテクスチャオフセットを更新して背景をスクロール
        Vector2 offset = materialInstance.mainTextureOffset;
        offset.x += ScrollSpeed * Time.deltaTime;
        materialInstance.mainTextureOffset = offset;
    }

    void OnDestroy()
    {
        // インスタンス化したマテリアルを解放
        if (materialInstance != null)
        {
            Destroy(materialInstance);
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}