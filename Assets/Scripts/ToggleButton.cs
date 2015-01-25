using UnityEngine;
using System.Collections;

public class ToggleButton : MonoBehaviour {

	public GameObject toggledItem;

	private bool isShow = false;

	void OnMouseDown() {
		toggledItem.SetActive (!isShow);
		isShow = !isShow;
	}
}
