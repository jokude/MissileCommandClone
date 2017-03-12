using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

  public PlayerRocket prefab;
  private PlayerRocket currentRocket = null;

  public void Initiatilize(){
    gameObject.SetActive(true);
    StartCoroutine("LoadRocket");
  }
    
  void OnTriggerEnter2D(Collider2D collider){
    if(collider.tag == "Enemy"){
      currentRocket.ReturnToPool();
      gameObject.SetActive(false);
      SendMessageUpwards("RemoveLauncher", this);
    }
  }

  public void LookAtPosition (Vector3 lookAt) {
    if(lookAt.y > transform.position.y) {
      lookAt.z = -90;
      transform.LookAt(lookAt, Vector3.forward);
      FollowTransform();
    }
  }

  public void LaunchRocket (Vector2 toPoint) {
    if(currentRocket != null){
      SoundControl.instance.Play(SoundControl.SoundClip.ROCKET_LAUNCH);
      currentRocket.Launch(toPoint);
      currentRocket = null;
      StartCoroutine("LoadRocket");
    }
  }

  private IEnumerator LoadRocket(){
    yield return new WaitForSeconds(0.1f);
    currentRocket = (PlayerRocket) prefab.GetInstance();
    FollowTransform();
    yield return null;
  }

  private void FollowTransform(){
    if(currentRocket != null){
      currentRocket.transform.rotation = transform.rotation;
      currentRocket.transform.position = transform.position;
    }
  }
}
