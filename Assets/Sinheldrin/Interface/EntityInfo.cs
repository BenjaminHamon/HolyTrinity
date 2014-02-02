using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin.Interface
{
    [ExecuteInEditMode]
	public class EntityInfo : MonoBehaviourBase
	{
        public Entity Entity;

        public override void Update()
        {
            base.Update();

            if (Entity != null)
            {
                DamageText.text = Damage.ToString();
            }
        }

        private int Damage { get { return Entity.Weapon.Damage; } }
        public TextMesh DamageText;

        private Resource Health { get { return Entity.Health; } }
	}
}
