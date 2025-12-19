using System;
using UnityEngine;
public class Pickup : MonoBehaviour
{

        private float _pickupRotation = 0;
        private float _pickupHeight = 1.1f;
        
        private bool _goingUp = true;

        private void Update()
        {
                _pickupRotation += 0.2f;
                
                if (_goingUp)
                {
                        _pickupHeight += 0.0005f;
                        if (_pickupHeight >= 1.5f)
                        {
                                _goingUp = false;
                        }
                } else if (!_goingUp)
                {
                        _pickupHeight -= 0.0005f;
                        if (_pickupHeight <= 1.1f)
                        {
                                _goingUp = true;
                        }
                }
                
                gameObject.transform.rotation = Quaternion.Euler(90, _pickupRotation, 0);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, _pickupHeight, gameObject.transform.position.z);
        }

        public void PickedUp()
        {
                gameObject.SetActive(false);
        }
}
