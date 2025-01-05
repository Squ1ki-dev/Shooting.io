using UnityEngine;

namespace CodeBase.Player
{
    public class KnifeAttack : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private PlayerStatsSO _playerConfig;

        private void Start() => Destroy(gameObject, lifeTime);
        private void Update() => transform.Translate(Vector3.forward * speed * Time.deltaTime);

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<EnemyHealth>())
            {
                Debug.Log($"Enemy has this comp");
                if (other.gameObject.TryGetComponent<IHealth>(out IHealth health))
                {
                    health.TakeDamage(_playerConfig.KnivesDamage);
                    ObjectPool.ReturnToPool(gameObject);
                }
            }
        }
    }
}