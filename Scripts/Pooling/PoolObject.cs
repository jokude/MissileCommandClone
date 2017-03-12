using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject: MonoBehaviour {

  public int maxInstances;
  public Pool parent = null;

  public virtual PoolObject GetInstance(){
    if(parent == null){
      parent = Pool.GetInstance(this, maxInstances);
    }
    return parent.Get();
  }

	public void ReturnToPool () {
    parent.Add(this);
	}
}
