using UnityEngine;
using UnityEngine.UI;

namespace TacticalCards
{
    public class GameFlowController : MonoBehaviour
    {
        [SerializeField] private string gameTitle = "TACTICAL CARDS";
        [SerializeField] private string gameSubtitle = "A compact card tactics prototype";

        private GameRoot gameRoot;
        private BattleUI battleUI;
        private TurnManager turnManager;

        private Canvas canvas;
        private GameObject titlePanel;
        private GameObject battlePanel;
        private GameObject resultPanel;
        private Text titleSummaryText;
        private Text resultTitleText;
        private Text resultSummaryText;

        public GameFlowState CurrentState { get; private set; }
        public GameResultData LastResult { get; private set; }

        public void Initialize(GameRoot targetGameRoot, BattleUI targetBattleUi, TurnManager targetTurnManager)
        {
            gameRoot = targetGameRoot;
            battleUI = targetBattleUi;
            turnManager = targetTurnManager;

            EnsureCanvas();
            BuildPanelsIfNeeded();
            battleUI?.AttachRuntimeUi(battlePanel.transform);

            if (turnManager != null)
            {
                turnManager.BattleEndedEvent -= HandleBattleEnded;
                turnManager.BattleEndedEvent += HandleBattleEnded;
            }

            ShowTitle();
        }

        public void ShowTitle()
        {
            LastResult = null;
            CurrentState = GameFlowState.Title;
            titleSummaryText.text = "Start a polished single-battle demo with clear UI flow, readable combat prompts, and replay support.";
            titlePanel.SetActive(true);
            battlePanel.SetActive(false);
            resultPanel.SetActive(false);
            battleUI?.SetVisible(false);
            gameRoot?.PrepareBattle();
        }

        public void StartGame()
        {
            gameRoot?.StartBattleSession();

            if (turnManager != null && turnManager.BattleEnded)
            {
                if (LastResult == null)
                {
                    HandleBattleEnded(turnManager.BuildResultData());
                }

                return;
            }

            CurrentState = GameFlowState.Battle;
            titlePanel.SetActive(false);
            battlePanel.SetActive(true);
            resultPanel.SetActive(false);
            battleUI?.SetVisible(true);
            battleUI?.Refresh();
        }

        public void ReplayBattle()
        {
            StartGame();
        }

        public void ReturnToMenu()
        {
            ShowTitle();
        }

        private void HandleBattleEnded(GameResultData result)
        {
            LastResult = result;
            CurrentState = GameFlowState.Result;
            titlePanel.SetActive(false);
            battlePanel.SetActive(false);
            resultPanel.SetActive(true);
            battleUI?.SetVisible(false);

            resultTitleText.text = result.Title;
            resultSummaryText.text = $"{result.Summary}\n\nPlayer turns taken: {result.PlayerTurnCount}";
        }

        private void EnsureCanvas()
        {
            if (canvas != null)
            {
                return;
            }

            var canvasObject = new GameObject("RuntimeCanvas");
            canvasObject.transform.SetParent(transform, false);
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1280f, 720f);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
            canvasObject.AddComponent<GraphicRaycaster>();

            var eventSystemObject = new GameObject("EventSystem");
            eventSystemObject.transform.SetParent(transform, false);
            eventSystemObject.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystemObject.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
        }

        private void BuildPanelsIfNeeded()
        {
            if (titlePanel != null)
            {
                return;
            }

            titlePanel = CreateFullScreenPanel("TitlePanel", new Color(0.06f, 0.10f, 0.15f, 0.96f), blocksRaycasts: true);
            battlePanel = CreateFullScreenPanel("BattlePanel", new Color(0f, 0f, 0f, 0f), blocksRaycasts: false);
            resultPanel = CreateFullScreenPanel("ResultPanel", new Color(0.07f, 0.09f, 0.13f, 0.92f), blocksRaycasts: true);

            BuildTitlePanel();
            BuildResultPanel();
        }

