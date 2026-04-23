using System;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalCards
{
    public class UnitVisualShell : MonoBehaviour
    {
        private const string PresentationRootName = "UnitPresentation";

        [SerializeField] private Transform presentationRoot;
        [SerializeField] private Renderer[] primaryRenderers = Array.Empty<Renderer>();
        [SerializeField] private Renderer[] accentRenderers = Array.Empty<Renderer>();
        [SerializeField] private Renderer[] detailRenderers = Array.Empty<Renderer>();

        private MeshRenderer rootRenderer;
        private Material primaryMaterial;
        private Material accentMaterial;
        private Material detailMaterial;
        private string currentArchetype;

        public void Apply(UnitController unit)
        {
            if (unit == null)
            {
                return;
            }

            rootRenderer ??= GetComponent<MeshRenderer>();
            if (rootRenderer != null)
            {
                rootRenderer.enabled = false;
            }

            var archetype = ResolveArchetype(unit);
            if (presentationRoot == null || !string.Equals(currentArchetype, archetype, StringComparison.Ordinal))
            {
                Rebuild(archetype);
            }

            var palette = ResolvePalette(unit, archetype);
            ApplyMaterial(primaryMaterial, palette.primary);
            ApplyMaterial(accentMaterial, palette.accent);
            ApplyMaterial(detailMaterial, palette.detail);
        }

        private void Rebuild(string archetype)
        {
            if (presentationRoot != null)
            {
                SafeDestroy(presentationRoot.gameObject);
            }

            currentArchetype = archetype;
            var rootObject = new GameObject(PresentationRootName);
            rootObject.transform.SetParent(transform, false);
            presentationRoot = rootObject.transform;

            var primary = new List<Renderer>();
            var accent = new List<Renderer>();
            var detail = new List<Renderer>();

            switch (archetype)
            {
                case "Ranger":
                    BuildRanger(presentationRoot, primary, accent, detail);
                    break;
                case "Spider":
                    BuildSpider(presentationRoot, primary, accent, detail);
                    break;
                default:
                    BuildKnight(presentationRoot, primary, accent, detail);
                    break;
            }

            primaryRenderers = primary.ToArray();
            accentRenderers = accent.ToArray();
            detailRenderers = detail.ToArray();

            primaryMaterial = WorldThemeResources.CreateMaterial($"{name}_Primary", WorldThemeResources.KnightPrimary);
            accentMaterial = WorldThemeResources.CreateMaterial($"{name}_Accent", WorldThemeResources.KnightAccent);
            detailMaterial = WorldThemeResources.CreateMaterial($"{name}_Detail", WorldThemeResources.NeutralMetal);

            AssignMaterial(primaryRenderers, primaryMaterial);
            AssignMaterial(accentRenderers, accentMaterial);
            AssignMaterial(detailRenderers, detailMaterial);
        }

        private static string ResolveArchetype(UnitController unit)
        {
            if (unit.Team == TeamType.Enemy && unit.DisplayName.Contains("Spider", StringComparison.OrdinalIgnoreCase))
            {
                return "Spider";
            }

            if (unit.DisplayName.Contains("Ranger", StringComparison.OrdinalIgnoreCase))
            {
                return "Ranger";
            }

            return unit.Team == TeamType.Enemy ? "Spider" : "Knight";
        }

        private static (Color primary, Color accent, Color detail) ResolvePalette(UnitController unit, string archetype)
        {
            var primary = archetype switch
            {
                "Ranger" => WorldThemeResources.RangerPrimary,
                "Spider" => WorldThemeResources.EnemyPrimary,
                _ => WorldThemeResources.KnightPrimary,
            };

            var accent = archetype switch
            {
                "Ranger" => WorldThemeResources.RangerAccent,
                "Spider" => WorldThemeResources.EnemySecondaryMarker,
                _ => WorldThemeResources.KnightAccent,
            };

            var detail = archetype switch
            {
                "Spider" => WorldThemeResources.EnemyAccent,
                _ => WorldThemeResources.NeutralMetal,
            };

            if (unit.Team == TeamType.Enemy && archetype != "Spider")
            {
                primary = WorldThemeResources.EnemyPrimary;
                accent = WorldThemeResources.EnemyMarker;
            }

            var healthRatio = unit.MaxHp <= 0 ? 1f : (float)unit.CurrentHp / unit.MaxHp;
            if (healthRatio < 0.5f)
            {
                var woundBlend = Mathf.InverseLerp(0.5f, 0f, healthRatio) * 0.55f;
                primary = WorldThemeResources.Blend(primary, WorldThemeResources.WoundTint, woundBlend);
            }

            if (unit.IsGuarding)
            {
                accent = WorldThemeResources.GuardAura;
            }

            return (primary, accent, detail);
        }

        private static void BuildKnight(Transform root, List<Renderer> primary, List<Renderer> accent, List<Renderer> detail)
        {
            CreatePart(PrimitiveType.Cylinder, root, "BaseDisc", new Vector3(0f, -0.55f, 0f), new Vector3(0.42f, 0.025f, 0.42f), Vector3.zero, accent);
            CreatePart(PrimitiveType.Capsule, root, "Torso", new Vector3(0f, -0.12f, 0f), new Vector3(0.18f, 0.28f, 0.16f), Vector3.zero, primary);
            CreatePart(PrimitiveType.Sphere, root, "Head", new Vector3(0f, 0.22f, 0f), new Vector3(0.20f, 0.20f, 0.20f), Vector3.zero, detail);
            CreatePart(PrimitiveType.Cube, root, "Shoulders", new Vector3(0f, 0.02f, 0f), new Vector3(0.38f, 0.08f, 0.18f), Vector3.zero, primary);
            CreatePart(PrimitiveType.Cube, root, "Shield", new Vector3(-0.24f, -0.04f, 0.15f), new Vector3(0.10f, 0.24f, 0.24f), new Vector3(8f, 14f, 0f), accent);
            CreatePart(PrimitiveType.Cube, root, "Blade", new Vector3(0.24f, -0.08f, 0.14f), new Vector3(0.05f, 0.32f, 0.05f), new Vector3(18f, 0f, -28f), detail);
            CreatePart(PrimitiveType.Cube, root, "HelmetBand", new Vector3(0f, 0.20f, 0.13f), new Vector3(0.22f, 0.04f, 0.08f), Vector3.zero, accent);
        }

        private static void BuildRanger(Transform root, List<Renderer> primary, List<Renderer> accent, List<Renderer> detail)
        {
            CreatePart(PrimitiveType.Cylinder, root, "BaseDisc", new Vector3(0f, -0.55f, 0f), new Vector3(0.40f, 0.025f, 0.40f), Vector3.zero, accent);
            CreatePart(PrimitiveType.Capsule, root, "Torso", new Vector3(0f, -0.12f, 0f), new Vector3(0.17f, 0.27f, 0.15f), Vector3.zero, primary);
            CreatePart(PrimitiveType.Sphere, root, "Head", new Vector3(0f, 0.20f, 0f), new Vector3(0.18f, 0.18f, 0.18f), Vector3.zero, detail);
            CreatePart(PrimitiveType.Cube, root, "Hood", new Vector3(0f, 0.18f, -0.02f), new Vector3(0.26f, 0.16f, 0.22f), new Vector3(0f, 0f, 0f), primary);
            CreatePart(PrimitiveType.Cube, root, "Cape", new Vector3(0f, -0.10f, -0.16f), new Vector3(0.24f, 0.28f, 0.08f), new Vector3(8f, 0f, 0f), accent);
            CreatePart(PrimitiveType.Cylinder, root, "Bow", new Vector3(0.26f, -0.05f, 0.08f), new Vector3(0.03f, 0.30f, 0.03f), new Vector3(0f, 0f, 72f), detail);
            CreatePart(PrimitiveType.Cube, root, "Quiver", new Vector3(-0.16f, 0.00f, -0.18f), new Vector3(0.10f, 0.24f, 0.10f), new Vector3(12f, 0f, 10f), accent);
        }

        private static void BuildSpider(Transform root, List<Renderer> primary, List<Renderer> accent, List<Renderer> detail)
        {
            CreatePart(PrimitiveType.Cylinder, root, "BaseDisc", new Vector3(0f, -0.56f, 0f), new Vector3(0.45f, 0.02f, 0.45f), Vector3.zero, accent);
            CreatePart(PrimitiveType.Sphere, root, "Abdomen", new Vector3(0f, -0.20f, -0.06f), new Vector3(0.34f, 0.24f, 0.40f), Vector3.zero, primary);
            CreatePart(PrimitiveType.Sphere, root, "Thorax", new Vector3(0f, -0.18f, 0.16f), new Vector3(0.24f, 0.18f, 0.24f), Vector3.zero, accent);
            CreatePart(PrimitiveType.Sphere, root, "Head", new Vector3(0f, -0.14f, 0.34f), new Vector3(0.14f, 0.12f, 0.16f), Vector3.zero, detail);

            var legOffsets = new[]
            {
                new Vector3(-0.28f, -0.28f, -0.18f),
                new Vector3(-0.30f, -0.26f, 0.00f),
                new Vector3(-0.26f, -0.22f, 0.18f),
                new Vector3(0.28f, -0.28f, -0.18f),
                new Vector3(0.30f, -0.26f, 0.00f),
                new Vector3(0.26f, -0.22f, 0.18f),
            };

            var legRotations = new[]
            {
                new Vector3(0f, 0f, 68f),
                new Vector3(0f, 0f, 88f),
                new Vector3(0f, 0f, 112f),
                new Vector3(0f, 0f, -68f),
                new Vector3(0f, 0f, -88f),
                new Vector3(0f, 0f, -112f),
            };

            for (var i = 0; i < legOffsets.Length; i++)
            {
                CreatePart(PrimitiveType.Capsule, root, $"Leg_{i}", legOffsets[i], new Vector3(0.035f, 0.26f, 0.035f), legRotations[i], detail);
            }
        }

        private static void CreatePart(
            PrimitiveType primitiveType,
            Transform parent,
            string objectName,
            Vector3 localPosition,
            Vector3 localScale,
            Vector3 localEulerAngles,
            List<Renderer> bucket)
        {
            var part = GameObject.CreatePrimitive(primitiveType);
            part.name = objectName;
            part.transform.SetParent(parent, false);
            part.transform.localPosition = localPosition;
            part.transform.localScale = localScale;
            part.transform.localEulerAngles = localEulerAngles;

            var collider = part.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
                SafeDestroy(collider);
            }

            if (part.TryGetComponent<Renderer>(out var renderer))
            {
                bucket.Add(renderer);
            }
        }

        private static void AssignMaterial(IEnumerable<Renderer> renderers, Material material)
        {
            foreach (var renderer in renderers)
            {
                if (renderer == null)
                {
                    continue;
                }

                renderer.sharedMaterial = material;
            }
        }

        private static void ApplyMaterial(Material material, Color color)
        {
            WorldThemeResources.ApplyColor(material, color);
        }

        private static void SafeDestroy(UnityEngine.Object target)
        {
            if (target == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                Destroy(target);
                return;
            }

            DestroyImmediate(target);
        }
    }
}
