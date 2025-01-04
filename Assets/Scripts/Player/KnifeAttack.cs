using UnityEngine;

namespace CodeBase.Player
{
    public class KnifeAttack : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;

        private void Start() => Destroy(gameObject, lifeTime);
        private void Update() => transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}