namespace SpecFlow.Helpers
{
    public class LocatorBuilder
    {
        readonly private string search = "%s";

        public LocatorBuilder()
        {
        }

        public string BuildLocator(string Locator, string ReplacePart)
        {
            return Locator.Replace("%s", ReplacePart);
        }

        public string BuildLocator(string Locator, string[] ReplaceParts)
        {
            foreach (string Replacement in ReplaceParts)
            {
                Locator = ReplaceFirst(Locator, Replacement);
            }

            return Locator;
        }

        private string ReplaceFirst(string text, string replace)
        {
            int pos = text.IndexOf(this.search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + this.search.Length);
        }
    }
}
