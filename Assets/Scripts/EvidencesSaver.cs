using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidencesSaver : MonoBehaviour
{
    [SerializeField] public int victoryNeededRightVariants = 2;
    [SerializeField]
    public List<int> rightVariants = new List<int>();
    [SerializeField]
    public List<int> choosedVariants = new List<int>();

    [SerializeField]
    public List<EvidenceController> allLevelEvidences = new List<EvidenceController>();
    
    public void ChooseVariant(int evidenceNumber, int choosedVariant)
    {
        choosedVariants[evidenceNumber] = choosedVariant;
    }

    public bool isVictoryNow()
    {
        var rightAnswers = 0;
        for (int i = 0; i < rightVariants.Count; i++)
        {
            if (choosedVariants[i] == rightVariants[i])
            {
                rightAnswers++;
            }
        }

        return rightAnswers >= victoryNeededRightVariants;
    }

    public List<string> getAllChoosedVariants()
    {
        var result = new List<string>();
        for (var i = 0; i < allLevelEvidences.Count; i++)
        {
            var answerIndex = choosedVariants[i];
            if (answerIndex != -1)
            {
                result.Add(allLevelEvidences[i].allEvidences[answerIndex].reasonText);
            }
        }

        return result;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
