using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Celebrities.Mappers;
using Celebrities.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace Celebrities.DAL
{
    public class CelebrityRepository : IRepository<Celebrity>
    {
        private readonly string dbPath = HttpContext.Current.Server.MapPath("~/App_Data/celebrities.json");
        private readonly IMapper<Celebrity> mapper;

        public CelebrityRepository(IMapper<Celebrity> mapper)
        {
            this.mapper = mapper;
            Init();
        }

        private void Init()
        {
            var url = "https://www.imdb.com/list/ls052283250";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            SaveChanges(mapper.MapAsList(doc));
        }

        public void Delete(int id)
        {
            List<Celebrity> celebrities = GetAll();
            Celebrity celebrity = celebrities.FirstOrDefault(c => c.Id == id);
            if(celebrity == null)
            {
                throw new KeyNotFoundException("The id was not found");
            }

            celebrity.isViewable = false;

            SaveChanges(celebrities);
        }

        public Celebrity Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<Celebrity> GetAll()
        {
            string json = File.ReadAllText(dbPath);
            List<Celebrity> celebrities = JsonConvert.DeserializeObject<List<Celebrity>>(json);
            return celebrities;
        }

        public int Insert(Celebrity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Celebrity entity)
        {
            throw new NotImplementedException();
        }

        private void SaveChanges(List<Celebrity> celebrities)
        {
            string contents = JsonConvert.SerializeObject(celebrities);
            File.WriteAllText(dbPath, contents);
        }

        public void Reset()
        {
            Init();
        }
    }
}