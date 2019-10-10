using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWorld : MonoBehaviour
{
	public void ReloadScene()
	{
		GameManager.Instance.ResetWorld();
	}
}
