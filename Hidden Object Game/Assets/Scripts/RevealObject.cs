using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealObject : MonoBehaviour
{

    public void RevealImage()
	{
		if (GameManager.Instance.Interactible)
		{
			gameObject.transform.GetChild(1).gameObject.SetActive(false);
			GameManager.Instance.RevealImage(gameObject);
		}
	}
}
