using UnityEngine;
using System.Collections;

public class NumberScript : MonoBehaviour {

	public SpriteRenderer render = null;
	public Sprite[] numbers;

	// Use this for initialization
	void Start (){
				if (numbers.Length == 0) {
						Debug.Log ("Not number Error");
				}
				if (render == null) {
						render = gameObject.GetComponent<SpriteRenderer> ();
				}
	}

	public void SetNumber(int num)
	{
				if (num > -1 && num < 10) {
						render.sprite = numbers [num];
				} else {
						render.sprite = numbers [0];
				}
	}
}
