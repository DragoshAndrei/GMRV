using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Text Score;
	public GameObject YouWon;
	public bool Interactible;

	private int itemsFound;
	private int nrOfActiveImages;
	private bool goodGuess;
	private Color sample;
	private List<GameObject> revealedItems;

	private static GameManager _gameManager;
	public static GameManager Instance
	{
		get
		{
			return _gameManager;
		}
	}

	private void Awake()
	{
		if(_gameManager == null)
		{
			_gameManager = this;
			ResetStats();
			DontDestroyOnLoad(_gameManager.gameObject);
		}
		else
		{
			Instance.ResetStats();
			Destroy(gameObject);
		}
	}

	public void RevealImage(GameObject revealedImage)
	{
		if(revealedItems.Count == 0)
		{
			Color temp = revealedImage.transform.GetChild(0).gameObject.GetComponent<Image>().color;
			sample = new Color(temp.r, temp.g, temp.b);
			goodGuess = true;
		}
		else
		{
			goodGuess = ColorCompare(revealedImage.transform.GetChild(0).gameObject.GetComponent<Image>().color, sample);
		}

		revealedItems.Add(revealedImage);
		nrOfActiveImages++;
		if (!goodGuess || nrOfActiveImages >= 4)
		{
			StartCoroutine(DisplayResult());
		}
	}

	IEnumerator DisplayResult()
	{
		nrOfActiveImages = 0;
		Interactible = false;
		yield return new WaitForSeconds(1);
		if(goodGuess)
		{
			IncrementScore();
		}
		else
		{
			HideObjects();
		}
		revealedItems.Clear();
		sample = new Color(0, 0, 0, 0);
		Interactible = true;
	}

	private bool ColorCompare( Color one, Color two)
	{
		if ((int)(one.a * 1000) != (int)(two.a * 1000)) return false;
		else if ((int)(one.r * 1000) != (int)(two.r * 1000)) return false;
		else if ((int)(one.g * 1000) != (int)(two.g * 1000)) return false;
		else if ((int)(one.b * 1000) != (int)(two.b * 1000)) return false;
		return true;
	}

	private void HideObjects()
	{
		foreach(GameObject image in revealedItems)
		{
			image.transform.GetChild(1).gameObject.SetActive(true);
		}
	}

	public void IncrementScore()
	{
		Score.text = "Score: " + ++itemsFound;
		if(itemsFound >= 4)
		{
			YouWon.SetActive(true);
		}
	}

	public void ResetWorld()
	{
		SceneManager.LoadScene(0);
		ResetStats();
	}

	private void ResetStats()
	{
		itemsFound = 0;
		Interactible = true;
		revealedItems = new List<GameObject>();
		if (Score == null)
		{
			Score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
		}
		if(YouWon == null)
		{
			YouWon = GameObject.FindGameObjectWithTag("Finish");
		}
		Score.text = "Score: " + itemsFound;
		YouWon.SetActive(false);
	}
}
