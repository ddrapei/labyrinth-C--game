namespace Items.Armour;
using Pastel;

// singleton pattern is used, since there will be always only one ArmourSetManager
public class ArmourSetManager
{
    private static ArmourSetManager? instance = null;

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

    public ArmourSet? GetSet(string setName)
    {
        if (registeredSets.ContainsKey(setName))
        {
            return registeredSets[setName];
        }
        return null;
    }
    
    // ChatGPT and Deepseek were used to better understand how dictionaries work in that scenario, how to properly iterate over them
    // and access key and values
    // https://chatgpt.com/share/692989f5-5348-8010-84e9-a7ef4b2baab7
    // https://chat.deepseek.com/share/le6v61zxdyv7xak5bp
    // https://chatgpt.com/share/69298c31-92c8-8010-839b-042a70e466e7
    // https://chatgpt.com/share/692acf71-73d8-8010-a655-470b5812722f
    // https://chatgpt.com/share/692ad71c-29f8-8010-8e8f-35b402bcb6b9
    // https://www.kimi.com/share/19acf6cd-7f72-80ac-8000-0000675f471d 
    // that playground was used for those purposes
    // https://www.programiz.com/online-compiler/3O0hJE7nDKQoR
    // https://www.programiz.com/online-compiler/6Z24L0B9Xc8QO




    // this method is run to check what are the bonuses availible
    public void CheckAndUpdateSetBonuses(Player player)
    {
        // Dictionary to count how many pieces of each set the player has equipped
        Dictionary<string, int> setCount = new Dictionary<string, int>();
    //  Dictonary<Armour.SetName, number of pieces equipped>



        // Check head armour
        // if something is equipped
        if (player.HeadArmourEquipped != null)
        {
            // takes what is equipped and stores it locally in a variable
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

                    // { "Armour.SetName": 0 }
                }
                setCount[headArmour.SetName]++;                
                // { "Armour.SetName": 1 }
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
                // { 
                // "Armour.SetName": 1 
                // "Armour.SetName2": 1
                // }
                // or (if the set is the same as head)
                // {
                // "Armour.SetName": 2
                // }
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

        // displays the values from the dictionary
        foreach (var kvp in setCount)
        {
            Console.WriteLine($"Set: {kvp.Key.Pastel("#255732")}, Pieces Equipped: {kvp.Value.ToString().Pastel("#57253d")}");
        }


        //setsToActivate and setsToDeactivateare required since
        //it wasn't allowing me to loop through and add or remove sets
        //simultaneously, the game was crushing

        // empty HashSet (unique values and we don't care about the order inside of it)
        HashSet<string> setsToActivate = new HashSet<string>();

        // Check which sets should be active (have all 3 pieces)
        // Iterates over each key-value-pair 
        // if one of the keys has the value 3 or higher adds to HashSet
        foreach (var kvp in setCount)
        {
            if (kvp.Value >= 3) // All 3 pieces equipped
            {
                setsToActivate.Add(kvp.Key);
            }
        }

        HashSet<string> setsToDeactivate = new HashSet<string>();
        
        // Deactivate sets that are no longer complete
        // iterates over active sets
        foreach (var kvp in activeSets)
        {
            if (!setsToActivate.Contains(kvp.Key))
            {
                setsToDeactivate.Add(kvp.Key);
            }
        }


        // removes ones the set is not full
        foreach (var setName in setsToDeactivate)
        {
            if (activeSets.TryGetValue(setName, out ArmourSet? set) && set != null)
            {
                set.Deactivate(player);         // deactivates set
                activeSets.Remove(setName);     // removes from the dictionary
            }
        }

        // Activate newly completed sets
        foreach (var setName in setsToActivate)
        {
            if (!activeSets.ContainsKey(setName))
            {
                ArmourSet? set = GetSet(setName);
                if (set != null)
                {
                    set.Activate(player);
                    activeSets.Add(setName, set);
                }
            }
        }
    }
}