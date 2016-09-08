using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class SceneScrollHandler : MonoBehaviour {

	public SceneSelectHandler parentHandler;

	public GameObject container;
	public GameObject scenePanelPrefab;

	public float leftOffset = -50;
	public float containerMinSize = 400;
	public int debugSpawn = 3;

	public ScenePanelHandler selectedPanel;

	private List<ScenePanelHandler> scenePanels = new List<ScenePanelHandler>();
	private RectTransform containerRectTransform;

	// Use this for initialization
	void Start () {
		containerRectTransform = container.GetComponent<RectTransform>();
		for(int i = 0; i < debugSpawn; i++)
			addScenePanel("debug");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onPanelSelect(ScenePanelHandler selected){
		if(selectedPanel != null){
			selectedPanel.deselectPanel();
		}
		selectedPanel = selected;
		parentHandler.onPanelSelected(scenePanels.IndexOf(selected)+1);
	}

	public void addScenePanel(string url){
		ScenePanelHandler currentPanel = ((GameObject)Instantiate(scenePanelPrefab,container.transform)).GetComponent<ScenePanelHandler>();
		positionScenePanel(currentPanel);

		currentPanel.parentHandler = this;
		currentPanel.name = name;

		currentPanel.setName(getNameFromUrl(url));
		currentPanel.setURL(getLocalURL(url));

		scenePanels.Add(currentPanel);
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
		return modURL;

	}

	public string getNameFromUrl(string url){
		string[] splitURL = url.Split('/');
		return splitURL[splitURL.Length-1].Substring(0, splitURL[splitURL.Length-1].Length-6);
	}

	void positionScenePanel(ScenePanelHandler currentPanel){
		RectTransform rt = currentPanel.GetComponent<RectTransform>();
		if(containerRectTransform.sizeDelta.y > 0 && containerRectTransform.sizeDelta.y < ((scenePanels.Count*60)+30)){
			containerRectTransform.sizeDelta += new Vector2(0, ((scenePanels.Count*60)+60)-containerRectTransform.sizeDelta.y);
		}
		rt.anchoredPosition = new Vector2(0,-((scenePanels.Count*60)+30));
	}

}
