using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

/// <summary>
/// Game controller.
/// The heart of the game, direct all data flow and scene changes.
/// </summary>
public class GameController : MonoBehaviour {

	public FileController 	gFile;

	private Save SaveData = null;
	private int CurrentAdventure;
	private int CurrentMap;

	private static bool Created = false;
	private List<GameObject> Enemies;
	private string SceneBeforeMatch;

	// Set up when a scene is loaded
	void Awake() {
		if (!Created)
		{
			DontDestroyOnLoad(this.gameObject);
			Created = true;
		}
	}

	/// <summary>
	/// Load Game Save file if current session is null
	/// </summary>
	void Start() {
		if (SaveData == null)
			SaveData = gFile.GetGameSave ();

		CurrentMap	 = SaveData.CurrentMap;
		CurrentAdventure = SaveData.CurrentAdventure;
	}

	/// <summary>
	/// Raises the application quit event.
	/// Fetch all important session data and save it on disk
	/// </summary>
	void OnApplicationQuit() {
		gFile.SetGameSave (SaveData);
		gFile.Save ();
	}

	/// <summary>
	/// Saves the session.
	/// </summary>
	public void SaveSession() {
		gFile.SetGameSave (SaveData);
		gFile.Save ();
	}

	public Player_Data GetPlayer() {
		return SaveData.Player;
	}

	public int GetAdventureProgress(int pAdventureIndex) {
		return SaveData.AdventureProgress[pAdventureIndex];
	}

	public List<int> GetMapProgress() {
		return SaveData.MapProgress;
	}

	/// <summary>
	/// Store match scene set up data and change scene.
	/// </summary>
	/// <param name="pSceneName">P scene name.</param>
	/// <param name="pEnemies">P enemies.</param>
	public void EnterFightScene(string pSceneName, string pSceneBeforeMatch, List<GameObject> pEnemies, int pAdventureIndex, int pMatchIndex) {
		Enemies = pEnemies;
		SceneController.ChangeScene (pSceneName);
		SceneBeforeMatch = pSceneBeforeMatch;
		CurrentAdventure = pMatchIndex;
		CurrentMap 		 = pAdventureIndex;
	}

	public void UnlockNewMatch() {
		
	}

	public List<GameObject> GetSceneEnemies() {
		return Enemies;
	}

	public void ChangeToSceneBeforeMatch() {
		SceneController.ChangeScene (SceneBeforeMatch);
	}

	public void SetSceneBeforeMatch(string pScene) {
		SceneBeforeMatch = pScene;
	}
}
