using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Rocket {

  public enum type:int { FAT = 0, THIN = 1 }
  public Explosion explosionPrefab;
  public Sprite[] rocketSkin;
  public float[] rocketSpeed;

  private type _type;
  private SpriteRenderer spriteRenderer;
  private CapsuleCollider2D _collider;

  public override void Awake () {
    _collider = GetComponent<CapsuleCollider2D>(); 
    spriteRenderer = GetComponent<SpriteRenderer>();
    base.Awake();
  }

  public EnemyRocket GetInstance (type type){
    EnemyRocket rocket = (EnemyRocket) base.GetInstance();
    if (rocket != null) {
      rocket.spriteRenderer.sprite = rocketSkin[(int)type];
      rocket.speed = rocketSpeed[(int)type];
      rocket._type = type;
    }
    return rocket;
  }

  void OnTriggerEnter2D(Collider2D collider){
    CheckCollision(collider);
  }

  void OnTriggerStay2D(Collider2D collider){
    CheckCollision(collider);
  }

  private void CheckCollision(Collider2D collider){
    if (collider.tag == "Player" || collider.tag == "PlayerAttack") {
      Explosion explosion = (Explosion)explosionPrefab.GetInstance ();
      if (explosion != null) {
        string points = collider.tag == "PlayerAttack" ? LevelControl.instance.IncreaseScore (_type).ToString () : "";
        explosion.Explode (transform.position, points);
      }
      LevelControl.instance.NotifyExplosion ();
      SoundControl.instance.Play (SoundControl.SoundClip.ENEMY_ROCKET_EXPLOSION);
      Reset();
    }
  }
}