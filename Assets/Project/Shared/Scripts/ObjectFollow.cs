using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        offset = _transform.position - target.position;
    }

    void Update()
    {
        _transform.position = target.position + offset;
    }
}
