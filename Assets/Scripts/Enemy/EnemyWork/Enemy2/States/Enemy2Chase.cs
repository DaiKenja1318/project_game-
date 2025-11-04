using UnityEngine;

namespace Enemy.EnemyWork.Enemy2.States
{
    public class Enemy2Chase : Enemy2State
    {
        [SerializeField] private float attackDistance = 5f;

        public override Enemy2State State(Enemy2Controller controller)
        {
            if (Vector2.Distance(controller.transform.position, controller.target.position) < attackDistance)
            {
                controller.animator.SetBool("Attack", true);
                controller.InvokeRepeating("HandleShooting", controller.shootingTime, controller.shootingDelay);
                return controller.attack;
            }

            //Chase Codes
            if (controller.TargetInDistance() && controller.followEnabled)
            {
                controller.PathFollow();
            }

            return this;
        }
    }
}
