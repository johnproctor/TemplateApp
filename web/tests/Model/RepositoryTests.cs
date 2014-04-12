using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xunit.Extensions;
using Xunit;
using Tests.Utility;
using System.Data.Entity;
using Model.Repository;
using Model;
using Ploeh.AutoFixture;
using FluentAssertions;
using System.IO;

namespace Tests.Model
{
    //These are more integration tests than unit test, but they test the repo
    //actually works against a localDB
    public class RepositoryTests : IDisposable
    {
        private DbContext _context;
        private IGenericRepository _repo;
        private Fixture _fixture;

        public RepositoryTests()
        {
            _context = new Model1Container();
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));
            _context.Database.CreateIfNotExists();
            _repo = new GenericRepository(_context);
            _fixture = new Fixture();
        }

        [Fact, AutoEF]
        public async Task InsertAsyncAddsEntity()
        {
            var table = _fixture.Create<Table>();

            var before = await _repo.FindAllAsync<Table>(x => x.Id > 0);

            var beforeCount = before.Count();

            var id = await _repo.InsertAsync<Table>(table);

            id.Should().BeGreaterThan(0);
        }

        [Fact, AutoEF]
        public async Task FindAllGetsResult()
        {
            var table = _fixture.Create<Table>();

            await _repo.InsertAsync<Table>(table);

            var result = await _repo.FindAllAsync<Table>(x => x.Name == table.Name);

            result.Count().Should().Equals(1);
        }

        [Fact, AutoEF]
        public async Task GetByIdReturnsEntity()
        {
            var table = _fixture.Create<Table>();

            var id = await _repo.InsertAsync<Table>(table);

            var entity = await _repo.GetByIdAsync<Table>(id);

            table.Name.Should().BeEquivalentTo(entity.Name);
        }

        [Fact, AutoEF]
        public async Task DeleteRemovesEntity()
        {
            var table = _fixture.Create<Table>();

            var id = await _repo.InsertAsync<Table>(table);

            await _repo.DeleteAsync<Table>(id);

            var result = await _repo.GetByIdAsync<Table>(id);

            result.Should().BeNull();

        }

        public void Dispose()
        {
            _context.Database.Delete();
            _context.Dispose();
        }
    }
}
