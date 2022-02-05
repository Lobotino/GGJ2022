using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFinalSceneScript : MonoBehaviour
{
    [SerializeField] public Bubble AndruhaDialog;

    [SerializeField] public EvidencesSaver Saver;
    // Start is called before the first frame update

    [SerializeField]
    public GameController m_GameController;

    void Start()
    {
        StartCoroutine(ShowResults());
    }

    private IEnumerator ShowResults()
    {
        yield return new WaitForSeconds(3);
        yield return AndruhaDialog.ShowTextWithDelay("Ну что же, пришло время подводить итоги расследования", 2);
        yield return AndruhaDialog.ShowTextWithDelay("Ты говоришь...", 2);
        yield return AndruhaDialog.ShowTextWithDelay("Что " + getFirstAnswer(), 2);
        yield return AndruhaDialog.ShowTextWithDelay(getSecondAnswer(), 2);
        yield return AndruhaDialog.ShowTextWithDelay(getThirdAnswer(), 2);

        yield return AndruhaDialog.ShowTextWithDelay("Ну, судя по всему", 2);
        yield return AndruhaDialog.ShowTextWithDelay(Saver.isVictoryNow() ? "Мы его точно найдем" : "Мы никогда его не найдем. Сам то в это веришь?", 2);

        yield return AndruhaDialog.ShowTextWithDelay(Saver.isVictoryNow() ? "Но сначала покурим" : "Пошли покурим и опять работать", 3);

        m_GameController.needToDark = true;
    }

    private string getFirstAnswer()
    {
        switch (Saver.choosedVariants[0])
        {
            case 0: return "преступник не с этой планеты.";
            case 1: return "это местный житель.";
            case 2: return "это погрузчик Петрович.";
            default: return "ты не знаешь откуда он.";
        }
    }

    private string getSecondAnswer()
    {
        switch (Saver.choosedVariants[1])
        {
            case 0: return "И он учёный.";
            case 1: return "И он хочет уничтожить планету";
            case 2: return "И он решил на этом заработать";
            default: return "И ты не знаешь зачем он это сделал";
        }
    }

    private string getThirdAnswer()
    {
        switch (Saver.choosedVariants[1])
        {
            case 0: return "И он из средневековья";
            case 1: return "И он из более развитой цивилизации будущего";
            case 2: return "И он из нашего времени";
            default: return "И ты не знаешь из какой он временной зоны";
        }
    }
}