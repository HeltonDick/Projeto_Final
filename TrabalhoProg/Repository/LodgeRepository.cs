using TrabalhoProg.Modelo;

namespace TrabalhoProg.Repository {
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