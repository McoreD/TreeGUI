using ShareX.HelpersLib;

namespace TreeGUI
{
    public class TreeGUISettings<T> : SettingsBase<T> where T : TreeGUISettings<T>, new()
    {
        public delegate void SettingsChangedEventHandler(T settings);
        public event SettingsChangedEventHandler SettingsChanged;

        public void TriggerSettingsChange()
        {
            OnSettingsChanged();
        }

        protected virtual void OnSettingsChanged()
        {
            if (SettingsChanged != null)
            {
                SettingsChanged((T)this);
            }
        }
    }
}