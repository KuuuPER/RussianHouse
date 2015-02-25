using System;
using RussianHouse.SectionsContent;

namespace RussianHouse.ViewModel
{
    public class FundRisingViewModel
    {
        public ISectionContent Content { get; set; }

        public PaymentsAndGiftsViewModel[] PaymentsAndGifts { get; set; }

        public RoadMapViewModel[] RoadMaps { get; set; }

        public Guid Guid { get; set; }
    }

    public class RoadMapViewModel
    {
        public int TotalSum { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }

    public class PaymentsAndGiftsViewModel
    {
        public int PaymentSum { get; set; }

        public string[] Gifts { get; set; }

        public bool IsPopular { get; set; }
    }
}