using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SimbirSoft.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(string country, string city, string street, int? house, int? zipcode, string start, string end)
        {
            SelectedCountry = country;
            SelectedCity = city;
            SelectedStreet = street;
            SelectedHouse = house;
            SelectedZipcode = zipcode;
            SelectedStartDate = start;
            SelectedEndDate = end;
        }

        public string SelectedCountry { get; private set; }
        public string SelectedCity { get; private set; }
        public string SelectedStreet { get; private set; }
        public int? SelectedHouse { get; private set; }
        public int? SelectedZipcode { get; private set; }
        public string SelectedStartDate { get; private set; }
        public string SelectedEndDate { get; private set; }
    }
}