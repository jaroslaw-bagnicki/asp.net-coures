using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        IEnumerable<Restaurant> FilterByName(string term);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant restaurant);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant {Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian },
                new Restaurant {Id = 2, Name = "Cinnamon Club", Location = "London", Cuisine = CuisineType.Indian },
                new Restaurant {Id = 3, Name = "La Costa", Location = "California", Cuisine = CuisineType.Mexican },
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                orderby r.Name
                select r;
        }

        public IEnumerable<Restaurant> FilterByName(string term = "")
        {
            var normalizeTerm = term.ToLowerInvariant();
            return from r in restaurants
                where r.Name.ToLowerInvariant().Contains(normalizeTerm)
                orderby r.Name 
                select r;
        }

        public Restaurant GetById(int id)
        {
            return restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant changedRestaurant)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == changedRestaurant.Id);
            if (restaurant == null)
            {
                throw(new Exception("Ups..."));
            }

            restaurant.Name = changedRestaurant.Name;
            restaurant.Location = changedRestaurant.Location;
            restaurant.Cuisine = changedRestaurant.Cuisine;

            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }
    }
}