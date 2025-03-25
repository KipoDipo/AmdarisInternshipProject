
namespace Assignment
{
    class ComputerComponentsRepository : IRepository<ComputerComponent, uint>
    {
        private List<ComputerComponent> components;

        private ComputerComponent Add(ComputerComponent entity)
        {
            ComputerComponent toAdd = new ComputerComponent()
            {
                Id = entity.Id,
                Manufacturer = entity.Manufacturer,
                Name = entity.Name,
                Price = entity.Price
            };
            components.Add(toAdd);
            return toAdd;
        }

        public ComputerComponentsRepository()
        {
            components = new List<ComputerComponent>();
        }

        public ComputerComponent Create(ComputerComponent entity)
        {
            var existingEntity = components.Find(x => x.Id == entity.Id);
            if (existingEntity != null)
                return existingEntity;

            return Add(entity);
        }

        public void Delete(ComputerComponent entity)
        {
            components.RemoveAll(x => x.Id == entity.Id);
        }

        public IEnumerable<ComputerComponent> Read()
        {
            return components;
        }

        public ComputerComponent? ReadById(uint id)
        {
            return components.Find(x => x.Id == id);
        }

        public ComputerComponent Update(ComputerComponent entity)
        {
            var existingEntity = components.Find(x => x.Id == entity.Id);
            if (existingEntity == null)
                return Add(entity);

            existingEntity.Manufacturer = entity.Manufacturer;
            existingEntity.Name = entity.Name;
            existingEntity.Price = entity.Price;

            return existingEntity;
        }
    }
}
