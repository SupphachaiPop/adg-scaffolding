﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {
        public List<job> GetActiveJobList(int pLocationId)
        {
            var jobList = dataDao.GetJobRepairList(pLocationId: pLocationId);
            return jobList.Where(i => i.is_active == true && i.is_deleted == false).ToList();
        }

        public List<job> GetAvaliableJobList(int pLocationId)
        {
            var jobList = dataDao.GetJobRepairList(pLocationId: pLocationId);
            return jobList.Where(i => i.is_deleted == false).ToList();
        }

        public List<result_search_job_zone> SearchJobZoneList(param_search_job_zone param)
        {
            return dataDao.SearchJobZoneList(param);
        }

        public List<result_info_job_zone>  GetJobZoneInfo(int zoneId, int statusId)
        {
            return dataDao.GetJobZoneInfo(zoneId: zoneId, statusId: statusId);
        }

        public int UpdateJob(List<param_create_job> entities)
        {
            return dataDao.UpdateJob(entities);
        }
    }
}
