using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public CustomerTable[] tables;
    public int groupSizeMin = 1;
    public int groupSizeMax = 2;
    public bool canSpawnExcessCustomer = false;
    public int spawnIntervalSec = 30;

    [SerializeField]
    private float spawnTimer = 0;

    public void SpawnCustomer()
    {
        int groupSize = GetGroupSize();
        if (!TryFindAvailableTable(groupSize, out CustomerTable newBookedTable))
        {
            // A little scuffed since we're essentially bruteforcing until we get a small enough group size.
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

            SpawnCustomerGameObject(GetSpawnLocation(), table.Chairs[i]);
            table.OccupiedChairs[i] = true;
            groupSize -= 1;
        }
    }

    private Vector3 GetSpawnLocation() => Vector3.zero;

    private int GetGroupSize() => Random.Range(groupSizeMin, groupSizeMax + 1);

    private void SpawnCustomerGameObject(Vector3 position, Transform chairTarget)
    {
        var newCustomer = Instantiate(CustomerPrefab, position, Quaternion.identity);
        CustomerAI agent = newCustomer.GetComponent<CustomerAI>();
        agent.GotoTable(chairTarget);
    }

    public bool TryFindAvailableTable(int lookingForSeats, out CustomerTable availableTable)
    {
        print("Try");
        foreach (var table in tables)
        {
            int emptySeats = table.OccupiedChairs.Count(p => p == false);
            print(emptySeats);
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

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnIntervalSec)
        {
            SpawnCustomer();
        }
    }
}
