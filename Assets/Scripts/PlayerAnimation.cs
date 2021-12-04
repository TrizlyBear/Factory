using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerMain player;

    public Animator animator;

    public bool isRunning = false;

    [Range(0f, 1f)] public float distanceToGround = 0.2f;

    private void Update()
    {
        animator.SetBool("IsRunning", isRunning);
    }

    public void SetValue(string parameter, float value)
    {
        animator.SetFloat(parameter, value);
    }

    public void Trigger(string parameter)
    {
        animator.SetTrigger(parameter);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        /*
        if (animator)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);

            if (Physics.Raycast(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down, out RaycastHit leftHit, distanceToGround + 1f))
            {
                Vector3 footPos = leftHit.point;
                footPos.y += distanceToGround;
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPos);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, leftHit.normal));

            }

            if (Physics.Raycast(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down, out RaycastHit rightHit, distanceToGround + 1f))
            {
                Vector3 footPos = rightHit.point;
                footPos.y += distanceToGround;
                animator.SetIKPosition(AvatarIKGoal.RightFoot, footPos);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, rightHit.normal));

            }
        }
        */
    }
}
