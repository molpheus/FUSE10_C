using UnityEngine;
using System.Collections;

public class BackGroundScript : MonoBehaviour {

	// 背景の移動スピード
	public static float spd = 0.05f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// 背景をX軸方向に移動
		transform.position += new Vector3(spd * GameUtility.speed, 0, 0);

		// 背景が指定位置まで来たら削除
		if(transform.position.x >= 40) {
			transform.position = new Vector3(-7.5f,5,5);
		}
	}
}
