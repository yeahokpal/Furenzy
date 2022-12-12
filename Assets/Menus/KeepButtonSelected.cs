/*
 * Programmer: Caden
 * Purpose: Make sure buttons stay highlighted after pressed
 * Input: Player button press
 * Output: N/A
 */
using UnityEngine;
using UnityEngine.EventSystems;

public class KeepButtonSelected : MonoBehaviour
{
    #region Variables
    private EventSystem eventSystem;
    private GameObject lastSelected = null;
    #endregion

    #region Default Methods
    void Start()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (eventSystem != null)
        {
            if (eventSystem.currentSelectedGameObject != null)
            {
                lastSelected = eventSystem.currentSelectedGameObject;
            }
            else
            {
                eventSystem.SetSelectedGameObject(lastSelected);
            }
        }
    }
    #endregion
}