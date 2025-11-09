namespace Items.Armour;

// singleton pattern is used, since there will be always only one ArmourSetManager
public class ArmourSetManager
{
    private static ArmourSetManager instance = null;

    public static ArmourSetManager GetInstance()
    {
        {
            if (instance == null)
            {
                instance = new ArmourSetManager();
            }
            return instance;
        }
    }

    private Dictionary<string, ArmourSet> registeredSets;
    private Dictionary<string, ArmourSet> activeSets;

    public Dictionary<string, ArmourSet> ActiveSets
    {
        get { return activeSets; }
    }

    public ArmourSetManager()
    {
        this.registeredSets = new Dictionary<string, ArmourSet>();
        this.activeSets = new Dictionary<string, ArmourSet>();
    }

    public void RegisterSet(ArmourSet set)
    {
        if (set != null && !registeredSets.ContainsKey(set.Name))
        {
            registeredSets.Add(set.Name, set);
        }

    }

    public ArmourSet GetSet(string setName)
    {
        if (registeredSets.ContainsKey(setName))
        {
            return registeredSets[setName];
        }
        return null;
    }

    // this method is run to check what are the bonuses availible
    public void CheckAndUpdateSetBonuses(Player player)
    {
        // Dictionary to count how many pieces of each set the player has equipped
        Dictionary<string, int> setCount = new Dictionary<string, int>();



        // Check head armour
        if (player.HeadArmourEquipped != null)
        {
            Armour headArmour = (Armour)player.HeadArmourEquipped;

            // does it have a set name
            if (!string.IsNullOrEmpty(headArmour.SetName))
            {
                // have we seen the same set name in the setCount?
                if (!setCount.ContainsKey(headArmour.SetName))
                {
                    // assigns to the key value of setName number 0 on the first start
                    // then it checks again (setName was already added) and returns for instance : "Leather: 1"
                    setCount[headArmour.SetName] = 0;
                }
                setCount[headArmour.SetName]++;
            }
        }

        // Check torso armour
        if (player.TorsoArmourEquipped != null)
        {
            Armour torsoArmour = (Armour)player.TorsoArmourEquipped;
            if (!string.IsNullOrEmpty(torsoArmour.SetName))
            {
                if (!setCount.ContainsKey(torsoArmour.SetName))
                {
                    setCount[torsoArmour.SetName] = 0;
                }

                // if it already has Leather in it, it will return "Leather: 2"
                setCount[torsoArmour.SetName]++;
            }
        }

        // Check legs armour
        if (player.LegsArmourEquipped != null)
        {
            Armour legsArmour = (Armour)player.LegsArmourEquipped;
            if (!string.IsNullOrEmpty(legsArmour.SetName))
            {
                if (!setCount.ContainsKey(legsArmour.SetName))
                {
                    setCount[legsArmour.SetName] = 0;
                }
                setCount[legsArmour.SetName]++;
            }
        }

        // Check which sets should be active (have all 3 pieces)
        HashSet<string> setsToActivate = new HashSet<string>();
        foreach (var kvp in setCount)
        {
            if (kvp.Value >= 3) // All 3 pieces equipped
            {
                setsToActivate.Add(kvp.Key);
            }
        }

        // Deactivate sets that are no longer complete

        List<string> setsToDeactivate = new List<string>();
        foreach (var kvp in activeSets)
        {
            if (!setsToActivate.Contains(kvp.Key))
            {
                setsToDeactivate.Add(kvp.Key);
            }
        }

        foreach (var setName in setsToDeactivate)
        {
            ArmourSet set = activeSets[setName];
            set.Deactivate(player);
            activeSets.Remove(setName);
        }

        // Activate newly complete sets
        foreach (var setName in setsToActivate)
        {
            if (!activeSets.ContainsKey(setName))
            {
                ArmourSet set = GetSet(setName);
                if (set != null)
                {
                    set.Activate(player);
                    activeSets.Add(setName, set);
                }
            }
        }
    }
}