using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo{

    public enum ChMoveState
    {
        Idle, IdleJump, Run, RunJump
    };

    public enum ChPlaceState
    {
        OnGround, InAir
    };

    public class PlayerGlobalInfo
    {
        public static ChMoveState moveState = ChMoveState.Idle;
        public static ChPlaceState placeState = ChPlaceState.OnGround;
    }  
}
