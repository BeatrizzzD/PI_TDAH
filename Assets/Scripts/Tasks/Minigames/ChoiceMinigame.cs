using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ChoiceMinigame : MinigameBase
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private Image[] optionImages;
    [SerializeField] private TextMeshProUGUI[] optionTexts;
    [SerializeField] private TextMeshProUGUI timerText;

    private static readonly List<ChoiceQuestion> questions = new()
    {
        new("Qual o prazo do relatório mensal?",
            new[] { "Amanhã", "Sexta-feira", "Próxima semana", "Hoje" }, 3),
        new("O cliente solicitou reunião para:",
            new[] { "Discutir orçamento", "Apresentar resultados", "Assinar contrato", "Cancelar projeto" }, 1),
        new("Qual setor aprova as despesas?",
            new[] { "RH", "TI", "Financeiro", "Marketing" }, 2),
        new("Quantas cópias do documento enviar?",
            new[] { "1", "2", "3", "4" }, 1),
        new("A reunião de equipe é às:",
            new[] { "9h", "10h", "14h", "15h" }, 2),
    };

    private int correctIndex;

    protected override void OnMinigameStart()
    {
        var q = questions[taskData.Id % questions.Count];
        questionText.text = q.Question;
        correctIndex = q.CorrectIndex;

        for (int i = 0; i < optionButtons.Length && i < q.Options.Length; i++)
        {
            optionImages[i].color = Color.white;
            optionButtons[i].interactable = true;
            int captured = i;
            optionTexts[i].text = q.Options[i];
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(captured));
        }
    }

    protected override void Update()
    {
        base.Update();
        if (timerText) timerText.text = Mathf.Ceil(timeRemaining).ToString();
    }

    private void OnOptionSelected(int index)
    {
        foreach (var btn in optionButtons) btn.interactable = false;
        bool correct = index == correctIndex;
        optionImages[index].color = correct ? Color.green : Color.red;
        if (!correct) optionImages[correctIndex].color = Color.green;
        StartCoroutine(DelayedComplete(correct, 0.8f));
    }

    private IEnumerator DelayedComplete(bool success, float delay)
    {
        yield return new WaitForSeconds(delay);
        Complete(success);
    }

    private class ChoiceQuestion
    {
        public string Question;
        public string[] Options;
        public int CorrectIndex;

        public ChoiceQuestion(string question, string[] options, int correctIndex)
        {
            Question = question;
            Options = options;
            CorrectIndex = correctIndex;
        }
    }
}
