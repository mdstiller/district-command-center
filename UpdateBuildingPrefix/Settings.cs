using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ColossalFramework.Plugins;

namespace UpdateBuildingPrefix.SettingsIO
{
    public class Settings
    {
        private static Settings _instance;
        private const string _settingsFileName = "UpdateBuildingPrefixSettings.xml";

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = createFromFile();
                }

                return _instance;
            }
        }

        public bool EnableMod { get; internal set; } = true;

        public void Save()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(Settings));
                using (TextWriter writer = new StreamWriter(_settingsFileName))
                {
                    ser.Serialize(writer, this);
                }
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Ex. Building Info: " + ex.Message);
            }
        }

        public static Settings createFromFile()
        {
            if (!File.Exists(_settingsFileName))
                return new Settings();

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(Settings));
                using (TextReader reader = new StreamReader(_settingsFileName))
                {
                    Settings instance = (Settings)ser.Deserialize(reader);

                    return instance;
                }
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Ex. Building Info: " + ex.Message);

                return new Settings();
            }
        }
    }
}
