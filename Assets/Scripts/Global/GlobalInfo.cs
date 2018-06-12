using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo{

	public class PlayerGlobalInfo
    {
        enum MoveStaete
        {
            OnGround, InAir
        };

        static MoveStaete moveState = MoveStaete.OnGround;

    }  
}
