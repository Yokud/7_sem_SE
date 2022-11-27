namespace UnitTests
{
    public class CostStoriesRepositoryTests
    {
        [Fact]
        public void TestGet()
        {
            ICostStoryRepository rep = new PgSQLCostStoryRepository();

            CostStory cs = rep.Get(1);

            Assert.Equal(5522, cs.Cost);
        }

        [Fact]
        public void TestCreate()
        {
            ICostStoryRepository rep = new PgSQLCostStoryRepository();
            CostStory newCs = new CostStory(2022, 1, 666, 123);

            rep.Create(newCs);

            Assert.Equal(123, rep.GetAll().Where(x => x.Id == 188893).First().AvailabilityId);
        }

        [Fact]
        public void TestUpdate() 
        {
            ICostStoryRepository rep = new PgSQLCostStoryRepository();
            CostStory newCs = new CostStory(2022, 1, 666, 123);

            newCs.AvailabilityId = 456;
            rep.Update(newCs);

            Assert.Equal(456, rep.GetAll().Where(x => x.Id == 188893).First().AvailabilityId);
        }

        [Fact]
        public void TestDelete() 
        {
            ICostStoryRepository rep = new PgSQLCostStoryRepository();
            CostStory newCs = new CostStory(2022, 1, 666, 123);

            rep.Delete(newCs);

            Assert.Equal(Array.Empty<CostStory>(), rep.GetAll().Where(x => x.Id == 188893).ToList());
        }
    }
}