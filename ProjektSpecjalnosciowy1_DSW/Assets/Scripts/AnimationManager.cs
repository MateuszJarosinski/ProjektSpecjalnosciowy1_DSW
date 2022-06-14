using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;
    private string _currentState;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (_currentState == newState) return;
        
        _animator.Play(newState);
        _currentState = newState;
    }
}
