using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeSnippets.UIToolkitExamples
{
    public class TestAddressables : MonoBehaviour
    {
        [SerializeField] private Transform addressableAnchor;
        [SerializeField] private List<AssetReference> objectAssetReferenceList;

        private GameObject addressabeInstance;

        private AsyncOperationHandle<GameObject> ObjectLoadOpHandle;

        public void LoadAddressable()
        {
            // Destroy current one
            if (addressabeInstance != null) 
            {
                Destroy(addressabeInstance);
                Addressables.ReleaseInstance(ObjectLoadOpHandle);
            }

            // Select a random one from the list
            int randomAddress = Random.Range(0, objectAssetReferenceList.Count);
            if (!objectAssetReferenceList[randomAddress].RuntimeKeyIsValid())
            {
                return;
            }

            ObjectLoadOpHandle = objectAssetReferenceList[randomAddress].LoadAssetAsync<GameObject>();
            ObjectLoadOpHandle.Completed += OnAddressabeLoadComplete;
        }


        private void OnAddressabeLoadComplete(AsyncOperationHandle<GameObject> asyncOperationHandle)
        {
            
            Debug.Log($"AsyncOperationHandle Status: {asyncOperationHandle.Status}");

            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                addressabeInstance = Instantiate(asyncOperationHandle.Result);
                addressabeInstance.transform.position = addressableAnchor.transform.position;
            }
        }

        private void OnDisable()
        {
            ObjectLoadOpHandle.Completed -= OnAddressabeLoadComplete;
        }
    }
}