        private void BuildTitlePanel()
        {
            var brand = CreateText("BrandMark", titlePanel.transform, gameTitle, 42, FontStyle.Bold, TextAnchor.UpperCenter);
            SetRect(brand.rectTransform, new Vector2(0.15f, 0.72f), new Vector2(0.85f, 0.88f), Vector2.zero, Vector2.zero);

            var subtitle = CreateText("Subtitle", titlePanel.transform, gameSubtitle, 22, FontStyle.Italic, TextAnchor.UpperCenter);
            subtitle.color = new Color(0.83f, 0.88f, 0.92f);
            SetRect(subtitle.rectTransform, new Vector2(0.18f, 0.60f), new Vector2(0.82f, 0.72f), Vector2.zero, Vector2.zero);

            titleSummaryText = CreateText("TitleSummary", titlePanel.transform, string.Empty, 18, FontStyle.Normal, TextAnchor.MiddleCenter);
            titleSummaryText.horizontalOverflow = HorizontalWrapMode.Wrap;
            titleSummaryText.verticalOverflow = VerticalWrapMode.Overflow;
            SetRect(titleSummaryText.rectTransform, new Vector2(0.20f, 0.40f), new Vector2(0.80f, 0.58f), Vector2.zero, Vector2.zero);

            var startButton = CreateButton("StartButton", titlePanel.transform, "Start Battle", StartGame);
            SetRect(startButton.GetComponent<RectTransform>(), new Vector2(0.38f, 0.24f), new Vector2(0.62f, 0.34f), Vector2.zero, Vector2.zero);
        }

        private void BuildResultPanel()
        {
            var brand = CreateText("ResultBrand", resultPanel.transform, gameTitle, 28, FontStyle.Bold, TextAnchor.UpperCenter);
            SetRect(brand.rectTransform, new Vector2(0.20f, 0.78f), new Vector2(0.80f, 0.90f), Vector2.zero, Vector2.zero);

            resultTitleText = CreateText("ResultTitle", resultPanel.transform, "Result", 32, FontStyle.Bold, TextAnchor.MiddleCenter);
            SetRect(resultTitleText.rectTransform, new Vector2(0.24f, 0.60f), new Vector2(0.76f, 0.74f), Vector2.zero, Vector2.zero);

            resultSummaryText = CreateText("ResultSummary", resultPanel.transform, string.Empty, 18, FontStyle.Normal, TextAnchor.MiddleCenter);
            resultSummaryText.horizontalOverflow = HorizontalWrapMode.Wrap;
            resultSummaryText.verticalOverflow = VerticalWrapMode.Overflow;
            SetRect(resultSummaryText.rectTransform, new Vector2(0.22f, 0.36f), new Vector2(0.78f, 0.60f), Vector2.zero, Vector2.zero);

            var replayButton = CreateButton("ReplayButton", resultPanel.transform, "Replay", ReplayBattle);
            SetRect(replayButton.GetComponent<RectTransform>(), new Vector2(0.28f, 0.18f), new Vector2(0.46f, 0.28f), Vector2.zero, Vector2.zero);

            var menuButton = CreateButton("MenuButton", resultPanel.transform, "Back To Menu", ReturnToMenu);
            SetRect(menuButton.GetComponent<RectTransform>(), new Vector2(0.54f, 0.18f), new Vector2(0.72f, 0.28f), Vector2.zero, Vector2.zero);
        }

        private GameObject CreateFullScreenPanel(string name, Color background, bool blocksRaycasts)
        {
            var panel = new GameObject(name);
            panel.transform.SetParent(canvas.transform, false);
            var image = panel.AddComponent<Image>();
            image.color = background;
            image.raycastTarget = blocksRaycasts;
            var rectTransform = panel.GetComponent<RectTransform>() ?? panel.AddComponent<RectTransform>();
            SetRect(rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            return panel;
        }

        private static Text CreateText(string name, Transform parent, string content, int fontSize, FontStyle fontStyle, TextAnchor alignment)
        {
            var textObject = new GameObject(name);
            textObject.transform.SetParent(parent, false);
            var text = textObject.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.text = content;
            text.fontSize = fontSize;
            text.fontStyle = fontStyle;
            text.alignment = alignment;
            text.color = new Color(0.94f, 0.96f, 0.98f);
            text.raycastTarget = false;
            return text;
        }

        private static Button CreateButton(string name, Transform parent, string label, UnityEngine.Events.UnityAction action)
        {
            var buttonObject = new GameObject(name);
            buttonObject.transform.SetParent(parent, false);
            var image = buttonObject.AddComponent<Image>();
            image.color = new Color(0.87f, 0.54f, 0.22f, 0.95f);
            var button = buttonObject.AddComponent<Button>();
            button.targetGraphic = image;
            button.onClick.AddListener(action);

            var labelText = CreateText("Label", buttonObject.transform, label, 18, FontStyle.Bold, TextAnchor.MiddleCenter);
            labelText.color = new Color(0.12f, 0.10f, 0.08f);
            SetRect(labelText.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            return button;
        }

        private static void SetRect(RectTransform rectTransform, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
        {
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.offsetMin = offsetMin;
            rectTransform.offsetMax = offsetMax;
        }
    }
}
