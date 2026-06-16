using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ChoiceMinigame : MinigameBase
{
    [SerializeField] private TextMeshProUGUI contextText;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private Image[] optionImages;
    [SerializeField] private TextMeshProUGUI[] optionTexts;
    [SerializeField] private TextMeshProUGUI timerText;

    private static readonly List<ChoiceQuestion> questions = new()
    {
        new("Na reunião da manhã o chefe comentou que o relatório trimestral pode esperar até a próxima semana, que a apresentação para o cliente ficou para sexta, mas que o relatório mensal mesmo, esse não dá para adiar: precisa estar fechado e na mesa dele ainda hoje, antes de você sair.",
            "Qual o prazo do relatório mensal?",
            new[] { "Amanhã", "Sexta-feira", "Próxima semana", "Hoje" }, 3),
        new("O cliente ligou três vezes hoje. Primeiro perguntou sobre o orçamento, depois mencionou que talvez precise revisar o contrato mais para a frente. No fim da ligação, deixou claro o motivo principal do contato: quer uma reunião para que a equipe apresente os resultados obtidos no último trimestre.",
            "O cliente solicitou reunião para:",
            new[] { "Discutir orçamento", "Apresentar resultados", "Assinar contrato", "Cancelar projeto" }, 1),
        new("O processo de despesas envolve várias áreas: o RH confere os dados do funcionário, a TI libera o acesso ao sistema e o Marketing às vezes opina sobre campanhas. Mas quem de fato dá o aval final e aprova o pagamento de qualquer despesa é o setor Financeiro.",
            "Qual setor aprova as despesas?",
            new[] { "RH", "TI", "Financeiro", "Marketing" }, 2),
        new("O memorando que chegou de manhã estava cheio de instruções: assinar na última página, usar papel timbrado e arquivar o original. No meio de tudo isso estava a parte importante: enviar duas cópias do documento, uma para o arquivo interno e outra para o cliente.",
            "Quantas cópias do documento enviar?",
            new[] { "1", "2", "3", "4" }, 1),
        new("A agenda da semana mudou bastante: o café da equipe passou para as 9h, o alinhamento com a diretoria ficou para as 10h e o happy hour é às 15h de sexta. A reunião de equipe em si, depois de remarcada, ficou para as 14h, logo após o horário de almoço.",
            "A reunião de equipe é às:",
            new[] { "9h", "10h", "14h", "15h" }, 2),
    };

    private int correctIndex;

    protected override void OnMinigameStart()
    {
        var q = questions[taskData.Id % questions.Count];
        if (contextText) contextText.text = q.Context;
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
        public string Context;
        public string Question;
        public string[] Options;
        public int CorrectIndex;

        public ChoiceQuestion(string context, string question, string[] options, int correctIndex)
        {
            Context = context;
            Question = question;
            Options = options;
            CorrectIndex = correctIndex;
        }
    }
}
