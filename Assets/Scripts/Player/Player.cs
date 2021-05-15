using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Block _templateBlock;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _maxDropDelay;
    
    private float _dropDelay;
    private Block _currentBlock;

    private Vector2 _mousePosition;
    private Vector2 _worldPosition;

    private void Start()
    {
        _currentBlock = SpawnNewRandomBlock();
        _currentBlock.transform.position = _spawnPoint.position;
    }

    private void Update()
    {
        _dropDelay -= Time.deltaTime;

        if (_dropDelay <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                _worldPosition = Camera.main.ScreenToWorldPoint(_mousePosition);
                _currentBlock.transform.position = new Vector2(_worldPosition.x, _currentBlock.transform.position.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _currentBlock.Falling();
                _currentBlock = SpawnNewRandomBlock();
                _currentBlock.transform.position = _spawnPoint.position;

                _dropDelay = _maxDropDelay;
            }
        }
    }

    private Block SpawnNewRandomBlock() => Instantiate(_templateBlock);
}
