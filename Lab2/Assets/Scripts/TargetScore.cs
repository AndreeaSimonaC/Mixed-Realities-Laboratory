using UnityEngine;

public class TargetScore : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Dart")
        {
            Vector3 hitPoint = collision.contacts[0].point;

            float distance = Vector3.Distance(
                hitPoint,
                transform.position
            );

            int score;

            if (distance < 0.25f)
            {
                score = 100;
            }
            else if (distance < 0.6f)
            {
                score = 50;
            }
            else
            {
                score = 20;
            }

            scoreManager.AddScore(score);
        }
    }
}