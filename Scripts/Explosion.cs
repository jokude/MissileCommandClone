using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion:PoolObject {

  public float maxRadius;
  public float startRadius;
  public float endRadius;
  public float increaseSteep;

  private TextLayerOrder textLayer;
  private SpriteRenderer spriteRenderer;

  void Awake(){
    textLayer = transform.GetComponentInChildren<TextLayerOrder>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.enabled = false;
  }

  public void Explode(Vector2 position, string text = null){
    if (text != null) {
      textLayer.SetText (text);
    }
    transform.position = position;
    StartCoroutine(_Explode());
  }

  private IEnumerator _Explode(){

    transform.localScale = new Vector2(startRadius, startRadius);
    spriteRenderer.enabled = true;

    while(transform.localScale.x < maxRadius){
      float size = transform.localScale.x + increaseSteep;
      transform.localScale = new Vector2(size, size);
      yield return new WaitForFixedUpdate();
    }

    while(transform.localScale.x > endRadius){
      float size = transform.localScale.x - increaseSteep;
      transform.localScale = new Vector2(size, size);
      yield return new WaitForFixedUpdate();
    }

    spriteRenderer.enabled = false;
    ReturnToPool();
  }
}
