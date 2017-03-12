using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour {

  public AudioSource rocketLaunch;
  public AudioSource playerRocketExplosion;
  public AudioSource enemyRocketExplosion;
  public AudioSource buildingExplosion;

  public static SoundControl instance = null;
  public enum SoundClip { ROCKET_LAUNCH, PLAYER_ROCKET_EXPLOSION, ENEMY_ROCKET_EXPLOSION, BUILDING_EXPLOSION }

  void Awake(){
    instance = this;
  }
	
  public void Play(SoundClip clip) {
    switch(clip){
      case SoundClip.ROCKET_LAUNCH:
        rocketLaunch.Play();
        break;
      case SoundClip.PLAYER_ROCKET_EXPLOSION:
        playerRocketExplosion.Play();
        break;
      case SoundClip.ENEMY_ROCKET_EXPLOSION:
        enemyRocketExplosion.Play();
        break;
      case SoundClip.BUILDING_EXPLOSION:
        buildingExplosion.Play();
        break;
    }
	}
}
