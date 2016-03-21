namespace Rhyous.SectionFinder
{
    public class Line
    {
        public string Text { get; set; }
        public bool AddedToSection { get; set; }

        public void Clear()
        {
            Text = null;
            AddedToSection = false;
        }
    }
}
