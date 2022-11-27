﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLib.DB;
using DBLib.SysEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DBLib.Models
{
    public class PgSQLCostsRepository : ICostsRepository
    {
        SpsrLtDbContext db;

        ILogger logger;

        public PgSQLCostsRepository(ILogger logger = null)
        {
            db = new SpsrLtDbContext();
            db.Costs.Load();

            this.logger = logger;
        }

        public PgSQLCostsRepository(SpsrLtDbContext spsr, ILogger logger)
        {
            db = spsr;
            db.Costs.Load();

            this.logger = logger;
        }

        public void Create(Cost item)
        {
            return;
        }

        public void Delete(Cost item)
        {
            return;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Cost Get(int id)
        {
            try
            {
                var elem = db.Costs.ToList().Find(x => x.Availabilityid == id);

                if (elem != null)
                    return new Cost(id, (int)elem.CostValue);
                else
                    throw new Exception("Can\'t find cost.\n");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                logger?.LogError(e.Message);
                return null;
            }
        }

        public IEnumerable<Cost> GetAll()
        {
            var list = new ObservableCollection<Cost>();

            foreach (var c in db.Costs)
                list.Add(new Cost((int)c.Availabilityid, (int)c.CostValue));

            return list;
        }

        public Cost GetByShopProductCost(Shop shop, Product product)
        {
            try
            {
                var avail = db.Availabilities.ToList().Find(x => x.Shopid == shop.Id && x.Productid == product.Id);
                var elem = db.Costs.ToList().Find(x => x.Availabilityid == avail.Id);

                if (elem != null)
                    return new Cost((int)elem.Availabilityid, (int)elem.CostValue);
                else
                    throw new Exception("Can\'t find cost.\n");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                logger?.LogError(e.Message);
                return null;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Cost item)
        {
            return;
        }
    }
}
