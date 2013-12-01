using UnityEngine;
using System.Collections;

public class SplashAnimation : MonoBehaviour {

	public GameObject[] sprites;

	// Use this for initialization
	void Start () {
		if (sprites.Length == 0)
		{
			Application.LoadLevel("TitleScene");
		}
		else
		{
			for(int i = 0; i < sprites.Length; i++)
			{
				sprites[i].renderer.material.color = new Color(255, 255, 255, 0);
			}
		}

		StartCoroutine("Splash");
	}

	float a = 0;
	int cnt = 0;

	IEnumerator Splash()
	{
		while(true)
		{
			if (cnt == sprites.Length) { break; }

			float deltaTime = 0;

			change = false;

			while(deltaTime < 1)
			{
				if (change) { break; }

				deltaTime += Mathf.Clamp01(Time.deltaTime);

				sprites[cnt].renderer.material.color = new Color(255, 255, 255, deltaTime);

				yield return null;
			}

			deltaTime = 0;
			yield return null;

			while (deltaTime < 1)
			{
				if (change) { break; }

				deltaTime += Mathf.Clamp01(Time.deltaTime);

				a = 1 - deltaTime;
			
				sprites[cnt].renderer.material.color = new Color(255, 255, 255, a);

				yield return null;
			}
			if (change) { continue; }
			cnt++;
			yield return null;
		}
		yield return null;

		Application.LoadLevel("TitleScene");
		yield return null;
	}

	bool change = false;
	void onTch()
	{
		if (cnt == sprites.Length) { return; }
		sprites[cnt].renderer.material.color = new Color(255, 255, 255, 0);
		cnt++;
		change = true;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			onTch();
		}

		if (Input.touchCount != 0)
		{
			if(Input.touches[0].phase == TouchPhase.Began)
			{
				onTch();
			}
		}
	}
}
