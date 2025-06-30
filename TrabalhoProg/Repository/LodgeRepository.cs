using Modelo;

namespace Repository {
    public class LodgeRepository {
        public Lodge Retrieve(int id)
        {
            foreach (Lodge l in CustomerData.Lodges)
            {
                if (l.LodgeId == id)
                    return l;
            }
            return null!;
        }

        public List<Lodge> RetrieveByName(string name)
        {
            List<Lodge> ret = new List<Lodge>();
            foreach (Lodge l in CustomerData.Lodges)
                if (l.Name!.ToLower().Contains(name.ToLower()))
                    ret.Add(l);
            return ret;
        }

        public List<Lodge> RetrieveAll()
        {
            return CustomerData.Lodges;
        }

        public void Save(Lodge lodge)
        {
            lodge.LodgeId = GetCount() + 1;
            CustomerData.Lodges.Add(lodge);
        }

        public bool Delete(Lodge lodge)
        {
            return CustomerData.Lodges.Remove(lodge);
        }

        public bool DeleteById(int id)
        {
            Lodge lodge = Retrieve(id);
            if (lodge != null)
                return Delete(lodge);
            return false;
        }

        public void Update(Lodge newLodge)
        {
            Lodge oldLodge = Retrieve(newLodge.LodgeId);
            oldLodge.Name = newLodge.Name;
            oldLodge.Description = newLodge.Description;
            oldLodge.BedRooms = newLodge.BedRooms;
            oldLodge.GarageVacancies = newLodge.GarageVacancies;
            oldLodge.Address = newLodge.Address;
            oldLodge.Category = newLodge.Category;
            oldLodge.CurrentPricePerNight = newLodge.CurrentPricePerNight;
        }
    }
}