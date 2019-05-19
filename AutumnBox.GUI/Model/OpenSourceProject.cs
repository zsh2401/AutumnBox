using AutumnBox.GUI.MVVM;

namespace AutumnBox.GUI.Model
{
    class OpenSourceProject : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LicenseName { get; set; }
        public string Url { get; set; }
        public string Owner { get; set; }

        public OpenSourceProject(string name, string description, string licenseName, string url,string owner)
        {
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            Description = description ?? throw new System.ArgumentNullException(nameof(description));
            LicenseName = licenseName;
            Url = url;
            Owner = owner;
        }
    }
}
