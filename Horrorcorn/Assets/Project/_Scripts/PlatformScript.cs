using System;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float platformHeight;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private float platformSpeed;
        
    private bool goingUp = true;

    private void Start()
    {
            
    }

    private void LateUpdate()
    {
            
       
        if (goingUp)
        {
                platformHeight += platformSpeed * Time.deltaTime;
                if (platformHeight >= maxHeight)
                {
                        goingUp = false;
                }
        } else if (!goingUp)
        {
                platformHeight -= platformSpeed * Time.deltaTime;
                if (platformHeight <= minHeight)
                {
                        goingUp = true;
                }
        }


        gameObject.transform.position = new Vector3(gameObject.transform.position.x, platformHeight, gameObject.transform.position.z);
        
    }
}
