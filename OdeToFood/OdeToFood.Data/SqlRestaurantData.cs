using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _db.Restaurants.OrderBy(r => r.Name);
        }

        public IEnumerable<Restaurant> FilterByName(string term)
        {
            var normalizeTerm = term.ToLowerInvariant();
            return _db.Restaurants
                    .Where(r => r.Name.ToLowerInvariant().Contains(normalizeTerm))
                    .OrderBy(r => r.Name);
        }

        public Restaurant GetById(int? id)
        {
            return _db.Restaurants.Find(id);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var entity = _db.Restaurants.Attach(restaurant);
            entity.State = EntityState.Modified;
            return restaurant;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            _db.Add(restaurant);
            return restaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant == null)
            {
                throw (new Exception("Ups... Restaurant not found."));
            }
            _db.Restaurants.Remove(restaurant); 
            return restaurant;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public int GetCount()
        {
            return _db.Restaurants.Count();
        }
    }
}