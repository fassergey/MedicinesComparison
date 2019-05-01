using MongoDB.Bson;

namespace MedicinesUrlCrawler
{
    public class Medicine
    {
        public ObjectId Id { get; set; }
        public string PharmGroup { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string InternationalName { get; set; }
        public string ActiveSubstance { get; set; }
        public string Cooperation { get; set; }
    }
}
