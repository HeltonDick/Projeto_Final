
namespace TrabalhoProg.Modelo
{
    public class Lodge
    {
        #region Attributes
        public int LodgeId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime LodgeDate { get; set; }
        public List<LodgeProperty>? LodgeProperty { get; set; }
        #endregion


        public Lodge() 
        {
            LodgeDate = DateTime.Now;
            LodgeProperty = new List<LodgeProperty>();
        }

        public Lodge (int lodgeId) : this() {
            this.LodgeId = lodgeId;
        }

        public class Save(Lodge lodge) {
        }

        public Lodge Retrieve(int LodgeId) {
            return new Lodge();
        }

        public List<Lodge> Retrieve() {
            return new List<Lodge>();
        }
    }
}