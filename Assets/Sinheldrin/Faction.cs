using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sinheldrin
{
    public enum FactionRelation
    {
        None,
        Ally,
        Neutral,
        Enemy,
    }

	public class Faction : MonoBehaviourBase
	{
        public string Name;

        public List<Faction> AllyCollection;
        public List<Faction> NeutralCollection;
        public List<Faction> EnemyCollection;

        public FactionRelation GetRelationWith(Faction other)
        {
            if ((other == this) || (AllyCollection.Contains(other)))
                return FactionRelation.Ally;
            if (NeutralCollection.Contains(other))
                return FactionRelation.Neutral;
            if (EnemyCollection.Contains(other))
                return FactionRelation.Enemy;
            return FactionRelation.None;
        }
	}

    public static class FactionExtensions
    {
        public static Color GetColor(this FactionRelation relation)
        {
            switch (relation)
            {
                case FactionRelation.None: return Color.white;
                case FactionRelation.Ally: return Color.green;
                case FactionRelation.Neutral: return Color.yellow;
                case FactionRelation.Enemy: return Color.red;
                default: return Color.white;
            }
        }
    }
}
