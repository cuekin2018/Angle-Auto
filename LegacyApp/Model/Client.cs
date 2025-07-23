using LegacyApp.Common;

namespace LegacyApp.Model
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ClientStatus ClientStatus { get; set; }
    }
}
