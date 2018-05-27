using ClassifiedApp.DataAccess.Repositories;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Extensions
{
    //public static class ClassifiedExtensions
    //{
    //    public static Expression<Func<Classified, bool>> ContainsInLastName(this EntityBaseRepository<Classified> classifiedRepository, params string[] keywords)
    //    {
    //        var predicate = PredicateBuilder.False<TC_User>();
    //        foreach (string keyword in keywords)
    //        {
    //            string temp = keyword;
    //            predicate = predicate.Or(p => p.LastName.Contains(temp));
    //        }
    //        return predicate;
    //    }
    //}
}
