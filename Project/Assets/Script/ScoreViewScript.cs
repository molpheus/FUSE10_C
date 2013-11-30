using UnityEngine;
using System.Collections;

public class ScoreViewScript : MonoBehaviour {

	public NumberScript[] nScripte;

	public bool maxscore;
	// Use this for initialization
	void Start ()
	{
				if (maxscore) {
						SetNumbers (GameUtility.maxScore);
				} else {
						SetNumbers (0);
				}
				SetNumbers (1234567);
	}

	// set numbers.
	public void SetNumbers(int num)
		{
				if (num == 0) {
						foreach (NumberScript sc in nScripte) {
								sc.SetNumber (0);
						}
				} else {
						string numStr = num.ToString();
						int length = numStr.Length;
						while (true) {
								if (length > 0) {

										int max = 1;
										for (int i = 0; i < length; i++) {
												max *= 10;
										}
								
										int temp = num / max;
										nScripte [length].SetNumber (temp);
								
										num -= (temp * max);
								
										length--;
								} else {
										nScripte [0].SetNumber (num);
										break;
								}
						}
				}
		}
}
