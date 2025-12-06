#if CI
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// Game classes
public class MoneyTaker : MonoBehaviour
{
    public static MoneyTaker Instance;

    public void GenerateMoneyTaker(float value) { }
}

public class TrashSystem: MonoBehaviour
{
    public GameObject[] room1Trash;
    public GameObject[] room2Trash;
    public GameObject[] room3Trash;
    public GameObject[] room4Trash;
    public GameObject[] room5Trash;
    public GameObject[] room6Trash;
    public GameObject[] room7Trash;
    public GameObject[] room8Trash;
    public GameObject[] room9Trash;
}

public class PlayerStats : MonoBehaviour
{
    public float hungry;
}

public class CivilManager : MonoBehaviour { }

public class WorkersPanel : MonoBehaviour
{
    public Button buyChefButton;
    public Button buybodyguardButton;
    public GameObject chef;
    public GameObject bodyguard;
}

public class BeggarManager : MonoBehaviour { }
public class ThiefManager : MonoBehaviour { }
public class SaveManager : Object { }
#endif
