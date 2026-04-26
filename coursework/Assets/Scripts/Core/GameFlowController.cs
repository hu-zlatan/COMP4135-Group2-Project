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
            titleSummaryText.text = "Lead a short tactical duel.\nSelect a unit, spend 2 energy, then end the turn to refresh.";
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
            CreateCornerOrnaments(titlePanel.transform, "TitleCorners", UiThemeResources.Paths.IconCrown, new Color(0.86f, 0.75f, 0.49f, 0.30f));
            var banner = CreateDecorativeImage("TitleBanner", titlePanel.transform, UiThemeResources.Paths.TitleBanner, new Color(1f, 1f, 1f, 0.95f));
            SetRect(banner.rectTransform, new Vector2(0.18f, 0.80f), new Vector2(0.82f, 0.96f), Vector2.zero, Vector2.zero);

            var contentPanel = CreatePanel("TitleContent", titlePanel.transform, new Color(0.34f, 0.26f, 0.18f, 0.98f), UiThemeResources.Paths.PanelPrimary);
            SetRect(contentPanel.GetComponent<RectTransform>(), new Vector2(0.18f, 0.10f), new Vector2(0.82f, 0.75f), Vector2.zero, Vector2.zero);

            var crest = CreateDecorativeImage("BrandIcon", contentPanel.transform, UiThemeResources.Paths.IconCrown, new Color(0.90f, 0.80f, 0.56f, 1f));
            SetRect(crest.rectTransform, new Vector2(0.45f, 0.79f), new Vector2(0.55f, 0.91f), Vector2.zero, Vector2.zero);

            var brand = CreateText("BrandMark", contentPanel.transform, gameTitle, 36, FontStyle.Bold, TextAnchor.UpperCenter);
            SetRect(brand.rectTransform, new Vector2(0.10f, 0.64f), new Vector2(0.90f, 0.77f), Vector2.zero, Vector2.zero);

            var subtitle = CreateText("Subtitle", contentPanel.transform, gameSubtitle, 22, FontStyle.Italic, TextAnchor.UpperCenter);
            UiThemeResources.ApplyTextStyle(subtitle, UiThemeResources.MutedTextColor);
            SetRect(subtitle.rectTransform, new Vector2(0.12f, 0.54f), new Vector2(0.88f, 0.62f), Vector2.zero, Vector2.zero);

            var summaryPanel = CreatePanel("SummaryPanel", contentPanel.transform, new Color(0.21f, 0.18f, 0.14f, 0.96f), UiThemeResources.Paths.CardPanel);
            SetRect(summaryPanel.GetComponent<RectTransform>(), new Vector2(0.14f, 0.38f), new Vector2(0.86f, 0.52f), Vector2.zero, Vector2.zero);

            titleSummaryText = CreateText("TitleSummary", summaryPanel.transform, string.Empty, 17, FontStyle.Normal, TextAnchor.MiddleCenter);
            titleSummaryText.horizontalOverflow = HorizontalWrapMode.Wrap;
            titleSummaryText.verticalOverflow = VerticalWrapMode.Truncate;
            UiThemeResources.ApplyTextStyle(titleSummaryText, UiThemeResources.MutedTextColor);
            SetRect(titleSummaryText.rectTransform, new Vector2(0.06f, 0.08f), new Vector2(0.94f, 0.92f), Vector2.zero, Vector2.zero);

            var featureStrip = new GameObject("FeatureStrip");
            featureStrip.transform.SetParent(contentPanel.transform, false);
            var featureStripRect = featureStrip.AddComponent<RectTransform>();
            SetRect(featureStripRect, new Vector2(0.08f, 0.18f), new Vector2(0.92f, 0.32f), Vector2.zero, Vector2.zero);

            CreateFeatureCard(
                "CommandFeature",
                featureStrip.transform,
                new Vector2(0.00f, 0.00f),
                new Vector2(0.31f, 1.00f),
                UiThemeResources.Paths.IconCharacter,
                "Command",
                "Pick a unit and commit a clean line of play.");
            CreateFeatureCard(
                "DeckFeature",
                featureStrip.transform,
                new Vector2(0.345f, 0.00f),
                new Vector2(0.655f, 1.00f),
                UiThemeResources.Paths.IconCardsFan,
                "Deck Flow",
                "Card prompts, highlights, and replay-ready battle flow.");
            CreateFeatureCard(
                "PressureFeature",
                featureStrip.transform,
                new Vector2(0.69f, 0.00f),
                new Vector2(1.00f, 1.00f),
                UiThemeResources.Paths.IconStrike,
                "Pressure",
                "Enemy reinforcements keep the skirmish from stalling.");

            var startButton = CreateButton("StartButton", contentPanel.transform, "Start Battle", StartGame, false);
            SetRect(startButton.GetComponent<RectTransform>(), new Vector2(0.34f, 0.05f), new Vector2(0.66f, 0.13f), Vector2.zero, Vector2.zero);
        }

        private void BuildResultPanel()
        {
            CreateCornerOrnaments(resultPanel.transform, "ResultCorners", UiThemeResources.Paths.IconCardsFan, new Color(0.86f, 0.75f, 0.49f, 0.28f));
            var banner = CreateDecorativeImage("ResultBanner", resultPanel.transform, UiThemeResources.Paths.ResultBanner, new Color(1f, 1f, 1f, 0.95f));
            SetRect(banner.rectTransform, new Vector2(0.24f, 0.79f), new Vector2(0.76f, 0.94f), Vector2.zero, Vector2.zero);

            var contentPanel = CreatePanel("ResultContent", resultPanel.transform, new Color(0.34f, 0.26f, 0.18f, 0.98f), UiThemeResources.Paths.PanelPrimary);
            SetRect(contentPanel.GetComponent<RectTransform>(), new Vector2(0.22f, 0.13f), new Vector2(0.78f, 0.72f), Vector2.zero, Vector2.zero);

            var brand = CreateText("ResultBrand", contentPanel.transform, gameTitle, 26, FontStyle.Bold, TextAnchor.UpperCenter);
            UiThemeResources.ApplyTextStyle(brand, UiThemeResources.MutedTextColor);
            SetRect(brand.rectTransform, new Vector2(0.14f, 0.82f), new Vector2(0.86f, 0.92f), Vector2.zero, Vector2.zero);

            var crest = CreateDecorativeImage("ResultIcon", contentPanel.transform, UiThemeResources.Paths.IconCardsFan, new Color(0.92f, 0.83f, 0.62f, 1f));
            SetRect(crest.rectTransform, new Vector2(0.44f, 0.69f), new Vector2(0.56f, 0.80f), Vector2.zero, Vector2.zero);

            resultTitleText = CreateText("ResultTitle", contentPanel.transform, "Result", 32, FontStyle.Bold, TextAnchor.MiddleCenter);
            SetRect(resultTitleText.rectTransform, new Vector2(0.16f, 0.52f), new Vector2(0.84f, 0.66f), Vector2.zero, Vector2.zero);

            resultSummaryText = CreateText("ResultSummary", contentPanel.transform, string.Empty, 18, FontStyle.Normal, TextAnchor.MiddleCenter);
            resultSummaryText.horizontalOverflow = HorizontalWrapMode.Wrap;
            resultSummaryText.verticalOverflow = VerticalWrapMode.Truncate;
            UiThemeResources.ApplyTextStyle(resultSummaryText, UiThemeResources.MutedTextColor);
            SetRect(resultSummaryText.rectTransform, new Vector2(0.14f, 0.28f), new Vector2(0.86f, 0.48f), Vector2.zero, Vector2.zero);

            CreateResultBadge("ReplayBadge", contentPanel.transform, new Vector2(0.18f, 0.16f), new Vector2(0.36f, 0.24f), UiThemeResources.Paths.IconMove, "Replay");
            CreateResultBadge("OutcomeBadge", contentPanel.transform, new Vector2(0.41f, 0.16f), new Vector2(0.59f, 0.24f), UiThemeResources.Paths.IconCrown, "Resolve");
            CreateResultBadge("MenuBadge", contentPanel.transform, new Vector2(0.64f, 0.16f), new Vector2(0.82f, 0.24f), UiThemeResources.Paths.IconCard, "Menu");

            var replayButton = CreateButton("ReplayButton", contentPanel.transform, "Replay", ReplayBattle, false);
            SetRect(replayButton.GetComponent<RectTransform>(), new Vector2(0.16f, 0.04f), new Vector2(0.44f, 0.14f), Vector2.zero, Vector2.zero);

            var menuButton = CreateButton("MenuButton", contentPanel.transform, "Back To Menu", ReturnToMenu, true);
            SetRect(menuButton.GetComponent<RectTransform>(), new Vector2(0.56f, 0.04f), new Vector2(0.84f, 0.14f), Vector2.zero, Vector2.zero);
        }

        private static void CreateFeatureCard(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, string iconPath, string title, string body)
        {
            var feature = CreatePanel(name, parent, new Color(0.27f, 0.21f, 0.15f, 0.98f), UiThemeResources.Paths.CardPanel);
            SetRect(feature.GetComponent<RectTransform>(), anchorMin, anchorMax, Vector2.zero, Vector2.zero);

            var icon = CreateDecorativeImage("Icon", feature.transform, iconPath, new Color(0.38f, 0.26f, 0.14f, 1f));
            SetRect(icon.rectTransform, new Vector2(0.05f, 0.18f), new Vector2(0.23f, 0.82f), Vector2.zero, Vector2.zero);

            var titleText = CreateText("Title", feature.transform, title, 14, FontStyle.Bold, TextAnchor.UpperLeft);
            UiThemeResources.ApplyTextStyle(titleText, UiThemeResources.BrightTextColor);
            SetRect(titleText.rectTransform, new Vector2(0.28f, 0.48f), new Vector2(0.95f, 0.88f), Vector2.zero, Vector2.zero);

            var bodyText = CreateText("Body", feature.transform, body, 10, FontStyle.Normal, TextAnchor.UpperLeft);
            UiThemeResources.ApplyTextStyle(bodyText, UiThemeResources.MutedTextColor, withShadow: false);
            bodyText.horizontalOverflow = HorizontalWrapMode.Wrap;
            bodyText.verticalOverflow = VerticalWrapMode.Truncate;
            SetRect(bodyText.rectTransform, new Vector2(0.28f, 0.12f), new Vector2(0.95f, 0.52f), Vector2.zero, Vector2.zero);
        }

        private static void CreateResultBadge(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, string iconPath, string label)
        {
            var badge = CreatePanel(name, parent, new Color(0.27f, 0.21f, 0.15f, 0.96f), UiThemeResources.Paths.CardPanel);
            SetRect(badge.GetComponent<RectTransform>(), anchorMin, anchorMax, Vector2.zero, Vector2.zero);

            var icon = CreateDecorativeImage("Icon", badge.transform, iconPath, new Color(0.35f, 0.23f, 0.14f, 1f));
            SetRect(icon.rectTransform, new Vector2(0.08f, 0.16f), new Vector2(0.34f, 0.84f), Vector2.zero, Vector2.zero);

            var text = CreateText("Label", badge.transform, label, 14, FontStyle.Bold, TextAnchor.MiddleLeft);
            UiThemeResources.ApplyTextStyle(text, UiThemeResources.BrightTextColor);
            SetRect(text.rectTransform, new Vector2(0.40f, 0.10f), new Vector2(0.92f, 0.90f), Vector2.zero, Vector2.zero);
        }

        private static void CreateCornerOrnaments(Transform parent, string rootName, string iconPath, Color tint)
        {
            var ornamentRoot = new GameObject(rootName);
            ornamentRoot.transform.SetParent(parent, false);

            CreateCornerIcon(ornamentRoot.transform, "TopLeft", iconPath, tint, new Vector2(0.03f, 0.80f), new Vector2(0.13f, 0.94f), 0f);
            CreateCornerIcon(ornamentRoot.transform, "TopRight", iconPath, tint, new Vector2(0.87f, 0.80f), new Vector2(0.97f, 0.94f), 0f);
            CreateCornerIcon(ornamentRoot.transform, "BottomLeft", iconPath, tint, new Vector2(0.04f, 0.06f), new Vector2(0.12f, 0.18f), 180f);
            CreateCornerIcon(ornamentRoot.transform, "BottomRight", iconPath, tint, new Vector2(0.88f, 0.06f), new Vector2(0.96f, 0.18f), 180f);
        }

        private static void CreateCornerIcon(Transform parent, string name, string iconPath, Color tint, Vector2 anchorMin, Vector2 anchorMax, float rotationZ)
        {
            var icon = CreateDecorativeImage(name, parent, iconPath, tint);
            SetRect(icon.rectTransform, anchorMin, anchorMax, Vector2.zero, Vector2.zero);
            icon.rectTransform.localEulerAngles = new Vector3(0f, 0f, rotationZ);
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
