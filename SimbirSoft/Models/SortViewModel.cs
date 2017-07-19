using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbirSoft.Models
{
    public class SortViewModel
    {
        public SortState CountrySort { get; private set; }
        public SortState CitySort { get; private set; }
        public SortState StreetSort { get; private set; }
        public SortState HouseSort { get; private set; }
        public SortState ZipcodeSort { get; private set; }
        public SortState DateSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            CountrySort = sortOrder == SortState.CountryAsc ? SortState.CountryDesc : SortState.CountryAsc;
            CitySort = sortOrder == SortState.CityAsc ? SortState.CityDesc : SortState.CityAsc;
            StreetSort = sortOrder == SortState.StreetAsc ? SortState.StreetDesc : SortState.StreetAsc;
            HouseSort = sortOrder == SortState.HouseAsc ? SortState.HouseDesc : SortState.HouseAsc;
            ZipcodeSort = sortOrder == SortState.ZipcodeAsc ? SortState.ZipcodeDesc : SortState.ZipcodeAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            Current = sortOrder;
        }
    }
}