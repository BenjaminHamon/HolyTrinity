using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin
{
    [ExecuteInEditMode]
	public class GameManager : MonoBehaviourBase
	{
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (GameManager)FindObjectOfType(typeof(GameManager));
                return _instance;
            }
        }

        public World World;

        private HashSet<Entity> _entityCollection = new HashSet<Entity>();

        public void AddEntity(Entity entity)
        {
            //Debug.Log("GameManager.AddEntity " + entity);
            _entityCollection.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            //Debug.Log("GameManager.RemoveEntity " + entity);
            _entityCollection.Remove(entity);
        }

        public Entity GetNearestEnemy(Entity caller)
        {
            if (caller.Faction == null)
            {
                Debug.LogWarning(caller + " has no faction.");
                return null;
            }
            Entity nearest = null;
            float nearestDistance = float.PositiveInfinity;
            foreach (Entity otherEntity in _entityCollection)
            {
                if (otherEntity.Faction == null)
                {
                    Debug.LogWarning(otherEntity + " has no faction.");
                    continue;
                }
                if ((otherEntity != caller) && (caller.Faction.GetRelationWith(otherEntity.Faction) == FactionRelation.Enemy))
                {
                    float distance = Vector2.Distance(caller.transform.position, otherEntity.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearest = otherEntity;
                    }
                }
            }
            return nearest;
        }

        public Entity GetNearestAlly(Entity caller)
        {
            if (caller.Faction == null)
            {
                Debug.LogWarning(caller + " has no faction.");
                return null;
            }
            return GetNearestEntity(caller, entity => caller.Faction.GetRelationWith(entity.Faction) == FactionRelation.Ally);
        }

        /// <summary>
        /// Gets the entity nearest to the calling entity by comparing distance between the transform positions.
        /// The returned entity cannot be the calling entity and satisfies the provided condition.
        /// </summary>
        /// <param name="caller">The entity for which we are searching the nearest entity.</param>
        /// <param name="condition">The condition the result entity must satisfy.</param>
        /// <returns>The nearest entity to caller not being caller and satisfying condition. Can be null.</returns>
        public Entity GetNearestEntity(Entity caller, Func<Entity, bool> condition)
        {
            Entity nearest = null;
            float nearestDistance = float.PositiveInfinity;
            foreach (Entity otherEntity in _entityCollection)
            {
                if ((otherEntity != caller) && condition(otherEntity))
                {
                    float distance = Vector2.Distance(caller.transform.position, otherEntity.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearest = otherEntity;
                    }
                }
            }
            return nearest;
        }
	}
}
