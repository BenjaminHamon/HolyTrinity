using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin
{
    [ExecuteInEditMode]
	public class Resource : MonoBehaviourBase
	{
        public int Maximum = 100;
        [SerializeField]
        private int _current = 100;
        public int Current
        {
            get { return _current; }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > Maximum)
                    value = Maximum;
                _current = value;
            }
        }

        private float Percentage { get { return Maximum != 0 ? (float)_current / (float)Maximum : 1; } }
        public bool IsFull { get { return Current >= Maximum; } }

        public override void Update()
        {
            transform.localScale = new Vector3(Percentage, 1, 1);
        }

        public string ToShortString()
        {
            return Current + "/" + Maximum;
        }
	}
}
