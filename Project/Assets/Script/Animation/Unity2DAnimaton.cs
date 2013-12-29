using UnityEngine;
using System.Collections;

public class Unity2DAnimaton : MonoBehaviour {
	public Sprite[] sprite;

	bool flg = false;

	int cnt = 1;

	int num = 0;

	int number = 0;

	public SpriteRenderer render;

	void Start()
	{
		if (sprite.Length == 0) {
						flg = true;
				} else {
			render.sprite = sprite[number];
				}

	}

	void Update()
	{
		if (flg) {
						return;
				}

		if (cnt < num) {
			number++;
			  			if(number >= sprite.Length)
			{
				number = 0;
			}
			render.sprite = sprite[number];
			num = 0;
				}
		num++;
	}
}
