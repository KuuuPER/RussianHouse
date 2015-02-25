using RussianHouse.SectionsContent;

namespace RussianHouse.ViewModel
{
    public class TeamMemberViewModel
    {
        public ISectionContent Content { get; set; }

        public TeamMember[] Members { get; set; }
    }

    public class TeamMember
    {
        public string Title { get; set; }
        public string Name { get; set; }
    }
}