using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLayerOrder : MonoBehaviour {

  public string layer = "Default";
  public int order = 0;

  private TextMesh textMesh;

	void Awake() {
    MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
    mesh.sortingLayerName = layer;
    mesh.sortingOrder = order;
    textMesh = gameObject.GetComponent<TextMesh>();
	}

  public void SetText(string text){
    textMesh.text = text;
  }
}
