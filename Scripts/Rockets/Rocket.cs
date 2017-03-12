using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket:PoolObject {

  protected float speed;
  protected Rigidbody2D body;

  public virtual void Awake () {
    body = GetComponent<Rigidbody2D>();
  }

  public virtual void Launch(Vector2 toPoint){
    StartCoroutine("Move", toPoint);
  }

  IEnumerator Move(Vector2 toPoint) {
    Vector2 direction = (toPoint - body.position).normalized;
    while(true){
      body.MovePosition(body.position + direction * speed * Time.deltaTime);
      yield return new WaitForFixedUpdate();
    }
  }

  public void Reset(){
    StopCoroutine("Move");
    ReturnToPool();
  }
    
}
