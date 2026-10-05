using UnityEngine;
using Vuforia;

public class CactusProximity : MonoBehaviour
{
    [SerializeField] private ObserverBehaviour target1;
    [SerializeField] private ObserverBehaviour target2;

    [SerializeField] private Animator animator1;
    [SerializeField] private Animator animator2;

    [SerializeField] private Transform cactus1;
    [SerializeField] private Transform cactus2;

    [SerializeField] private float attackDistance = 0.25f;
    [SerializeField] private float rotationOffsetY = 0f;

    private bool IsTracked(ObserverBehaviour target)
    {
        Status status = target.TargetStatus.Status;
        return status == Status.TRACKED || status == Status.EXTENDED_TRACKED;
    }

    private void FaceEachOther()
    {
        Vector3 pos1 = cactus1.position;
        Vector3 pos2 = cactus2.position;

        pos1.y = cactus2.position.y;
        pos2.y = cactus1.position.y;

        cactus1.LookAt(pos2);
        cactus2.LookAt(pos1);

        cactus1.Rotate(0f, rotationOffsetY, 0f);
        cactus2.Rotate(0f, rotationOffsetY, 0f);
    }

    private void Update()
    {
        bool bothTracked = IsTracked(target1) && IsTracked(target2);

        if (!bothTracked)
        {
            animator1.SetBool("isAttacking", false);
            animator2.SetBool("isAttacking", false);
            return;
        }

        FaceEachOther();

        float distance = Vector3.Distance(target1.transform.position, target2.transform.position);
        bool shouldAttack = distance <= attackDistance;

        animator1.SetBool("isAttacking", shouldAttack);
        animator2.SetBool("isAttacking", shouldAttack);
    }
}