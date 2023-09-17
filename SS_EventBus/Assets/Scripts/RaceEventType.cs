using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RaceEventType 
{
    //These represent specific events outlining the stages of a race
    //  from start to finish
    //We are restricting ourselves to handling events tih just a global
    //  space.

    COUNTDOWN, START, RESTART, PAUSE, STOP, FINISH, QUIT
}
