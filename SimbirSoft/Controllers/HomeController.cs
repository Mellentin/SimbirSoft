using System;
using System.Collections.Generic;
using System.Linq;
using SimbirSoft.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;

namespace SimbirSoft.Controllers
{
    public class HomeController : Controller
    {
        AddressesEntities db = new AddressesEntities();

        public async Task<ActionResult> Index(string country, string city, string street, int? house, int? zipcode, string start, string end, int page = 1, SortState sortOrder = SortState.CountryAsc)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            DateTime.TryParse(start, out startDate);
            DateTime.TryParse(end, out endDate);

            int pageSize = 100;

            //фильтрация
            IQueryable<Address> addressess = db.Address.AsQueryable();

            if (!String.IsNullOrEmpty(country))
            {
                addressess = addressess.Where(p => p.country.Contains(country));
            }

            if (!String.IsNullOrEmpty(city))
            {
                addressess = addressess.Where(p => p.city.Contains(city));
            }

            if (!String.IsNullOrEmpty(street))
            {
                addressess = addressess.Where(p => p.street.Contains(street));
            }

            if (house != null && house != 0)
            {
                addressess = addressess.Where(p => p.house == house);
            }

            if (zipcode != null && zipcode != 0)
            {
                addressess = addressess.Where(p => p.zipcode == zipcode);
            }

            if (start != null && end != null)
            {
                addressess = addressess.Where(p => p.date > startDate && p.date < endDate);
            }

            //сортировка
            switch (sortOrder)
            {
                case SortState.CountryDesc:
                    addressess = addressess.OrderByDescending(s => s.country);
                    break;

                case SortState.CityAsc:
                    addressess = addressess.OrderBy(s => s.city);
                    break;

                case SortState.CityDesc:
                    addressess = addressess.OrderByDescending(s => s.city);
                    break;

                case SortState.StreetAsc:
                    addressess = addressess.OrderBy(s => s.street);
                    break;

                case SortState.StreetDesc:
                    addressess = addressess.OrderByDescending(s => s.street);
                    break;

                case SortState.HouseAsc:
                    addressess = addressess.OrderBy(s => s.house);
                    break;

                case SortState.HouseDesc:
                    addressess = addressess.OrderByDescending(s => s.house);
                    break;

                case SortState.ZipcodeAsc:
                    addressess = addressess.OrderBy(s => s.zipcode);
                    break;

                case SortState.ZipcodeDesc:
                    addressess = addressess.OrderByDescending(s => s.zipcode);
                    break;

                case SortState.DateAsc:
                    addressess = addressess.OrderBy(s => s.date);
                    break;

                case SortState.DateDesc:
                    addressess = addressess.OrderByDescending(s => s.date);
                    break;

                default:
                    addressess = addressess.OrderBy(s => s.country);
                    break;
            }

            // пагинация
            var count = await addressess.CountAsync();
            var items = await addressess.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["TotalItems"] = addressess.Count();

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(country, city, street, house, zipcode, start, end),
                Addressess = items
            };

            return View(viewModel);
        }
    }
}