﻿using DapperExtensions;
using SampleJWT.Dto;
using SampleJWT.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleJWT.Services.Customer.Controllers
{
    public class CustomerController : ApiController
    {
        public IEnumerable<SampleJWT.Model.Data.Customer> GetPublicCustomers()
        {
            Repository.DapperRepository<SampleJWT.Model.Data.Customer> repo = new Repository.DapperRepository<SampleJWT.Model.Data.Customer>();
            return repo.Get(DapperExtensions.Predicates.Field<SampleJWT.Model.Data.Customer>(customer => customer.Type, DapperExtensions.Operator.Eq
                        , "Public"));
        }

        [Restricted(Roles = "User")]
        public IEnumerable<SampleJWT.Model.Data.Customer> GetAllCustomers(int page, int pageSize)
        {
            Repository.DapperRepository<SampleJWT.Model.Data.Customer> repo = new Repository.DapperRepository<SampleJWT.Model.Data.Customer>();
            IList<ISort> sort = new List<ISort>
                                    {
                                        Predicates.Sort<SampleJWT.Model.Data.Customer>(c => c.Name)
                                    };
            

            return repo.Get(DapperExtensions.Predicates.Field<SampleJWT.Model.Data.Customer>(customer => customer.Type, DapperExtensions.Operator.Eq
                     , "Normal"), sort, page, pageSize);
        }

        [Restricted(Roles = "User")]
        [HttpGet()]
        public long CountAllCustomers()
        {
            Repository.DapperRepository<SampleJWT.Model.Data.Customer> repo = new Repository.DapperRepository<SampleJWT.Model.Data.Customer>();            
            return repo.Count(DapperExtensions.Predicates.Field<SampleJWT.Model.Data.Customer>(customer => customer.Type, DapperExtensions.Operator.Eq
                     , "Normal"));
        }

        [Restricted(Roles = "Admin")]
        public IEnumerable<SampleJWT.Model.Data.Customer> GetSpecialCustomers()
        {
            Repository.DapperRepository<SampleJWT.Model.Data.Customer> repo = new Repository.DapperRepository<SampleJWT.Model.Data.Customer>();
            return repo.Get(DapperExtensions.Predicates.Field<SampleJWT.Model.Data.Customer>(customer => customer.Type, DapperExtensions.Operator.Eq
                      , "Special"));
        }

    }
}
