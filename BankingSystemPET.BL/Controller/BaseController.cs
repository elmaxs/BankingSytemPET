﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Controller
{
    public abstract class BaseController
    {
        private readonly IDataSaver manager = new JsonSaverController();

        protected void Save<T>(List<T> item) where T : class
        {
            manager.Save(item);
            DBSaver.Save(item);
        }

        protected List<T> Load<T>() where T : class
        {
            return DBSaver.Load<T>();
        }
    }
}
