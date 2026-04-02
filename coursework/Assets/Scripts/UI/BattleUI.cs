using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace TacticalCards
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] private Vector2 panelMargin = new(16f, 16f);
        [SerializeField] private float logPanelWidth = 280f;
        [SerializeField] private float logPanelHeight = 250f;
        [SerializeField] private float handPanelHeight = 112f;
        [SerializeField] private float cardWidth = 108f;
        [SerializeField] private float cardHeight = 78f;
        [SerializeField] private float cardSpacing = 10f;

        private TurnManager turnManager;
        private DeckManager deckManager;
        private CardResolver cardResolver;
        private GridManager gridManager;

        private UnitController selectedUnit;
        private string statusMessage = "Select a player unit.";
        private Vector2 scrollPosition;
        private Vector2 handScrollPosition;
        private readonly List<string> actionLog = new();
        private GUIStyle titleStyle;
        private GUIStyle bodyStyle;
        private GUIStyle cardStyle;
        private GUIStyle selectedCardStyle;
        private GUIStyle smallButtonStyle;
        private GUIStyle cardTitleStyle;
        private GUIStyle cardBodyStyle;

        public CardData SelectedCard { get; private set; }
        private bool IsBattleOver => turnManager != null && turnManager.BattleEnded;

        public void Initialize(
            TurnManager targetTurnManager,
            DeckManager targetDeckManager,
            CardResolver targetCardResolver,
            GridManager targetGridManager)
        {
            turnManager = targetTurnManager;
            deckManager = targetDeckManager;
            cardResolver = targetCardResolver;
            gridManager = targetGridManager;
            statusMessage = "Select a player unit.";
            actionLog.Clear();
            AddLog("Battle UI initialized.");
            Refresh();
        }

        private void Update()
        {
            HandleWorldClick();
        }

        public void Refresh()
        {
            if (gridManager == null)
            {
                return;
            }

            gridManager.ClearHighlights();

            if (IsBattleOver)
            {
                return;
            }

            if (selectedUnit == null || !selectedUnit.IsAlive)
            {
                return;
            }

            if (SelectedCard != null)
            {
                switch (SelectedCard.CardType)
                {
                    case CardType.Move:
                        gridManager.HighlightMoveRange(selectedUnit.GridPosition, SelectedCard.MoveDistance);
                        break;
                    case CardType.Strike:
                    case CardType.Push:
                        gridManager.HighlightAttackRange(selectedUnit.GridPosition, SelectedCard.Range);
                        break;
                }
            }

            gridManager.GetTile(selectedUnit.GridPosition)?.SetSelected();
        }

        public void SelectCard(CardData card)
        {
            if (IsBattleOver)
            {
                ApplyBattleOutcomeStatus(logOutcome: false);
                Refresh();
                return;
            }

            SelectedCard = card;

            if (selectedUnit == null)
            {
                statusMessage = "Select a unit first.";
                AddLog(statusMessage);
                Refresh();
                return;
            }

            if (card != null && card.TargetType == TargetType.Self)
            {
                var currentTile = gridManager?.GetTile(selectedUnit.GridPosition);
                if (currentTile != null)
                {
                    TryPlayCardOnTile(selectedUnit, currentTile);
                }

                return;
            }

            statusMessage = card == null ? "Card selection cleared." : $"Selected {card.CardName}.";
            AddLog(statusMessage);
            Refresh();
        }

        public void ClearSelectedCard()
        {
            SelectedCard = null;
            statusMessage = GetIdleStatusMessage();
            AddLog(statusMessage);
            Refresh();
        }

        public bool TryPlayCardOnTile(UnitController caster, TileView targetTile)
        {
            if (IsBattleOver || SelectedCard == null || turnManager == null || deckManager == null || cardResolver == null || caster == null || targetTile == null)
            {
                return false;
            }

            if (!CanPlayTileCard(caster, targetTile))
            {
                statusMessage = "Invalid tile target.";
                AddLog(statusMessage);
                Refresh();
                return false;
            }

            if (!turnManager.TryConsumeCardPlay())
            {
                statusMessage = "No card plays remaining.";
                AddLog(statusMessage);
                Refresh();
                return false;
            }

            var playedCard = SelectedCard;
            var success = cardResolver.ResolveTileCard(playedCard, caster, targetTile);
            if (!success)
            {
                statusMessage = "Card failed to resolve.";
                AddLog(statusMessage);
                Refresh();
                return false;
            }

            deckManager.DiscardFromHand(playedCard);
            SelectedCard = null;
            var actionSummary = $"{caster.DisplayName} used {playedCard.CardName}.";
            var battleEnded = turnManager.CheckBattleState();
            statusMessage = actionSummary;
            AddLog(actionSummary);
            if (battleEnded)
            {
                ApplyBattleOutcomeStatus(logOutcome: true);
            }

            Refresh();
            return true;
        }

        public bool TryPlayCardOnUnit(UnitController caster, UnitController targetUnit)
        {
            if (IsBattleOver || SelectedCard == null || turnManager == null || deckManager == null || cardResolver == null || caster == null || targetUnit == null)
            {
                return false;
            }

            if (!CanPlayUnitCard(caster, targetUnit))
            {
                statusMessage = "Invalid unit target.";
                AddLog(statusMessage);
                Refresh();
                return false;
            }

            if (!turnManager.TryConsumeCardPlay())
            {
                statusMessage = "No card plays remaining.";
                AddLog(statusMessage);
                Refresh();
                return false;
            }

            var playedCard = SelectedCard;
            var success = cardResolver.ResolveUnitCard(playedCard, caster, targetUnit);
            if (!success)
            {
                statusMessage = "Card failed to resolve.";
                AddLog(statusMessage);
                Refresh();
                return false;
            }

            deckManager.DiscardFromHand(playedCard);
            SelectedCard = null;
            var actionSummary = $"{caster.DisplayName} used {playedCard.CardName} on {targetUnit.DisplayName}. HP: {targetUnit.CurrentHp}/{targetUnit.MaxHp}.";
            var battleEnded = turnManager.CheckBattleState();
            statusMessage = actionSummary;
            AddLog(actionSummary);
            if (battleEnded)
            {
                ApplyBattleOutcomeStatus(logOutcome: true);
            }

            Refresh();
            return true;
        }

        private bool CanPlayTileCard(UnitController caster, TileView targetTile)
        {
            if (SelectedCard == null)
            {
                return false;
            }

            if (SelectedCard.CardType == CardType.Guard)
            {
                return targetTile.Coord == caster.GridPosition;
            }

            foreach (var tile in cardResolver.GetValidTiles(SelectedCard, caster))
            {
                if (tile == targetTile)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CanPlayUnitCard(UnitController caster, UnitController targetUnit)
        {
            if (SelectedCard == null || targetUnit.Team == caster.Team || !targetUnit.IsAlive)
            {
                return false;
            }

            foreach (var tile in cardResolver.GetValidTiles(SelectedCard, caster))
            {
                if (tile != null && tile.Occupant == targetUnit)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetInvalidUnitTargetMessage()
        {
            if (SelectedCard == null)
            {
                return "No card selected.";
            }

            return SelectedCard.CardType switch
            {
                CardType.Strike => $"Target out of range. {SelectedCard.CardName} range is {SelectedCard.Range}.",
                CardType.Push => $"Target out of range. {SelectedCard.CardName} range is {SelectedCard.Range}.",
                _ => "Invalid unit target.",
            };
        }

        private void HandleWorldClick()
        {
            if (Camera.main == null || turnManager == null || !turnManager.IsPlayerTurn || turnManager.BattleEnded)
            {
                return;
            }

            var mouse = Mouse.current;
            if (mouse == null || !mouse.leftButton.wasPressedThisFrame)
            {
                return;
            }

            var ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
            if (!Physics.Raycast(ray, out var hit, 200f))
            {
                return;
            }

            var clickedUnit = hit.collider.GetComponentInParent<UnitController>();
            if (clickedUnit != null)
            {
                if (clickedUnit.Team == TeamType.Player && clickedUnit.IsAlive)
                {
                    selectedUnit = clickedUnit;
                    statusMessage = $"Selected {clickedUnit.DisplayName}.";
                    AddLog($"{clickedUnit.DisplayName} selected at {clickedUnit.GridPosition.x},{clickedUnit.GridPosition.y}.");
                    Refresh();
                    return;
                }

                if (selectedUnit != null && SelectedCard != null)
                {
                    if (!CanPlayUnitCard(selectedUnit, clickedUnit))
                    {
                        statusMessage = GetInvalidUnitTargetMessage();
                        AddLog($"{statusMessage} Caster {selectedUnit.DisplayName}@{selectedUnit.GridPosition.x},{selectedUnit.GridPosition.y}, Target {clickedUnit.DisplayName}@{clickedUnit.GridPosition.x},{clickedUnit.GridPosition.y}.");
                        Refresh();
                    }
                    else
                    {
                        TryPlayCardOnUnit(selectedUnit, clickedUnit);
                    }
                }

                return;
            }

            var clickedTile = hit.collider.GetComponentInParent<TileView>();
            if (clickedTile != null && selectedUnit != null && SelectedCard != null)
            {
                TryPlayCardOnTile(selectedUnit, clickedTile);
            }
        }

        private void OnGUI()
        {
            EnsureStyles();
            DrawTopBar();
            DrawLogPanel();
            DrawHandBar();
        }

        private Rect GetLogPanelRect()
        {
            var width = Mathf.Min(logPanelWidth, Screen.width - (panelMargin.x * 2f));
            var topOffset = 60f;
            var maxHeight = Screen.height - handPanelHeight - (panelMargin.y * 3f) - topOffset;
            var height = Mathf.Min(logPanelHeight, maxHeight);
            return new Rect(panelMargin.x, panelMargin.y + topOffset, Mathf.Max(180f, width), Mathf.Max(180f, height));
        }

        private Rect GetTopBarRect()
        {
            return new Rect(panelMargin.x, panelMargin.y, Mathf.Max(300f, Screen.width - (panelMargin.x * 2f)), 48f);
        }

        private Rect GetHandPanelRect()
        {
            var width = Screen.width - (panelMargin.x * 2f);
            var y = Screen.height - handPanelHeight - panelMargin.y;
            return new Rect(panelMargin.x, y, Mathf.Max(220f, width), handPanelHeight);
        }

        private void DrawTopBar()
        {
            var rect = GetTopBarRect();
            GUI.Box(rect, GUIContent.none);

            var turnLabel = GetTurnLabel();
            var selectedUnitLabel = selectedUnit == null
                ? "Unit: None"
                : $"Unit: {selectedUnit.DisplayName} ({selectedUnit.CurrentHp}/{selectedUnit.MaxHp})";
            var selectedCardLabel = SelectedCard == null
                ? "Card: None"
                : $"Card: {SelectedCard.CardName}";

            GUI.Label(new Rect(rect.x + 12f, rect.y + 8f, 320f, 28f), $"{turnLabel}  |  Plays Left: {turnManager?.RemainingCardPlays ?? 0}", titleStyle);
            GUI.Label(new Rect(rect.x + 12f, rect.y + 26f, 360f, 18f), $"{selectedUnitLabel}  |  {selectedCardLabel}", bodyStyle);

            var clearRect = new Rect(rect.xMax - 220f, rect.y + 10f, 100f, 28f);
            var previousEnabled = GUI.enabled;
            GUI.enabled = !IsBattleOver;
            if (GUI.Button(clearRect, "Clear", smallButtonStyle))
            {
                selectedUnit = null;
                ClearSelectedCard();
            }

            var endTurnRect = new Rect(rect.xMax - 110f, rect.y + 10f, 98f, 28f);
            GUI.enabled = turnManager != null && turnManager.IsPlayerTurn && !turnManager.BattleEnded;
            if (GUI.Button(endTurnRect, "End Turn", smallButtonStyle))
            {
                turnManager?.EndPlayerTurn();
                SelectedCard = null;
                selectedUnit = null;
                statusMessage = turnManager != null && turnManager.BattleEnded
                    ? turnManager.BattleOutcomeSummary
                    : turnManager?.LastEnemyActionSummary ?? "Enemy turn resolved. New player turn started.";
                AddLog(statusMessage);
                Refresh();
            }

            GUI.enabled = previousEnabled;
        }

        private void DrawLogPanel()
        {
            var rect = GetLogPanelRect();
            GUI.Box(rect, GUIContent.none);

            GUILayout.BeginArea(new Rect(rect.x + 10f, rect.y + 10f, rect.width - 20f, rect.height - 20f));
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
            GUILayout.Label("Battle Log", titleStyle);
            GUILayout.Space(6f);
            GUILayout.Label(statusMessage, bodyStyle);
            GUILayout.Space(10f);
            GUILayout.Label("Units", titleStyle);
            DrawUnitSnapshot();
            GUILayout.Space(10f);
            GUILayout.Label("Recent", titleStyle);
            DrawActionLog();
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        private void DrawHandBar()
        {
            var rect = GetHandPanelRect();
            GUI.Box(rect, GUIContent.none);

            GUI.Label(new Rect(rect.x + 12f, rect.y + 8f, 300f, 24f), "Hand", titleStyle);

            if (deckManager == null)
            {
                GUI.Label(new Rect(rect.x + 12f, rect.y + 36f, 200f, 24f), "No deck manager.", bodyStyle);
                return;
            }

            var handSnapshot = new List<CardData>(deckManager.Hand);
            if (handSnapshot.Count == 0)
            {
                GUI.Label(new Rect(rect.x + 12f, rect.y + 36f, 260f, 24f), "No cards in hand.", bodyStyle);
                return;
            }

            var viewRect = new Rect(rect.x + 10f, rect.y + 34f, rect.width - 20f, rect.height - 44f);
            var contentWidth = handSnapshot.Count * (cardWidth + cardSpacing) + cardSpacing;
            var contentRect = new Rect(0f, 0f, Mathf.Max(viewRect.width - 16f, contentWidth), cardHeight + 10f);
            handScrollPosition = GUI.BeginScrollView(viewRect, handScrollPosition, contentRect, false, true);

            for (var i = 0; i < handSnapshot.Count; i++)
            {
                var card = handSnapshot[i];
                if (card == null)
                {
                    continue;
                }

                var cardRect = new Rect(cardSpacing + (i * (cardWidth + cardSpacing)), 0f, cardWidth, cardHeight);
                DrawCardButton(card, cardRect);
            }

            GUI.EndScrollView();
        }

        private void DrawCardButton(CardData card, Rect rect)
        {
            var style = SelectedCard == card ? selectedCardStyle : cardStyle;
            var previousColor = GUI.color;
            var previousEnabled = GUI.enabled;
            GUI.color = GetCardTint(card, SelectedCard == card);
            GUI.enabled = turnManager != null && turnManager.IsPlayerTurn && !turnManager.BattleEnded;
            if (GUI.Button(rect, GUIContent.none, style))
            {
                SelectCard(card);
            }
            GUI.enabled = previousEnabled;
            GUI.color = previousColor;

            var titleRect = new Rect(rect.x + 8f, rect.y + 7f, rect.width - 16f, 18f);
            var typeRect = new Rect(rect.x + 8f, rect.y + 24f, rect.width - 16f, 16f);
            var descRect = new Rect(rect.x + 8f, rect.y + 40f, rect.width - 16f, 18f);
            var statRect = new Rect(rect.x + 8f, rect.y + rect.height - 20f, rect.width - 16f, 14f);

            GUI.Label(titleRect, card.CardName, cardTitleStyle);
            GUI.Label(typeRect, card.CardType.ToString(), cardBodyStyle);
            GUI.Label(descRect, GetCompactCardDescription(card), cardBodyStyle);
            GUI.Label(statRect, GetCardStats(card), cardBodyStyle);
        }

        private string GetCardDescription(CardData card)
        {
            if (!string.IsNullOrWhiteSpace(card.Description))
            {
                return card.Description;
            }

            return card.CardType switch
            {
                CardType.Move => $"Move up to {card.MoveDistance} tiles.",
                CardType.Strike => $"Deal {card.Power} damage.",
                CardType.Push => $"Deal {card.Power} damage and push 1.",
                CardType.Guard => "Reduce the next hit by 1.",
                _ => card.CardType.ToString(),
            };
        }

        private string GetCardStats(CardData card)
        {
            return card.CardType switch
            {
                CardType.Move => $"Move {card.MoveDistance}",
                CardType.Guard => "Self",
                _ => $"Range {card.Range}  Power {card.Power}",
            };
        }

        private string GetCompactCardDescription(CardData card)
        {
            return card.CardType switch
            {
                CardType.Move => $"Move {card.MoveDistance} tiles",
                CardType.Strike => $"Deal {card.Power} damage",
                CardType.Push => $"Hit and push 1",
                CardType.Guard => "Block next hit",
                _ => GetCardDescription(card),
            };
        }

        private Color GetCardTint(CardData card, bool selected)
        {
            var tint = card.CardType switch
            {
                CardType.Move => new Color(0.75f, 0.88f, 0.80f, 1f),
                CardType.Strike => new Color(0.93f, 0.76f, 0.76f, 1f),
                CardType.Push => new Color(0.97f, 0.85f, 0.70f, 1f),
                CardType.Guard => new Color(0.78f, 0.82f, 0.93f, 1f),
                _ => new Color(0.88f, 0.88f, 0.88f, 1f),
            };

            if (selected)
            {
                tint *= 1.1f;
            }

            tint.a = 1f;
            return tint;
        }

        private void AddLog(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            actionLog.Add(message);
            if (actionLog.Count > 12)
            {
                actionLog.RemoveAt(0);
            }
        }

        private string GetTurnLabel()
        {
            if (turnManager == null)
            {
                return "No Battle";
            }

            if (turnManager.BattleEnded)
            {
                return turnManager.WinningTeam == TeamType.Player ? "Victory" : "Defeat";
            }

            return turnManager.IsPlayerTurn ? "Player Turn" : "Enemy Turn";
        }

        private string GetIdleStatusMessage()
        {
            if (IsBattleOver)
            {
                return turnManager.BattleOutcomeSummary;
            }

            return selectedUnit == null ? "Select a player unit." : $"Selected {selectedUnit.DisplayName}.";
        }

        private void ApplyBattleOutcomeStatus(bool logOutcome)
        {
            if (!IsBattleOver)
            {
                return;
            }

            SelectedCard = null;
            selectedUnit = null;
            statusMessage = turnManager.BattleOutcomeSummary;
            if (logOutcome)
            {
                AddLog(statusMessage);
            }
        }

        private void DrawActionLog()
        {
            if (actionLog.Count == 0)
            {
                GUILayout.Label("No actions yet.", bodyStyle);
                return;
            }

            for (var i = actionLog.Count - 1; i >= 0; i--)
            {
                GUILayout.Label(actionLog[i], bodyStyle);
            }
        }

        private void DrawUnitSnapshot()
        {
            if (turnManager == null)
            {
                GUILayout.Label("No turn manager.", bodyStyle);
                return;
            }

            foreach (var unit in turnManager.PlayerUnits)
            {
                DrawUnitLine(unit);
            }

            foreach (var unit in turnManager.EnemyUnits)
            {
                DrawUnitLine(unit);
            }
        }

        private void DrawUnitLine(UnitController unit)
        {
            if (unit == null)
            {
                return;
            }

            var world = unit.transform.position;
            GUILayout.Label($"{unit.DisplayName} HP {unit.CurrentHp}/{unit.MaxHp} Grid {unit.GridPosition.x},{unit.GridPosition.y} World {world.x:0.0},{world.z:0.0}", bodyStyle);
        }

        private void EnsureStyles()
        {
            if (titleStyle != null)
            {
                return;
            }

            titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                wordWrap = true,
                normal = { textColor = new Color(0.95f, 0.95f, 0.95f) }
            };

            bodyStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 11,
                wordWrap = true,
                normal = { textColor = new Color(0.88f, 0.88f, 0.88f) }
            };

            cardStyle = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(8, 8, 8, 8),
                margin = new RectOffset(0, 0, 0, 0),
                alignment = TextAnchor.UpperLeft,
                wordWrap = true,
                normal =
                {
                    background = Texture2D.whiteTexture,
                    textColor = Color.black
                }
            };

            selectedCardStyle = new GUIStyle(cardStyle);
            selectedCardStyle.normal.textColor = new Color(0.1f, 0.1f, 0.1f);

            smallButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 11,
                fontStyle = FontStyle.Bold
            };

            cardTitleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                wordWrap = false,
                clipping = TextClipping.Clip,
                normal = { textColor = new Color(0.14f, 0.14f, 0.14f) }
            };

            cardBodyStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 10,
                wordWrap = false,
                clipping = TextClipping.Clip,
                normal = { textColor = new Color(0.2f, 0.2f, 0.2f) }
            };
        }
    }
}
