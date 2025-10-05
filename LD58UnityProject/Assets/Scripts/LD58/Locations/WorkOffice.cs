using LD58.Interactables;
using LD58.Records;
using UnityEngine;

namespace LD58.Locations
{
    public class WorkOffice : MonoBehaviour, IInteractable
    {
        [SerializeField] private PresetLocation _presetLocation;
        [SerializeField] private Transform _incomesTable;

        public Vector3 Position => _incomesTable.position;
        public string InteractionDisplayText => "Take Note";
        public Vector3 TooltipWorldAnchor => _incomesTable.position;
        public bool IsInteractable => !JobRecord.Current.KnownIncomes;
        public float PriorityFactor => 20;

        public void Interact() => JobRecord.Current.LearnIncomes();
    }
}