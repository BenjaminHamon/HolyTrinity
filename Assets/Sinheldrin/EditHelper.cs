using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin
{
	public class EditHelper : MonoBehaviourBase
	{
        private static EditHelper _instance;
        public static EditHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (EditHelper)FindObjectOfType(typeof(EditHelper));
                return _instance;
            }
        }

        public bool DrawLineToTarget;
	}
}
