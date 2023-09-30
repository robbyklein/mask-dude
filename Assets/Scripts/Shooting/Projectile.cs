using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable {
    private ObjectPool objectPool;

    private void OnCollisionEnter2D(Collision2D collision) {
        objectPool.Return(gameObject);
    }

    public void SetObjectPool(ObjectPool pool) {
        objectPool = pool;
    }

}
