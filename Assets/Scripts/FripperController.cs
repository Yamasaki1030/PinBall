using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {

    private HingeJoint myHingeJoint;

    // 初期の傾きと弾いた時の傾き
    private float defaultAngle = 20;
    private float flickAngle = -20;

    // フリッパーが動いているか
    private bool posL = false;
    private bool posR = false;

	// Use this for initialization
	void Start () {
        this.myHingeJoint = GetComponent<HingeJoint>();

        //　フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }
	
	// Update is called once per frame
	void Update () {

        
        // フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        
        // フリッパーを元に戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if(Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }

        // タップの場合
        if(Input.touchCount > 0)
        {
            Touch[] myTouches = Input.touches;

            for(int i = 0; i < myTouches.Length; i++)
            {
                switch (myTouches[i].phase)
                {
                    case TouchPhase.Began:
                        // タップする
                        if (myTouches[i].position.x <= Screen.width / 2 && tag == "LeftFripperTag")
                        {
                            SetAngle(this.flickAngle);
                            posL = true;
                        }
                        if (myTouches[i].position.x > Screen.width / 2 && tag == "RightFripperTag")
                        {
                            SetAngle(this.flickAngle);
                            posR = true;
                        }
                        break;
                    case TouchPhase.Moved:
                        // 画面に触れたまま中心線を超えた場合
                        if (posL == true && myTouches[i].position.x > Screen.width / 2 && tag == "LeftFripperTag")
                        {
                            SetAngle(this.defaultAngle);
                            posL = false;
                        }
                        if (posR == true && myTouches[i].position.x <= Screen.width / 2 && tag == "RightFripperTag")
                        {
                            SetAngle(this.defaultAngle);
                            posR = false;
                        }
                        break;
                    case TouchPhase.Ended:
                        // 指を離す
                        if (myTouches[i].position.x <= Screen.width / 2 && tag == "LeftFripperTag")
                        {
                            SetAngle(this.defaultAngle);
                        }
                        if (myTouches[i].position.x > Screen.width / 2 && tag == "RightFripperTag")
                        {
                            SetAngle(this.defaultAngle);
                        }
                        break;
                    default:
                        break;
                }
            
            
            }
        }
	}

    // フリッパーの傾きを設定
    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}
