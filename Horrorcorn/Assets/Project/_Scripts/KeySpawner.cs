using UnityEngine;
using System.Collections.Generic;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    HashSet<int> numbers = new HashSet<int>();
    void Start()
    {
        RandomNumber();
    }
    
    
    private void RandomNumber()
    {
        while (numbers.Count < 4)
        {
            int randomNum = Random.Range(1, 8);
            numbers.Add(randomNum);
        }
        KeyLocations();
    }
    
    private void KeyLocations()
    {
        foreach (int number in numbers)
        {
            InstantiateKeyAtPosition(number);
        }
    }
    private void InstantiateKeyAtPosition(int position)
    {
        Transform keyTransform = transform.Find("Key" + position);

        if (keyTransform != null)
        {
            Instantiate(keyPrefab, keyTransform.position, keyTransform.rotation, keyTransform);
        }
    }

    
    
}
