# Task 12 — 3 Tipos de Evento + EventManager Completo

**Data:** 2026-05-31  
**Branch:** dev-ana  
**Status:** Aprovado

---

## Contexto

O `EventManager.cs` atual é um stub vazio. `GameEventBase` e `EventManagerLogic` foram implementados na Task 11. A Task 12 adiciona os 3 tipos concretos de evento e o `EventManager` real.

---

## Arquivos Modificados

### `EventData.cs`
Adicionar campo:
```csharp
public bool HasFired;
```
Necessário para o `EventManager` não re-disparar um evento que já foi ativado.

### `GameEventBase.cs`
Adicionar hook `OnEventEnd()` virtual e chamá-lo em `EndEvent()`:
```csharp
protected void EndEvent()
{
    OnEventEnd();
    onComplete?.Invoke();
}
protected virtual void OnEventEnd() { }
```
Permite que subclasses façam limpeza (esconder painel, restaurar alpha, re-habilitar input) sem depender de `OnDisable`.

---

## Novos Arquivos

Todos em `Assets/Scripts/Events/`.

### `DialogueBoxEvent.cs`
Subclasse de `GameEventBase`. Exibe um painel de diálogo que bloqueia visão.

- `[SerializeField] GameObject dialoguePanel`
- `[SerializeField] Button closeButton`
- `OnEventStart()`: ativa `dialoguePanel`, registra `OnCloseClicked` no `closeButton`
- `OnCloseClicked()`: chama `EndEvent()` — encerra o evento antes do timer
- `OnEventEnd()`: desativa `dialoguePanel`, remove o listener do botão

**Comportamento duplo:** o jogador pode fechar clicando no botão OU o evento se encerra automaticamente ao fim de `Duration` via `AutoComplete` em `GameEventBase`.

### `ImpulseWalkEvent.cs`
Subclasse de `GameEventBase`. Força o jogador a andar involuntariamente em direção a um Transform configurável.

- `[SerializeField] Transform impulseTarget` — objeto alvo na cena (ex: geladeira futura)
- `OnEventStart()`: 
  1. Encontra o Player pela tag `"Player"`
  2. Calcula `dir = (impulseTarget.position - player.position).normalized`
  3. Chama `player.ForceMove(dir, currentEvent.Duration)`
- `OnEventEnd()`: chama `player.SetInputEnabled(true)` — segurança caso `EndEvent` seja chamado antes do timer do `ForceMove` terminar
- Guard: se `impulseTarget == null` ou player não encontrado, retorna sem erros

### `TextBlurEvent.cs`
Subclasse de `GameEventBase`. Borra o HUD reduzindo alpha do `CanvasGroup`.

- `[SerializeField] CanvasGroup hudCanvasGroup`
- `OnEventStart()`: `hudCanvasGroup.alpha = 0.25f`
- `OnEventEnd()`: `hudCanvasGroup.alpha = 1f`
- Guard: verificações de null antes de modificar alpha

---

## `EventManager.cs` — Substituição do Stub

Mesmo padrão arquitetural do `MinigameManager`.

```
[SerializeField] DialogueBoxEvent dialogueBoxEvent
[SerializeField] ImpulseWalkEvent impulseWalkEvent
[SerializeField] TextBlurEvent textBlurEvent

Initialize()
  └── events = EventManagerLogic.GenerateEvents()
      isRunning = true

Update()
  ├── Guard: !isRunning, State == null → return
  ├── Guard: IsEventActive → return (um evento por vez)
  └── Loop nos events:
        if !evt.HasFired && TimeElapsed >= evt.TriggerTime
          → TriggerEvent(evt); break

TriggerEvent(evt)
  ├── evt.HasFired = true
  ├── GameManager.OnEventStarted(evt)
  └── GetHandler(evt.Type).StartEvent(evt, callback)
        callback:
          └── GameManager.OnEventEnded()

GetHandler(EventType) → switch expression → DialogueBoxEvent / ImpulseWalkEvent / TextBlurEvent
```

---

## Testes

Nenhum novo teste EditMode — todos os arquivos são MonoBehaviours Unity. TDD se aplica apenas a lógica pura (classes estáticas / data structures), conforme CLAUDE.md.

Os 14 testes existentes não devem quebrar (apenas `EventData` e `GameEventBase` são tocados com adições backwards-compatible).

---

## Fora do Escopo

- Conteúdo textual dos diálogos (texto hardcoded placeholder é suficiente)
- Prefabs e configuração de cena (Task 15)
- Qualquer UI além do painel de diálogo já existente ou a ser criado no Inspector
