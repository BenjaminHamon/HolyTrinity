using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinheldrin
{
	public class HealingSpell : Spell
	{
        protected override void Affect(Entity target)
        {
            target.Health.Current += Strength;
        }
	}
}
