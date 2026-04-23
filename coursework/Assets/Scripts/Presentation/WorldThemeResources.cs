using System.Collections.Generic;
using UnityEngine;

namespace TacticalCards
{
    public static class WorldThemeResources
    {
        public static readonly Color BoardTileLight = new(0.86f, 0.84f, 0.78f, 1f);
        public static readonly Color BoardTileDark = new(0.72f, 0.69f, 0.63f, 1f);
        public static readonly Color MoveHighlight = new(0.31f, 0.73f, 0.49f, 1f);
        public static readonly Color AttackHighlight = new(0.78f, 0.29f, 0.26f, 1f);
        public static readonly Color SelectedTile = new(0.90f, 0.76f, 0.26f, 1f);
        public static readonly Color BoardBase = new(0.20f, 0.15f, 0.10f, 1f);
        public static readonly Color BoardFrame = new(0.28f, 0.20f, 0.13f, 1f);
        public static readonly Color BoardTrim = new(0.49f, 0.38f, 0.23f, 1f);
        public static readonly Color TableSurface = new(0.36f, 0.30f, 0.22f, 1f);
        public static readonly Color FeltInset = new(0.30f, 0.24f, 0.17f, 1f);
        public static readonly Color PropPaper = new(0.92f, 0.88f, 0.76f, 1f);
        public static readonly Color PropWax = new(0.66f, 0.19f, 0.16f, 1f);
        public static readonly Color PropBone = new(0.83f, 0.80f, 0.70f, 1f);
        public static readonly Color PlayerMarker = new(0.21f, 0.48f, 0.75f, 1f);
        public static readonly Color PlayerSecondaryMarker = new(0.17f, 0.63f, 0.43f, 1f);
        public static readonly Color EnemyMarker = new(0.69f, 0.24f, 0.24f, 1f);
        public static readonly Color EnemySecondaryMarker = new(0.43f, 0.11f, 0.11f, 1f);
        public static readonly Color KnightPrimary = new(0.20f, 0.38f, 0.70f, 1f);
        public static readonly Color KnightAccent = new(0.84f, 0.71f, 0.33f, 1f);
        public static readonly Color RangerPrimary = new(0.21f, 0.58f, 0.41f, 1f);
        public static readonly Color RangerAccent = new(0.70f, 0.58f, 0.33f, 1f);
        public static readonly Color EnemyPrimary = new(0.67f, 0.23f, 0.23f, 1f);
        public static readonly Color EnemyAccent = new(0.29f, 0.10f, 0.10f, 1f);
        public static readonly Color NeutralMetal = new(0.29f, 0.31f, 0.35f, 1f);
        public static readonly Color GuardAura = new(0.88f, 0.83f, 0.53f, 1f);
        public static readonly Color WoundTint = new(0.54f, 0.17f, 0.17f, 1f);

        private static readonly Dictionary<string, Material> SharedMaterialCache = new();

        public static Material GetSharedMaterial(string key, Color color, bool unlit = false)
        {
            if (SharedMaterialCache.TryGetValue(key, out var material) && material != null)
            {
                ApplyColor(material, color);
                return material;
            }

            material = CreateMaterial($"Shared_{key}", color, unlit);
            SharedMaterialCache[key] = material;
            return material;
        }

        public static Material CreateMaterial(string name, Color color, bool unlit = false)
        {
            var shader = ResolveShader(unlit);
            var material = new Material(shader)
            {
                name = name
            };

            ApplyColor(material, color);
            return material;
        }

        public static void ApplyColor(Material material, Color color)
        {
            if (material == null)
            {
                return;
            }

            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", color);
            }

            if (material.HasProperty("_Color"))
            {
                material.SetColor("_Color", color);
            }
        }

        public static Color Blend(Color from, Color to, float t)
        {
            return Color.Lerp(from, to, Mathf.Clamp01(t));
        }

        private static Shader ResolveShader(bool unlit)
        {
            if (unlit)
            {
                return Shader.Find("Universal Render Pipeline/Unlit")
                    ?? Shader.Find("Sprites/Default")
                    ?? Shader.Find("Standard");
            }

            return Shader.Find("Universal Render Pipeline/Lit")
                ?? Shader.Find("Standard")
                ?? Shader.Find("Sprites/Default");
        }
    }
}
