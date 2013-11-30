using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public enum SOUND : int
	{
		BGM = 0,
	}

	// Use this for initialization
	void Start () {
		SoundUtil.instance.Play2D((int)SOUND.BGM);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
