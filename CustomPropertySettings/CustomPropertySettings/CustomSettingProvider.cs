using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Xml.Linq;
using System.IO;

namespace CustomPropertySettings
{
    class CustomSettingProvider : SettingsProvider
    {
        const string NAME = "name";
        const string SERIALIZE_AS = "serializeAs";
        const string CONFIG = "configuration";
        const string USER_SETTINGS = "userSettings";
        const string SETTING = "setting";


        /// <summary>
        /// Helper struct.
        /// </summary>
        internal struct SettingStruct
        {
            internal string name;
            internal string serializeAs;
            internal string value;
        }

        /// <summary>
        /// In memory storage of the settings values
        /// </summary>
        private Dictionary<string, SettingStruct> settingsDictionary;

        private bool loaded = false;
        private string fileName = "";
        private bool needMerge = false;


        public CustomSettingProvider()
        {
            settingsDictionary = new Dictionary<string, SettingStruct>();
        }

        /// <summary>
        /// Override.
        /// </summary>
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(ApplicationName, config);
        }

        public override string ApplicationName
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name;
            }

            set
            {
                //Do nothing...
            }
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            //load the file
            if (!loaded)
            {
                loaded = true;

                string[] temp = (context["GroupName"] as string).Split('.');
                if(temp != null && temp.Length > 0)
                {
                    fileName = temp[temp.Length - 1];
                }

                LoadValuesFromFile();
            }

            //collection that will be returned.
            SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();

            //itterate thought the properties we get from the designer, checking to see if the setting is in the dictionary
            foreach (SettingsProperty setting in collection)
            {
                SettingsPropertyValue value = new SettingsPropertyValue(setting);
                value.IsDirty = false;

                //need the type of the value for the strong typing
                var t = Type.GetType(setting.PropertyType.FullName);

                if (settingsDictionary.ContainsKey(setting.Name))
                {
                    value.SerializedValue = settingsDictionary[setting.Name].value;
                    value.PropertyValue = Convert.ChangeType(settingsDictionary[setting.Name].value, t);
                }
                else //use defaults in the case where there are no settings yet
                {
                    value.SerializedValue = setting.DefaultValue;
                    value.PropertyValue = Convert.ChangeType(setting.DefaultValue, t);
                }

                values.Add(value);
            }

            if (needMerge)
                MergeFromDefaultSettings(values);

            return values;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            //grab the values from the collection parameter and update the values in our dictionary.
            foreach (SettingsPropertyValue value in collection)
            {
                var setting = new SettingStruct()
                {
                    value = (value.PropertyValue == null ? String.Empty : value.PropertyValue.ToString()),
                    name = value.Name,
                    serializeAs = value.Property.SerializeAs.ToString()
                };

                if (!settingsDictionary.ContainsKey(value.Name))
                {
                    settingsDictionary.Add(value.Name, setting);
                }
                else
                {
                    settingsDictionary[value.Name] = setting;
                }
            }

            //now that our local dictionary is up-to-date, save it to disk.
            SaveValuesToFile();
        }



        /// <summary>
        /// The setting key this is returning must set before the settings are used.
        /// e.g. <c>Properties.Settings.Default.SettingsKey = @"C:\temp\user.config";</c>
        /// </summary>
        private string UserConfigPath
        {
            get
            {
                return Properties.Settings.Default.ConfigPath;
            }

        }

        /// <summary>
        /// Creates an empty user.config file...looks like the one MS creates.  
        /// This could be overkill a simple key/value pairing would probably do.
        /// </summary>
        private void CreateEmptyConfig()
        {
            var doc = new XDocument();
            var declaration = new XDeclaration("1.0", "utf-8", "true");
            var config = new XElement(CONFIG);
            var userSettings = new XElement(USER_SETTINGS);
            var group = new XElement(typeof(Properties.Settings).FullName);
            userSettings.Add(group);
            config.Add(userSettings);
            doc.Add(config);
            doc.Declaration = declaration;
            doc.Save(UserConfigPath);
        }

        /// <summary>
        /// merge from the default settings.
        /// </summary>
        private void MergeFromDefaultSettings(SettingsPropertyValueCollection values)
        {
            needMerge = false;
            foreach (SettingsPropertyValue vv in Properties.Settings.Default.PropertyValues)
            {
                if (values[vv.Name] != null)
                {
                    values.Remove(vv.Name);
                    values.Add(vv);
                    var setting = new SettingStruct()
                    {
                        value = (vv.PropertyValue == null ? String.Empty : vv.PropertyValue.ToString()),
                        name = vv.Name,
                        serializeAs = vv.Property.SerializeAs.ToString()
                    };
                    settingsDictionary[vv.Name] = setting;
                }
            }

            SaveValuesToFile();
        }

        /// <summary>
        /// Loads the values of the file into memory.
        /// </summary>
        private void LoadValuesFromFile()
        {
            if (!File.Exists(UserConfigPath))
            {
                //if the config file is not where it's supposed to be create a new one.
                CreateEmptyConfig();

                //if the config file is not existing. Copy the default settings
                //MergeFromDefaultSettings();
                needMerge = true;
            }

            //load the xml
            var configXml = XDocument.Load(UserConfigPath);

            //get all of the <setting name="..." serializeAs="..."> elements.
            var settingElements = configXml.Element(CONFIG).Element(USER_SETTINGS).Element(typeof(Properties.Settings).FullName).Elements(SETTING);

            //iterate through, adding them to the dictionary, (checking for nulls, xml no likey nulls)
            //using "String" as default serializeAs...just in case, no real good reason.
            foreach (var element in settingElements)
            {
                var newSetting = new SettingStruct()
                {
                    name = element.Attribute(NAME) == null ? String.Empty : element.Attribute(NAME).Value,
                    serializeAs = element.Attribute(SERIALIZE_AS) == null ? "String" : element.Attribute(SERIALIZE_AS).Value,
                    value = element.Value ?? String.Empty
                };
                settingsDictionary[element.Attribute(NAME).Value] = newSetting;
            }
        }

        /// <summary>
        /// Saves the in memory dictionary to the user config file
        /// </summary>
        private void SaveValuesToFile()
        {
            //load the current xml from the file.
            var import = XDocument.Load(UserConfigPath);

            //get the settings group (e.g. <Company.Project.Desktop.Settings>)
            var settingsSection = import.Element(CONFIG).Element(USER_SETTINGS).Element(typeof(Properties.Settings).FullName);

            //iterate though the dictionary, either updating the value or adding the new setting.
            foreach (var entry in settingsDictionary)
            {
                var setting = settingsSection.Elements().FirstOrDefault(e => e.Attribute(NAME).Value == entry.Key);
                if (setting == null) //this can happen if a new setting is added via the .settings designer.
                {
                    var newSetting = new XElement(SETTING);
                    newSetting.Add(new XAttribute(NAME, entry.Value.name));
                    newSetting.Add(new XAttribute(SERIALIZE_AS, entry.Value.serializeAs));
                    newSetting.Value = (entry.Value.value ?? String.Empty);
                    settingsSection.Add(newSetting);
                }
                else //update the value if it exists.
                {
                    setting.Value = (entry.Value.value ?? String.Empty);
                }
            }
            import.Save(UserConfigPath);
        }

    }
}
