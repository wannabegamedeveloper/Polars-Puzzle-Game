using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWin : MonoBehaviour
{
    [SerializeField] private int numberOfPlayers;

    private int _index;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Toxic"))
            _index++;

        if (_index == numberOfPlayers)
            StartCoroutine(Change());
    }

    private static IEnumerator Change()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerExit(Collider other)
    {
        _index--;
    }
}
