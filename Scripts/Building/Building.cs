using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
  
  void OnTriggerEnter2D(Collider2D collider){
    if(collider.tag == "Enemy"){
      SoundControl.instance.Play(SoundControl.SoundClip.BUILDING_EXPLOSION);
      SendMessageUpwards("UpdateBuildingCount");
      gameObject.SetActive(false);
    }
  }
}