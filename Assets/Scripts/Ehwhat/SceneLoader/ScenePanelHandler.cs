using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ScenePanelHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public Color textSelectedColour = Color.black;
	public Color panelSelectedColour = Color.white;
	public Color textHoveredColour = Color.white;
	public Color panelHoveredColour = Color.gray;

	private Color panelOrignalColor;
	private Color textOrignalColor;

	public Text scenePanelURL;
	public Text scenePanelName;
	public Image scenePanelImage;

	public SceneScrollHandler parentHandler;

	private bool selected = false;

	// Use this for initialization
	void Start () {
		panelOrignalColor = scenePanelImage.color;
		textOrignalColor = scenePanelName.color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setName(string name){
		scenePanelName.text = name;
	}

	public void setURL(string url){
		scenePanelURL.text = url;
	}

	public void OnPointerEnter(PointerEventData pointerData){
		if(!selected){
			scenePanelImage.color = panelHoveredColour;
			scenePanelName.color = textHoveredColour;
			scenePanelURL.color = textHoveredColour;
		}
	}

	public void OnPointerExit(PointerEventData pointerData){
		if(!selected){
			scenePanelImage.color = panelOrignalColor;
			scenePanelName.color = textOrignalColor;
			scenePanelURL.color = textOrignalColor;
		}
	}

	public void OnPointerClick(PointerEventData pointerData){
		if(parentHandler != null){
			parentHandler.onPanelSelect(this);
		}
		scenePanelImage.color = panelSelectedColour;
		scenePanelName.color = textSelectedColour;
		scenePanelURL.color = textSelectedColour;
		selected = true;
	}

	public void deselectPanel(){
		scenePanelImage.color = panelOrignalColor;
		scenePanelName.color = textOrignalColor;
		scenePanelURL.color = textOrignalColor;
		selected = false;
	}

}
