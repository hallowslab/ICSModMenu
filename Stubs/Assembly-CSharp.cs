#if CI
using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public float hungry;
}

public static class GameLogic
{
    public static void SetMoney(float amount) { }
}

public class CivilManager : MonoBehaviour
{
    public List<Civil> readyToCustomerCivil;
}

public class Civil { }

public class TrashSystem : MonoBehaviour
{
    public GameObject[] room1Trash;
    public GameObject[] room2Trash;
}
#endif