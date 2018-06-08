namespace WorldBuilder.WorldEditor.Buttons
{
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;
    using Zone;

    // TODO Replace with dynamic UI system.
    [RequireComponent(typeof(GameObjectContext))]
    public class ZonePicker : MonoBehaviour
    {
        // Dependencies
        [Inject] private ZoneFactory zoneFactory;

        // Inspector
        public InputField ZoneField;

        public void OnLoadZoneButtonPressed()
        {
            zoneFactory.LoadZoneFromPacks(ZoneField.text + ".zone");
        }
    }
}