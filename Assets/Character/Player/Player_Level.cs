using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Level : MonoBehaviour
{

    public int CurrentLevel = 0;

    public List<string> Competences = new List<string>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CurrentLevel++;
            foreach (var item in CompetencesTree.ShowCompetencesChoice(this.CurrentLevel))
            {
              Debug.Log(item);   
            }
        }
    }
}

public class CompetencesTree
{
    public static Dictionary<int, List<string>> Competences = new Dictionary<int, List<string>>
    {
        {1, new List<string>(){"Comp1_1", "Comp1_2", "Comp1_3" } },
        {2, new List<string>(){"Comp2_1", "Comp2_2", "Comp2_3" } },
        {3, new List<string>(){"Comp3_1", "Comp3_2", "Comp3_3" } },
    };

    public static List<string> ShowCompetencesChoice(int level = 1)
    {
        List<string> comp;
        CompetencesTree.Competences.TryGetValue(level, out comp);
        return comp;
    }
}
