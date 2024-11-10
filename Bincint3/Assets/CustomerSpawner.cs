using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] CustomerPrefabs;
    public CustomerTable[] tables;
    public int groupSizeMin = 1;
    public int groupSizeMax = 2;
    public bool canSpawnExcessCustomer = false;
    public int spawnIntervalSec = 30;

    [SerializeField]
    private float spawnTimer = 0;

    public static List<GameObject> customers = new();

    public void SpawnCustomer()
    {
        int groupSize = GetGroupSize();
        if (!TryFindAvailableTable(groupSize, out CustomerTable newBookedTable))
        {
            print("No available table");
            return;
        }
        
        spawnTimer = 0;

        SpawnCustomerAndBookTable(newBookedTable, groupSize);
    }

    private void SpawnCustomerAndBookTable(CustomerTable table, int groupSize)
    {
        for (int i = 0; i < table.Chairs.Length; i++)
        {
            if (groupSize == 0)
            {
                break;
            }

            if (table.OccupiedChairs[i])
            {
                continue;
            }

            // Spawn customer directly at chair position
            SpawnCustomerGameObject(table.Chairs[i].position, table.Chairs[i]);
            table.OccupiedChairs[i] = true;
            groupSize -= 1;
        }
    }

    private int GetGroupSize() => Random.Range(groupSizeMin, groupSizeMax + 1);

    private GameObject GetRandomCustomerPrefab() => CustomerPrefabs[Random.Range(0, CustomerPrefabs.Length)];

    private void SpawnCustomerGameObject(Vector3 chairPosition, Transform chairTarget)
    {
        var newCustomer = Instantiate(GetRandomCustomerPrefab(), chairPosition, Quaternion.identity);
        Vector3 position = newCustomer.transform.position;
        position.z = -1;
        newCustomer.transform.position = position;
        // CustomerAI agent = newCustomer.GetComponent<CustomerAI>();
        // agent.GotoTable(chairTarget);
        customers.Add(newCustomer);
    }

    public bool TryFindAvailableTable(int lookingForSeats, out CustomerTable availableTable)
    {
        foreach (var table in tables)
        {
            int emptySeats = table.OccupiedChairs.Count(p => !p);
            if (emptySeats >= lookingForSeats)
            {
                availableTable = table;
                return true;
            }
        }

        availableTable = null;
        return false;
    }

    private void Start()
    {
        SpawnCustomer();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnIntervalSec)
        {
            SpawnCustomer();
        }
    }
}
