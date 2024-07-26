namespace Raindance
{
    public class Configuration
    {
        public string IhubRepoPath { get; set; }
        public System.Collections.Generic.List<ItemWithCheckState> StopItems { get; set; }
        public System.Collections.Generic.List<ItemWithCheckState> DeleteItems { get; set; }
        public System.Collections.Generic.List<ItemWithCheckState> RunItems { get; set; }
    }

    public class ItemWithCheckState
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
