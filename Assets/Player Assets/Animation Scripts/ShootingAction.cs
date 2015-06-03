using UnityEngine;
using System.Collections;

public class ShootingAction : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        animator.SetBool("Shoot", false);

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player.GetComponent<PlayerController>() == null);
        PlayerController playerController = player.GetComponent<PlayerController>();
        Debug.Log(playerController.GetCurrentProjectile() == null);
        Transform projectile = Instantiate(playerController.GetCurrentProjectile());
        playerController.SetDamageAmount(projectile);

        playerController.fireShootSource.Play();

        projectile.
            GetComponent<ProjectileController>().Fire(player.position,
                                                      (playerController.directionIsRight ? PlayerController.RIGHT : PlayerController.LEFT),
                                                      playerController.GetProjectileXOffset());
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //    
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
