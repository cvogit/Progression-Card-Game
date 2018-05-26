using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;


/// <summary>
/// File controller.
/// Responsible for saving and loading Save.cs
/// </summary>
public class FileController : MonoBehaviour {

	private Save GameSave;

	private static bool Created = false;

	/// <summary>
	/// Try to load a save file on awake, create a new one if there are none
	/// </summary>
	void Awake() {
		if (!Created)
		{
			DontDestroyOnLoad(this.gameObject);
			Created = true;
		}
		Load ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/diremire.gd")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/diremire.gd", FileMode.Open);
			GameSave = (Save)bf.Deserialize (file);
			file.Close ();
		} else {
			GameSave = new Save ();
		}
	}

	public void Save() {
		if (GameSave == null)
			Load ();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/diremire.gd");
		bf.Serialize(file, GameSave);
		file.Close();
	}

	public Save GetGameSave() {
		return GameSave;
	}

	public void SetGameSave(Save pSave) {
		GameSave = pSave;
	}
}
