using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RED2
{
    public class ConfigurationManger
    {
        private XMLSettings settings = null;
        private Dictionary<string, Control> controls = new Dictionary<string, Control>();
        private Dictionary<string, string> defaultValues = new Dictionary<string, string>();

        // Registry keys
        private string MenuName = "Folder\\shell\\{0}";
        private string Command = "Folder\\shell\\{0}\\command";

        public event EventHandler OnSettingsSaved;
        public int DeletedFolderCount { get; set; }   
     
        public ConfigurationManger(string configPath)
        {
            // Settings object:
            settings = new XMLSettings(configPath);

            this.MenuName = String.Format(MenuName, RED2.Properties.Resources.registry_name);
            this.Command = String.Format(Command, RED2.Properties.Resources.registry_name);
        }

        public void AddControl(string name, Control c, string defaultValue)
        {
            this.controls.Add(name, c);
            this.defaultValues.Add(name, defaultValue);
        }

        public void LoadOptions()
        {
            foreach (var key in this.controls.Keys)
            {
                var c = this.controls[key];

                if (key == "registry_explorer_integration") ((CheckBox)c).Checked = SystemFunctions.IsRegKeyIntegratedIntoWindowsExplorer(MenuName);
                else if (c is CheckBox) ((CheckBox)c).Checked = this.settings.Read(key, Boolean.Parse(this.defaultValues[key]));
                else if (c is TextBox) ((TextBox)c).Text = this.settings.Read(key, SystemFunctions.FixLineBreaks(this.defaultValues[key]));
                else if (c is NumericUpDown) ((NumericUpDown)c).Value = (int)this.settings.Read(key, Int32.Parse(this.defaultValues[key]));
                else if (c is ComboBox) ((ComboBox)c).SelectedIndex = (int)this.settings.Read(key, Int32.Parse(this.defaultValues[key]));
                else if (c is Label) {
                    this.DeletedFolderCount = (int)this.settings.Read(key, Int32.Parse(this.defaultValues[key]));
                    ((Label)c).Text = String.Format(RED2.Properties.Resources.red_deleted, this.DeletedFolderCount);
                }
                else
                    throw new Exception("Unknown control type: " + c.GetType().ToString());
            }

            foreach (var key in this.controls.Keys)
            {
                var c = this.controls[key];

                if (c is CheckBox) ((CheckBox)c).CheckedChanged += new EventHandler(Options_CheckedChanged);
                else if (c is TextBox) ((TextBox)c).TextChanged += new EventHandler(Options_CheckedChanged);
                else if (c is NumericUpDown) ((NumericUpDown)c).ValueChanged += new EventHandler(Options_CheckedChanged);
                else if (c is ComboBox) ((ComboBox)c).SelectedIndexChanged += new EventHandler(Options_CheckedChanged);
                else if (!(c is Label)) 
                    throw new Exception("Unknown control type: " + c.GetType().ToString());
            }
        }

        private void Options_CheckedChanged(object sender, EventArgs e)
        {
            this.Save();
        }

        internal void Save()
        {
            foreach (var key in this.controls.Keys)
            {
                var c = this.controls[key];

                if (key == "registry_explorer_integration")
                {
                    SystemFunctions.AddOrRemoveRegKey(!((CheckBox)c).Checked, MenuName, Command);
                }
                else if (c is CheckBox) this.settings.Write(key, ((CheckBox)c).Checked);
                else if (c is TextBox) this.settings.Write(key, ((TextBox)c).Text);
                else if (c is NumericUpDown) this.settings.Write(key, (int)((NumericUpDown)c).Value);
                else if (c is ComboBox) this.settings.Write(key, (int)((ComboBox)c).SelectedIndex);
                else if (c is Label)  this.settings.Write(key, this.DeletedFolderCount);
                else
                    throw new Exception("Unknown control type: " + c.GetType().ToString());
            }

            if (OnSettingsSaved != null)
                this.OnSettingsSaved(this, new EventArgs());

        }

        internal int GetCustomSetting(string key, int defaultValue)
        {
            return this.settings.Read(key, defaultValue);
        }

        internal void SaveCustom(string key, int value)
        {
            this.settings.Write(key, value);
        }
    }
}
