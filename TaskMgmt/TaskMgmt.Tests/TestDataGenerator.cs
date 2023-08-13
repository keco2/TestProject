using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DAL;

namespace TaskMgmt.Tests
{
    public static class TestDataGenerator
    {
        public static IEnumerable<T> GenerateListOfT<T>(int count)
        {
            var data = new List<T>();

            for (int i = 0; i < count; i++)
            {
                data.Add(GenerateT<T>(i));
            }

            return data;
        }

        public static T GenerateT<T>(int withId)
        {
            var obj = Activator.CreateInstance<T>();
            var prop = typeof(T).GetProperty("ID", typeof(Guid));
            if (prop == null)
            {
                throw new NotSupportedException($"{nameof(T)} not supported. Type must implement property called ID (of type:Guid).");
            }
            prop.SetValue(obj, new Guid(withId.ToString().PadLeft(32, '0')));
            return obj;
        }

        public static DbQuery<T> GenerateDbQueryMockWithTestData<T>(int count) where T : class
        {
            var mockSet = Substitute.For<DbSet<T>, IQueryable<T>>();
            var dataQueryable = GenerateListOfT<T>(count).AsQueryable();

            ((IQueryable<T>)mockSet).Provider.Returns(dataQueryable.Provider);
            ((IQueryable<T>)mockSet).Expression.Returns(dataQueryable.Expression);
            ((IQueryable<T>)mockSet).ElementType.Returns(dataQueryable.ElementType);
            ((IQueryable<T>)mockSet).GetEnumerator().Returns(dataQueryable.GetEnumerator());

            return mockSet;
        }
    }
}
