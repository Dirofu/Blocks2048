using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MeshRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private float _jumpForce;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private List<Material> _materials;

    private MeshRenderer _renderer;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private TMP_Text _scoreText;

    private int _currentMaterial = 0;
    public int Score => _score;
    public bool IsStay { get; private set; }

    private void Start()
    {
        _scoreText = GetComponentInChildren<TMP_Text>();
        _renderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        _collider.enabled = false;
        IsStay = false;
        SetBlockSettings();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Block>().Score == _score)
        {
            IsStay = false;
            _score += _score;
            _scoreText.text = _score.ToString();

            _explosion.Play();
            _currentMaterial++;
            _renderer.material = _materials[_currentMaterial];

            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetComponent<Block>().Score != _score)
        {
            IsStay = true;
        }
    }

    private void SetBlockSettings()
    {
        _currentMaterial = UnityEngine.Random.Range(1, 4);
        _score = (int)Math.Pow(2, _currentMaterial);

        _scoreText.text = _score.ToString();
        _renderer.material = _materials[_currentMaterial];
    }

    public void Falling()
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
    }
}
