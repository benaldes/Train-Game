    using System;
    using UnityEngine;

    public class Movement: CoreComponent
    {
        public Rigidbody2D RB { get; private set; }

        public int FacingDirection { get; private set; }
        public bool CanSetVelocity;
        public Vector2 CurrentVelocity { get; private set; }
        
        private Vector2 workspace;

        protected override void Awake()
        {
            base.Awake();
            RB = GetComponentInParent<Rigidbody2D>();

            FacingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            CurrentVelocity = RB.velocity;
        }

        #region Set Functions

        public void SetVelocityZero()
        {
            workspace = Vector2.zero;
            SetFinalVelocity();
        }
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction,angle.y * velocity);
            SetFinalVelocity();
        }
        public void SetVelocity(float velocity, float angle, int direction)
        {
            Vector2 angleVector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            // Normalize the angle vector to ensure consistent magnitude
            angleVector.Normalize();

            // Calculate the final velocity components based on the angle, velocity, and direction
            float finalVelocityX = angleVector.x * velocity * direction;
            float finalVelocityY = angleVector.y * velocity;

            // Set the final velocity components to your workspace vector or wherever you want
            workspace.Set(finalVelocityX, finalVelocityY);

            // Optionally, you can perform any other operations related to setting the final velocity
            SetFinalVelocity();
        }
        public void SetVelocityX(float velocity)
        {
            workspace.Set(velocity,CurrentVelocity.y);
            SetFinalVelocity();
        }
        public void SetVelocityY(float velocity)
        {
            workspace.Set(CurrentVelocity.x,velocity);
            SetFinalVelocity();
        }
        private void SetFinalVelocity()
        {
            if (CanSetVelocity)
            {
                RB.velocity = workspace;
                CurrentVelocity = workspace;
            }
        }
        
        #endregion
        
        public void CheckIfShouldFlip(int xInput)
        {
            if(xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }
        public void Flip()
        {
            FacingDirection *= -1;
            RB.transform.Rotate(0.0f,180,0.0f);
        }
    }
