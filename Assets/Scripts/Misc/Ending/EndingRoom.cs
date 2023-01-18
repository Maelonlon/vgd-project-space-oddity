using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingRoom : MonoBehaviour
{
    public GameObject button;

    public CircleCollider2D area;

    public void DestroyButton() => Destroy(button);


    private void Awake()
    {
        if (button)
            SpawnButtonRandom();


    }

    void SpawnButtonRandom()
    {
        Vector2 randomPoint = (Vector2)transform.position + Random.insideUnitCircle * area.radius;
        button.transform.position = randomPoint;

        Vector2 targetRotVector = (transform.position - button.transform.position).normalized;

        button.transform.rotation = Quaternion.FromToRotation(button.transform.up, targetRotVector) * button.transform.rotation;
    }

    private void Update()
    {
        if (button)
            RotateButtonTowardsPortal();

    }

    void RotateButtonTowardsPortal()
    {
        Vector2 targetRotVector = (transform.position - button.transform.position).normalized;

        Quaternion targetRot = Quaternion.FromToRotation(button.transform.up, targetRotVector);

        button.transform.rotation = Quaternion.Slerp(button.transform.rotation, targetRot * button.transform.rotation, Time.deltaTime);
    }

    public void LoadFinalLevel()
    {

        Locator.GetInstance().GetUIManager().FadeIn();
        SaveLoadUtils.SetCurrentLevel(1);
        SaveLoadUtils.SetCurrentSpawnPointIndex(0);
        StartCoroutine(LoadFinalLevelAfter(1f));
    }

    IEnumerator LoadFinalLevelAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Level2");
    }


}
