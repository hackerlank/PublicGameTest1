using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public struct ScenesInfo{
	public string[] scenesURL;
}


public class SceneSelectHandler : MonoBehaviour {

	public SceneScrollHandler scrollHandler;

	private int selectedPanel = 0;

	// Use this for initialization
	void Start () {
		ScenesInfo scenes = SaveLoadUtility.loadDataFromResources<ScenesInfo>("Ehwhat/Levels");
		for(int i = 1; i < scenes.scenesURL.Length; i++){
			scrollHandler.addScenePanel(scenes.scenesURL[i]);
		}
	}
	

	public void onPanelSelected(int panelIndex){
		selectedPanel = panelIndex;
	}

	public void loadScene(){
		SceneManager.LoadScene(selectedPanel);
	}

	public string getLocalURL(string url){
		int assetOffset = 0;
		string[] splitURL = url.Split('/');
		foreach(string s in splitURL){
			if(s == "Assets"){
				break; 
			}
			assetOffset++;
		}
		string modURL = "";
		for(int i = assetOffset; i < splitURL.Length; i++){
			modURL += "/" + splitURL[i];
		}
		return modURL.Substring(1);

	}

}
