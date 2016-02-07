using HelpersLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLib
{
    public class SettingsBaseEx<T> : SettingsBase<T> where T : SettingsBaseEx<T>, new()
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