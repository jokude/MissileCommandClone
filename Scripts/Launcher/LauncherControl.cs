using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherControl : MonoBehaviour {

  public Camera mainCamera;
  public Texture2D cursor;

  private int leftZone;
  private int rightZone;
  private float division;
  private List<Launcher> launchers = new List<Launcher>();
  private List<float> zones = new List<float>();

  void Awake(){
    division = Screen.width/transform.childCount;
    Cursor.SetCursor(cursor, new Vector2(cursor.width/2, cursor.height/2), CursorMode.Auto);
  }
    	
	void Update () {

    Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
    Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

    for(int i = 0; i < launchers.Count; i++){
      launchers[i].LookAtPosition(worldPosition);
    }

    if(Input.GetMouseButtonDown(0)){
      for(int i = 0; i < launchers.Count; i++){
        if (mousePosition.x < zones [i]) {
          launchers[i].LaunchRocket(worldPosition);
          break;
        }
      }
    }
	}

  public void RemoveLauncher(Launcher launcher){
    int index = launchers.IndexOf(launcher);
    if (index == 0) {
      zones.RemoveAt(index);
    } else if(index == launchers.Count-1){
      zones.RemoveAt(index-1);
    } else {
      zones[index] = (zones[index-1]+zones[index])/2;
      zones.RemoveAt(index-1);
    }
    launchers.RemoveAt(index);
  }

  public void ActivateLaunchers(){
    for (int i = 0; i < transform.childCount ; i++) {
      launchers.Add(transform.GetChild(i).GetComponent<Launcher>());
      launchers[i].Initiatilize();
      zones.Add((i+1)*division);
    }
  }
}