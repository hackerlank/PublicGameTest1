using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[InitializeOnLoad]
public class GetLevels : UnityEditor.AssetModificationProcessor{

	static GetLevels(){
		EditorApplication.playmodeStateChanged += Awake;
	}

	public static string[] OnWillSaveAssets(string[] paths){
		Awake();
		return paths;
	}

	static void Awake () {
		if(!Application.isPlaying){
			List<string> sceneURLs = new List<string>();
			EditorBuildSettingsScene[] orignalScenes = EditorBuildSettings.scenes;
			List<EditorBuildSettingsScene> modifiedScenes = new List<EditorBuildSettingsScene>();
			modifiedScenes.AddRange(orignalScenes);

			DirectoryInfo dir = new DirectoryInfo(Application.dataPath);
			List<FileInfo> filesFound = new List<FileInfo>();
			filesFound.AddRange(dir.GetFiles("*.unity", SearchOption.AllDirectories));

			List<EditorBuildSettingsScene> tempList = new List<EditorBuildSettingsScene>();

			for(int i = 0; i < filesFound.Count; i++){
				bool taken = false;
				EditorBuildSettingsScene converted = new EditorBuildSettingsScene(filesFound[i].FullName,true);
				if(modifiedScenes.Count > i){
					foreach(EditorBuildSettingsScene modScene in modifiedScenes){
						if(modScene.path.Replace("/","\\").Equals(filesFound[i].FullName)){
							taken = true;
							break;
						}
					}
				}
				if(!taken)
					tempList.Add(converted);
			}
			tempList.Reverse();
			modifiedScenes.AddRange(tempList);
			EditorBuildSettings.scenes = modifiedScenes.ToArray();

			foreach(EditorBuildSettingsScene s in EditorBuildSettings.scenes){
				sceneURLs.Add(s.path);
			}
			ScenesInfo si = new ScenesInfo();
			si.scenesURL = sceneURLs.ToArray();
			SaveLoadUtility.saveDataToResources<ScenesInfo>(si,"Ehwhat/Levels.txt");
		}
	}



}
