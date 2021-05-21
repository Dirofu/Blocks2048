using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BlockMaterial))]
[RequireComponent(typeof(BlockScore))]
public class Block : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private ParticleSystem _explosion;

    private BlockMaterial _blockMaterial;
    private BlockScore _blockScore;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    public bool IsStay { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _blockMaterial = GetComponent<BlockMaterial>();
        _blockScore = GetComponent<BlockScore>();

        _collider.enabled = false;
        IsStay = false;
        SetStartSettings();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Platform>())
        {
            IsStay = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BlockScore>().Score == _blockScore.Score)
        {
            IsStay = false;
            _explosion.Play();

            _blockScore.SetNextScore();
            _blockMaterial.SetNextMaterial();

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetComponent<BlockScore>().Score != _blockScore.Score)
        {
            if (collision.gameObject.GetComponent<Block>().IsStay)
            {
                IsStay = true;
            }
        }
    }

    private void SetStartSettings()
    {
        int startMaterial = _blockMaterial.CurrentMaterial;
        int startScore = (int)Math.Pow(2, ++startMaterial);

        _blockScore.SetStartSettings(startScore);
    }

    public void Falling()
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
    }
}