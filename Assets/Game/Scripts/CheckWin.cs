using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWin : MonoBehaviour
{
    [SerializeField] private int numberOfPlayers;
    [SerializeField] private int index;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Toxic"))
        {
            index++;
            other.GetComponent<PlayerMovement>().inWin = true;
        }

        if (index == numberOfPlayers)
            StartCoroutine(Change());
    }

    private static IEnumerator Change()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerExit(Collider other)
    {
        index--;
        other.GetComponent<PlayerMovement>().inWin = false;
    }
}
