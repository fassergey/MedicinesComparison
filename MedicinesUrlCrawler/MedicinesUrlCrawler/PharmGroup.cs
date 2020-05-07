using MongoDB.Bson;

namespace MedicinesUrlCrawler
{
    public class PharmGroup
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
