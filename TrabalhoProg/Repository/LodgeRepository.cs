using TrabalhoProg.Modelo;

namespace TrabalhoProg.Repository
{
    public class LodgeRepository
    {
        public Lodge Retrieve(int id)
        {
            foreach (Lodge l in CustomerData.Lodges)
            {
                if (l.LodgeId == id)
                    return l;
            }
            return null!;
        }

        public List<Lodge> RetrieveAll()
        {
            return CustomerData.Lodges;
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

        public void Save(Lodge lodge)
        {
            lodge.LodgeId = GetCount() + 1;
            CustomerData.Lodges.Add(lodge);
        }

        public int GetCount()
        {
            return CustomerData.Lodges.Count;
        }

        public void Update(Lodge newLodge)
        {
            Lodge oldLodge = Retrieve(newLodge.LodgeId);
            oldLodge.Customer = newLodge.Customer;
            oldLodge.LodgeDate = newLodge.LodgeDate;
            oldLodge.LodgeProperty = newLodge.LodgeProperty;
        }
    }
}