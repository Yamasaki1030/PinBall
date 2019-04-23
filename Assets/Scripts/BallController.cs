using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    // ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;
    // テキスト
    private GameObject scoreText;
    private GameObject gameoverText;
    // 得点
    private int score = 0;

	// Use this for initialization
	void Start () {
        // Textオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
        this.gameoverText = GameObject.Find("GameOverText");
	}
	
	// Update is called once per frame
	void Update () {
		// ボールが画面外に出た場合
        if(this.transform.position.z < this.visiblePosZ)
        {
            // ゲームオーバーを表示
            this.gameoverText.GetComponent<Text>().text = "Game Over";
        }
	}

    // 得点
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "SmallStarTag":
                this.score += 10;
                break;
            case "SmallCloudTag":
                this.score += 20;
                break;
            case "LargeStarTag":
                this.score += 30;
                break;
            case "LargeCloudTag":
                this.score += 50;
                break;
            default:
                break;


        }

        this.scoreText.GetComponent<Text>().text = "Score: " + this.score + "pt";
    }
}
