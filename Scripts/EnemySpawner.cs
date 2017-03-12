using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

  public Camera mainCamera;
  public EnemyRocket prefab;

  private float spawnY = 11;
  private float width = 15;
  private List<Vector2> targets = new List<Vector2>();

  void Awake(){
    width = mainCamera.orthographicSize*mainCamera.aspect - 2.0f;

    foreach (Transform child in GameObject.Find("Launchers").transform) {
      targets.Add(child.position);
    };
    foreach (Transform child in GameObject.Find("Buildings").transform) {
      targets.Add(child.position);
    };
  }

  public void StartDropping(float dropRate, int fatNumber, int thinNumber){
    StartCoroutine(_StartDropping(dropRate, fatNumber, thinNumber));
  }

  IEnumerator _StartDropping(float dropRate, int fatNumber, int thinNumber){

    int[] rockets = { fatNumber, thinNumber };
    EnemyRocket.type type;
    bool isLast;

    while(true){
      isLast = (rockets[0] + rockets[1] == 1);

      if(rockets[0] > 0){
        if(rockets[1] > 0){
          type = Random.Range (0, 1.0f) <= 0.5 ? EnemyRocket.type.FAT : EnemyRocket.type.THIN;
        } else {
          type = EnemyRocket.type.FAT;
        }
      } else {
        type = EnemyRocket.type.THIN;
      }

      if(!DropRocket(type)){
        yield return new WaitForSeconds(1.0f);
        continue;
      }
      LevelControl.instance.UpdateCount(type, --rockets[(int) type]);

      if (isLast) {
        yield break;
      }
      yield return new WaitForSeconds(dropRate);
    }
  }
	
  private bool DropRocket (EnemyRocket.type type) {

    EnemyRocket rocket = prefab.GetInstance(type);
    bool isNotNull = (rocket != null);

    if(isNotNull){
      Vector2 source = new Vector2 (Random.Range (-width, width), spawnY);
      Vector2 target = targets[Random.Range (0, targets.Count)];
      rocket.transform.position = source;
      rocket.transform.rotation = Quaternion.FromToRotation (Vector2.down, target - source);
      rocket.Launch(target);
    }
    return isNotNull;
  }
}
