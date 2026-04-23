using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace TacticalCards
{
    public class BattleUI : MonoBehaviour
    {
        private const float RuntimeTopBarHeight = 148f;
        private const float RuntimeLeftPanelBottomInset = 148f;
        private const float RuntimeBottomPanelTopInset = 136f;
        private const float RuntimeBottomPanelBottomInset = 8f;
        private const float RuntimeHudButtonStripWidth = 236f;
        private const float RuntimeCardCellWidth = 152f;
        private const float RuntimeCardCellHeight = 78f;
        private const float RuntimeCardCellSpacing = 10f;

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
        private GameObject panelRoot;
        private RectTransform panelRootRect;
        private Text turnLabelText;
        private Text energyLabelText;
        private Text selectedLabelText;
        private Text promptTitleText;
        private Text promptDetailText;
        private Text statusText;
        private Text unitListText;
        private Text logText;
        private Transform cardContainer;
        private RectTransform worldHudRoot;
        private Button clearButton;
        private Button endTurnButton;
        private readonly List<CardButtonView> runtimeCardButtons = new();
        private readonly List<UnitHealthBarView> runtimeUnitBars = new();
        private BattleHudSnapshot latestSnapshot;
        private bool isVisible;

        public CardData SelectedCard { get; private set; }
        public string StatusMessage => statusMessage;
        public IReadOnlyList<string> ActionLog => actionLog;
        public UnitController SelectedUnit => selectedUnit;
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

        public void AttachRuntimeUi(Transform parent)
        {
            if (panelRoot != null)
            {
                return;
            }

            panelRoot = new GameObject("BattleHudPanel");
            panelRoot.transform.SetParent(parent, false);
            panelRootRect = panelRoot.AddComponent<RectTransform>();
            panelRootRect.anchorMin = Vector2.zero;
            panelRootRect.anchorMax = Vector2.one;
            panelRootRect.offsetMin = Vector2.zero;
            panelRootRect.offsetMax = Vector2.zero;

            CreateBackdrop(panelRoot.transform, new Color(0.08f, 0.06f, 0.04f, 0.28f));
            var worldHudObject = new GameObject("WorldHudLayer");
            worldHudObject.transform.SetParent(panelRoot.transform, false);
            worldHudRoot = worldHudObject.AddComponent<RectTransform>();
            SetRect(worldHudRoot, Vector2.zero, Vector2.one);
            var topBar = CreatePanel("TopBar", panelRoot.transform, new Color(0.28f, 0.22f, 0.16f, 0.96f), Vector2.zero, Vector2.one, UiThemeResources.Paths.PanelAccent);
            SetRect(topBar.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(16f, -RuntimeTopBarHeight), new Vector2(-16f, -16f));
            var leftPanel = CreatePanel("LeftPanel", panelRoot.transform, new Color(0.23f, 0.24f, 0.27f, 0.95f), Vector2.zero, Vector2.one, UiThemeResources.Paths.PanelSecondary);
            SetRect(leftPanel.GetComponent<RectTransform>(), new Vector2(0f, 0f), new Vector2(0f, 1f), new Vector2(16f, RuntimeLeftPanelBottomInset), new Vector2(360f, -(RuntimeTopBarHeight + 18f)));
            var bottomPanel = CreatePanel("BottomPanel", panelRoot.transform, new Color(0.30f, 0.23f, 0.16f, 0.98f), Vector2.zero, Vector2.one, UiThemeResources.Paths.PanelPrimary);
            SetRect(bottomPanel.GetComponent<RectTransform>(), new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(188f, RuntimeBottomPanelBottomInset), new Vector2(-16f, RuntimeBottomPanelTopInset));

            turnLabelText = CreateText("TurnLabel", topBar.transform, 20, FontStyle.Bold, TextAnchor.UpperLeft);
            SetRect(turnLabelText.rectTransform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(16f, -42f), new Vector2(228f, -12f));
            energyLabelText = CreateText("EnergyLabel", topBar.transform, 15, FontStyle.Bold, TextAnchor.UpperLeft);
            SetRect(energyLabelText.rectTransform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(240f, -42f), new Vector2(420f, -12f));
            selectedLabelText = CreateText("SelectionLabel", topBar.transform, 13, FontStyle.Normal, TextAnchor.UpperLeft);
            SetRect(selectedLabelText.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(16f, -74f), new Vector2(-RuntimeHudButtonStripWidth, -48f));
            promptTitleText = CreateText("PromptTitle", topBar.transform, 13, FontStyle.Bold, TextAnchor.UpperLeft);
            SetRect(promptTitleText.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(16f, -104f), new Vector2(-RuntimeHudButtonStripWidth, -80f));
            promptDetailText = CreateText("PromptDetail", topBar.transform, 12, FontStyle.Normal, TextAnchor.UpperLeft);
            promptDetailText.verticalOverflow = VerticalWrapMode.Overflow;
            SetRect(promptDetailText.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(16f, -138f), new Vector2(-RuntimeHudButtonStripWidth, -108f));
            clearButton = CreateButton("ClearButton", topBar.transform, "Clear", false);
            SetRect(clearButton.GetComponent<RectTransform>(), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(-(RuntimeHudButtonStripWidth - 12f), 16f), new Vector2(-116f, -16f));
            clearButton.onClick.AddListener(ClearSelectionState);
            endTurnButton = CreateButton("EndTurnButton", topBar.transform, "End Turn", true);
            SetRect(endTurnButton.GetComponent<RectTransform>(), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(-108f, 16f), new Vector2(-12f, -16f));
            endTurnButton.onClick.AddListener(HandleRuntimeEndTurn);

            statusText = CreateText("StatusText", leftPanel.transform, 16, FontStyle.Bold, TextAnchor.UpperLeft);
            SetRect(statusText.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(16f, -72f), new Vector2(-16f, -16f));
            unitListText = CreateText("UnitList", leftPanel.transform, 14, FontStyle.Normal, TextAnchor.UpperLeft);
            unitListText.verticalOverflow = VerticalWrapMode.Overflow;
            SetRect(unitListText.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(16f, -252f), new Vector2(-16f, -92f));
            logText = CreateText("LogText", leftPanel.transform, 13, FontStyle.Normal, TextAnchor.UpperLeft);
            logText.verticalOverflow = VerticalWrapMode.Overflow;
            SetRect(logText.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(16f, 16f), new Vector2(-16f, -268f));

            var handLabel = CreateText("HandLabel", bottomPanel.transform, 18, FontStyle.Bold, TextAnchor.UpperLeft);
            handLabel.text = "Hand";
            SetRect(handLabel.rectTransform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(16f, -40f), new Vector2(100f, -10f));

            var cardArea = new GameObject("CardArea");
            cardArea.transform.SetParent(bottomPanel.transform, false);
            cardContainer = cardArea.AddComponent<RectTransform>();
            SetRect((RectTransform)cardContainer, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(112f, 10f), new Vector2(-14f, -10f));
            var layout = cardArea.AddComponent<GridLayoutGroup>();
            layout.cellSize = new Vector2(RuntimeCardCellWidth, RuntimeCardCellHeight);
            layout.spacing = new Vector2(RuntimeCardCellSpacing, RuntimeCardCellSpacing);
            layout.childAlignment = TextAnchor.MiddleLeft;
            layout.startCorner = GridLayoutGroup.Corner.UpperLeft;
            layout.startAxis = GridLayoutGroup.Axis.Horizontal;
            layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            layout.constraintCount = 1;
            panelRoot.SetActive(false);
        }

        public void SetVisible(bool visible)
        {
            isVisible = visible;
            if (panelRoot != null)
            {
                panelRoot.SetActive(visible);
            }

            Refresh();
        }

        private void Update()
        {
            if (!isVisible && panelRoot != null)
            {
                return;
            }

            HandleWorldClick();
            UpdateWorldUnitBarPositions();
        }

        public void Refresh()
        {
            if (gridManager == null)
            {
                UpdateRuntimeUi();
                return;
            }

            gridManager.ClearHighlights();

            if (IsBattleOver)
            {
                UpdateRuntimeUi();
                return;
            }

            if (selectedUnit == null || !selectedUnit.IsAlive)
            {
                UpdateRuntimeUi();
                return;
            }

            if (SelectedCard != null)
            {
                switch (SelectedCard.CardType)
                {
                    case CardType.Move:
                    case CardType.Dash:
                        gridManager.HighlightMoveRange(selectedUnit.GridPosition, SelectedCard.MoveDistance);
                        break;
                    case CardType.Strike:
                    case CardType.Push:
                        gridManager.HighlightAttackRange(selectedUnit.GridPosition, SelectedCard.Range);
                        break;
                }
            }

            gridManager.GetTile(selectedUnit.GridPosition)?.SetSelected();
            UpdateRuntimeUi();
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
                statusMessage = GetSelectionStatusMessage();
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

            statusMessage = GetSelectionStatusMessage();
            AddLog(statusMessage);
            Refresh();
        }

        public void ClearSelectedCard()
        {
            SelectedCard = null;
            statusMessage = GetSelectionStatusMessage();
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
                statusMessage = GetInvalidUnitTargetMessage(targetUnit);
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

        private string GetInvalidUnitTargetMessage(UnitController targetUnit)
        {
            if (SelectedCard == null)
            {
                return "No card selected.";
            }

            if (selectedUnit == null)
            {
                return "Select a player unit first.";
            }

            if (targetUnit == null)
            {
                return "Invalid unit target.";
            }

            if (!targetUnit.IsAlive)
            {
                return $"{targetUnit.DisplayName} is already defeated.";
            }

            if (targetUnit.Team == selectedUnit.Team)
            {
                return "Select an enemy unit.";
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

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
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
                    HandlePlayerUnitSelected(clickedUnit);
                    return;
                }

                if (selectedUnit != null && SelectedCard != null)
                {
                    if (!CanPlayUnitCard(selectedUnit, clickedUnit))
                    {
                        statusMessage = GetInvalidUnitTargetMessage(clickedUnit);
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
            if (panelRoot != null)
            {
                return;
            }

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
                statusMessage = GetEndTurnStatusMessage();
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
                CardType.Dash => $"Dash up to {card.MoveDistance} tiles.",
                CardType.Strike => $"Deal {card.Power} damage.",
                CardType.Push => $"Deal {card.Power} damage and push 1.",
                CardType.Guard => "Reduce the next hit by 1.",
                CardType.Heal => $"Restore {card.Power} health.",
                _ => card.CardType.ToString(),
            };
        }

        private string GetCardStats(CardData card)
        {
            return card.CardType switch
            {
                CardType.Move => $"Move {card.MoveDistance}",
                CardType.Dash => $"Dash {card.MoveDistance}",
                CardType.Guard => "Self",
                CardType.Heal => $"Heal {card.Power}",
                _ => $"Range {card.Range}  Power {card.Power}",
            };
        }

        private string GetCompactCardDescription(CardData card)
        {
            return card.CardType switch
            {
                CardType.Move => $"Move {card.MoveDistance} tiles",
                CardType.Dash => $"Dash {card.MoveDistance} tiles",
                CardType.Strike => $"Deal {card.Power} damage",
                CardType.Push => $"Hit and push 1",
                CardType.Guard => "Block next hit",
                CardType.Heal => $"Recover {card.Power} HP",
                _ => GetCardDescription(card),
            };
        }

        private Color GetCardTint(CardData card, bool selected)
        {
            var tint = card.CardType switch
            {
                CardType.Move => new Color(0.75f, 0.88f, 0.80f, 1f),
                CardType.Dash => new Color(0.53f, 0.77f, 0.93f, 1f),
                CardType.Strike => new Color(0.93f, 0.76f, 0.76f, 1f),
                CardType.Push => new Color(0.97f, 0.85f, 0.70f, 1f),
                CardType.Guard => new Color(0.78f, 0.82f, 0.93f, 1f),
                CardType.Heal => new Color(0.70f, 0.92f, 0.78f, 1f),
                _ => new Color(0.88f, 0.88f, 0.88f, 1f),
            };

            if (selected)
            {
                tint *= 1.1f;
            }

            tint.a = 1f;
            return tint;
        }

        private bool CanUseCards()
        {
            return turnManager != null
                && turnManager.IsPlayerTurn
                && !turnManager.BattleEnded
                && turnManager.RemainingCardPlays > 0;
        }

        private string GetGlobalCardDisabledReason()
        {
            if (turnManager == null)
            {
                return "No Battle";
            }

            if (turnManager.BattleEnded)
            {
                return "Battle Over";
            }

            if (!turnManager.IsPlayerTurn)
            {
                return "Enemy Turn";
            }

            if (turnManager.RemainingCardPlays <= 0)
            {
                return "No Energy";
            }

            return string.Empty;
        }

        private string GetCardUseHint(CardData card)
        {
            if (card == null)
            {
                return "Choose a card from the hand.";
            }

            return card.TargetType switch
            {
                TargetType.Tile when card.CardType == CardType.Move => $"Select an empty tile within {card.MoveDistance}.",
                TargetType.Tile when card.CardType == CardType.Dash => $"Dash to an empty tile within {card.MoveDistance}.",
                TargetType.Tile => "Select a valid target tile.",
                TargetType.EnemyUnit => $"Select an enemy in range {card.Range}.",
                TargetType.Self => "Use this on the selected unit.",
                _ => "Choose a legal target.",
            };
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

        private void HandlePlayerUnitSelected(UnitController unit)
        {
            if (unit == null)
            {
                return;
            }

            selectedUnit = unit;
            statusMessage = GetSelectionStatusMessage();
            AddLog(BuildSelectionLogMessage(unit));
            Refresh();
        }

        private string BuildSelectionLogMessage(UnitController unit)
        {
            var baseMessage = $"{unit.DisplayName} selected at {unit.GridPosition.x},{unit.GridPosition.y}.";
            if (SelectedCard == null)
            {
                return $"{baseMessage} Next: choose a card.";
            }

            return $"{baseMessage} {GetNextStepMessage()}";
        }

        private string GetSelectionStatusMessage()
        {
            if (IsBattleOver)
            {
                return turnManager.BattleOutcomeSummary;
            }

            if (selectedUnit == null)
            {
                return SelectedCard == null
                    ? "Select a player unit."
                    : $"{SelectedCard.CardName} selected. Next: select a player unit.";
            }

            if (SelectedCard == null)
            {
                return $"{selectedUnit.DisplayName} selected. Next: choose a card.";
            }

            return $"{selectedUnit.DisplayName} selected. {SelectedCard.CardName} ready. {GetNextStepMessage()}";
        }

        private string GetNextStepMessage()
        {
            if (SelectedCard == null)
            {
                return "Next: choose a card.";
            }

            return SelectedCard.TargetType switch
            {
                TargetType.Tile => "Next: choose a tile target.",
                TargetType.EnemyUnit => "Next: choose an enemy target.",
                TargetType.Self => "Next: choose your unit tile.",
                _ => "Next: choose a target.",
            };
        }

        private string GetIdleStatusMessage()
        {
            return GetSelectionStatusMessage();
        }

        private string GetPromptTitle()
        {
            if (turnManager == null)
            {
                return "No Active Battle";
            }

            if (turnManager.BattleEnded)
            {
                return turnManager.WinningTeam == TeamType.Player ? "Victory" : "Defeat";
            }

            if (!turnManager.IsPlayerTurn)
            {
                return "Enemy Phase";
            }

            if (turnManager.RemainingCardPlays <= 0)
            {
                return "No Energy Left";
            }

            if (SelectedCard != null && selectedUnit == null)
            {
                return $"{SelectedCard.CardName} Selected";
            }

            if (SelectedCard != null)
            {
                return $"{SelectedCard.CardName} Ready";
            }

            if (selectedUnit != null)
            {
                return $"{selectedUnit.DisplayName} Ready";
            }

            return "Choose A Unit";
        }

        private string GetPromptDetail()
        {
            if (turnManager == null)
            {
                return "Set up the battle managers before using the HUD.";
            }

            if (turnManager.BattleEnded)
            {
                return turnManager.BattleOutcomeSummary;
            }

            if (!turnManager.IsPlayerTurn)
            {
                return "Wait for enemy actions to finish before spending cards again.";
            }

            if (turnManager.RemainingCardPlays <= 0)
            {
                return "End the turn to refresh your hand and restore energy.";
            }

            if (SelectedCard != null && selectedUnit == null)
            {
                return $"Choose a player unit to cast {SelectedCard.CardName} from.";
            }

            if (SelectedCard != null)
            {
                return GetCardUseHint(SelectedCard);
            }

            if (selectedUnit != null)
            {
                return "Pick a card from the hand to continue the turn.";
            }

            return "Click a player unit on the board to begin your action.";
        }

        private string GetEndTurnStatusMessage()
        {
            if (turnManager == null)
            {
                return "Enemy phase: Enemy turn resolved. New player turn started.";
            }

            if (turnManager.BattleEnded)
            {
                return turnManager.BattleOutcomeSummary;
            }

            var summary = string.IsNullOrWhiteSpace(turnManager.LastEnemyActionSummary)
                ? "Enemy turn resolved. New player turn started."
                : turnManager.LastEnemyActionSummary;
            return $"Enemy phase: {summary}";
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

        public BattleHudSnapshot BuildSnapshot()
        {
            var unitSnapshots = new List<UnitHudSnapshot>();
            if (turnManager != null)
            {
                foreach (var unit in turnManager.PlayerUnits)
                {
                    AddUnitSnapshot(unitSnapshots, unit);
                }

                foreach (var unit in turnManager.EnemyUnits)
                {
                    AddUnitSnapshot(unitSnapshots, unit);
                }
            }

            var handSnapshots = new List<CardHudSnapshot>();
            if (deckManager != null)
            {
                var disabledReason = GetGlobalCardDisabledReason();
                foreach (var card in deckManager.Hand)
                {
                    if (card == null)
                    {
                        continue;
                    }

                    handSnapshots.Add(new CardHudSnapshot
                    {
                        Card = card,
                        Title = card.CardName,
                        Subtitle = card.CardType.ToString(),
                        Description = GetCompactCardDescription(card),
                        Stats = GetCardStats(card),
                        UseHint = GetCardUseHint(card),
                        DisabledReason = disabledReason,
                        Tint = GetCardTint(card, SelectedCard == card),
                        IsSelected = SelectedCard == card,
                        IsPlayable = string.IsNullOrWhiteSpace(disabledReason),
                    });
                }
            }

            var selectedCardSnapshot = SelectedCard == null ? null : handSnapshots.Find(snapshot => snapshot.Card == SelectedCard);
            if (selectedCardSnapshot == null && SelectedCard != null)
            {
                selectedCardSnapshot = new CardHudSnapshot
                {
                    Card = SelectedCard,
                    Title = SelectedCard.CardName,
                    Subtitle = SelectedCard.CardType.ToString(),
                    Description = GetCompactCardDescription(SelectedCard),
                    Stats = GetCardStats(SelectedCard),
                    UseHint = GetCardUseHint(SelectedCard),
                    DisabledReason = GetGlobalCardDisabledReason(),
                    Tint = GetCardTint(SelectedCard, true),
                    IsSelected = true,
                    IsPlayable = string.IsNullOrWhiteSpace(GetGlobalCardDisabledReason()),
                };
            }

            return new BattleHudSnapshot
            {
                TurnLabel = GetTurnLabel(),
                StatusMessage = statusMessage,
                RemainingCardPlays = turnManager?.RemainingCardPlays ?? 0,
                MaxCardPlays = turnManager?.MaxCardPlaysPerTurn ?? 0,
                IsBattleOver = IsBattleOver,
                CanEndTurn = turnManager != null && turnManager.IsPlayerTurn && !turnManager.BattleEnded,
                CanPlayCards = CanUseCards(),
                PromptTitle = GetPromptTitle(),
                PromptDetail = GetPromptDetail(),
                SelectedUnit = selectedUnit == null ? null : new UnitHudSnapshot
                {
                    DisplayName = selectedUnit.DisplayName,
                    Team = selectedUnit.Team,
                    CurrentHp = selectedUnit.CurrentHp,
                    MaxHp = selectedUnit.MaxHp,
                    HealthRatio = selectedUnit.MaxHp <= 0 ? 0f : (float)selectedUnit.CurrentHp / selectedUnit.MaxHp,
                    GridPosition = selectedUnit.GridPosition,
                    WorldPosition = selectedUnit.transform.position + new Vector3(0f, 0.72f, 0f),
                    IsSelected = true,
                },
                SelectedCard = selectedCardSnapshot,
                Units = unitSnapshots,
                Hand = handSnapshots,
                ActionLog = new List<string>(actionLog),
            };
        }

        private void UpdateRuntimeUi()
        {
            if (panelRoot == null)
            {
                return;
            }

            var snapshot = BuildSnapshot();
            latestSnapshot = snapshot;
            turnLabelText.text = snapshot.TurnLabel;
            energyLabelText.text = $"Energy {snapshot.RemainingCardPlays}/{Mathf.Max(1, snapshot.MaxCardPlays)}";
            UiThemeResources.ApplyTextStyle(
                energyLabelText,
                snapshot.CanPlayCards || snapshot.IsBattleOver || turnManager == null || !turnManager.IsPlayerTurn
                    ? UiThemeResources.BrightTextColor
                    : new Color(0.98f, 0.74f, 0.52f, 1f));
            selectedLabelText.text = snapshot.SelectedUnit == null
                ? (snapshot.SelectedCard == null ? "Unit: None  |  Card: None" : $"Unit: None  |  Card: {snapshot.SelectedCard.Title}")
                : $"Unit: {snapshot.SelectedUnit.DisplayName} ({snapshot.SelectedUnit.CurrentHp}/{snapshot.SelectedUnit.MaxHp})  |  Card: {(snapshot.SelectedCard == null ? "None" : snapshot.SelectedCard.Title)}";
            promptTitleText.text = snapshot.PromptTitle;
            promptDetailText.text = snapshot.PromptDetail;
            UiThemeResources.ApplyTextStyle(
                promptTitleText,
                snapshot.RemainingCardPlays <= 0 && turnManager != null && turnManager.IsPlayerTurn && !snapshot.IsBattleOver
                    ? new Color(0.98f, 0.74f, 0.52f, 1f)
                    : UiThemeResources.BrightTextColor);
            UiThemeResources.ApplyTextStyle(promptDetailText, UiThemeResources.MutedTextColor, withShadow: false);
            statusText.text = snapshot.StatusMessage;
            unitListText.text = BuildUnitListText(snapshot.Units);
            logText.text = BuildActionLogText(snapshot.ActionLog);
            clearButton.interactable = !snapshot.IsBattleOver;
            endTurnButton.interactable = snapshot.CanEndTurn;
            RenderRuntimeHand(snapshot.Hand);
            RenderRuntimeUnitBars(snapshot.Units);
            UpdateWorldUnitBarPositions();
        }

        private void RenderRuntimeHand(IReadOnlyList<CardHudSnapshot> hand)
        {
            if (cardContainer == null)
            {
                return;
            }

            while (runtimeCardButtons.Count < hand.Count)
            {
                runtimeCardButtons.Add(CreateRuntimeCardButton(cardContainer));
            }

            for (var i = 0; i < runtimeCardButtons.Count; i++)
            {
                var isActive = i < hand.Count;
                runtimeCardButtons[i].gameObject.SetActive(isActive);
                if (isActive)
                {
                    runtimeCardButtons[i].Bind(hand[i], this);
                }
            }
        }

        private void RenderRuntimeUnitBars(IReadOnlyList<UnitHudSnapshot> units)
        {
            if (worldHudRoot == null || units == null)
            {
                return;
            }

            while (runtimeUnitBars.Count < units.Count)
            {
                runtimeUnitBars.Add(CreateRuntimeUnitBar(worldHudRoot));
            }

            for (var i = 0; i < runtimeUnitBars.Count; i++)
            {
                var isActive = i < units.Count && isVisible;
                runtimeUnitBars[i].gameObject.SetActive(isActive);
                if (isActive)
                {
                    runtimeUnitBars[i].Bind(units[i]);
                }
            }
        }

        private void UpdateWorldUnitBarPositions()
        {
            if (!isVisible || latestSnapshot?.Units == null || worldHudRoot == null || panelRootRect == null)
            {
                return;
            }

            var camera = Camera.main;
            if (camera == null)
            {
                return;
            }

            for (var i = 0; i < latestSnapshot.Units.Count && i < runtimeUnitBars.Count; i++)
            {
                runtimeUnitBars[i].UpdatePosition(panelRootRect, camera);
            }
        }

        private void HandleRuntimeEndTurn()
        {
            turnManager?.EndPlayerTurn();
            SelectedCard = null;
            selectedUnit = null;
            statusMessage = GetEndTurnStatusMessage();
            AddLog(statusMessage);
            if (IsBattleOver)
            {
                ApplyBattleOutcomeStatus(logOutcome: true);
            }

            Refresh();
        }

        private void ClearSelectionState()
        {
            selectedUnit = null;
            ClearSelectedCard();
        }

        private void AddUnitSnapshot(List<UnitHudSnapshot> units, UnitController unit)
        {
            if (unit == null)
            {
                return;
            }

            units.Add(new UnitHudSnapshot
            {
                DisplayName = unit.DisplayName,
                Team = unit.Team,
                CurrentHp = unit.CurrentHp,
                MaxHp = unit.MaxHp,
                HealthRatio = unit.MaxHp <= 0 ? 0f : (float)unit.CurrentHp / unit.MaxHp,
                GridPosition = unit.GridPosition,
                WorldPosition = unit.transform.position + new Vector3(0f, 0.72f, 0f),
                IsSelected = selectedUnit == unit,
            });
        }

        private string BuildUnitListText(IReadOnlyList<UnitHudSnapshot> units)
        {
            if (units == null || units.Count == 0)
            {
                return "Warband\nNo units registered.";
            }

            var lines = new List<string> { "Warband" };
            foreach (var unit in units)
            {
                lines.Add($"{unit.DisplayName} [{unit.Team}]  HP {unit.CurrentHp}/{unit.MaxHp}  Grid {unit.GridPosition.x},{unit.GridPosition.y}");
            }

            return string.Join("\n", lines);
        }

        private string BuildActionLogText(IReadOnlyList<string> entries)
        {
            if (entries == null || entries.Count == 0)
            {
                return "Recent Moves\nNo actions yet.";
            }

            var lines = new List<string> { "Recent Moves" };
            for (var i = entries.Count - 1; i >= 0; i--)
            {
                lines.Add(entries[i]);
            }

            return string.Join("\n", lines);
        }

        private static void CreateBackdrop(Transform parent, Color color)
        {
            var backdrop = new GameObject("Backdrop");
            backdrop.transform.SetParent(parent, false);
            var image = backdrop.AddComponent<Image>();
            UiThemeResources.ApplySprite(image, UiThemeResources.Paths.PanelSecondary, color);
            image.raycastTarget = false;
            var rectTransform = backdrop.GetComponent<RectTransform>() ?? backdrop.AddComponent<RectTransform>();
            SetRect(rectTransform, Vector2.zero, Vector2.one);
        }

        private static GameObject CreatePanel(string name, Transform parent, Color color, Vector2 anchorMin, Vector2 anchorMax, string spritePath = null)
        {
            var panel = new GameObject(name);
            panel.transform.SetParent(parent, false);
            var image = panel.AddComponent<Image>();
            UiThemeResources.ApplySprite(image, spritePath, color);
            image.raycastTarget = false;
            var rectTransform = panel.GetComponent<RectTransform>() ?? panel.AddComponent<RectTransform>();
            SetRect(rectTransform, anchorMin, anchorMax);
            return panel;
        }

        private static Text CreateText(string name, Transform parent, int fontSize, FontStyle fontStyle, TextAnchor alignment)
        {
            var textObject = new GameObject(name);
            textObject.transform.SetParent(parent, false);
            var text = textObject.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = fontSize;
            text.fontStyle = fontStyle;
            text.alignment = alignment;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;
            text.resizeTextForBestFit = false;
            text.raycastTarget = false;
            UiThemeResources.ApplyTextStyle(text, UiThemeResources.BrightTextColor);
            return text;
        }

        private static Button CreateButton(string name, Transform parent, string label, bool useDangerStyle)
        {
            var buttonObject = new GameObject(name);
            buttonObject.transform.SetParent(parent, false);
            var image = buttonObject.AddComponent<Image>();
            UiThemeResources.ApplySprite(
                image,
                useDangerStyle ? UiThemeResources.Paths.ButtonDanger : UiThemeResources.Paths.ButtonPrimary,
                Color.white);
            var button = buttonObject.AddComponent<Button>();
            button.targetGraphic = image;
            var labelText = CreateText("Label", buttonObject.transform, 14, FontStyle.Bold, TextAnchor.MiddleCenter);
            labelText.text = label;
            UiThemeResources.ApplyTextStyle(labelText, UiThemeResources.InkColor);
            SetRect(labelText.rectTransform, Vector2.zero, Vector2.one);
            return button;
        }

        private static CardButtonView CreateRuntimeCardButton(Transform parent)
        {
            var buttonObject = new GameObject("CardButton");
            buttonObject.transform.SetParent(parent, false);
            var rect = buttonObject.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(RuntimeCardCellWidth, RuntimeCardCellHeight);
            var layoutElement = buttonObject.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = RuntimeCardCellWidth;
            layoutElement.preferredHeight = RuntimeCardCellHeight;
            layoutElement.minWidth = RuntimeCardCellWidth;
            layoutElement.minHeight = RuntimeCardCellHeight;
            var image = buttonObject.AddComponent<Image>();
            UiThemeResources.ApplySprite(image, UiThemeResources.Paths.CardPanel, Color.white);
            var button = buttonObject.AddComponent<Button>();
            button.targetGraphic = image;

            var icon = new GameObject("Icon");
            icon.transform.SetParent(buttonObject.transform, false);
            var iconImage = icon.AddComponent<Image>();
            iconImage.raycastTarget = false;
            SetRect(iconImage.rectTransform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(12f, -36f), new Vector2(40f, -10f));

            var title = CreateText("Title", buttonObject.transform, 13, FontStyle.Bold, TextAnchor.UpperLeft);
            title.horizontalOverflow = HorizontalWrapMode.Overflow;
            SetRect(title.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(46f, -28f), new Vector2(-8f, -8f));
            var body = CreateText("Body", buttonObject.transform, 11, FontStyle.Normal, TextAnchor.UpperLeft);
            body.lineSpacing = 0.9f;
            SetRect(body.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(46f, 22f), new Vector2(-8f, -28f));
            var stats = CreateText("Stats", buttonObject.transform, 10, FontStyle.Normal, TextAnchor.LowerLeft);
            stats.horizontalOverflow = HorizontalWrapMode.Overflow;
            SetRect(stats.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(10f, 6f), new Vector2(-8f, 18f));
            UiThemeResources.ApplyTextStyle(stats, new Color(0.27f, 0.18f, 0.11f, 1f));

            var disabledOverlay = new GameObject("DisabledOverlay");
            disabledOverlay.transform.SetParent(buttonObject.transform, false);
            var disabledOverlayImage = disabledOverlay.AddComponent<Image>();
            disabledOverlayImage.color = new Color(0.08f, 0.08f, 0.10f, 0.72f);
            disabledOverlayImage.raycastTarget = false;
            SetRect(disabledOverlayImage.rectTransform, Vector2.zero, Vector2.one);

            var disabledText = CreateText("DisabledText", disabledOverlay.transform, 12, FontStyle.Bold, TextAnchor.MiddleCenter);
            disabledText.horizontalOverflow = HorizontalWrapMode.Wrap;
            disabledText.verticalOverflow = VerticalWrapMode.Overflow;
            SetRect(disabledText.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(10f, 6f), new Vector2(-10f, -6f));
            disabledOverlay.SetActive(false);

            var view = buttonObject.AddComponent<CardButtonView>();
            view.Configure(button, image, iconImage, title, body, stats, disabledOverlay, disabledText);
            return view;
        }

        private static UnitHealthBarView CreateRuntimeUnitBar(Transform parent)
        {
            var barObject = new GameObject("UnitHealthBar");
            barObject.transform.SetParent(parent, false);
            var rect = barObject.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(110f, 34f);

            var frame = barObject.AddComponent<Image>();
            frame.color = new Color(0.08f, 0.08f, 0.10f, 0.84f);
            frame.raycastTarget = false;

            var nameText = CreateText("Name", barObject.transform, 11, FontStyle.Bold, TextAnchor.UpperCenter);
            nameText.horizontalOverflow = HorizontalWrapMode.Overflow;
            SetRect(nameText.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(8f, -16f), new Vector2(-8f, -2f));

            var barBackObject = new GameObject("BarBack");
            barBackObject.transform.SetParent(barObject.transform, false);
            var barBack = barBackObject.AddComponent<Image>();
            barBack.color = new Color(0.15f, 0.16f, 0.18f, 0.92f);
            barBack.raycastTarget = false;
            SetRect(barBack.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(8f, 6f), new Vector2(-8f, 18f));

            var fillObject = new GameObject("Fill");
            fillObject.transform.SetParent(barBackObject.transform, false);
            var fill = fillObject.AddComponent<Image>();
            fill.type = Image.Type.Filled;
            fill.fillMethod = Image.FillMethod.Horizontal;
            fill.fillOrigin = (int)Image.OriginHorizontal.Left;
            fill.color = new Color(0.22f, 0.68f, 0.43f, 1f);
            fill.raycastTarget = false;
            SetRect(fill.rectTransform, Vector2.zero, Vector2.one, new Vector2(2f, 2f), new Vector2(-2f, -2f));

            var valueText = CreateText("Value", barObject.transform, 11, FontStyle.Bold, TextAnchor.MiddleCenter);
            SetRect(valueText.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(8f, 2f), new Vector2(-8f, 18f));

            var view = barObject.AddComponent<UnitHealthBarView>();
            view.Configure(rect, frame, fill, nameText, valueText);
            return view;
        }

        private static void SetRect(RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax)
        {
            SetRect(rectTransform, anchorMin, anchorMax, Vector2.zero, Vector2.zero);
        }

        private static void SetRect(RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
        {
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.offsetMin = offsetMin;
            rectTransform.offsetMax = offsetMax;
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
