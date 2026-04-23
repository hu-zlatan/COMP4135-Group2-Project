using System.Collections.Generic;
using UnityEngine;

namespace TacticalCards
{
    public sealed class BattleHudSnapshot
    {
        public string TurnLabel { get; set; }
        public string StatusMessage { get; set; }
        public int RemainingCardPlays { get; set; }
        public int MaxCardPlays { get; set; }
        public bool IsBattleOver { get; set; }
        public bool CanEndTurn { get; set; }
        public bool CanPlayCards { get; set; }
        public string PromptTitle { get; set; }
        public string PromptDetail { get; set; }
        public UnitHudSnapshot SelectedUnit { get; set; }
        public CardHudSnapshot SelectedCard { get; set; }
        public IReadOnlyList<UnitHudSnapshot> Units { get; set; }
        public IReadOnlyList<CardHudSnapshot> Hand { get; set; }
        public IReadOnlyList<string> ActionLog { get; set; }
    }

    public sealed class UnitHudSnapshot
    {
        public string DisplayName { get; set; }
        public TeamType Team { get; set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public float HealthRatio { get; set; }
        public Vector2Int GridPosition { get; set; }
        public Vector3 WorldPosition { get; set; }
        public bool IsSelected { get; set; }
    }

    public sealed class CardHudSnapshot
    {
        public CardData Card { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Stats { get; set; }
        public string UseHint { get; set; }
        public string DisabledReason { get; set; }
        public Color Tint { get; set; }
        public bool IsSelected { get; set; }
        public bool IsPlayable { get; set; }
    }
}
