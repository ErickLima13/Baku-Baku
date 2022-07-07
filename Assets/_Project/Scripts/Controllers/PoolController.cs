using UnityEngine;
using UnityEngine.Pool;

public class PoolController : Singleton<PoolController>
{
    public IObjectPool<GameObject> pool;


    private void Awake()
    {
        pool = new ObjectPool<GameObject>
            (createFunc: () => new GameObject("PooledObject"),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false, defaultCapacity: 10, maxSize: 50);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
