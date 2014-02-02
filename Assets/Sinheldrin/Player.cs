using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin
{
	public class Player : MonoBehaviourBase
	{
        public override void Update()
        {
            base.Update();

            if (Input.GetMouseButtonDown(0)) // Left click
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    Entity entity = hit.collider.gameObject.GetComponent<Entity>();
                    Select(entity);
                }
            }
            if (Input.GetMouseButtonDown(1)) // Right click
            {
                if (ControlledEntity != null)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider != null)
                        ControlledEntity.StartMovingToward(hit.point);
                }
            }
        }

        public Entity ControlledEntity;

        public Entity SelectedEntity;

        private void Select(Entity entity)
        {
            if (entity != SelectedEntity)
            {
                if (SelectedEntity != null)
                    SelectedEntity.Unselect();
                if (entity != null)
                    entity.Select(ControlledEntity.Faction);
                SelectedEntity = entity;
            }
        }
	}
}
