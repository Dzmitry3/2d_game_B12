using UnityEngine;
using Enemies;
using System.Collections;

public class BulletsMoveComponent : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private float _damage = 1f;
    private int _direction = 1;
    private bool _isExploding;

    private void OnEnable()
    {
        _isExploding = false;
        var animator = GetComponent<Animator>();
        animator.Play("Shot", 0, 0f);
        CancelInvoke();
        Invoke(nameof(Deactivate), _lifeTime);
    }

    public void Init(int direction)
    {
        _direction = direction;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        var animator = GetComponent<Animator>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            animator.SetTrigger("Explode");
            CancelInvoke(nameof(Deactivate));
            _isExploding = true;
            float explodeLength = GetAnimationClipLength(animator, "Explode");
            StartCoroutine(DisableAfter(explodeLength));
        }
    }
    
    IEnumerator DisableAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    
    float GetAnimationClipLength(Animator animator, string clipName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return clip.length;
        }
        return 0f;
    }

    
    void Update()
    {
        if (_isExploding) return;
        transform.Translate(Vector3.right * _direction * (Time.deltaTime * _speed));
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}