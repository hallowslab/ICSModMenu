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

public class DangerLock : MonoBehaviour
{
    public float dist;
    public bool lockOpen;
    public GameObject dangerCollider;
    private GameObject _gameObject = new GameObject("DangerLock");
    public GameObject gameObject => _gameObject;
}

public class icstore : MonoBehaviour
{
	public DangerLock firstLock;
	public DangerLock secondLock;
	public DangerLock thirdLock;
	public DangerLock fourthLock;
	public DangerLock fivethLock;
	public DangerLock sixLock;
    public DangerLock kitchenLock;

	public Button btn_firstLock;
	public Button btn_secondLock;
	public Button btn_thirdLock;
	public Button btn_fourthLock;
	public Button btn_fivethLock;
	public Button btn_sixLock;
	public Button btn_Kitchen;

	public GameObject tick1;
	public GameObject tick2;
	public GameObject tick3;
	public GameObject tick4;
	public GameObject tick5;
	public GameObject tick6;
	public GameObject tickKitchen;

	public GameObject lock2;
	public GameObject lock3;
	public GameObject lock4;
	public GameObject lock5;
	public GameObject lock6;
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
