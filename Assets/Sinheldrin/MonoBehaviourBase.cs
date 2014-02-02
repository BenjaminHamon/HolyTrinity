using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin
{
	public abstract class MonoBehaviourBase : MonoBehaviour
	{
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void OnDestroy() { }

        public virtual void FixedUpdate() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }

        public virtual void OnDrawGizmos() { }
        public virtual void OnDrawGizmosSelected() { }
        public virtual void OnGUI() { }
	}
}
