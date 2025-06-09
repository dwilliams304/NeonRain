using System.Collections.Generic;
using UnityEngine;

namespace Samples.Utility
{
    [System.Serializable]
    public class PooledObject
    {
        [Header("Pool Settings")]
        public GameObject ObjectToPool;
        [Min(1)]
        [SerializeField] private int AmountToPool = 50;
        [SerializeField] private bool PoolCanGrow = false;

        [Header("Initialization")]
        //In the chance that this object may never necessarily be needed
        //Do we want to be able to initialize it later instead of from the start?
        [SerializeField] private bool initializeOnRetrieve = false;

        private const string POOL_PARENT_TAG = "AllObjectPools";
        private const string POOL_SUFFIX = "(s) -- ObjectPool";

        //Nothing is allowed to change this except this script!
        private readonly List<GameObject> currentPool = new List<GameObject>();
        //To ensure that the pool was initialized and that there are objects in the pool
        private bool isInitialized = false;

        private GameObject parentObject;

        private readonly HashSet<GameObject> activeObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set up the pool by instantiating the object and setting them to be inactive
        /// </summary>
        public void InitializePool()
        {
            if (isInitialized) return;
            
            if (ObjectToPool == null)
            {
                Debug.LogError("We don't have an object to pool and cannot initialize it!");
                return;
            }

            isInitialized = true;
            CreatePoolParent();

            for (int i = 0; i < AmountToPool; i++)
            {
                CreateObject();
            }

            Debug.Log($"Initialized pool for {ObjectToPool.name} with {AmountToPool} objects");
        }

        private void CreatePoolParent()
        {
            GameObject globalParentObject = GameObject.FindWithTag(POOL_PARENT_TAG);
            parentObject = new GameObject($"{ObjectToPool.name}{POOL_SUFFIX}");

            if (globalParentObject != null)
            {
                parentObject.transform.SetParent(globalParentObject.transform);
            }

            else Debug.LogWarning($"No GameObject with tag '{POOL_PARENT_TAG}' found. Pool parent will be created at root level");
            
        }

        /// <summary>
        /// Create an object by instantiating it, and adding them to the pool list
        /// </summary>
        /// <param name="andRetrieve">Do we also want to retrieve the freshly created object?</param>
        /// <param name="position">Position for the object</param>
        /// <param name="rotation">Rotation for the object</param>
        /// <returns>The created GameObject</returns>
        private GameObject CreateObject(bool andRetrieve = false, Vector3 position = default, Quaternion rotation = default)
        {
            GameObject pooledObj = GameObject.Instantiate(ObjectToPool);
            pooledObj.transform.SetParent(parentObject.transform);

            currentPool.Add(pooledObj);
            
            if (andRetrieve)
            {
                SetObjectActive(pooledObj, position, rotation);
            }
            else
            {
                pooledObj.SetActive(false);
            }
            
            return pooledObj;
        }

        /// <summary>
        /// Retrieve an object from the pool
        /// </summary>
        /// <param name="position">Position to place the object</param>
        /// <param name="rotation">Rotation to set the object (defaults to identity)</param>
        /// <returns>Retrieved GameObject or null if unavailable</returns>
        public GameObject Retrieve(Vector3 position, Quaternion rotation = default)
        {
            if (!EnsureInitialized()) return null;

            CleanupDestroyedObjects();

            GameObject availableObject = FindInactiveObject();
            if (availableObject != null)
            {
                SetObjectActive(availableObject, position, rotation == default ? Quaternion.identity : rotation);
                return availableObject;
            }

            // Try to grow the pool if allowed
            if (PoolCanGrow)
            {
                return CreateObject(true, position, rotation == default ? Quaternion.identity : rotation);
            }

            Debug.LogWarning($"All objects in pool for {ObjectToPool.name} are active and pool cannot grow!");
            return null;
        }

        /// <summary>
        /// Retrieve an object, setting both object position and rotation to the target Transform
        /// </summary>
        /// <param name="target">Transform to place object at</param>
        /// <returns>Retrieved GameObject or null if unavailable</returns>
        public GameObject Retrieve(Transform target)
        {
            if (target == null)
            {
                Debug.LogError("Target transform is null in Retrieve method");
                return null;
            }

            return Retrieve(target.position, target.rotation);
        }

        private bool EnsureInitialized()
        {
            if (!isInitialized && !initializeOnRetrieve)
            {
                Debug.LogWarning($"Object pool for: {ObjectToPool.name} has not been initialized and initializeOnRetrieve is set to false! Is this intentional?");
                return false;
            }

            if (!isInitialized && initializeOnRetrieve)
            {
                Debug.LogWarning($"We didn't initialize the pool for: {ObjectToPool.name}, but created one after retrieving!");
                InitializePool();
            }

            return true;
        }

        private GameObject FindInactiveObject()
        {
            for (int i = 0; i < currentPool.Count; i++)
            {
                if (currentPool[i] != null && !currentPool[i].activeInHierarchy)
                {
                    return currentPool[i];
                }
            }
            return null;
        }

        private void SetObjectActive(GameObject obj, Vector3 position, Quaternion rotation)
        {
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            activeObjects.Add(obj);
        }

        /// <summary>
        /// Return an object to the pool (deactivate it)
        /// </summary>
        /// <param name="obj">Object to return to pool</param>
        public void ReturnToPool(GameObject obj)
        {
            if (obj == null) return;

            if (currentPool.Contains(obj))
            {
                obj.SetActive(false);
                activeObjects.Remove(obj);
                
                // Reset object to pool parent
                if (parentObject != null)
                {
                    obj.transform.SetParent(parentObject.transform);
                }
            }
            else
            {
                Debug.LogWarning($"Attempted to return object {obj.name} that doesn't belong to this pool");
            }
        }

        private void CleanupDestroyedObjects()
        {
            for (int i = currentPool.Count - 1; i >= 0; i--)
            {
                if (currentPool[i] == null)
                {
                    currentPool.RemoveAt(i);
                }
            }

            // Clean up active objects tracking
            activeObjects.RemoveWhere(obj => obj == null);
        }

        /// <summary>
        /// Get the current number of active objects
        /// </summary>
        public int GetActiveCount()
        {
            CleanupDestroyedObjects();
            return activeObjects.Count;
        }

        /// <summary>
        /// Get the current number of inactive objects
        /// </summary>
        public int GetInactiveCount()
        {
            CleanupDestroyedObjects();
            return currentPool.Count - activeObjects.Count;
        }

        /// <summary>
        /// Get the total pool size
        /// </summary>
        public int GetTotalCount()
        {
            CleanupDestroyedObjects();
            return currentPool.Count;
        }

        /// <summary>
        /// Return all active objects to the pool
        /// </summary>
        public void ReturnAllToPool()
        {
            var activeObjectsCopy = new List<GameObject>(activeObjects);
            foreach (var obj in activeObjectsCopy)
            {
                if (obj != null)
                {
                    ReturnToPool(obj);
                }
            }
        }

        /// <summary>
        /// Destroy the entire pool and clean up
        /// </summary>
        public void DestroyPool()
        {
            foreach (var obj in currentPool)
            {
                if (obj != null)
                {
                    GameObject.Destroy(obj);
                }
            }

            currentPool.Clear();
            activeObjects.Clear();

            if (parentObject != null)
            {
                GameObject.Destroy(parentObject);
            }

            isInitialized = false;
        }

        /// <summary>
        /// Check if the pool has been initialized
        /// </summary>
        public bool IsInitialized => isInitialized;

        /// <summary>
        /// Check if the pool can grow
        /// </summary>
        public bool CanGrow => PoolCanGrow;
    }
}