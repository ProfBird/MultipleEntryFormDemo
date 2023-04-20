using System;
using Microsoft.AspNetCore.Http;
using MultipleEntryFormDemo.Models;
using Newtonsoft.Json;

namespace MultipleEntryFormDemo.Data
{
    public class SightingRepository
    {
        const string SIGHTING_LOCATION = "SightingLocation";
        const string SIGHTING_DATE = "SightingDate";
        const string SIGHTING_BIRDER = "SightingBirder";
        const string MAX_SIGHTING_ID = "MaxSightingId";
        const string SIGHTING_BIRD_IDS = "SightingBirdIds";

        public List<Sighting> GetAllSightings(HttpContext httpContext)
        {
            int maxId = httpContext.Session.GetInt32(MAX_SIGHTING_ID) ?? 0;
            var sightings = new List<Sighting>();
            for (int i = 1; i <= maxId; i++)
            {
                var sighting = FindSighting(i, httpContext);
                if (sighting != null)
                {
                    sightings.Add(sighting);
                }
            }
            return sightings;
        }

        public Sighting FindSighting(int id, HttpContext httpContext)
        {
            Sighting sighting = null;
            int maxId = httpContext.Session.GetInt32(MAX_SIGHTING_ID) ?? 0;
            if (id <= maxId && id > 0)
            {
                sighting = new Sighting
                {
                    Location = httpContext.Session.GetString(SIGHTING_LOCATION + id),
                    Date = DateOnly.Parse(httpContext.Session.GetString(SIGHTING_DATE + id)),
                    SightingId = id
                };
                string jsonBirdIds = httpContext.Session.GetString(SIGHTING_BIRD_IDS + id);
                List<int> birdIds = JsonConvert.DeserializeObject<List<int>>(jsonBirdIds);
                // get all the Bird objects
                foreach (int birdId in birdIds)
                {
                    var bird = FindBird(birdId, httpContext);
                    sighting.Birds.Add(bird);
                }
            }
            return (sighting);
        }

        public void AddSighting(Sighting model, HttpContext httpContext)
        {
            // Get and increment the ID
            int id = httpContext.Session.GetInt32(MAX_SIGHTING_ID) ?? 0;
            httpContext.Session.SetInt32(MAX_SIGHTING_ID, ++id);
            // Store the Sighting data
            httpContext.Session.SetString(SIGHTING_LOCATION + id, model.Location);
            httpContext.Session.SetString(SIGHTING_BIRDER + id, model.Birder);
            httpContext.Session.SetString(SIGHTING_DATE + id, model.Date.ToShortDateString());
            // Store the Birds from the Birds list
            foreach (Bird bird in model.Birds)
            {
                AddBird(bird, httpContext);
            }
            // Store the IDs of from the Birds list
            List<int> birdIds = (from Bird bird in model.Birds select bird.BirdId).ToList();
            string jsonBirdIds = JsonConvert.SerializeObject(birdIds);
            httpContext.Session.SetString(SIGHTING_BIRD_IDS + id, jsonBirdIds);

        }

        public void UpdateSighting(Sighting model, HttpContext httpContext)
        {
            // TODO: Implement this
        }

        /********* Bird Model Methods **********/

        // Keys for session storage
        const string BIRD_NAME = "BirdName";
        const string BIRD_ORDER = "BirdOrder";
        const string NUMBER_BIRDS = "NumberOfBirds";
        const string MAX_BIRD_ID = "MaxBirdId";

        public List<Bird> GetAllBirds(HttpContext httpContext)
        {
            int maxId = httpContext.Session.GetInt32(MAX_BIRD_ID) ?? 0;
            var birds = new List<Bird>();
            for (int i = 1; i <= maxId; i++)
            {
                var bird = FindBird(i, httpContext);
                if (bird != null)
                {
                    birds.Add(bird);
                }
            }
            return birds;
        }

        public Bird FindBird(int id, HttpContext httpContext)
        {
            int maxId = httpContext.Session.GetInt32(MAX_BIRD_ID) ?? 0;
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

        public int AddBird(Bird model, HttpContext httpContext)
        {
            // Get and incrent the id
            int id = httpContext.Session.GetInt32(MAX_BIRD_ID) ?? 0;
            httpContext.Session.SetInt32(MAX_BIRD_ID, ++id);
            model.BirdId = id;
            // Store the Bird data
            httpContext.Session.SetString(BIRD_NAME + id, model.Name);
            httpContext.Session.SetString(BIRD_ORDER + id, model.Order);
            httpContext.Session.SetInt32(NUMBER_BIRDS + id, model.Number);
            return id;
        }

        public List<String> GetAllOrders(HttpContext httpContext)
        {
            // For now, the list of orders is hard coded. Later it will support CRUD operations
            List<String> birds = new()
            {
                "Struthioniformes",
                "Galliformes",
                "Anseriformes",
                "Psittaciformes",
                "Strigiformes",
                "Apodiformes",
                "Coraciiformes",
                "Falconiformes",
                "Gaviiformes",
                "Piciformes",
                "Charadriformes",
                "Ciconiiformes",
                "Columbiformes",
                "Passeriformes"
            };

            return birds;
        }
    }
}

