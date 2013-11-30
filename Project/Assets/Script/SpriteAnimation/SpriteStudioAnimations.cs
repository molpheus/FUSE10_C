using UnityEngine;
using System.Collections;

public class SpriteStudioAnimations : MonoBehaviour {

	// sprite Studio animation script.
	public SsSprite sprites = null;

	public SsAnimation[] anim;

	// Use this for initialization
	void Start ()
	{
		if(sprites == null)
		{
			sprites = gameObject.GetComponent<SsSprite>();
		}
	}

	void OnGUI()
		{
			if (GUI.Button (new Rect (0, 0, 50, 50), "0")) {
					sprites.Animation = anim [0];
					sprites.Play ();
			}
			if (GUI.Button (new Rect (50, 0, 50, 50), "1")) {
				sprites.Animation = anim [1];
				sprites.Play ();
			}
			if (GUI.Button (new Rect (0, 50, 50, 50), "2")) {
				sprites.Animation = anim [2];
				sprites.Play ();
			}
		}

	public void Wait()
	{

	}
}
