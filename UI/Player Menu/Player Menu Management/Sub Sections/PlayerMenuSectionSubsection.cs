using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerMenuSectionSubsection : MonoBehaviour
{
    [SerializeField]
    protected PlayerMenuSectionSubsection leftNeighboringSubsection;
    [SerializeField]
    protected PlayerMenuSectionSubsection rightNeighboringSubsection;
    [SerializeField]
    protected PlayerMenuSectionSubsection topNeighboringSubsection;
    [SerializeField]
    protected PlayerMenuSectionSubsection bottomNeighboringSubsection; 

    public PlayerMenuSectionSubsection LeftNeighboringSubsection => leftNeighboringSubsection;
    public PlayerMenuSectionSubsection RightNeighboringSubsection => rightNeighboringSubsection;
    public PlayerMenuSectionSubsection TopNeighboringSubsection => topNeighboringSubsection;
    public PlayerMenuSectionSubsection BottomNeighboringSubsection => bottomNeighboringSubsection;
    
    public abstract void StartNavigation();

    public abstract void ResumeNavigation();

    public abstract void Navigate(InputAction.CallbackContext context);

    public abstract void StopNavigation();
}
