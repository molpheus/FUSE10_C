using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour {

	// 床の移動スピード
	public static float spd = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// 床をX軸方向に移動
		transform.position += new Vector3(spd * GameUtility.speed, 0, 0);

		// 床が指定位置まで来たら削除
		if(transform.position.x >= 50) {
			transform.position = new Vector3(0,0,0);
		}
	}
}
