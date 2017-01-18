using System.Configuration;

namespace Animal_Xing_Planner
{
    public class UserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        [DefaultSettingValue("")]
        public Profile CurrentProfile
        {
            get
            {
                return ((Profile)this["CurrentProfile"]);
            }
            set
            {
                this["CurrentProfile"] = value;
            }
        }
    }
}
