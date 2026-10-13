using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode _highlightTownHallButton;
    [SerializeField] private KeyCode _flagCreationButton;

    public event Action HighlightTownHallButtonPressed;
    public event Action FlagCreationButtonPressed;

    private void Update()
    {
        if (Input.GetKeyDown(_highlightTownHallButton))
        {
            HighlightTownHallButtonPressed?.Invoke();
        }

        if (Input.GetKeyDown(_flagCreationButton))
        {
            FlagCreationButtonPressed?.Invoke();
        }
    }
}
