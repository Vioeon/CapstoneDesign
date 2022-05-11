using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingDisplay : MonoBehaviour
{
    private const byte FIRE = 0, INTERACT = 1, MOVE = 2;
    [SerializeField] private InputActionReference[] keyValues; //Fire, Interact, Move
    [SerializeField] private PlayerMovement playerMovement;
    private Text uiText;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    // Start is called before the first frame update
    void Start()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if(!string.IsNullOrEmpty(rebinds))
            playerMovement._playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        // TODO : Move to another script which loads on start of the game
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRebinding(int index, int subindex, GameObject buttonObject)
    {
        buttonObject.SetActive(false);
        uiText = buttonObject.gameObject.GetComponentInChildren<Text>();
        Debug.Log($"{index} {subindex}");
        Debug.Log(InputControlPath.ToHumanReadableString(keyValues[index].action.bindings[keyValues[index].action.GetBindingIndexForControl(keyValues[index].action.controls[subindex<0?0:subindex])].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice));

        playerMovement._playerInput.SwitchCurrentActionMap("Menu");

        if(index != 2)
            rebindingOperation = keyValues[index].action.PerformInteractiveRebinding()
                .WithExpectedControlType("Keyboard").OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete(index, 0, buttonObject)).Start();
        // TODO : How to combine with wasd move?
        else
        {
            buttonObject.SetActive(true);
            keyValues[index].action.PerformInteractiveRebinding(subindex)
                .WithExpectedControlType("Keyboard").OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete(index, subindex, buttonObject)).Start();
        }
    }

    private void RebindComplete(int index, int subindex, GameObject buttonObject)
    {
        int bindex = keyValues[index].action.GetBindingIndexForControl(keyValues[index].action.controls[subindex]);
        uiText.text = InputControlPath.ToHumanReadableString(keyValues[index].action.bindings[bindex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);  //TODO

        rebindingOperation.Dispose();

        var rebinds = playerMovement._playerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);


        playerMovement._playerInput.SwitchCurrentActionMap("Player");

        buttonObject.SetActive(true);
    }

}
