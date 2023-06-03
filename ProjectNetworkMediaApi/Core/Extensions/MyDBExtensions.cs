using Application.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace ProjectNetworkMediaApi.Core.Extensions
{
    public static class MyDBExtensions
    {
        public static void Deactivate(this DbContext context, Entity entity)
        {
            entity.isActive = false;
            context.Entry(entity).State = EntityState.Modified;
        }

        public static void Deactivate<T>(this DbContext context, int id)
            where T : Entity
        {
            var itemToDeactivate = context.Set<T>().Find(id);

            //if (itemToDeactivate == null)
            //{
            //    throw new EntityNotFoundException(id, typeof(itemToDeactivate));
            //}

            itemToDeactivate.isActive = false;
        }

        public static void Deactivate<T>(this DbContext context, IEnumerable<int> ids)
            where T : Entity
        {
            var toDeactivate = context.Set<T>().Where(x => ids.Contains(x.Id));

            //var nonExistingIds = ids.Except(toDeactivate.Select(x => x.Id));

            foreach (var d in toDeactivate)
            {
                d.isActive = false;
            }

        }
    }
}
