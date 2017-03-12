using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool:MonoBehaviour {

  private PoolObject prefab;
  private int maxInstances;
  private int instanceCount = 0;
  private Queue<PoolObject> queue = new Queue<PoolObject>();

  public static Pool GetInstance (PoolObject prefab, int maxInstances){
    GameObject obj = new GameObject(prefab.name + " Pool");
    Pool pool = obj.AddComponent<Pool>();
    pool.prefab = prefab;
    pool.maxInstances = maxInstances;
    return pool;
  }

  public PoolObject Get(){
    PoolObject obj = null;

    if(queue.Count > 0){
      obj = queue.Dequeue();
      obj.gameObject.SetActive(true);
    } else if(instanceCount < maxInstances){
      instanceCount++;
      obj = Instantiate<PoolObject>(prefab);
      obj.transform.parent = transform;
      obj.gameObject.SetActive(true);
    }

    return obj;
  }

  public void Add(PoolObject obj){
    obj.gameObject.SetActive(false);
    queue.Enqueue(obj);
  }
}
