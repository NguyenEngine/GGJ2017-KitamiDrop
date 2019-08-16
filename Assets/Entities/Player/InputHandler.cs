using UnityEngine;
using System.Collections;

public class InputHandler : Singleton<InputHandler> {

    private string m_trigger = "XboxTriggers",
               m_leftStickX = "XboxHorizontalLeft",
               m_leftStickY = "XboxVerticalLeft",
               m_rightStickX = "XboxHorizontalRight",
               m_rightStickY = "XboxVerticalRight",
               m_bumperLeft = "XboxLeftBumper",
               m_bumberRight = "XboxRightBumper";
               
    public void SetInputStrings(PlayerInput input)
    {
        int playerId = input.GetComponent<PlayerBase>().m_playerId;
        input.m_trigger = this.m_trigger + playerId;
        input.m_leftStickX = this.m_leftStickX + playerId;
        input.m_leftStickY = this.m_leftStickY + playerId;
        input.m_rightStickX = this.m_rightStickX + playerId;
        input.m_rightStickY = this.m_rightStickY + playerId;
        input.m_leftBumper = this.m_bumperLeft + playerId;
        input.m_rightBumper = this.m_bumberRight + playerId;
    }
}
