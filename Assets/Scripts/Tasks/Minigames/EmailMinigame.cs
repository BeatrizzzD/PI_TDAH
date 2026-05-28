using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class EmailMinigame : MinigameBase
{
    [SerializeField] private TextMeshProUGUI subjectText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private TextMeshProUGUI expectedText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Color matchingColor = Color.green;
    [SerializeField] private Color divergingColor = Color.red;
    [SerializeField] private Color neutralColor = Color.black;

    private static readonly List<EmailScenario> scenarios = new()
    {
        new("RE: Relatório Pendente",
            "Precisamos do relatório mensal até amanhã. Pode confirmar a entrega?",
            "Confirmado, enviarei o relatório finalizado até o fim do dia de amanhã."),
        new("Reunião de Emergência",
            "Todos na sala de conferência às 15h para discutir o novo projeto. Você pode comparecer?",
            "Sim, estarei presente na sala de conferência às 15h conforme solicitado."),
        new("Aprovação de Orçamento",
            "O cliente aprovou o orçamento revisado. Podemos prosseguir com a contratação?",
            "Aprovado, pode seguir com a contratação e me avise quando estiver finalizada."),
    };

    private string expectedAnswer;
    private bool completed;

    protected override void OnMinigameStart()
    {
        var s = scenarios[taskData.Id % scenarios.Count];
        subjectText.text = s.Subject;
        bodyText.text = s.Body;
        expectedText.text = $"Digite: «{s.Expected}»";
        expectedAnswer = s.Expected;
        completed = false;

        inputField.text = string.Empty;
        inputField.textComponent.color = neutralColor;
        inputField.interactable = true;
        inputField.onValueChanged.RemoveAllListeners();
        inputField.onValueChanged.AddListener(OnInputChanged);
        inputField.ActivateInputField();
    }

    protected override void Update()
    {
        base.Update();
        if (timerText) timerText.text = Mathf.Ceil(timeRemaining).ToString();
    }

    private void OnInputChanged(string current)
    {
        if (completed) return;
        string trimmed = current.Trim();
        string expectedTrim = expectedAnswer.Trim();

        if (trimmed.Equals(expectedTrim, System.StringComparison.OrdinalIgnoreCase))
        {
            completed = true;
            inputField.textComponent.color = matchingColor;
            inputField.interactable = false;
            StartCoroutine(DelayedComplete(true, 0.5f));
            return;
        }

        bool isValidPrefix = expectedTrim.StartsWith(trimmed, System.StringComparison.OrdinalIgnoreCase);
        inputField.textComponent.color = trimmed.Length == 0
            ? neutralColor
            : (isValidPrefix ? matchingColor : divergingColor);
    }

    public override void Interrupt()
    {
        completed = true;
        base.Interrupt();
    }

    private IEnumerator DelayedComplete(bool success, float delay)
    {
        yield return new WaitForSeconds(delay);
        Complete(success);
    }

    private class EmailScenario
    {
        public string Subject;
        public string Body;
        public string Expected;

        public EmailScenario(string subject, string body, string expected)
        {
            Subject = subject;
            Body = body;
            Expected = expected;
        }
    }
}
