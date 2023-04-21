using System;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
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
                "Anseriformes",
                "Apodiformes",
                "Charadriformes",
                "Ciconiiformes",
                "Columbiformes",
                "Coraciiformes",
                "Falconiformes",
                "Galliformes",
                "Gaviiformes",
                "Passeriformes",
                "Piciformes",
                "Psittaciformes",
                "Strigiformes",
                "Struthioniformes"
            };

            return birds;
        }
        public List<String> GetFamiliesByOrder(string order, HttpContext httpContext)
        {
            // For now, the list of orders is hard coded. Later it will support CRUD operations
            List<String> families = new();

            // Get the families for the order
            switch (order)
            {
                case "Anseriformes":
                    families.Add("Anatidae");
                    families.Add("Anhimidae");
                    families.Add("Anseranatidae");
                    families.Add("Anatinae");
                    families.Add("Anserinae");
                    families.Add("Anhiminae");
                    families.Add("Anhimini");
                    families.Add("Anserini");
                    families.Add("Anatini");
                    break;
                case "Apodiformes":
                    families.Add("Apodidae");
                    families.Add("Trochilidae");
                    families.Add("Trochilinae");
                    families.Add("Apodinae");
                    families.Add("Apodini");
                    families.Add("Trochilini");
                    break;
                case "Charadriformes":
                    families.Add("Charadriidae");
                    families.Add("Charadriinae");
                    families.Add("Charadriini");
                    break;
                case "Ciconiiformes":
                    families.Add("Ciconiidae");
                    families.Add("Ciconiinae");
                    families.Add("Ciconiini");
                    break;
                case "Columbiformes":
                    families.Add("Columbidae");
                    families.Add("Columbinae");
                    families.Add("Columbini");
                    break;
                case "Coraciiformes":
                    families.Add("Alcedinidae");
                    families.Add("Alcedininae");
                    families.Add("Alcedinini");
                    families.Add("Cerylidae");
                    families.Add("Cerylinae");
                    families.Add("Cerylinae");
                    families.Add("Cerylini");
                    families.Add("Cisticolidae");
                    families.Add("Cisticolinae");
                    families.Add("Cisticolini");
                    families.Add("Coraciidae");
                    families.Add("Coraciinae");
                    families.Add("Coraciini");
                    families.Add("Meropidae");
                    families.Add("Meropinae");
                    families.Add("Meropini");
                    families.Add("Momotidae");
                    families.Add("Momotinae");
                    families.Add("Momotini");
                    families.Add("Trochilidae");
                    families.Add("Trochilinae");
                    families.Add("Trochilini");
                    break;
                default: break;
            }

            return families;
        }
    }
}

