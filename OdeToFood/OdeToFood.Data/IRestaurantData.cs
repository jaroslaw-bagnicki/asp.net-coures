using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        IEnumerable<Restaurant> FilterByName(string term);
        Restaurant GetById(int? id);
        Restaurant Update(Restaurant restaurant);
        Restaurant Add(Restaurant restaurant);
        Restaurant Delete(int id);
        int Commit();
    }
}