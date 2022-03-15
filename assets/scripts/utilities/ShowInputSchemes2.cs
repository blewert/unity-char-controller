using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]

public class ShowInputSchemes : MonoBehaviour
{
    /// <summary>
    /// The text element to update with
    /// </summary>
    public TMP_Text textElement;

    /// <summary>
    /// The text element for player movement
    /// </summary>
    public TMP_Text playerMovement;

    /// <summary>
    /// The input action asset
    /// </summary>
    public InputActionAsset inputActionAsset;

    /// <summary>
    /// The player input 
    /// </summary>
    private PlayerInput playerInput;

    /// <summary>
    /// Movement vec
    /// </summary>
    private Vector2 movementVec = default;



    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Set character movement if held
        if (context.performed)
            movementVec = context.ReadValue<Vector2>();

        //Otherwise, reset
        else if (context.canceled)
            movementVec = default;
    }



    // Start is called before the first frame update
    void Start()
    {
        //Get player input
        playerInput = GetComponent<PlayerInput>();
    }

    private void UpdateMovementDebug()
    {
        //Show text
        playerMovement.text = $"<u>{playerInput.currentControlScheme}</u>\n";
        playerMovement.text += $"input move: {movementVec}\n";
        playerMovement.text += $"velocity: {GetComponent<Rigidbody>().velocity}";
    }

    public void Update()
    {
        //Update movement debug text
        UpdateMovementDebug();

        //Schemes
        var controlSchemes = inputActionAsset.controlSchemes;

        //Build string
        string buildString = "INPUT SCHEMES\n";

        foreach(var scheme in controlSchemes)
        {
            if(scheme.name.Equals(playerInput.currentControlScheme))
                buildString += $"- <u>{scheme.name}</u> (active)\n";
            
            else
                buildString += $"- {scheme.name}\n";
        }

        //Set text 
        textElement.text = buildString;
    }
}
