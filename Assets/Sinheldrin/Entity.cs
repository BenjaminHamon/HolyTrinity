using UnityEngine;
using System.Collections;
using SysDiag = System.Diagnostics;
using System;

namespace Sinheldrin
{
    [ExecuteInEditMode]
    public class Entity : MonoBehaviourBase
    {
        #region Initialization

        public override void Start()
        {
            GameManager.Instance.AddEntity(this);
        }

        public override void OnEnable()
        {
            FindComponents();
        }

        [SysDiag.Conditional("EDIT_HELPERS")]
        protected virtual void FindComponents()
        {
            if (Animator == null)
                Animator = GetComponent<Animator>();
            if (Weapon == null)
                Weapon = GetComponentInChildren<Weapon>();
            if (Health == null)
                Health = GetComponentInChildren<Resource>();
            if (Selection == null)
            {
                Selection = transform.Find("Selection").gameObject;
                SelectionSprite = Selection.GetComponent<SpriteRenderer>();
            }
        }

        #endregion // Initialization

        public Animator Animator;
        public bool IsLogEnabled;

        public override void Update()
        {
            if (Application.isPlaying == false)
                return;

            if ((Target == null) || (Target.IsAlive == false))
                SearchTarget();
            if (Target != null)
                ActOnTarget();
            if ((_destination != null) && MoveToward(_destination.Value, _worldMoveSpeed))
                StopMoving();
        }

        protected virtual void ActOnTarget()
        {
            if (Weapon != null)
            {
                if (Weapon.IsInRange(Target))
                {
                    if (Weapon.Attack(Target) && Animator != null)
                    {
                        SetRotation(Target.transform.position);
                        Animator.SetTrigger("Attack");
                    }
                    if (this is Monster)
                        StopMoving();
                }
                else if (this is Monster)
                    StartMovingToward(Target.transform.position);
            }
        }

        public override void LateUpdate()
        {
            if (Application.isPlaying == false)
                return;

            if (IsAlive == false)
                Destroy(gameObject);
        }

        #region Move

        public float MoveSpeed;
        private float _worldMoveSpeed { get { return GameManager.Instance.World.ToWorldSpace(MoveSpeed); } }
        private Vector2? _destination;

        /// <summary>
        /// Sets a destination for the entity.
        /// The entity will start moving toward it at the next update.
        /// </summary>
        /// <param name="point"></param>
        public void StartMovingToward(Vector2 point)
        {
            _destination = point;
            SetRotation(point);
            if (Animator != null)
                Animator.SetBool("IsMoving", true);
        }

        public void StopMoving()
        {
            _destination = null;
            if (Animator != null)
                Animator.SetBool("IsMoving", false);
        }
        
        /// <summary>
        /// Moves toward a destination.
        /// </summary>
        /// <param name="destination">The destination point, in world space.</param>
        /// <param name="speed">The entity speed, in world space.</param>
        /// <returns>True when the destination is reached, false otherwise.</returns>
        private bool MoveToward(Vector2 destination, float speed)
        {
            Vector2 totalMove = _destination.Value - (Vector2)transform.position;
            Vector2 speedVector = new Vector2(totalMove.normalized.x * speed, totalMove.normalized.y * speed);
            speedVector *= Time.deltaTime;

            // If the speed is greater than the total move to the destination,
            // then this means we are reaching the destination at this step so snap to the destination.
            if (totalMove.sqrMagnitude < speedVector.sqrMagnitude)
            {
                transform.position = destination;
                return true;
            }

            transform.Translate(speedVector, Space.World);
            return false;
        }

        public int Direction = 6;

        private static Vector3 NormalScale = new Vector3(1, 1, 1);
        private static Vector3 ReversedScale = new Vector3(-1, 1, 1);

        private void SetRotation(Vector2 lookAt)
        {
            Vector2 diff = lookAt - (Vector2)transform.position;
            // Vector2.Angle returns an angle between 0 and 180,
            // so we use cross to know if the actual angle is between 0 and 180 or 180 and 360.
            float angle = Vector2.Angle(diff, Vector2.right);
            Vector3 cross = Vector3.Cross(diff, Vector2.right);

            if (cross.z > 0)
                angle = 360 - angle;

            int newDirection = (int)((angle + 22.5f) / 45f);
            if (newDirection != Direction)
            {
                Direction = newDirection;
                if (Animator != null)
                    Animator.SetTrigger("Rotate");
                transform.localScale = (Direction >= 3) && (Direction <= 5) ? ReversedScale : NormalScale;
                if (Animator != null)
                    Animator.SetInteger("Rotation", Direction);
            }
        }

        #endregion // Move

        public Resource Health;
        public bool IsAlive { get { return Health.Current > 0; } }

        public Weapon Weapon;
        public Faction Faction;
        public Entity Target;

        protected virtual void SearchTarget()
        {
            Target = GameManager.Instance.GetNearestEnemy(this);
        }

        #region Selection

        public GameObject Selection;
        public SpriteRenderer SelectionSprite;

        public void Select(Faction selectedBy)
        {
            SelectionSprite.color = Faction.GetRelationWith(selectedBy).GetColor();
            Selection.SetActive(true);
        }

        public void Unselect()
        {
            Selection.SetActive(false);
        }

        #endregion // Selection

        public override string ToString()
        {
            return base.ToString() + " HP=" + Health.ToShortString() + " Position=" + transform.position;
        }

        public override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            if (EditHelper.Instance.DrawLineToTarget && (Target != null))
                Gizmos.DrawLine(transform.position, Target.transform.position);
        }

        public override void OnDestroy()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.RemoveEntity(this);
        }
    }
}