using System;
using RussianHouse.Properties;

namespace RussianHouse.SectionsContent
{
    public static class SectionContentCreator
    {
        public static ISectionContent GetContentBySection(EnSection section)
        {
            switch (section)
            {
                case EnSection.AboutProject:
                    return new TextContent {Content = Resources.AboutProject, Title = "О проекте"};
                case EnSection.FundRising:
                    return new TextContent { Content = Resources.FundRising, Title = "Сбор средств" };
                case EnSection.News:
                    return new TextContent { Content = Resources.News, Title = "Новости" };
                default:
                    throw new ArgumentException(@"Wrong section name!", "section");
            }
        }
    }
}