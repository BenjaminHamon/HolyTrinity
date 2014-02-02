using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin.Interface
{
    [ExecuteInEditMode]
	public class PlayerInterface : MonoBehaviourBase
	{
        public Player Player;

        public EntityInfo PlayerInfo;
        public EntityInfo TargetInfo;

        public override void Update()
        {
            base.Update();

            PlayerInfo.Entity = Player.ControlledEntity;
            TargetInfo.Entity = Player.SelectedEntity;
            TargetInfo.gameObject.SetActive(Player.SelectedEntity != null);
        }
	}
}
