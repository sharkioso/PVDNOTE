using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Backend.Tests.UnitTests.Utils
{
    public static class DbSetMockHelper
    {
         public static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression)
                .Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType)
                .Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
                .Returns(() => queryable.GetEnumerator());
            
            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));

            return mockSet;
        }

        private class TestAsyncEnumerator<T>(IEnumerator<T> inner) : IAsyncEnumerator<T>
        {
            public T Current => inner.Current;
            public ValueTask DisposeAsync() => new ValueTask();
            public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(inner.MoveNext());
        }

        private class TestAsyncQueryProvider<T>(IQueryProvider inner) : IAsyncQueryProvider
        {
            public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<T>(expression);
            public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestAsyncEnumerable<TElement>(expression);
            public object Execute(Expression expression) => inner.Execute(expression);
            public TResult Execute<TResult>(Expression expression) => inner.Execute<TResult>(expression);
            
            public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
            {
                var expectedResultType = typeof(TResult).GetGenericArguments()[0];
                var executeMethod = typeof(IQueryProvider)
                    .GetMethods()
                    .First(m => m is { Name: nameof(IQueryProvider.Execute), IsGenericMethod: true })
                    .MakeGenericMethod(expectedResultType);
                
                var result = executeMethod.Invoke(this, [expression]);
                return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                    ?.MakeGenericMethod(expectedResultType)
                    .Invoke(null, new[] { result });
            }
        }

        private class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
            public TestAsyncEnumerable(Expression expression) : base(expression) { }
            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) 
                => new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
    }
}