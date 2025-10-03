using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolExample
{
    public class TestPoolInstance : MonoBehaviour
    {
        public void Initialize()
        {
            gameObject.SetActive(false);
        }

        public void Setup(Vector3 newPosition)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = newPosition;
        }
    }
}
