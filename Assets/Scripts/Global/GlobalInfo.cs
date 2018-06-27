using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo{

	public class PlayerGlobalInfo
    {
        enum AnimeStaete
        {
            Idle, IdleJump, Run
        };

        static AnimeStaete moveState = AnimeStaete.Idle;

    }  
}
