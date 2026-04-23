using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalCards
{
    public static class UiThemeResources
    {
        public static class Paths
        {
            public const string TitleBanner = "UITheme/Panels/banner_classic_curtain";
            public const string ResultBanner = "UITheme/Panels/banner_hanging";
            public const string PanelPrimary = "UITheme/Panels/panel_brown_dark";
            public const string PanelSecondary = "UITheme/Panels/panel_grey_bolts_dark";
            public const string PanelAccent = "UITheme/Panels/panel_brown_arrows_dark";
            public const string CardPanel = "UITheme/Panels/panel_grey_blue";
            public const string ButtonPrimary = "UITheme/Panels/button_brown";
            public const string ButtonDanger = "UITheme/Panels/button_red";
            public const string IconCard = "UITheme/Icons/card";
            public const string IconCardsFan = "UITheme/Icons/cards_fan";
            public const string IconCrown = "UITheme/Icons/crown_a";
            public const string IconCharacter = "UITheme/Icons/character";
            public const string IconMove = "UITheme/Icons/arrow_right";
            public const string IconDash = "UITheme/Icons/arrow_right_curve";
            public const string IconStrike = "UITheme/Icons/sword";
            public const string IconGuard = "UITheme/Icons/shield";
            public const string IconPush = "UITheme/Icons/character";
            public const string IconHeal = "UITheme/Icons/suit_hearts";
        }

        public static readonly Color InkColor = new(0.18f, 0.12f, 0.08f, 1f);
        public static readonly Color BrightTextColor = new(0.97f, 0.93f, 0.84f, 1f);
        public static readonly Color MutedTextColor = new(0.89f, 0.82f, 0.69f, 1f);
        public static readonly Color ShadowColor = new(0.07f, 0.04f, 0.03f, 0.8f);
        public static readonly Color IconTint = new(0.31f, 0.19f, 0.10f, 1f);

        private static readonly Dictionary<string, Sprite> SpriteCache = new();

        public static Sprite GetSprite(string resourcePath)
        {
            if (string.IsNullOrWhiteSpace(resourcePath))
            {
                return null;
            }

            if (SpriteCache.TryGetValue(resourcePath, out var cachedSprite))
            {
                return cachedSprite;
            }

            var texture = Resources.Load<Texture2D>(resourcePath);
            if (texture == null)
            {
                return null;
            }

            var sprite = Sprite.Create(
                texture,
                new Rect(0f, 0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f);

            SpriteCache[resourcePath] = sprite;
            return sprite;
        }

        public static Sprite GetCardIcon(CardType cardType)
        {
            return GetSprite(cardType switch
            {
                CardType.Move => Paths.IconMove,
                CardType.Strike => Paths.IconStrike,
                CardType.Guard => Paths.IconGuard,
                CardType.Push => Paths.IconPush,
                CardType.Heal => Paths.IconHeal,
                CardType.Dash => Paths.IconDash,
                _ => Paths.IconCard,
            });
        }

        public static void ApplySprite(Image image, string resourcePath, Color tint, bool preserveAspect = false)
        {
            if (image == null)
            {
                return;
            }

            var sprite = GetSprite(resourcePath);
            image.sprite = sprite;
            image.color = tint;
            image.preserveAspect = preserveAspect;
        }

        public static void ApplyTextStyle(Text text, Color color, bool withShadow = true)
        {
            if (text == null)
            {
                return;
            }

            text.color = color;
            if (!withShadow)
            {
                return;
            }

            var shadow = text.GetComponent<Shadow>() ?? text.gameObject.AddComponent<Shadow>();
            shadow.effectColor = ShadowColor;
            shadow.effectDistance = new Vector2(1f, -1f);
        }
    }
}
