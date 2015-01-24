using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour {

	public List<Sprite> bgs;

	public SpriteRenderer spriteRenderer;

	private int currentPhase = 0;
	private GameState gameState = GameState.GetInstance();

	void Awake() {
		gameState.OnRemoveGuy += NextPhase;
	}

	public void NextPhase() {
		currentPhase = (currentPhase + 1) % bgs.Count;
		spriteRenderer.sprite = bgs [currentPhase];
	}
}
