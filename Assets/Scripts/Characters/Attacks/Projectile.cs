using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable {
    private ObjectPool objectPool;

    private void OnTriggerEnter2D(Collider2D collision) {
        objectPool.Return(gameObject);
    }

    public void SetObjectPool(ObjectPool pool) {
        objectPool = pool;
    }

    int age = 13;
}
