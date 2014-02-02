using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin
{
	public class Weapon : MonoBehaviourBase
	{
        public float Speed;
        public int Damage;
        public int Range;
        private float _worldRange { get { return GameManager.Instance.World.ToWorldSpace(Range); } }

        private float _untilNextAttack;

        public override void Update()
        {
            base.Update();

            if (_untilNextAttack > 0)
                _untilNextAttack -= Time.deltaTime;
        }

        public bool IsInRange(Entity target)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            return distance <= _worldRange;
        }

        public bool Attack(Entity target)
        {
            if (_untilNextAttack > 0)
                return false;
            target.Health.Current -= Damage;
            _untilNextAttack = Speed;
            return true;
        }

        public override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, _worldRange);
        }
	}
}
