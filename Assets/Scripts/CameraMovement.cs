using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float followSpeed = 4f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        
        // transform.position es la posición de la camara
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
