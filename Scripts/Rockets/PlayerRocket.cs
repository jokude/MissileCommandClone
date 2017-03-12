using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : Rocket {

  public PoolObject targetPrefab;
  public Explosion explosionPrefab;
  public float rocketSpeed;
            
  private PoolObject target;

  void Start(){
    speed = rocketSpeed;
  }

  void OnTriggerEnter2D(Collider2D collider){
    if(collider.tag == "Target" && collider.gameObject.GetInstanceID() == target.gameObject.GetInstanceID()){
      Reset();
      ((Explosion) explosionPrefab.GetInstance()).Explode(target.transform.position);
      SoundControl.instance.Play(SoundControl.SoundClip.PLAYER_ROCKET_EXPLOSION);
      target.ReturnToPool();
    }
  }

  public override void Launch(Vector2 toPoint) {
    target = targetPrefab.GetInstance();
    target.transform.position = toPoint;
    base.Launch(toPoint);
  }
 
}
