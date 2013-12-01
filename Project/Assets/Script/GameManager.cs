using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject[] prefubObj;

	// Use this for initialization
	void Start () {
	
	}

	int cnt = 0;
	// Update is called once per frame
	void Update ()
	{
		if(cnt % 60 == 0)
		{
			if (prefubObj.Length != 0)
			{
				int r = Random.Range(0, prefubObj.Length);
				Instantiate(prefubObj[r]);
			}
			cnt = 0;
		}
		cnt++;
	}
}
