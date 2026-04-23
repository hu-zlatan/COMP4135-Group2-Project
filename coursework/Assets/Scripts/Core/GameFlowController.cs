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

            titlePanel = CreateFullScreenPanel("TitlePanel", new Color(0.10f, 0.08f, 0.06f, 0.98f), blocksRaycasts: true, UiThemeResources.Paths.PanelSecondary);
            battlePanel = CreateFullScreenPanel("BattlePanel", new Color(0f, 0f, 0f, 0f), blocksRaycasts: false);
            resultPanel = CreateFullScreenPanel("ResultPanel", new Color(0.10f, 0.08f, 0.06f, 0.96f), blocksRaycasts: true, UiThemeResources.Paths.PanelSecondary);

            BuildTitlePanel();
            BuildResultPanel();
        }

        private void BuildTitlePanel()
        {
            var banner = CreateDecorativeImage("TitleBanner", titlePanel.transform, UiThemeResources.Paths.TitleBanner, new Color(1f, 1f, 1f, 0.95f));
            SetRect(banner.rectTransform, new Vector2(0.19f, 0.76f), new Vector2(0.81f, 0.95f), Vector2.zero, Vector2.zero);

            var contentPanel = CreatePanel("TitleContent", titlePanel.transform, new Color(0.34f, 0.26f, 0.18f, 0.98f), UiThemeResources.Paths.PanelPrimary);
            SetRect(contentPanel.GetComponent<RectTransform>(), new Vector2(0.21f, 0.18f), new Vector2(0.79f, 0.72f), Vector2.zero, Vector2.zero);

            var crest = CreateDecorativeImage("BrandIcon", contentPanel.transform, UiThemeResources.Paths.IconCrown, new Color(0.90f, 0.80f, 0.56f, 1f));
            SetRect(crest.rectTransform, new Vector2(0.43f, 0.72f), new Vector2(0.57f, 0.92f), Vector2.zero, Vector2.zero);

            var brand = CreateText("BrandMark", contentPanel.transform, gameTitle, 40, FontStyle.Bold, TextAnchor.UpperCenter);
            SetRect(brand.rectTransform, new Vector2(0.10f, 0.54f), new Vector2(0.90f, 0.78f), Vector2.zero, Vector2.zero);

            var subtitle = CreateText("Subtitle", contentPanel.transform, gameSubtitle, 22, FontStyle.Italic, TextAnchor.UpperCenter);
            UiThemeResources.ApplyTextStyle(subtitle, UiThemeResources.MutedTextColor);
            SetRect(subtitle.rectTransform, new Vector2(0.12f, 0.42f), new Vector2(0.88f, 0.56f), Vector2.zero, Vector2.zero);

            titleSummaryText = CreateText("TitleSummary", contentPanel.transform, string.Empty, 18, FontStyle.Normal, TextAnchor.MiddleCenter);
            titleSummaryText.horizontalOverflow = HorizontalWrapMode.Wrap;
            titleSummaryText.verticalOverflow = VerticalWrapMode.Overflow;
            UiThemeResources.ApplyTextStyle(titleSummaryText, UiThemeResources.MutedTextColor);
            SetRect(titleSummaryText.rectTransform, new Vector2(0.12f, 0.20f), new Vector2(0.88f, 0.42f), Vector2.zero, Vector2.zero);

            var startButton = CreateButton("StartButton", contentPanel.transform, "Start Battle", StartGame, false);
            SetRect(startButton.GetComponent<RectTransform>(), new Vector2(0.32f, 0.06f), new Vector2(0.68f, 0.18f), Vector2.zero, Vector2.zero);
        }

        private void BuildResultPanel()
        {
            var banner = CreateDecorativeImage("ResultBanner", resultPanel.transform, UiThemeResources.Paths.ResultBanner, new Color(1f, 1f, 1f, 0.95f));
            SetRect(banner.rectTransform, new Vector2(0.22f, 0.77f), new Vector2(0.78f, 0.94f), Vector2.zero, Vector2.zero);

            var contentPanel = CreatePanel("ResultContent", resultPanel.transform, new Color(0.34f, 0.26f, 0.18f, 0.98f), UiThemeResources.Paths.PanelPrimary);
            SetRect(contentPanel.GetComponent<RectTransform>(), new Vector2(0.23f, 0.16f), new Vector2(0.77f, 0.70f), Vector2.zero, Vector2.zero);

            var brand = CreateText("ResultBrand", contentPanel.transform, gameTitle, 26, FontStyle.Bold, TextAnchor.UpperCenter);
            UiThemeResources.ApplyTextStyle(brand, UiThemeResources.MutedTextColor);
            SetRect(brand.rectTransform, new Vector2(0.14f, 0.78f), new Vector2(0.86f, 0.92f), Vector2.zero, Vector2.zero);

            var crest = CreateDecorativeImage("ResultIcon", contentPanel.transform, UiThemeResources.Paths.IconCardsFan, new Color(0.92f, 0.83f, 0.62f, 1f));
            SetRect(crest.rectTransform, new Vector2(0.42f, 0.58f), new Vector2(0.58f, 0.76f), Vector2.zero, Vector2.zero);

            resultTitleText = CreateText("ResultTitle", contentPanel.transform, "Result", 32, FontStyle.Bold, TextAnchor.MiddleCenter);
            SetRect(resultTitleText.rectTransform, new Vector2(0.20f, 0.42f), new Vector2(0.80f, 0.60f), Vector2.zero, Vector2.zero);

            resultSummaryText = CreateText("ResultSummary", contentPanel.transform, string.Empty, 18, FontStyle.Normal, TextAnchor.MiddleCenter);
            resultSummaryText.horizontalOverflow = HorizontalWrapMode.Wrap;
            resultSummaryText.verticalOverflow = VerticalWrapMode.Overflow;
            UiThemeResources.ApplyTextStyle(resultSummaryText, UiThemeResources.MutedTextColor);
            SetRect(resultSummaryText.rectTransform, new Vector2(0.14f, 0.16f), new Vector2(0.86f, 0.42f), Vector2.zero, Vector2.zero);

            var replayButton = CreateButton("ReplayButton", contentPanel.transform, "Replay", ReplayBattle, false);
            SetRect(replayButton.GetComponent<RectTransform>(), new Vector2(0.16f, 0.04f), new Vector2(0.44f, 0.14f), Vector2.zero, Vector2.zero);

            var menuButton = CreateButton("MenuButton", contentPanel.transform, "Back To Menu", ReturnToMenu, true);
            SetRect(menuButton.GetComponent<RectTransform>(), new Vector2(0.56f, 0.04f), new Vector2(0.84f, 0.14f), Vector2.zero, Vector2.zero);
        }

        private GameObject CreateFullScreenPanel(string name, Color background, bool blocksRaycasts, string spritePath = null)
        {
            var panel = new GameObject(name);
            panel.transform.SetParent(canvas.transform, false);
            var image = panel.AddComponent<Image>();
            UiThemeResources.ApplySprite(image, spritePath, background);
            image.raycastTarget = blocksRaycasts;
            var rectTransform = panel.GetComponent<RectTransform>() ?? panel.AddComponent<RectTransform>();
            SetRect(rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            return panel;
        }

        private static GameObject CreatePanel(string name, Transform parent, Color background, string spritePath)
        {
            var panel = new GameObject(name);
            panel.transform.SetParent(parent, false);
            var image = panel.AddComponent<Image>();
            UiThemeResources.ApplySprite(image, spritePath, background);
            image.raycastTarget = false;
            return panel;
        }

        private static Image CreateDecorativeImage(string name, Transform parent, string spritePath, Color tint)
        {
            var imageObject = new GameObject(name);
            imageObject.transform.SetParent(parent, false);
            var image = imageObject.AddComponent<Image>();
            UiThemeResources.ApplySprite(image, spritePath, tint, preserveAspect: true);
            image.raycastTarget = false;
            return image;
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
            text.raycastTarget = false;
            UiThemeResources.ApplyTextStyle(text, UiThemeResources.BrightTextColor);
            return text;
        }

        private static Button CreateButton(string name, Transform parent, string label, UnityEngine.Events.UnityAction action, bool useDangerStyle)
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
            button.onClick.AddListener(action);

            var labelText = CreateText("Label", buttonObject.transform, label, 18, FontStyle.Bold, TextAnchor.MiddleCenter);
            UiThemeResources.ApplyTextStyle(labelText, UiThemeResources.InkColor);
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
