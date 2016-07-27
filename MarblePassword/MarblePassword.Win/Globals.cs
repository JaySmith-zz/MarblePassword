namespace MarblePassword.Win
{
    public class Globals
    {
        public static string CurrentPasswordDb
        {
            get
            {
                return Properties.Settings.Default.CurrentPasswordDb;
            }

            set
            {
                Properties.Settings.Default.CurrentPasswordDb = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
