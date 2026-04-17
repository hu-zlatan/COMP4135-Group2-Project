using UnityEngine;

namespace TacticalCards
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private Renderer tileRenderer;
        [SerializeField] private Color baseColor = Color.white;
        [SerializeField] private Color moveHighlightColor = new(0.35f, 0.8f, 0.45f);
        [SerializeField] private Color attackHighlightColor = new(0.9f, 0.35f, 0.35f);
        [SerializeField] private Color selectedColor = new(0.95f, 0.85f, 0.25f);

        private Material runtimeMaterial;

        public Vector2Int Coord { get; private set; }
        public bool IsOccupied => Occupant != null;
        public UnitController Occupant { get; private set; }

        private void Awake()
        {
            EnsureRenderer();
        }

        public void Initialize(Vector2Int coord)
        {
            Coord = coord;
            SetBaseVisual();
        }

        public void SetOccupant(UnitController occupant)
        {
            Occupant = occupant;
        }

        public void ClearOccupant(UnitController occupant)
        {
            if (Occupant == occupant)
            {
                Occupant = null;
            }
        }

        public void SetBaseVisual()
        {
            SetColor(baseColor);
        }

        public void SetMoveHighlight()
        {
            SetColor(moveHighlightColor);
        }

        public void SetAttackHighlight()
        {
            SetColor(attackHighlightColor);
        }

        public void SetSelected()
        {
            SetColor(selectedColor);
        }

        private void EnsureRenderer()
        {
            if (tileRenderer == null)
            {
                tileRenderer = GetComponentInChildren<Renderer>();
            }

            EnsureMaterial();
        }

        private void SetColor(Color color)
        {
            EnsureRenderer();
            if (tileRenderer == null)
            {
                return;
            }

            ApplyColor(color);
        }

        private void EnsureMaterial()
        {
            if (tileRenderer == null || runtimeMaterial != null)
            {
                return;
            }

            var shader = ResolveCompatibleShader();
            runtimeMaterial = new Material(shader)
            {
                name = $"TileRuntime_{GetInstanceID()}"
            };

            tileRenderer.sharedMaterial = runtimeMaterial;
            ApplyColor(baseColor);
        }

        private void ApplyColor(Color color)
        {
            if (runtimeMaterial == null)
            {
                runtimeMaterial = tileRenderer.sharedMaterial;
            }

            if (runtimeMaterial.HasProperty("_BaseColor"))
            {
                runtimeMaterial.SetColor("_BaseColor", color);
            }

            if (runtimeMaterial.HasProperty("_Color"))
            {
                runtimeMaterial.SetColor("_Color", color);
            }
        }

        private static Shader ResolveCompatibleShader()
        {
            return Shader.Find("Universal Render Pipeline/Unlit")
                ?? Shader.Find("Universal Render Pipeline/Lit")
                ?? Shader.Find("Sprites/Default")
                ?? Shader.Find("Standard");
        }
    }
}
