﻿using DATA.EF;
using System.Collections.Generic;
using System.Linq;

namespace BIZ.Locations
{
    public class ProvinceServices
    {
        private RAMEntities db = new RAMEntities();

        public List<ProvinceList> GetAllProvinces(string lang, string token)
        {
            var response = db.getProvinceList(lang, token).ToList();
            return response;
        }

        public ProvinceList GetProvinceByID(string lang, int pid, string token)
        {
            var response = db.getProvinceByID(lang, pid, token).FirstOrDefault();
            return response;
        }
    }
}