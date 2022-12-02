using System;
using Microsoft.AspNetCore.Http;
using MultipleEntryFormDemo.Models;

namespace MultipleEntryFormDemo.Data
{
    public class BirdRepository
    {
        const string BIRD_NAME = "BirdName";
        const string BIRD_ORDER = "BirdOrder";
        const string NUMBER_BIRDS = "NumberOfBirds";
        const string MAX_ID = "MaxId";

        public List<Bird> GetAllBirds(HttpContext httpContext)
        {
            int maxId = httpContext.Session.GetInt32(MAX_ID) ?? 0;
            var birds = new List<Bird>();
            for (int i = 1; i <= maxId; i++)
            {
                var bird = Find(i, httpContext);
                if (bird != null)
                {
                    birds.Add(bird);
                }
            }
            return birds;
        }

        public Bird Find(int id, HttpContext httpContext)
        {
            int maxId = httpContext.Session.GetInt32(MAX_ID) ?? 0;
            Bird model = null;
            if (id <= maxId && id > 0)
            {
                model = new Bird
                {
                    BirdId = id,
                    Name = httpContext.Session.GetString(BIRD_NAME + id) ?? "",
                    Order = httpContext.Session.GetString(BIRD_ORDER + id) ?? "",
                    Number = httpContext.Session.GetInt32(NUMBER_BIRDS + id) ?? 0
                };
            }
            return (model);
        }

        public void Add(Bird model, HttpContext httpContext)
        {
            int id = httpContext.Session.GetInt32(MAX_ID) ?? 0;
            httpContext.Session.SetInt32(MAX_ID, ++id);
            httpContext.Session.SetString(BIRD_NAME + id, model.Name);
            httpContext.Session.SetString(BIRD_ORDER + id, model.Order);
            httpContext.Session.SetInt32(NUMBER_BIRDS + id, model.Number);
        }
    }
}

