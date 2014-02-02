using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinheldrin
{
	public class Healer : Entity
	{
        protected override void FindComponents()
        {
            base.FindComponents();

            if (Spell == null)
                Spell = GetComponentInChildren<HealingSpell>();
        }

        protected override void SearchTarget()
        {
            Target = GameManager.Instance.GetNearestEntity(this,
                entity => (Faction.GetRelationWith(entity.Faction) == FactionRelation.Ally) && (entity.Health.IsFull == false));
        }

        protected override void ActOnTarget()
        {
            base.ActOnTarget();

            if (Spell != null)
            {
                if (Spell.IsInRange(Target))
                {
                    Spell.Cast(Target);
                    if (Target.Health.IsFull)
                        Target = null;
                    StopMoving();
                }
                else
                    StartMovingToward(Target.transform.position);
            }
        }

        public HealingSpell Spell;
	}
}
