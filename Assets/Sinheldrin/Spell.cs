using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin
{
	public class Spell : MonoBehaviourBase
	{
        public float Cooldown;
        public int Strength;
        public int Range;
        private float _worldRange { get { return GameManager.Instance.World.ToWorldSpace(Range); } }

        private float _untilNextCast;

        public bool IsInRange(Entity target)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            return distance <= _worldRange;
        }

        public void Cast(Entity target)
        {
            _untilNextCast -= Time.deltaTime;
            if (_untilNextCast > 0)
                return;

            Affect(target);

            _untilNextCast = Cooldown;
        }

        protected virtual void Affect(Entity target)
        {
        }

        public override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, _worldRange);
        }
	}
}
