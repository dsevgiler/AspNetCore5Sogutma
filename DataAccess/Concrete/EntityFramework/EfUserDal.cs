using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Entites.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, UcsanContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new UcsanContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims 
                                on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

                //System.Diagnostics.Trace.WriteLine("Output Deniz");

                return result.ToList();
            }
        }
    }
}
