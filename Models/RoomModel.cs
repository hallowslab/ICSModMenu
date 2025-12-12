using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ICSModMenu.Models
{   
    public class RoomDefinition
{
    public DangerLock dangerLock;   // The actual in-game lock object (firstLock, secondLock, etc.)
    public Button curButton;        // Button to unlock this room
    public Button nextButton;       // Button to unlock the next room (optional)
    public GameObject Tick;         // Tick mark for this room
    public string PrefsKey;         // PlayerPrefs key, e.g. "buyfirstlock"
    public GameObject nextLock;     // Optional UI overlay for next lock, e.g. lock2
}

    public class RoomManager
    {
        public readonly List<RoomDefinition> Rooms;

        public RoomManager(icstore store)
        {
            Rooms = new List<RoomDefinition>
            {
                new RoomDefinition
                {
                    curButton = store.btn_Kitchen,
                    nextButton = null,       // Kitchen has no next button
                    dangerLock = store.kitchenLock,
                    Tick = store.tickKitchen,
                    nextLock = null,         // No next lock
                    PrefsKey = "buykitchen"
                },
                new RoomDefinition
                {
                    curButton = store.btn_firstLock,
                    nextButton = store.btn_secondLock,
                    dangerLock = store.firstLock,
                    Tick = store.tick1,
                    nextLock = store.lock2,
                    PrefsKey = "buyfirstlock"
                },
                new RoomDefinition
                {
                    curButton = store.btn_secondLock,
                    nextButton = store.btn_thirdLock,
                    dangerLock = store.secondLock,
                    Tick = store.tick2,
                    nextLock = store.lock3,
                    PrefsKey = "buysecondlock"
                },
                new RoomDefinition
                {
                    curButton = store.btn_thirdLock,
                    nextButton = store.btn_fourthLock,
                    dangerLock = store.thirdLock,
                    Tick = store.tick3,
                    nextLock = store.lock4,
                    PrefsKey = "buythirdlock"
                },
                new RoomDefinition
                {
                    curButton = store.btn_fourthLock,
                    nextButton = store.btn_fivethLock,
                    dangerLock = store.fourthLock,
                    Tick = store.tick4,
                    nextLock = store.lock5,
                    PrefsKey = "buyfourthlock"
                },
                new RoomDefinition
                {
                    curButton = store.btn_fivethLock,
                    nextButton = store.btn_sixLock,
                    dangerLock = store.fivethLock,
                    Tick = store.tick5,
                    nextLock = store.lock6,
                    PrefsKey = "buyfivethlock"
                },
                new RoomDefinition
                {
                    curButton = store.btn_sixLock,
                    nextButton = null,
                    dangerLock = store.sixLock,
                    Tick = store.tick6,
                    nextLock = null,
                    PrefsKey = "buysixthlock"
                },
            };
        }
    }

}