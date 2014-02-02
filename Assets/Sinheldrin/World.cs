using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin
{
    [ExecuteInEditMode]
	public class World : MonoBehaviourBase
	{
        public float Scale;
        public bool ApplyScaleNow;

        public override void Update()
        {
            if (ApplyScaleNow)
            {
                transform.localScale = new Vector3(Scale, Scale, Scale);
                ApplyScaleNow = false;
            }
        }

        public float ToWorldSpace(float value)
        {
            return (float)value * Scale;
        }
	}
}
