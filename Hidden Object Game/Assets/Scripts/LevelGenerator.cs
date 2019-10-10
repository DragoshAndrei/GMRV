using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
	public Color[] Pairs;
	public int PerPair = 4;

    // Start is called before the first frame update
    void Start()
    {
		List<Color> colors = new List<Color>();

		for(int index = 0; index < PerPair; index++)
		{
			for (int j = 0; j < Pairs.Length; j++)
			{
				colors.Add(Pairs[j]);
			}
		}

		for (int child = 0; child < gameObject.transform.childCount; child++)
		{
			int index = Random.Range(0, colors.Count);
			Color color = gameObject.transform.GetChild(child).transform.GetChild(0).GetComponent<Image>().color = colors[index];
			colors.RemoveAt(index);
			Debug.Log(color);
		}
    }
}
