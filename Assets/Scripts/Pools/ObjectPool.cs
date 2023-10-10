using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int initialAmount = 50;
    [SerializeField] private int bufferAmount = 20;

    private Stack<GameObject> pool = new();

    private void Awake() {
        AddToPool(initialAmount);
    }

    public GameObject GetObject(bool enableOnGet = true) {
        if (pool.Count < bufferAmount) {
            AddToPool(initialAmount);
        }

        GameObject go = pool.Pop();
        if (enableOnGet) go.SetActive(true);

        IPoolable poolable = go.GetComponent<IPoolable>();
        if (poolable != null) poolable.SetObjectPool(this);

        return go;
    }

    public void Return(GameObject go, bool disableOnReturn = true) {
        if (disableOnReturn) go.SetActive(false);
        pool.Push(go);
    }

    private void AddToPool(int amount) {
        for (int i = 0; i < amount; i++) {
            GameObject go = Instantiate(objectPrefab);
            go.transform.SetParent(transform);
            Return(go);
        }
    }
}